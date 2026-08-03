using System;
using cGUI.Abstraction.Structs;
using cGUI.Animations;
using cGUI.Convert.Extensions;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Input;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Math;

namespace cGUI.Elements.BaseElements;

public class HoverableElement : SimpleElement, IEventHandler<MouseMoveEvent>, IEventHandler<PreRenderEvent>
{
    protected static readonly Func<float, float, float, float> TweenLerp = GUIMath.LerpUnclamped;

    protected readonly GUIColor[] m_HoveredColor;
    protected readonly GUIColor[] m_ColorBuffer = new GUIColor[4];
    protected readonly StateTween<float> m_HoverTween;
    protected bool m_IsHovered;

    public HoverableElement(string id, ElementOption options, GUIColor[] hoveredColor, ElementTweenOptions tweenOptions = default) : base(id, options)
    {
        IsHittable = true;

        m_HoveredColor = hoveredColor;
        m_HoverTween = new StateTween<float>(tweenOptions.HoverInDuration, tweenOptions.HoverOutDuration, TweenLerp, tweenOptions.HoverEasing);
    }

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        m_HoverTween.Update(m_IsHovered, reason.DeltaTime);
        ComputeColors();
        BuildMesh(m_ColorBuffer);
        return IsActive;
    }

    bool IEventHandler<MouseMoveEvent>.Handle(MouseMoveEvent reason)
    {
        m_IsHovered = HitTest(reason.GlobalMousePosition.ToPoint(), out var _);
        return IsActive && IsHittable;
    }

    protected virtual void ComputeColors()
    {
        float t = m_HoverTween.Evaluate(0f, 1f);
        LerpColors(m_Color, m_HoveredColor, t, m_ColorBuffer);
    }

    protected static void LerpColors(GUIColor[] from, GUIColor[] to, float t, GUIColor[] result)
    {
        for (int i = 0; i < from.Length; i++)
            result[i] = new GUIColor(from[i]).Lerp(to[i], t);
    }

    protected static void LerpColorsInPlace(GUIColor[] buffer, GUIColor[] to, float t)
    {
        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = new GUIColor(buffer[i]).Lerp(to[i], t);
    }
}
