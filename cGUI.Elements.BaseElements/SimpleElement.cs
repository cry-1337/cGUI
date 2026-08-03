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
        GUIRectangle drawBounds = ClippingUtility.GetClippedBounds(this, Bounds);

        if (drawBounds.Width > 0 && drawBounds.Height > 0)
        {
            m_Context.AddRect(drawBounds, colors[0], colors[1], colors[2], colors[3], ref meshData);
        }
    }
}
