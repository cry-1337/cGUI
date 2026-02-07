using cGUI.Abstraction.Structs;
using cGUI.Animations;
using cGUI.Convert.Extensions;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Input;
using cGUI.Events.Models.Layout;
using cGUI.Math;

namespace cGUI.Elements.BaseElements;

public class HoverableElement : SimpleElement, IEventHandler<MouseMoveEvent>, IEventHandler<PostLayoutEvent>
{
    protected readonly GUIColor[] m_HoveredColor;
    protected readonly StateTween<float> m_HoverTween;
    protected bool m_IsHovered;

    public HoverableElement(string id, ElementOption options, GUIColor[] hoveredColor, ElementTweenOptions tweenOptions = default) : base(id, options)
    {
        IsHittable = true;

        m_HoveredColor = hoveredColor;
        m_HoverTween = new StateTween<float>(tweenOptions.HoverInDuration, tweenOptions.HoverOutDuration, (a, b, t) => GUIMath.LerpUnclamped(a, b, t), tweenOptions.HoverEasing);
    }

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        m_HoverTween.Update(m_IsHovered, reason.DeltaTime);
        BuildMesh(ComputeColors());
        return IsActive;
    }

    bool IEventHandler<MouseMoveEvent>.Handle(MouseMoveEvent reason)
    {
        m_IsHovered = HitTest(reason.GlobalMousePosition.ToPoint(), out var _);
        return IsActive && IsHittable;
    }

    protected virtual GUIColor[] ComputeColors()
    {
        float t = m_HoverTween.Evaluate(0f, 1f);
        return LerpColorArrays(m_Color, m_HoveredColor, t);
    }

    protected static GUIColor[] LerpColorArrays(GUIColor[] from, GUIColor[] to, float t)
    {
        var result = new GUIColor[from.Length];

        for (int i = 0; i < from.Length; i++)
        {
            result[i] = new GUIColor(from[i]).Lerp(to[i], t);
        }

        return result;
    }
}
