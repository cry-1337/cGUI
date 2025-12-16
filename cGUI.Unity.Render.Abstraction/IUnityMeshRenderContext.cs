using cGUI.Render.Abstraction;
using System.Collections.Generic;

namespace cGUI.Unity.Render.Abstraction;

public interface IUnityMeshRenderContext : IMeshRenderContext 
{
    List<IUnityMeshData> Meshes { get; }
    int MeshCount { get; }
}