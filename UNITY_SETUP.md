# cGUI Unity Integration - Complete Setup Guide

## Overview

This guide shows how to integrate cGUI into a Unity project and set up rendering for the new element system.

## Step 1: Create Material from Shader

Place your shader in `Assets/Shaders/cGUI_SolidQuad.shader` and create a material:

1. In Project tab, right-click → Create → Material
2. Name it `cGUI_SolidQuad`
3. In Inspector, set Shader to `cGUI/SolidQuad`
4. Save to `Assets/Materials/cGUI_SolidQuad.mat`

## Step 2: Create UIManager Script

Create `Assets/Scripts/UIManager.cs` in your Unity project:

```csharp
using UnityEngine;
using cGUI.Abstraction.Structs;
using cGUI.Element.Models;
using cGUI.Event;
using cGUI.Events.Models.Input;
using cGUI.Events.Models.Layout;
using cGUI.Events.Models.Render;
using cGUI.Layout;
using cGUI.Layout.Abstraction;
using cGUI.Render.Abstraction;
using cGUI.Unity.Render;
using cGUI.Unity.Render.Abstraction;
using cGUI.Visual;
using cGUI.Examples;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Material m_GuiMaterial;

    private EventDispatcher m_EventDispatcher = new();
    private IElementLayout m_Layout = new ElementLayout();
    private IRender<IMeshRenderContext<UnityMeshData>> m_Renderer;
    private VisualElement m_RootElement;

    private Vector2 m_LastMousePos = Vector2.zero;

    private void Start()
    {
        // Load material
        if (m_GuiMaterial == null)
            m_GuiMaterial = Resources.Load<Material>("Materials/cGUI_SolidQuad");

        GUIGlobals.GlobalMaterial = m_GuiMaterial;

        // Initialize renderer
        var renderGraphics = new UnityMeshRenderGraphics();
        m_Renderer = new UnityMeshRender(renderGraphics);

        // Create UI from example
        var exampleUI = new ExampleUI();
        m_RootElement = exampleUI.CreateRootPanel();

        // Register root element for events
        if (m_RootElement is IEventHandler<RenderEvent> renderHandler)
            m_EventDispatcher.Register(m_RootElement, renderHandler);
        if (m_RootElement is IEventHandler<LayoutEvent> layoutHandler)
            m_EventDispatcher.Register(m_RootElement, layoutHandler);
        if (m_RootElement is IEventHandler<PostLayoutEvent> postLayoutHandler)
            m_EventDispatcher.Register(m_RootElement, postLayoutHandler);
        if (m_RootElement is IEventHandler<MouseMoveEvent> mouseMoveHandler)
            m_EventDispatcher.Register(m_RootElement, mouseMoveHandler);
        if (m_RootElement is IEventHandler<MouseKeyDownEvent> mouseDownHandler)
            m_EventDispatcher.Register(m_RootElement, mouseDownHandler);
        if (m_RootElement is IEventHandler<MouseKeyUpEvent> mouseUpHandler)
            m_EventDispatcher.Register(m_RootElement, mouseUpHandler);

        m_LastMousePos = Input.mousePosition;
    }

    private void Update()
    {
        // Reset layout for new frame
        m_Layout.Reset();

        // 1. Layout pass: Compute element positions
        var layoutEvent = new LayoutEvent(m_Layout, force: false);
        m_EventDispatcher.Dispatch(m_RootElement, layoutEvent);

        // Perform layout computation
        var context = new LayoutContext
        {
            ParentRect = new GUIRectangle(0, 0, Screen.width, Screen.height),
            RemainingRect = new GUIRectangle(0, 0, Screen.width, Screen.height),
            CurrentOffset = new GUIVector2(0, 0),
            ElementsLeft = 0
        };
        m_Layout.PerformLayout(context, overrideElementsCount: true);

        // 2. Input pass: Dispatch mouse events
        DispatchInputEvents();

        // 3. Render pass: Push meshes to buffer
        var renderEvent = new RenderEvent(new UnityRenderAdapter(m_Renderer));
        m_EventDispatcher.Dispatch(m_RootElement, renderEvent);

        // 4. PostLayout pass: Build color-blended meshes
        var postLayoutEvent = new PostLayoutEvent(m_Layout, Time.deltaTime);
        m_EventDispatcher.Dispatch(m_RootElement, postLayoutEvent);

        // 5. Flush buffer: Render all accumulated meshes
        m_Renderer.ProcessBuffer();
    }

    private void DispatchInputEvents()
    {
        Vector2 currentMousePos = Input.mousePosition;
        Vector2 mouseDelta = currentMousePos - m_LastMousePos;

        // Mouse move
        var mouseMoveEvent = new MouseMoveEvent(
            new GUIVector2(currentMousePos.x, currentMousePos.y),
            new GUIVector2(mouseDelta.x, mouseDelta.y)
        );
        m_EventDispatcher.Dispatch(m_RootElement, mouseMoveEvent);

        // Mouse down
        if (Input.GetMouseButtonDown(0))
        {
            var mouseDownEvent = new MouseKeyDownEvent(
                new GUIVector2(currentMousePos.x, currentMousePos.y),
                0
            );
            m_EventDispatcher.Dispatch(m_RootElement, mouseDownEvent);
        }

        // Mouse up
        if (Input.GetMouseButtonUp(0))
        {
            var mouseUpEvent = new MouseKeyUpEvent(
                new GUIVector2(currentMousePos.x, currentMousePos.y),
                0
            );
            m_EventDispatcher.Dispatch(m_RootElement, mouseUpEvent);
        }

        m_LastMousePos = currentMousePos;
    }

    private void OnDestroy()
    {
        GUIGlobals.GlobalMaterial = null;
        m_Renderer?.Dispose();
    }

    /// <summary>
    /// Adapter to pass IRender into RenderEvent
    /// </summary>
    private class UnityRenderAdapter : IRender<IMeshRenderContext<UnityMeshData>>
    {
        private readonly IRender<IMeshRenderContext<UnityMeshData>> m_Renderer;

        public UnityRenderAdapter(IRender<IMeshRenderContext<UnityMeshData>> renderer) => m_Renderer = renderer;

        public void PushMesh(IMeshRenderContext<UnityMeshData> ctx) => m_Renderer.PushMesh(ctx);
        public void PushRenderGraphics(IRenderGraphics<IMeshRenderContext<UnityMeshData>> graphics) => m_Renderer.PushRenderGraphics(graphics);
        public void ProcessBuffer() => m_Renderer.ProcessBuffer();
        public void Dispose() => m_Renderer.Dispose();
    }
}

public static class GUIGlobals
{
    public static Material GlobalMaterial { get; set; }
}
```

## Step 3: Setup Scene

1. Create new scene
2. Add empty GameObject named "UIManager"
3. Add UIManager component to it
4. In Inspector, drag `cGUI_SolidQuad.mat` to "M Gui Material" field
5. Hit Play

## Frame Rendering Pipeline

Each frame executes in order:

```
Update() {
    m_Layout.Reset()

    // 1. Layout Phase
    DispatchLayoutEvent()              // Tells elements to register their nodes
    m_Layout.PerformLayout()           // Computes positions for all nodes

    // 2. Input Phase
    DispatchInputEvents()              // Mouse move/click → MouseMoveEvent, MouseKeyDown/UpEvent

    // 3. Render Phase
    DispatchRenderEvent()              // Elements push meshes to UnityMeshRender buffer

    // 4. PostLayout Phase
    DispatchPostLayoutEvent()          // Elements update tweens and build colored meshes

    // 5. GPU Flush
    m_Renderer.ProcessBuffer()         // Accumulates all meshes and calls Graphics.ExecuteCommandBuffer() once
}
```

## Event Dispatch Order

Each dispatcher call uses `EventDispatcher.Dispatch()` which calls registered handlers:

```csharp
m_EventDispatcher.Dispatch(m_RootElement, layoutEvent)
  ↓
m_RootElement.Handle(layoutEvent)  // IEventHandler<LayoutEvent>
  ↓ (if container)
foreach child: child.Handle(layoutEvent)  // Cascades via VisualContainer
```

## Key Points

### 1. Containers Cascade Events Automatically
When you dispatch to a `PanelElement`, it automatically calls `HandleEvents()` which:
- Calls the panel's own `IEventHandler<T>.Handle()` first
- Then cascades to all children

So you only register the **root element** - everything else cascades.

### 2. Render Buffer Accumulation
Before: Each element called `ProcessBuffer()` immediately → stale mesh references
Now: `PushMesh()` just appends data, `ProcessBuffer()` called once → correct batching

### 3. Color Tweens Update Every Frame
In `PostLayoutEvent`, each element:
- Updates its `StateTween<float>` with current frame delta
- Computes blended colors (Normal → Hover → Press → Toggle)
- Builds mesh with blended colors

No per-frame allocations - buffers pre-allocated in constructors.

### 4. Input Hit Testing
`MouseMoveEvent` → Element does `HitTest()` → sets `m_IsHovered`
`MouseKeyDownEvent` → Element does `HitTest()` → sets `m_IsPressed`
`MouseKeyUpEvent` → If was pressed AND still hit → `OnClick()`

## Customization

### Custom UI Tree

Instead of `ExampleUI`, create your own:

```csharp
var myUI = CreateMyUI();  // Returns VisualElement (typically PanelElement)
m_RootElement = myUI;
```

### Custom Element Types

```csharp
public class CustomSlider : HoverableElement
{
    public float Value { get; private set; }

    protected override void OnClick()
    {
        Value = ComputeFromMousePos();
    }
}
```

### Custom Tweens

```csharp
var options = new ElementTweenOptions
{
    HoverInDuration = 0.3f,
    HoverOutDuration = 0.2f,
    HoverEasing = EaseType.InOutCubic,  // Any EaseType value
    PressInDuration = 0.1f,
    PressOutDuration = 0.15f,
    PressEasing = EaseType.OutQuad,
    ToggleDuration = 0.5f,
    ToggleEasing = EaseType.InOutElastic
};

var button = new ButtonElement("btn", options, /* colors... */, tweenOptions: options);
```

## Troubleshooting

### Elements not rendering
- Verify material is assigned and shader is found
- Check `GUIGlobals.GlobalMaterial` is set in `Start()`
- Verify `ProcessBuffer()` is called once per frame

### Colors animating too fast/slow
- Check `Time.deltaTime` is passed to `PostLayoutEvent`
- Verify `ElementTweenOptions` durations (in seconds)

### Input not working
- Verify `Input.mousePosition` is world/screen space (should be screen)
- Check element's `IsHittable` flag
- Make sure parent containers are active (`IsActive`)

### Layout not working
- Verify `LayoutEvent` is dispatched before `RenderEvent`
- Check `PerformLayout()` is called after all nodes are pushed
- Verify `DesiredRect` is not zero-sized

## Performance Tips

1. **Pre-allocate colors**: Done automatically in `HoverableElement`, `ClickableElement`, `ToggleElement`
2. **Batch render calls**: `ProcessBuffer()` called once/frame accumulates all meshes
3. **No input lag**: Input events dispatched in same frame as render
4. **Efficient tweening**: `StateTween<float>` uses single blend factor, not per-color tweens

## File Structure

```
Assets/
├── Materials/
│   └── cGUI_SolidQuad.mat
├── Shaders/
│   └── cGUI_SolidQuad.shader
├── Scripts/
│   └── UIManager.cs
└── Scenes/
    └── UIScene.unity
```

That's it! You now have a working cGUI implementation with rendering, layout, input, and color animations.
