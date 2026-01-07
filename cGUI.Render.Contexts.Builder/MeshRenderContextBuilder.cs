using cGUI.Abstraction.Structs;
using cGUI.Math;
using cGUI.Render.Abstraction;
using System;
using System.Runtime.CompilerServices;

namespace cGUI.Render.Contexts.Builder;

//@todo: rework
public abstract partial class MeshRenderContextBuilder<TContextValue, TMeshValue>(TContextValue ctx, TMeshValue? meshValue) : IRenderContextBuilder<TContextValue, TMeshValue>
    where TContextValue : IMeshRenderContext<TMeshValue>
    where TMeshValue : IMeshData
{
    protected TContextValue m_RenderContext = ctx;
    protected TMeshValue? m_MeshValue = meshValue;

    public IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect, in GUIColor color)
    {
        if (m_MeshValue == null) ThrowMeshDataNull();
        return AddRect(rect, color, m_MeshValue!);
    }

    public IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight)
    {
        if (m_MeshValue == null) ThrowMeshDataNull();
        return AddRect(rect, colTopLeft, colTopRight, colBotLeft, colBotRight, m_MeshValue!);
    }

    public IRenderContextBuilder<TContextValue, TMeshValue> AddRect(in GUIRectangle rect, in GUIColor col, TMeshValue meshData)
        => AddRect(rect, col, col, col, col, meshData);

    public IRenderContextBuilder<TContextValue, TMeshValue> AddRect(
        in GUIRectangle rect,
        in GUIColor colTopLeft, in GUIColor colTopRight,
        in GUIColor colBotLeft, in GUIColor colBotRight,
        TMeshValue meshData)
    {
        var vertices = m_RenderContext.Vertices;
        var indices = m_RenderContext.Indicies;

        int baseVtx = vertices.Count;
        int baseIdx = indices.Count;

        vertices.Add(new Vertex(new(rect.X, rect.Y), colBotLeft, new(0, 0)));
        vertices.Add(new Vertex(new(rect.X + rect.Width, rect.Y), colBotRight, new(1, 0)));
        vertices.Add(new Vertex(new(rect.X + rect.Width, rect.Y + rect.Height), colTopRight, new(1, 1)));
        vertices.Add(new Vertex(new(rect.X, rect.Y + rect.Height), colTopLeft, new(0, 1)));

        indices.Add(baseVtx + 0);
        indices.Add(baseVtx + 1);
        indices.Add(baseVtx + 2);
        indices.Add(baseVtx + 2);
        indices.Add(baseVtx + 3);
        indices.Add(baseVtx + 0);

        meshData.VerticesOffset = baseVtx;
        meshData.IndiciesOffset = baseIdx;
        meshData.VerticiesCount = 4;
        meshData.IndicesCount = 6;

        m_RenderContext.Meshes.Add(meshData);

        return this;
    }

    public IRenderContextBuilder<TContextValue, TMeshValue> AddLine(GUIVector2 a, GUIVector2 b,
        GUIColor color,
        TMeshValue meshData,
        float thickness = 1f,
        float aaSize = 1f)
    {
        float dx = b.X - a.X;
        float dy = b.Y - a.Y;
        float lenSq = dx * dx + dy * dy;

        if (lenSq <= float.Epsilon) return this;

        float len = (float) System.Math.Sqrt(lenSq);
        float nx = -dy / len;
        float ny = dx / len;

        float half = thickness * 0.5f;
        float outer = half + aaSize;

        GUIColor transparent = new(color.R, color.G, color.B, 0);
        GUIColor opaque = new(color.R, color.G, color.B, 255);

        InternalAddStrip(ref meshData, a, b, nx, ny, -half, half, color, color);

        if (aaSize > 0)
        {
            InternalAddStrip(ref meshData, a, b, nx, ny, half, outer, opaque, transparent);
            InternalAddStrip(ref meshData, a, b, nx, ny, -outer, -half, transparent, opaque);
        }

        return this;
    }

    public abstract TContextValue Build();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void InternalAddStrip(ref TMeshValue meshData,
        GUIVector2 a, GUIVector2 b, float nx, float ny,
        float r0, float r1, in GUIColor c0, in GUIColor c1)
    {
        GUIVector2 p0 = new(a.X + nx * r0, a.Y + ny * r0);
        GUIVector2 p1 = new(a.X + nx * r1, a.Y + ny * r1);
        GUIVector2 p2 = new(b.X + nx * r1, b.Y + ny * r1);
        GUIVector2 p3 = new(b.X + nx * r0, b.Y + ny * r0);

        float minX = GUIMath.Min(GUIMath.Min(p0.X, p1.X), GUIMath.Min(p2.X, p3.X));
        float maxX = GUIMath.Max(GUIMath.Max(p0.X, p1.X), GUIMath.Max(p2.X, p3.X));
        float minY = GUIMath.Min(GUIMath.Min(p0.Y, p1.Y), GUIMath.Min(p2.Y, p3.Y));
        float maxY = GUIMath.Max(GUIMath.Max(p0.Y, p1.Y), GUIMath.Max(p2.Y, p3.Y));

        GUIRectangle rect = new(minX, minY, maxX - minX, maxY - minY);

        AddRect(rect, c0, c0, c1, c1, meshData);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowMeshDataNull()
        => throw new InvalidOperationException("MeshData is null. Make sure it was passed to the constructor.");
}