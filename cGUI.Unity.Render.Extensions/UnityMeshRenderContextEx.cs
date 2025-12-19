using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Extensions;

public static class UnityMeshRenderContextEx
{
    extension (IMeshRenderContext<IUnityMeshData> ctx)
    {
        public void AddQuad(Vertex v1, Vertex v2, Vertex v3, Vertex v4, IUnityMeshData meshData)
        {
            var vertices = ctx.Vertices;
            var indices = ctx.Indicies;

            int baseVtx = vertices.Count;
            int baseIdx = indices.Count;

            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);

            indices.Add(baseVtx + 0);
            indices.Add(baseVtx + 1);
            indices.Add(baseVtx + 2);
            indices.Add(baseVtx + 2);
            indices.Add(baseVtx + 3);
            indices.Add(baseVtx + 0);

            meshData.VerticesOffset = baseVtx;
            meshData.IndiciesOffset = baseIdx;
            meshData.VerticiesCount = 4;
            meshData.IndicesCount = 6;

            ctx.Meshes.Add(meshData);
        }
    }
}