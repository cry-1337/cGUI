using System.Collections.Generic;

namespace cGUI.Render.Abstraction;

public interface IMeshRenderContext : IRenderContext
{
    int VerticiesCount { get; }
    int IndiciesCount { get; }
    List<Vertex> Vertices { get; }
    List<int> Indicies { get; }
}

public interface IMeshRenderContext<TValue> : IMeshRenderContext where TValue : IMeshData
{
    List<TValue> Meshes { get; }
    int MeshesCount { get; }
}