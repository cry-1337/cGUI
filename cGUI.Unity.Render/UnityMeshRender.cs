using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Contexts;
using UnityEngine;

namespace cGUI.Unity.Render;

public sealed class UnityMeshRender(IRenderGraphics<IMeshRenderContext<UnityMeshData>> renderGraphics) : IRender<IMeshRenderContext<UnityMeshData>>
{
    private IRenderGraphics<IMeshRenderContext<UnityMeshData>> m_RenderGraphics = renderGraphics;
    private readonly UnityMeshRenderContext m_Buffer = new();

    public void PushMesh(IMeshRenderContext<UnityMeshData> ctx)
    {
        int baseVtx = m_Buffer.VerticiesCount;
        int baseIdx = m_Buffer.IndiciesCount;

        for (int i = 0; i < ctx.VerticiesCount; i++)
            m_Buffer.Vertices.Add(ctx.Vertices[i]);

        for (int i = 0; i < ctx.IndiciesCount; i++)
            m_Buffer.Indicies.Add(ctx.Indicies[i] + baseVtx);

        for (int i = 0; i < ctx.MeshesCount; i++)
        {
            var mesh = ctx.Meshes[i];
            mesh.VerticesOffset += baseVtx;
            mesh.IndiciesOffset += baseIdx;
            m_Buffer.Meshes.Add(mesh);
        }
    }

    public void PushRenderGraphics(IRenderGraphics<IMeshRenderContext<UnityMeshData>> graphics) => m_RenderGraphics = graphics;

    public void ProcessBuffer()
    {
        if (m_Buffer.MeshesCount == 0) return;

        m_RenderGraphics.SetViewProjection(new(0, 0, Screen.width, Screen.height));
        m_RenderGraphics.Process(m_Buffer);
        m_RenderGraphics.ExecuteBuffer();
        m_Buffer.Clear();
    }

    public void Dispose()
    {
        m_RenderGraphics.Dispose();
    }
}
