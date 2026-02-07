using cGUI.Abstraction.Structs;
using cGUI.Assert;
using cGUI.Elements.Globals;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Layout.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Contexts;
using cGUI.Unity.Render.Extensions;
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public class PanelElement : VisualContainer<BaseElement>, IEventHandler<RenderEvent>, IEventMicroController<RenderEvent>, IEventHandler<LayoutEvent>, IEventMicroController<LayoutEvent>, IEventHandler<PostLayoutEvent>
{
    private readonly GUIColor[] m_Color;
    private readonly IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();
    private LayoutNode m_Node;

    public PanelElement(string id, ElementOption options) : base(id)
    {
        GUIAssert.IsNull(options.DesiredRect, $"DesiredRect is null in {id}");
        GUIAssert.IsNull(options.Color, $"Color is null in {id}");

        IsActive = true;
        IsHittable = false;

        m_Color = options.Color.ToQuadColors();
        m_Node = new LayoutNode(this, options.DesiredRect, options.LayoutOptions);
    }

    bool IEventHandler<RenderEvent>.Handle(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
        return IsActive;
    }

    bool IEventHandler<LayoutEvent>.Handle(LayoutEvent reason)
    {
        reason.Layout.PushNode(m_Node);
        return IsActive;
    }

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        var meshData = new UnityMeshData(GUIGlobals.GlobalMaterial!);
        m_Context.AddRect(Bounds, m_Color[0], m_Color[1], m_Color[2], m_Color[3], ref meshData);
        return IsActive;
    }

    bool IEventMicroController<RenderEvent>.GetEvent(RenderEvent reason) => IsActive;

    bool IEventMicroController<LayoutEvent>.GetEvent(LayoutEvent reason) => IsActive;
}
