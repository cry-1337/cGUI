using cGUI.Render.Abstraction;
using cGUI.Render.Contexts.Builder;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Builder;

/// <param name="meshValue">Will be used in methods without MeshValue (example: AddRect(rect, color)) instead of creating new instances of MeshData</param>
public class UnityMeshRenderContextBuilder(IMeshRenderContext<UnityMeshData> ctx, UnityMeshData? meshValue) :
    MeshRenderContextBuilder<IMeshRenderContext<UnityMeshData>, UnityMeshData>(ctx, meshValue!.Value)
{
    public override IMeshRenderContext<UnityMeshData> Build() => m_RenderContext;
}