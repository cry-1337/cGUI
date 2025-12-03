using cGUI.Render.Abstraction;
using cGUI.Render.Contexts;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace cGUI.Unity.Render;

public class UnityQuadRenderGraphics : IRenderGraphics<IQuadRenderContext>, IDisposable
{
    private readonly CommandBuffer m_Buffer = new() { name = nameof(UnityQuadRenderGraphics) };
    private Mesh m_Mesh;

    public void Process(IQuadRenderContext ctx)
    {
        if (m_Mesh == null)
            m_Mesh = new Mesh();

        m_Mesh.Clear();

        m_Buffer.Clear();
    }

    public void Process(IRenderContext ctx) => Process(ctx as IQuadRenderContext);

    public void Dispose()
    {
        UnityEngine.Object.Destroy(m_Mesh);
    }
}
