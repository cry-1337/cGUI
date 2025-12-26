using cGUI.Abstraction.Structs;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
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

public class HoverableElement(string id, GUIRectangle dummy, Material material, EDockType dock, GUIColor color) 
    : VisualElement(id), IEventHandler<LayoutEvent>, IEventHandler<RenderEvent>
{
    private readonly GUIRectangle m_Dummy = dummy;
    private readonly Material m_Material = material;
    private readonly EDockType m_DockType = dock;
    private readonly GUIColor m_Color = color;
    private IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();

    bool IEventHandler<LayoutEvent>.Handle(LayoutEvent reason)
    {
        var layout = reason.Layout;
        var node = new LayoutNode(this, m_Dummy, [new DockOption(m_DockType)]);

        layout.PushNode(node);

        m_Context = new UnityMeshRenderContextBuilder(m_Context, null).AddRect(Bounds, m_Color, new UnityMeshData(m_Material)).Build();
        return true;
    }

    bool IEventHandler<RenderEvent>.Handle(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
        return true;
    }
}