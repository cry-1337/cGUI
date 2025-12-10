using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;

namespace cGUI.Render.Contexts.Builder;

public abstract partial class QuadRenderContextBuilder<TValue>(IQuadRenderContext ctx) : IRenderContextBuilder<TValue> where TValue : IRenderContext
{
    protected IQuadRenderContext RenderContext = ctx;

    public IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect, in GUIColor col)
        => AddRect(rect, col, col, col, col, new());

    public IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect, in GUIColor col, in GUIRectangle radiusRect)
        => AddRect(rect, col, col, col, col, radiusRect);

    public IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight)
        => AddRect(rect, colTopLeft, colTopRight, colBotLeft, colBotRight, new());

    public IRenderContextBuilder<TValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight,
        in GUIRectangle radiusRect)
    {
        RenderContext.CornerRoundRadius = radiusRect;

        int baseIdx = RenderContext.VerticiesCount;

        RenderContext.AddVertex(new Vertex(new(rect.X, rect.Y), colTopLeft, new(0, 0)));
        RenderContext.AddVertex(new Vertex(new(rect.Width + rect.X, rect.Y), colTopRight, new(1, 0)));
        RenderContext.AddVertex(new Vertex(new(rect.Width + rect.X, rect.Height + rect.Y), colBotRight, new(1, 1)));
        RenderContext.AddVertex(new Vertex(new(rect.X, rect.Height + rect.Y), colBotLeft, new(0, 1)));

        RenderContext.AddIndex(baseIdx + 0);
        RenderContext.AddIndex(baseIdx + 1);
        RenderContext.AddIndex(baseIdx + 2);

        RenderContext.AddIndex(baseIdx + 2);
        RenderContext.AddIndex(baseIdx + 3);
        RenderContext.AddIndex(baseIdx + 0);

        return this;
    }

    public IRenderContextBuilder<TValue> AddLine(GUIVector2 a, GUIVector2 b,
        GUIColor color,
        float thickness = 1f,
        float aaSize = 1f)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;

        float len = (float) System.Math.Sqrt(dx * dx + dy * dy);
        if (len <= float.Epsilon)
            return this;

        float nx = -dy / len;
        float ny = dx / len;

        float half = thickness * 0.5f;
        float outer = half + aaSize;

        GUIColor transparent = new(color.R, color.G, color.B, 0);

        void AddStrip(float r0, float r1, GUIColor c0, GUIColor c1)
        {
            GUIVector2 p0 = new(a.X + nx * r0, a.Y + ny * r0);
            GUIVector2 p1 = new(a.X + nx * r1, a.Y + ny * r1);
            GUIVector2 p2 = new(b.X + nx * r1, b.Y + ny * r1);
            GUIVector2 p3 = new(b.X + nx * r0, b.Y + ny * r0);

            float minX = System.Math.Min(System.Math.Min(p0.X, p1.X), System.Math.Min(p2.X, p3.X));
            float maxX = System.Math.Max(System.Math.Max(p0.X, p1.X), System.Math.Max(p2.X, p3.X));
            float minY = System.Math.Min(System.Math.Min(p0.Y, p1.Y), System.Math.Min(p2.Y, p3.Y));
            float maxY = System.Math.Max(System.Math.Max(p0.Y, p1.Y), System.Math.Max(p2.Y, p3.Y));
            
            GUIRectangle rect = new(minX, minY, maxX - minX, maxY - minY);

            AddRect(rect, c0, c0, c1, c1, default);
        }

        AddStrip(-half, +half, color, color);

        AddStrip(+half, +outer,
            new GUIColor(color.R, color.G, color.B, 255),
            transparent);

        AddStrip(-outer, -half,
            transparent,
            new GUIColor(color.R, color.G, color.B, 255));

        return this;
    }

    public abstract TValue Build();
}
