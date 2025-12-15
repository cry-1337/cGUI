using System;

namespace cGUI.Render.Abstraction;

public interface IRender : IDisposable
{
    void PushMesh(IMeshRenderContext ctx);
    void PushRenderGraphics(IRenderGraphics graphics);
    void Render();
    void ProcessBuffer();
}