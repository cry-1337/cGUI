using cGUI.Abstraction.Structs;
using cGUI.Events.Models;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Strategies;
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public class HoverableElement(string id, float width, float height) : VisualElement(id)
{
    private readonly GUIRectangle m_Dummy = new(0, 0, width, height);

    public override void OnLayout(LayoutEvent reason)
    {
        reason.Layout.Reset();
        reason.Layout.PushStrategy(new AlignmentStrategy());
        Bounds = reason.Layout.PerformLayout(m_Dummy, Parent.Bounds);
    }

    public override void OnRender(RenderEvent reason)
    {
    }
}