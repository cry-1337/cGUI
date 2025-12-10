using cGUI.Abstraction.Structs;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render.Abstraction;
using cGUI.Unity.Render.Extensions;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace cGUI.Unity.Render;

public sealed class UnityQuadRenderGraphics(Material material) : IRenderGraphics<IUnityQuadRenderContext>
{
    private const MeshUpdateFlags MESH_UPDATE_FLAGS =
        MeshUpdateFlags.DontNotifyMeshUsers |
        MeshUpdateFlags.DontRecalculateBounds |
        MeshUpdateFlags.DontResetBoneBounds |
        MeshUpdateFlags.DontValidateIndices;

    private static readonly VertexAttributeDescriptor[] m_VertexAttributes =
    [
        new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 1)
    ];

    private static readonly int m_ViewProjectionId = Shader.PropertyToID("_VP");

    private static readonly int m_InvColorMul = Shader.PropertyToID("_InvColorMul");
    private static readonly int m_MainTexId = Shader.PropertyToID("_MainTex");
    private static readonly int m_MaskEnabledId = Shader.PropertyToID("_MaskEnable");
    private static readonly int m_MaskRectId = Shader.PropertyToID("_MaskRect");
    private static readonly int m_MaskCornerRadiusId = Shader.PropertyToID("_MaskCornerRadius");

    private readonly CommandBuffer m_Buffer = new() { name = nameof(UnityQuadRenderGraphics) };

    private readonly Material m_Material = material;
    private readonly MaterialPropertyBlock m_MaterialProperties = new();

    private int[] m_Indicies;
    private Vertex[] m_Verticies;

    private Mesh m_Mesh;

    public void Process(IUnityQuadRenderContext ctx)
    {
        if (m_Mesh == null)
        {
            m_Mesh = new Mesh();
            m_Mesh.MarkDynamic();
        }

        EnsureArrays(ctx.VerticiesCount, ctx.IndiciesCount);

        ctx.CopyVerticices(m_Verticies);
        ctx.CopyIndicies(m_Indicies);

        m_Mesh.Clear();

        m_Mesh.SetIndexBufferParams(ctx.IndiciesCount, IndexFormat.UInt32);
        m_Mesh.SetVertexBufferParams(ctx.VerticiesCount, m_VertexAttributes);

        m_Mesh.SetVertexBufferData(m_Verticies, 0, 0, ctx.VerticiesCount, 0, MESH_UPDATE_FLAGS);
        m_Mesh.SetIndexBufferData(m_Indicies, 0, 0, ctx.IndiciesCount, MESH_UPDATE_FLAGS);

        m_Mesh.subMeshCount = 1;
        m_Mesh.SetSubMesh(0, new SubMeshDescriptor()
        {
            indexStart = 0,
            indexCount = ctx.IndiciesCount,
            vertexCount = ctx.VerticiesCount
        }, MESH_UPDATE_FLAGS);

        if (ctx.Texture != null) m_MaterialProperties.SetTexture(m_MainTexId, ctx.Texture);
        m_MaterialProperties.SetFloat(m_InvColorMul, ctx.ColorAlphaMultiplier);

        if (ctx.MaskRectangle != null)
        {
            m_MaterialProperties.SetInteger(m_MaskEnabledId, 1);
            m_MaterialProperties.SetVector(m_MaskRectId, ctx.MaskRectangle.Value.ToVector4());
            m_MaterialProperties.SetVector(m_MaskCornerRadiusId, ctx.CornerRoundRadius.ToVector4());
        }
        else m_MaterialProperties.SetInteger(m_MaskEnabledId, 0);

        m_Buffer.DrawMesh(m_Mesh, Matrix4x4.identity, m_Material, 0, -1, m_MaterialProperties);
    }

    public void Process(in IRenderContext ctx) => Process(ctx as IUnityQuadRenderContext);

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

    private void EnsureArrays(int vCount, int iCount)
    {
        if (m_Verticies == null || m_Verticies.Length < vCount)
            m_Verticies = new Vertex[vCount];

        if (m_Indicies == null || m_Indicies.Length < iCount)
            m_Indicies = new int[iCount];
    }
}