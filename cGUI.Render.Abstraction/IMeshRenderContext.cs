using System.Collections.Generic;

namespace cGUI.Render.Abstraction;

public interface IMeshRenderContext : IRenderContext
{
    int VerticiesCount { get; }
    int IndiciesCount { get; }
    List<Vertex> Vertices { get; }
    List<int> Indicies { get; }
    void AddVertex(Vertex vertex);
    void AddIndex(int index);
    void Clear();
}