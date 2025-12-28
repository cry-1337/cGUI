using cGUI.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render;

public sealed class UnityMeshRender(IRenderGraphics renderGraphics) : IRender
{
    private IRenderGraphics m_RenderGraphics = renderGraphics;

    public void PushMesh(in IMeshRenderContext ctx)
    {
        m_RenderGraphics.SetViewProjection(new(0, 0, Screen.width, Screen.height));
        m_RenderGraphics.Process(ctx);
        ProcessBuffer();
    }
    public void PushRenderGraphics(IRenderGraphics graphics) => m_RenderGraphics = graphics;

    public void ProcessBuffer() => m_RenderGraphics.ExecuteBuffer();

    public void Dispose()
    {
        m_RenderGraphics.Dispose();
    }
}
