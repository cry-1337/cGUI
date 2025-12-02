using cGUI.Render.Abstraction;
using cGUI.Render.Contexts;
using UnityEngine.Rendering;

namespace cGUI.Unity.Render;

public class UnityQuadRenderGraphics : IRenderGraphics<IQuadRenderContext>
{
    private readonly CommandBuffer m_Buffer = new() { name = nameof(UnityQuadRenderGraphics) };

    public void Process(IQuadRenderContext ctx)
    {
        m_Buffer.Clear();
    }

    public void Process(IRenderContext ctx) => Process(ctx as IQuadRenderContext);
}
