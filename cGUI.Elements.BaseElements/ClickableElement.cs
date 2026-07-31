using cGUI.Abstraction.Structs;
using cGUI.Animations;
using cGUI.Convert.Extensions;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Input;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;

namespace cGUI.Elements.BaseElements;

public class ClickableElement : HoverableElement, IEventHandler<MouseKeyDownEvent>, IEventHandler<MouseKeyUpEvent>, IEventHandler<PreRenderEvent>
{
    protected readonly GUIColor[] m_PressedColor;
    protected readonly StateTween<float> m_PressTween;
    protected bool m_IsPressed;

    public ClickableElement(string id, ElementOption options, GUIColor[] hoveredColor, GUIColor[] pressedColor, ElementTweenOptions tweenOptions = default) : base(id, options, hoveredColor, tweenOptions)
    {
        m_PressedColor = pressedColor;
        m_PressTween = new StateTween<float>(tweenOptions.PressInDuration, tweenOptions.PressOutDuration, TweenLerp, tweenOptions.PressEasing);
    }

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        m_HoverTween.Update(m_IsHovered, reason.DeltaTime);
        m_PressTween.Update(m_IsPressed, reason.DeltaTime);
        ComputeColors();
        BuildMesh(m_ColorBuffer);
        return IsActive;
    }

    bool IEventHandler<MouseKeyDownEvent>.Handle(MouseKeyDownEvent reason)
    {
        if (HitTest(reason.GlobalMousePosition.ToPoint(), out var _))
            m_IsPressed = true;

        return IsActive && IsHittable;
    }

    bool IEventHandler<MouseKeyUpEvent>.Handle(MouseKeyUpEvent reason)
    {
        if (m_IsPressed)
        {
            if (HitTest(reason.GlobalMousePosition.ToPoint(), out var _))
                OnClick();

            m_IsPressed = false;
        }

        return IsActive && IsHittable;
    }

    protected override void ComputeColors()
    {
        float hoverT = m_HoverTween.Evaluate(0f, 1f);
        float pressT = m_PressTween.Evaluate(0f, 1f);
        LerpColors(m_Color, m_HoveredColor, hoverT, m_ColorBuffer);
        LerpColorsInPlace(m_ColorBuffer, m_PressedColor, pressT);
    }

    protected virtual void OnClick() { }
}
