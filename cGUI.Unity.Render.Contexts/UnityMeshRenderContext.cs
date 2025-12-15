using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Unity.Render.Contexts;

public readonly struct UnityMeshRenderContext() : IUnityMeshRenderContext
{
    public readonly List<IUnityMeshData> Meshes => new(2);

    public readonly int MeshCount => Meshes.Count;

    public readonly int VerticiesCount => Vertices.Count;

    public readonly int IndiciesCount => Indicies.Count;

    public readonly List<Vertex> Vertices => new(16);

    public readonly List<int> Indicies => new(12);

    public readonly void AddIndex(int index) => Indicies.Add(index);

    public readonly void AddVertex(Vertex vertex) => Vertices.Add(vertex);

    public readonly void Clear()
    {
        Meshes.Clear();
        Vertices.Clear();
        Indicies.Clear();
    }
}