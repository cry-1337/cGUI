using System;
using cGUI.Abstraction.Structs;
using cGUI.Elements.Models;

namespace cGUI.Elements.BaseElements;

public class ButtonElement : ClickableElement
{
    private Action? m_OnClick;

    public ButtonElement(string id, ElementOption options, GUIColor[] hoveredColor, GUIColor[] pressedColor, Action? onClick = null, ElementTweenOptions tweenOptions = default) : base(id, options, hoveredColor, pressedColor, tweenOptions)
    {
        m_OnClick = onClick;
    }

    public void SetOnClick(Action? onClick) => m_OnClick = onClick;

    protected override void OnClick() => m_OnClick?.Invoke();
}
