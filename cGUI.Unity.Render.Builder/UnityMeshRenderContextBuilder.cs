using cGUI.Render.Abstraction;
using cGUI.Render.Contexts.Builder;
using cGUI.Unity.Render.Abstraction;

namespace cGUI.Unity.Render.Builder;

/// <param name="meshValue">Will be used in methods without MeshValue (example: AddRect(rect, color)) instead of creating new instances of MeshData</param>
public class UnityMeshRenderContextBuilder(IMeshRenderContext<IUnityMeshData> ctx, IUnityMeshData? meshValue) :
    MeshRenderContextBuilder<IMeshRenderContext<IUnityMeshData>, IUnityMeshData>(ctx, meshValue!)
{
    public override IMeshRenderContext<IUnityMeshData> Build() => m_RenderContext;
}