using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace cGUI.Unity.Render;

public sealed class UnityMeshRenderGraphics : IRenderGraphics<IMeshRenderContext<UnityMeshData>>
{
    private static readonly VertexAttributeDescriptor[] m_VertexAttributes =
    [
        new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 1)
    ];

    private CommandBuffer m_Buffer = new() { name = nameof(UnityMeshRenderGraphics) };
    private Mesh? m_Mesh;

    public void Process(IMeshRenderContext<UnityMeshData> ctx)
    {
        const MeshUpdateFlags MESH_UPDATE_FLAGS =
            MeshUpdateFlags.DontNotifyMeshUsers |
            MeshUpdateFlags.DontRecalculateBounds |
            MeshUpdateFlags.DontResetBoneBounds |
            MeshUpdateFlags.DontValidateIndices;

        if (m_Mesh == null)
        {
            m_Mesh = new Mesh();
            m_Mesh.MarkDynamic();
        }

        m_Mesh.Clear(true);

        m_Mesh.SetIndexBufferParams(ctx.IndiciesCount, IndexFormat.UInt32);
        m_Mesh.SetVertexBufferParams(ctx.VerticiesCount, m_VertexAttributes);

        m_Mesh.SetVertexBufferData(ctx.Vertices, 0, 0, ctx.VerticiesCount, 0, MESH_UPDATE_FLAGS);
        m_Mesh.SetIndexBufferData(ctx.Indicies, 0, 0, ctx.IndiciesCount, MESH_UPDATE_FLAGS);

        var descriptors = new NativeArray<SubMeshDescriptor>(ctx.MeshCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        for (int i = 0; i < ctx.MeshCount; i++)
        {
            IUnityMeshData data = ctx.Meshes.ElementAt(i);

            descriptors[i] = new SubMeshDescriptor()
            {
                topology = data.Topology,
                baseVertex = 0,
                firstVertex = data.VerticesOffset,
                vertexCount = data.VerticiesCount,
                indexStart = data.IndiciesOffset,
                indexCount = data.IndicesCount
            };
        }
        m_Mesh.SetSubMeshes(descriptors, MESH_UPDATE_FLAGS);

        m_Mesh.UploadMeshData(false);

        for (int i = 0; i < ctx.MeshCount; i++)
        {
            IUnityMeshData data = ctx.Meshes.ElementAt(i);
            m_Buffer.DrawMesh(m_Mesh, Matrix4x4.TRS(Vector3.zero, data.Rotation, Vector3.one), data.Material, i, -1, data.MaterialProperties);
        }
    }

    public void Process(IRenderContext ctx) => Process((IMeshRenderContext<UnityMeshData>) ctx);

    public void SetViewProjection(in GUIRectangle rect)
        => m_Buffer.SetViewProjectionMatrices(Matrix4x4.identity, GL.GetGPUProjectionMatrix(Matrix4x4.Ortho(rect.X, rect.Width, rect.Y, rect.Height, short.MinValue, short.MaxValue), false));

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