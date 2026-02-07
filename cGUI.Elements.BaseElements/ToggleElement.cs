using System;
using cGUI.Abstraction.Structs;
using cGUI.Animations;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Math;

namespace cGUI.Elements.BaseElements;

public class ToggleElement : ClickableElement, IEventHandler<PostLayoutEvent>
{
    private readonly GUIColor[] m_OnColor;
    private readonly GUIColor[] m_OnHoveredColor;
    private readonly GUIColor[] m_OnPressedColor;
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

        m_ToggleTween = new StateTween<float>(tweenOptions.ToggleDuration, tweenOptions.ToggleDuration, (a, b, t) => GUIMath.LerpUnclamped(a, b, t), tweenOptions.ToggleEasing);
    }

    public void SetOnToggle(Action<bool>? onToggle) => m_OnToggle = onToggle;

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        m_HoverTween.Update(m_IsHovered, reason.DeltaTime);
        m_PressTween.Update(m_IsPressed, reason.DeltaTime);
        m_ToggleTween.Update(m_IsOn, reason.DeltaTime);
        BuildMesh(ComputeColors());
        return IsActive;
    }

    protected override void OnClick()
    {
        m_IsOn = !m_IsOn;
        m_OnToggle?.Invoke(m_IsOn);
    }

    protected override GUIColor[] ComputeColors()
    {
        float hoverT = m_HoverTween.Evaluate(0f, 1f);
        float pressT = m_PressTween.Evaluate(0f, 1f);
        float toggleT = m_ToggleTween.Evaluate(0f, 1f);

        var offHovered = LerpColorArrays(m_Color, m_HoveredColor, hoverT);
        var offStack = LerpColorArrays(offHovered, m_PressedColor, pressT);

        var onHovered = LerpColorArrays(m_OnColor, m_OnHoveredColor, hoverT);
        var onStack = LerpColorArrays(onHovered, m_OnPressedColor, pressT);

        return LerpColorArrays(offStack, onStack, toggleT);
    }
}
