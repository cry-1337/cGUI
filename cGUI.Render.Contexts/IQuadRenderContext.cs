using cGUI.Render.Abstraction;

namespace cGUI.Render.Contexts;

public interface IQuadRenderContext : IRenderContext
{
    int VerticiesCount { get; }
    int IndiciesCount { get; }
    void CopyVerticices(Vertex[] array);
    void CopyIndicies(int[] array);
    void AddVertex(Vertex vertex);
    void AddIndex(int index);
    void Clear();
}