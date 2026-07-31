using cGUI.Abstraction.Structs;
using cGUI.Abstraction.Interfaces;
using cGUI.Assert;
using cGUI.Elements.Globals;
using cGUI.Elements.Models;
using cGUI.Event.Abstraction;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Layout.Abstraction;
using cGUI.Math;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Contexts;
using cGUI.Unity.Render.Extensions;
using cGUI.Visual;

namespace cGUI.Elements.BaseElements;

public class PanelElement : VisualContainer<BaseElement>, IEventHandler<RenderEvent>, IEventMicroController<RenderEvent>, IEventHandler<LayoutEvent>, IEventMicroController<LayoutEvent>, IEventHandler<PreRenderEvent>
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

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        m_Context.Clear();
        var meshData = new UnityMeshData(GUIGlobals.GlobalMaterial!);
        GUIRectangle drawBounds = Bounds;
        
        var parent = Parent;
        while (parent != null)
        {
            if (parent is IScrollable scrollable && scrollable.SupportsScroll)
            {
                var parentBounds = parent.Bounds;
                float x1 = GUIMath.Max(drawBounds.X, parentBounds.X);
                float y1 = GUIMath.Max(drawBounds.Y, parentBounds.Y);
                float x2 = GUIMath.Min(drawBounds.X + drawBounds.Width, parentBounds.X + parentBounds.Width);
                float y2 = GUIMath.Min(drawBounds.Y + drawBounds.Height, parentBounds.Y + parentBounds.Height);
                
                float w = GUIMath.Max(0f, x2 - x1);
                float h = GUIMath.Max(0f, y2 - y1);
                
                drawBounds = new GUIRectangle(x1, y1, w, h);
            }
            parent = parent.Parent;
        }

        if (drawBounds.Width > 0 && drawBounds.Height > 0)
        {
            m_Context.AddRect(drawBounds, m_Color[0], m_Color[1], m_Color[2], m_Color[3], ref meshData);
        }
        return IsActive;
    }

    bool IEventMicroController<RenderEvent>.GetEvent(RenderEvent reason) => IsActive;

    bool IEventMicroController<LayoutEvent>.GetEvent(LayoutEvent reason) => IsActive;
}
