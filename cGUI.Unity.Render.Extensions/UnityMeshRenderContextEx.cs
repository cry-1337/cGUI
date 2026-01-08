using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using UnityEngine;

namespace cGUI.Unity.Render.Extensions;

public static class UnityMeshRenderContextEx
{
    extension (IMeshRenderContext<UnityMeshData> ctx)
    {
        public void AddQuad(in Vertex v1, in Vertex v2, in Vertex v3, in Vertex v4, ref UnityMeshData meshData)
        {
            var vertices = ctx.Vertices;
            var indices = ctx.Indicies;
            var meshes = ctx.Meshes;

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

            meshes.Add(meshData);
        }

        public void AddRect(in GUIRectangle rect, in GUIColor color, in Material material)
        {
            var meshData = new UnityMeshData(material);
            ctx.AddRect(rect, color, color, color, color, ref meshData);
        }

        public void AddRect(in GUIRectangle rect, in GUIColor colTopLeft, in GUIColor colTopRight, in GUIColor colBotLeft, in GUIColor colBotRight, in Material material)
        {
            var meshData = new UnityMeshData(material);
            ctx.AddRect(rect, colTopLeft, colTopRight, colBotLeft, colBotRight, ref meshData);
        }

        public void AddRect(in GUIRectangle rect, in GUIColor color, ref UnityMeshData meshData) => ctx.AddRect(rect, color, color, color, color, ref meshData);

        public void AddRect(in GUIRectangle rect, in GUIColor colTopLeft, in GUIColor colTopRight, in GUIColor colBotLeft, in GUIColor colBotRight, ref UnityMeshData meshData)
        {
            var v1 = new Vertex(new(rect.X, rect.Y), colBotLeft, default);
            var v2 = new Vertex(new(rect.X + rect.Width, rect.Y), colBotRight, default);
            var v3 = new Vertex(new(rect.X + rect.Width, rect.Y + rect.Height), colTopRight, default);
            var v4 = new Vertex(new(rect.X, rect.Y + rect.Height), colTopLeft, default);

            ctx.AddQuad(v1, v2, v3, v4, ref meshData);
        }
    }
}