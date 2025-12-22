using cGUI.Abstraction.Structs;
using cGUI.Events.Models;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Options;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Builder;
using cGUI.Unity.Render.Contexts;
using cGUI.Visual;
using UnityEngine;

namespace cGUI.Elements.BaseElements;

public class HoverableElement(string id, GUIRectangle dummy, Material material, EDockType dock, GUIColor color) : VisualElement(id)
{
    private readonly GUIRectangle m_Dummy = dummy;
    private readonly Material m_Material = material;
    private readonly EDockType m_DockType = dock;
    private readonly GUIColor m_Color = color;
    private IMeshRenderContext<IUnityMeshData> m_Context = new UnityMeshRenderContext();

    public override void OnLayout(in LayoutEvent reason)
    {
        var layout = reason.Layout;
        var node = new LayoutNode(this, m_Dummy, [new DockOption(m_DockType)]);

        layout.PushNode(ref node);

        m_Context = new UnityMeshRenderContextBuilder(m_Context, null).AddRect(Bounds, m_Color, new UnityMeshData(m_Material)).Build();
    }

    public override void OnRender(in RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
    }
}