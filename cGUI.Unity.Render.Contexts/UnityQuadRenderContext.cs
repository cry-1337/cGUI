using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Unity.Render.Contexts;

public struct UnityQuadRenderContext() : IUnityMeshRenderContext
{
    public List<IUnityMeshData> Meshes => new(2);

    public int MeshCount => Meshes.Count;

    public int VerticiesCount => Vertices.Count;

    public int IndiciesCount => Indicies.Count;

    public List<Vertex> Vertices => new(16);

    public List<int> Indicies => new(12);

    public void AddIndex(int index) => Indicies.Add(index);

    public void AddVertex(Vertex vertex) => Vertices.Add(vertex);

    public void Clear()
    {
        Meshes.Clear();
        Vertices.Clear();
        Indicies.Clear();
    }
}