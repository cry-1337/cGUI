using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public readonly struct MeshRenderContext() : IMeshRenderContext
{
    public readonly int VerticiesCount => Vertices.Count;

    public readonly int IndiciesCount => Indicies.Count;

    public readonly List<Vertex> Vertices => new(12);

    public readonly List<int> Indicies => new(16);

    public readonly void AddIndex(int index) => Indicies.Add(index);

    public readonly void AddVertex(Vertex vertex) => Vertices.Add(vertex);

    public readonly void Clear()
    {
        Indicies.Clear(); 
        Vertices.Clear();
    }
}