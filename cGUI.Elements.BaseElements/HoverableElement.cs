using cGUI.Abstraction.Structs;
using cGUI.Assert;
using cGUI.Convert.Extensions;
using cGUI.Elements.Globals;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Input;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Layout.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Contexts;
using cGUI.Unity.Render.Extensions;

namespace cGUI.Elements.BaseElements;

public class HoverableElement : BaseElement, IEventHandler<MouseMoveEvent>, IEventHandler<PostLayoutEvent>
{
    private readonly GUIColor m_Color;
    private readonly GUIColor m_HoveredColor;
    private readonly IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();

    private LayoutNode m_Node;
    private bool m_IsHovered;

    public HoverableElement(string id, ElementOption options, GUIColor hoveredColor) : base(id)
    {
        GUIAssert.IsNull(options.DesiredRect, $"DesiredRect is null in {id}");
        GUIAssert.IsNull(options.Color, $"Color is null in {id}");

        IsActive = true;
        IsHittable = true;

        m_Color = options.Color!.Value;
        m_HoveredColor = hoveredColor;

        m_Node = new LayoutNode(this, options.DesiredRect, options.LayoutOptions);
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
    }

    public override void OnLayout(LayoutEvent reason)
    {
        var layout = reason.Layout;
        layout.PushNode(m_Node);
    }

    bool IEventHandler<PostLayoutEvent>.Handle(PostLayoutEvent reason)
    {
        var meshData = new UnityMeshData(GUIGlobals.GlobalMaterial!);
        m_Context.AddRect(Bounds, m_IsHovered ? m_HoveredColor : m_Color, ref meshData);
        return IsActive;
    }

    bool IEventHandler<MouseMoveEvent>.Handle(MouseMoveEvent reason)
    {
        m_IsHovered = HitTest(reason.GlobalMousePosition.ToPoint(), out var _);
        return IsActive && IsHittable;
    }
}