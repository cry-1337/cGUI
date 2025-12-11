using cGUI.Render.Abstraction;
using System.Collections.Generic;
using UnityEngine;

namespace cGUI.Unity.Render;

public sealed class UnityRender(IRenderGraphics renderGraphics) : IRender
{
    private readonly Queue<IRenderContext> m_RenderContexts = new(32);
    private IRenderGraphics m_RenderGraphics = renderGraphics;

    public void PushQuadContext(IQuadRenderContext ctx) => m_RenderContexts.Enqueue(ctx);
    public void PushRenderGraphics(IRenderGraphics graphics) => m_RenderGraphics = graphics;

    public void Render()
    {
        m_RenderGraphics.SetViewProjection(new(0, 0, Screen.width, Screen.height));

        while (m_RenderContexts.Count > 0)
        {
            var ctx = m_RenderContexts.Dequeue();
            m_RenderGraphics.Process(ctx);
        }
        ProcessBuffer();
    }

    public void ProcessBuffer() => m_RenderGraphics.ExecuteBuffer();

    public void Dispose()
    {
        m_RenderGraphics.Dispose();
    }
}
