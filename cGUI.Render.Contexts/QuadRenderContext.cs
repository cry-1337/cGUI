using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Render.Contexts;

public struct QuadRenderContext() : IQuadRenderContext
{
    private readonly List<int> m_Indicies = new(32);

    public List<Vertex> Verticies = new(32);

    public int VerticiesCount => Verticies.Count;

    public int IndiciesCount => m_Indicies.Count;

    public void AddIndex(int index) => m_Indicies.Add(index);

    public void AddVertex(Vertex vertex) => Verticies.Add(vertex);

    public void Clear()
    {
        m_Indicies?.Clear();
        Verticies.Clear();
    }

    public void CopyIndicies(int[] array) => m_Indicies.CopyTo(array);

    public void CopyVerticices(Vertex[] array) => Verticies.CopyTo(array);
}