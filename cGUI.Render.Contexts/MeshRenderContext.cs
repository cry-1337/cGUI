using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public readonly struct MeshRenderContext() : IMeshRenderContext
{
    private readonly List<Vertex> m_Vertices = new(12);
    private readonly List<int> m_Indices = new(16);

    public readonly int VerticiesCount => Vertices.Count;

    public readonly int IndiciesCount => Indicies.Count;

    public readonly List<Vertex> Vertices => m_Vertices;

    public readonly List<int> Indicies => m_Indices;

    public readonly void AddIndex(int index) => Indicies.Add(index);

    public readonly void AddVertex(Vertex vertex) => Vertices.Add(vertex);

    public readonly void Clear()
    {
        Indicies.Clear(); 
        Vertices.Clear();
    }
}