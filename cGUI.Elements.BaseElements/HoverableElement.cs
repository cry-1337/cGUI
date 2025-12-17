using cGUI.Abstraction.Structs;
using cGUI.Events.Models;
using cGUI.Layout.Strategies;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Builder;
using cGUI.Unity.Render.Contexts;
using cGUI.Visual;
using UnityEngine;

namespace cGUI.Elements.BaseElements;

public class HoverableElement(string id, float width, float height) : VisualElement(id)
{
    private readonly GUIRectangle m_Dummy = new(0, 0, width, height);
    private IMeshRenderContext<IUnityMeshData> m_Context = new UnityMeshRenderContext();

    public override void OnLayout(LayoutEvent reason)
    {
        reason.Layout.Reset();
        reason.Layout.PushStrategy(new AlignmentStrategy());
        Bounds = reason.Layout.PerformLayout(m_Dummy, Parent != null ? Parent.Bounds : new GUIRectangle(0, 0, Screen.width, Screen.height));

        m_Context = new UnityMeshRenderContextBuilder(m_Context, null).AddRect(Bounds, GUIColor.White).Build();
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
    }
}