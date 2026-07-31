using cGUI.Abstraction.Interfaces;
using cGUI.Abstraction.Structs;
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

namespace cGUI.Elements.BaseElements;

public class SimpleElement : BaseElement, IEventHandler<PreRenderEvent>
{
    protected readonly GUIColor[] m_Color;
    protected readonly IMeshRenderContext<UnityMeshData> m_Context = new UnityMeshRenderContext();
    protected LayoutNode m_Node;

    public SimpleElement(string id, ElementOption options) : base(id)
    {
        GUIAssert.IsNull(options.DesiredRect, $"DesiredRect is null in {id}");
        GUIAssert.IsNull(options.Color, $"Color is null in {id}");

        IsActive = true;
        IsHittable = false;

        m_Color = options.Color.ToQuadColors();
        m_Node = new LayoutNode(this, options.DesiredRect, options.LayoutOptions);
    }

    public override void OnRender(RenderEvent reason)
    {
        reason.Render.PushMesh(m_Context);
        m_Context.Clear();
    }

    public override void OnLayout(LayoutEvent reason)
    {
        reason.Layout.PushNode(m_Node);
    }

    bool IEventHandler<PreRenderEvent>.Handle(PreRenderEvent reason)
    {
        BuildMesh(m_Color);
        return IsActive;
    }

    protected void BuildMesh(GUIColor[] colors)
    {
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
            m_Context.AddRect(drawBounds, colors[0], colors[1], colors[2], colors[3], ref meshData);
        }
    }
}
