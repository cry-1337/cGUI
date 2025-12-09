using System;

namespace cGUI.Render.Abstraction;

public interface IRender : IDisposable
{
    void PushQuadContext(in IQuadRenderContext ctx);
    void PushRenderGraphics(IRenderGraphics graphics);
    void Render();
    void ProcessBuffer();
}