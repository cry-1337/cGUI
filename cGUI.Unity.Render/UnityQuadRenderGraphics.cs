using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Extensions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace cGUI.Unity.Render;

public sealed class UnityQuadRenderGraphics : IRenderGraphics<IUnityMeshRenderContext>
{
    private static readonly VertexAttributeDescriptor[] m_VertexAttributes =
    [
        new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 1)
    ];

    private static readonly int m_ViewProjectionId = Shader.PropertyToID("_VP");

    private readonly CommandBuffer m_Buffer;
    private readonly Mesh m_Mesh;

    public UnityQuadRenderGraphics()
    {
        (m_Mesh = new()).MarkDynamic();
        m_Buffer = new() { name = nameof(UnityQuadRenderGraphics) };
    }

    public void Process(IUnityMeshRenderContext ctx)
    {
        const MeshUpdateFlags MESH_UPDATE_FLAGS =
            MeshUpdateFlags.DontNotifyMeshUsers |
            MeshUpdateFlags.DontRecalculateBounds |
            MeshUpdateFlags.DontResetBoneBounds |
            MeshUpdateFlags.DontValidateIndices;

        var mesh = m_Mesh;

        mesh.Clear();

        mesh.SetIndexBufferParams(ctx.IndiciesCount, IndexFormat.UInt32);
        mesh.SetVertexBufferParams(ctx.VerticiesCount, m_VertexAttributes);

        mesh.SetVertexBufferData(ctx.Vertices, 0, 0, ctx.VerticiesCount, 0, MESH_UPDATE_FLAGS);
        mesh.SetIndexBufferData(ctx.Indicies, 0, 0, ctx.IndiciesCount, MESH_UPDATE_FLAGS);

        for (int i = 0; i < ctx.MeshCount; i++)
        {
            IUnityMeshData data = ctx.Meshes.ElementAt(i);

            var descriptor = new SubMeshDescriptor()
            {
                topology = data.Topology,
                baseVertex = 0,
                firstVertex = data.VerticesOffset,
                vertexCount = data.VerticiesCount,
                indexStart = data.IndicesOffset,
                indexCount = data.IndicesCount
            };

            mesh.SetSubMesh(i, descriptor, MESH_UPDATE_FLAGS);
        }

        var cmdBuffer = m_Buffer;
        cmdBuffer.Clear();
        
        for (int i = 0; i < ctx.MeshCount; i++)
        {
            IUnityMeshData data = ctx.Meshes.ElementAt(i);
            cmdBuffer.DrawMesh(mesh, Matrix4x4.TRS(Vector3.zero, data.Rotation, Vector3.one), data.Material, i, -1, data.MaterialProperties);
        }
    }

    public void Process(IRenderContext ctx) => Process(ctx as IUnityMeshRenderContext);

    public void SetViewProjection(in GUIRectangle rect)
    {
        var view = Matrix4x4.identity;
        var proj = Matrix4x4.Ortho(rect.X, rect.Width, rect.Y, rect.Height, short.MinValue, short.MaxValue);
        var gpuProj = GL.GetGPUProjectionMatrix(proj, true);

        m_Buffer.SetGlobalMatrix(m_ViewProjectionId, view * gpuProj);
    }

    public void ExecuteBuffer()
    {
        Graphics.ExecuteCommandBuffer(m_Buffer);
        m_Buffer.Clear();
    }

    public void Dispose()
    {
        if (m_Mesh != null)
            UnityEngine.Object.Destroy(m_Mesh);

        m_Buffer.Release();
    }
}