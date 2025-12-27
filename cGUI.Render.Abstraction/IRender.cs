using System;

namespace cGUI.Render.Abstraction;

public interface IRender : IDisposable
{
    void PushMesh(in IMeshRenderContext ctx);
    void PushRenderGraphics(IRenderGraphics graphics);
    void ProcessBuffer();
}