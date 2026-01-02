using System;

namespace cGUI.Render.Abstraction;

public interface IRender<TValue> : IDisposable
    where TValue : IRenderContext
{
    void PushMesh(TValue ctx);
    void PushRenderGraphics(IRenderGraphics<TValue> graphics);
    void ProcessBuffer();
}