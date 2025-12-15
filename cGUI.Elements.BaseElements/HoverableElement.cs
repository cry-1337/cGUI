using cGUI.Abstraction.Structs;
using cGUI.Events.Models;
using cGUI.Layout.Strategies;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Builder;
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public class HoverableElement(string id, float width, float height) : VisualElement(id)
{
    private readonly GUIRectangle m_Dummy = new(0, 0, width, height);
    private IUnityQuadRenderContext m_Context;

    public override void OnLayout(LayoutEvent reason)
    {
        reason.Layout.Reset();
        reason.Layout.PushStrategy(new AlignmentStrategy());
        Bounds = reason.Layout.PerformLayout(m_Dummy, Parent.Bounds);

        m_Context = new UnityQuadRenderContextBuilder(m_Context).AddRect(Bounds, GUIColor.White).Build();
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushQuadContext(m_Context);
        reason.Render.Render();
    }
}