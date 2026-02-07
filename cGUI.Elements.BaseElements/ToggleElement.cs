using System;
using cGUI.Abstraction.Structs;
using cGUI.Animations;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;

namespace cGUI.Elements.BaseElements;

public class ToggleElement : ClickableElement, IEventHandler<PostLayoutEvent>
{
    private readonly GUIColor[] m_OnColor;
    private readonly GUIColor[] m_OnHoveredColor;
    private readonly GUIColor[] m_OnPressedColor;
    private readonly GUIColor[] m_OnColorBuffer = new GUIColor[4];
    private readonly StateTween<float> m_ToggleTween;

    private bool m_IsOn;
    private Action<bool>? m_OnToggle;

    public bool IsOn => m_IsOn;

    public ToggleElement(string id, ElementOption options, GUIColor[] hoveredColor, GUIColor[] pressedColor, GUIColor[] onColor, GUIColor[] onHoveredColor, GUIColor[] onPressedColor, Action<bool>? onToggle = null, bool initialState = false, ElementTweenOptions tweenOptions = default) : base(id, options, hoveredColor, pressedColor, tweenOptions)
    {
        m_OnColor = onColor;
        m_OnHoveredColor = onHoveredColor;
        m_OnPressedColor = onPressedColor;
        m_OnToggle = onToggle;
        m_IsOn = initialState;

        m_ToggleTween = new StateTween<float>(tweenOptions.ToggleDuration, tweenOptions.ToggleDuration, TweenLerp, tweenOptions.ToggleEasing);
    }

    public void SetOnToggle(Action<bool>? onToggle) => m_OnToggle = onToggle;

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        m_HoverTween.Update(m_IsHovered, reason.DeltaTime);
        m_PressTween.Update(m_IsPressed, reason.DeltaTime);
        m_ToggleTween.Update(m_IsOn, reason.DeltaTime);
        ComputeColors();
        BuildMesh(m_ColorBuffer);
        return IsActive;
    }

    protected override void OnClick()
    {
        m_IsOn = !m_IsOn;
        m_OnToggle?.Invoke(m_IsOn);
    }

    protected override void ComputeColors()
    {
        float hoverT = m_HoverTween.Evaluate(0f, 1f);
        float pressT = m_PressTween.Evaluate(0f, 1f);
        float toggleT = m_ToggleTween.Evaluate(0f, 1f);

        LerpColors(m_Color, m_HoveredColor, hoverT, m_ColorBuffer);
        LerpColorsInPlace(m_ColorBuffer, m_PressedColor, pressT);

        LerpColors(m_OnColor, m_OnHoveredColor, hoverT, m_OnColorBuffer);
        LerpColorsInPlace(m_OnColorBuffer, m_OnPressedColor, pressT);

        LerpColorsInPlace(m_ColorBuffer, m_OnColorBuffer, toggleT);
    }
}
