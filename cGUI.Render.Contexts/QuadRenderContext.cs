using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public struct QuadRenderContext() : IMeshRenderContext
{
    public int VerticiesCount => Vertices.Count;

    public int IndiciesCount => Indicies.Count;

    public List<Vertex> Vertices => new(12);

    public List<int> Indicies => new(16);

    public void AddIndex(int index) => Indicies.Add(index);

    public void AddVertex(Vertex vertex) => Vertices.Add(vertex);

    public void Clear()
    {
        Indicies.Clear(); 
        Vertices.Clear();
    }
}