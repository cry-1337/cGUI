using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render;

public sealed class UnityMeshRender(IRenderGraphics<IMeshRenderContext<UnityMeshData>> renderGraphics) : IRender<IMeshRenderContext<UnityMeshData>>
{
    private IRenderGraphics<IMeshRenderContext<UnityMeshData>> m_RenderGraphics = renderGraphics;

    public void PushMesh(IMeshRenderContext<UnityMeshData> ctx)
    {
        m_RenderGraphics.SetViewProjection(new(0, 0, Screen.width, Screen.height));
        m_RenderGraphics.Process(ctx);
        ProcessBuffer();
    }
    public void PushRenderGraphics(IRenderGraphics<IMeshRenderContext<UnityMeshData>> graphics) => m_RenderGraphics = graphics;

    public void ProcessBuffer() => m_RenderGraphics.ExecuteBuffer();

    public void Dispose()
    {
        m_RenderGraphics.Dispose();
    }
}
