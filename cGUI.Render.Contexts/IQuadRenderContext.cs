using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;

namespace cGUI.Render.Contexts;

public interface IQuadRenderContext : IRenderContext
{
    GUIColor[] Colors { get; }
}