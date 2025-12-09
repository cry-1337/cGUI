using cGUI.Abstraction.Structs;

namespace cGUI.Render.Abstraction;

public interface IQuadRenderContext : IRenderContext
{
    int VerticiesCount { get; }
    int IndiciesCount { get; }
    GUIRectangle CornerRoundRadius { get; set; }
    void CopyVerticices(Vertex[] array);
    void CopyIndicies(int[] array);
    void AddVertex(Vertex vertex);
    void AddIndex(int index);
    void Clear();
}