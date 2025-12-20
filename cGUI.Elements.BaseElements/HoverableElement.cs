using cGUI.Abstraction.Structs;
using cGUI.Events.Models;
using cGUI.Layout.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Builder;
using cGUI.Unity.Render.Contexts;
using cGUI.Visual;
using UnityEngine;

namespace cGUI.Elements.BaseElements;

public class HoverableElement(string id, GUIRectangle dummy, Material material) : VisualElement(id)
{
    private readonly GUIRectangle m_Dummy = dummy;
    private readonly Material m_Material = material;
    private IMeshRenderContext<IUnityMeshData> m_Context = new UnityMeshRenderContext();

    public override void OnLayout(LayoutEvent reason)
    {
        var layout = reason.Layout;

        layout.PushNode(new LayoutNode(this, m_Dummy, []));
        layout.PerformLayout(Parent != null ? Parent.Bounds : new GUIRectangle(0, 0, Screen.width, Screen.height));

        m_Context = new UnityMeshRenderContextBuilder(m_Context, null).AddRect(Bounds, GUIColor.White, new UnityMeshData(m_Material)).Build();
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
    }
}