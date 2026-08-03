# cGUI Element System - Unity Example Setup

## Overview

This example demonstrates the complete element system with animated color transitions and Zero-GC text rendering:

- **SimpleElement**: Static colored rectangles (no interaction)
- **HoverableElement**: Smooth color transition on mouse hover
- **ButtonElement**: Click detection with hover + press feedback
- **ToggleElement**: On/off state with dual color sets
- **PanelElement**: Container for nesting elements with background + padding
- **ScrollPanelElement**: Scrollable container panel with automatic clipping
- **TextElement**: Zero-GC text rendering via bitmap font atlas

## Setup Instructions

### 1. Create Material from Shader

Add the `cGUI/SolidQuad` shader to your project:

```
Assets/
├── Materials/
│   └── cGUI_SolidQuad.mat
└── Shaders/
    └── cGUI_SolidQuad.shader
```

Create a Material using the shader:
- In Unity Editor: `Right-click → Create → Material`
- Set Shader to `cGUI/SolidQuad`
- Save as `Assets/Materials/cGUI_SolidQuad.mat`

### 2. Create Example Scene

1. Create new Scene: `File → New Scene`
2. Create empty GameObject: `GameObject → Create Empty`
3. Name it `UIManager`
4. Add component: `Add Component → ExampleUI`
5. In Inspector, drag `cGUI_SolidQuad.mat` to the `m_GuiMaterial` field

### 3. Set Up Rendering Pipeline

The example uses **accumulated buffer rendering**. Each frame:

1. **Input Phase**: Input events are processed first
2. **Layout Phase**: Layout system positions all elements
3. **PostLayout Phase**: Containers perform child layouts
4. **PreRender Phase**: Elements build their color-blended meshes
5. **Render Phase**: Elements push mesh data to `UnityMeshRender` buffer
6. **Buffer Flush**: Single `ProcessBuffer()` call accumulates and renders all at once

For example, 5 elements = 1 mesh + 5 draw calls (not 5 meshes × 5 GPU submissions)

### 4. Element Hierarchy

```
RootPanel (PanelElement)
├── TitleBar (SimpleElement)
└── ContentPanel (PanelElement) [padded 20px]
    ├── HoverLabel (SimpleElement)
    ├── HoverBox (HoverableElement)
    ├── ButtonLabel (SimpleElement)
    ├── ButtonPanel (PanelElement) [padded 10px]
    │   ├── IncrementButton (ButtonElement)
    │   └── DecrementButton (ButtonElement)
    ├── ToggleLabel (SimpleElement)
    ├── ToggleSwitch (ToggleElement)
    └── StatusText (TextElement) [Zero-GC]
```

## Element Types Explained

### SimpleElement
Static colored rectangle. No interaction.

```csharp
var box = new SimpleElement(
    "Box",
    new ElementOption
    {
        DesiredRect = new GUIRectangle(100, 100, 200, 200),
        Color = new ElementColor(new GUIColor(255, 0, 0)),
        LayoutOptions = new[] { new DockOption(EDockType.Left) }
    }
);
```

### HoverableElement
Color smoothly transitions when mouse hovers. Configurable duration + easing.

```csharp
var hoverBox = new HoverableElement(
    "HoverBox",
    new ElementOption { /* ... */ },
    hoveredColor: new GUIColor[] {
        new(255, 100, 100), new(255, 100, 100),
        new(255, 100, 100), new(255, 100, 100)
    },
    tweenOptions: new ElementTweenOptions
    {
        HoverInDuration = 0.2f,
        HoverOutDuration = 0.15f,
        HoverEasing = EaseType.SmoothStep
    }
);
```

### ButtonElement
Extends HoverableElement. Detects clicks and fires callback. Supports custom press color.

```csharp
var button = new ButtonElement(
    "SubmitBtn",
    new ElementOption { /* ... */ },
    hoveredColor: new GUIColor[] { /* ... */ },
    pressedColor: new GUIColor[] { /* ... */ },
    onClick: () => Debug.Log("Clicked!"),
    tweenOptions: new ElementTweenOptions
    {
        HoverInDuration = 0.15f,
        PressInDuration = 0.05f,
        PressOutDuration = 0.1f,
        PressEasing = EaseType.OutQuad
    }
);

// Change callback at runtime
button.SetOnClick(() => Debug.Log("New callback"));
```

### ToggleElement
On/off state with separate color sets for each state. Color blends between states.

```csharp
var toggle = new ToggleElement(
    "EnableFeature",
    new ElementOption { /* ... */ },
    hoveredColor: /* off hover */,
    pressedColor: /* off press */,
    onColor: new GUIColor[] { new(0, 255, 0), /* ... */ }, // Green when on
    onHoveredColor: /* on hover */,
    onPressedColor: /* on press */,
    onToggle: (isOn) => Debug.Log($"Toggle: {isOn}"),
    initialState: false,
    tweenOptions: new ElementTweenOptions
    {
        ToggleDuration = 0.3f,
        ToggleEasing = EaseType.SmoothStep
    }
);

// Check state anytime
if (toggle.IsOn) { /* ... */ }

// Change callback at runtime
toggle.SetOnToggle((isOn) => Debug.Log($"New: {isOn}"));
```

### PanelElement
Container for nested elements with background color. Supports padding via `PaddingOption`.

```csharp
var panel = new PanelElement(
    "MainPanel",
    new ElementOption
    {
        DesiredRect = new GUIRectangle(50, 50, 500, 600),
        Color = new ElementColor(new GUIColor(40, 40, 50)),
        LayoutOptions = new ILayoutOption[]
        {
            new DockOption(EDockType.Fill),
            new PaddingOption(20) // 20px on all sides
        }
    }
);

// Add children
panel.Add(new SimpleElement(...));
panel.Add(new ButtonElement(...));

// Query children
if (panel.Has("ButtonID")) { /* ... */ }
var button = panel.Find("ButtonID");
```

### ScrollPanelElement
Container for scrollable child elements with viewport clipping and MaxScroll auto-calculation.

```csharp
var scrollPanel = new ScrollPanelElement(
    "ScrollPanel",
    new ElementOption { DesiredRect = new GUIRectangle(10, 10, 300, 400), Color = new ElementColor(new GUIColor(30, 30, 30)) },
    padding: 10f
);

// Scroll position control
scrollPanel.ScrollY += 20f;
scrollPanel.ConstrainScroll();
```

### TextElement
Zero-GC text rendering using FontAtlas. Formats strings and numbers without heap allocations.

```csharp
var fontAtlas = FontAtlas.CreateGridAtlas(charWidth: 8f, charHeight: 16f);
var label = new TextElement("FPSCounter", options, fontAtlas, initialText: "FPS: 144");

// Zero-GC integer update (no string allocation per frame)
label.SetText(currentFps);
```

## Color Animations

### Three-Level Blend Stack

Colors blend in layers:

```
1. Base Color
   ↓ [HoverTween]
2. Hovered Color
   ↓ [PressTween]
3. Pressed Color
```

For ToggleElement, an additional toggle blend:

```
Off Stack (Normal → Hover → Pressed)
   ↓ [ToggleTween]
On Stack (OnColor → OnHovered → OnPressed)
```

All blends happen in `ComputeColors()` every frame with no allocations.

## Layout Options

Build flexible layouts with `ILayoutOption[]`:

### DockOption
Dock element to edge or fill:
- `EDockType.Left` / `Right` / `Top` / `Bottom` / `Fill`

```csharp
new DockOption(EDockType.Top) // Dock to top, remaining height for next
```

### MarginOption
Add margin around element:

```csharp
new MarginOption(10) // 10px on all sides
new MarginOption(5, 10) // 5px horizontal, 10px vertical
new MarginOption(5, 10, 15, 20) // Left, Top, Right, Bottom
```

### PaddingOption (NEW)
Shrink remaining rect for child layout (container only):

```csharp
new PaddingOption(20) // Shrink remaining by 20px on all sides
new PaddingOption(10, 15) // 10px h, 15px v
```

### MaxSizeOption
Constrain size:

```csharp
new MaxSizeOption(200, 150) // Max 200px wide, 150px tall
```

### ChangeRectOption
Override position/size completely:

```csharp
new ChangeRectOption(new GUIRectangle(100, 100, 200, 200))
```

## Performance Notes

### No Per-Frame Allocations
- Color buffers pre-allocated in element constructors
- In-place lerp operations
- Shared tween lerp delegate across all elements
- TextElement & ToggleElement: **0 allocations/frame**

### Efficient Rendering
- `UnityMeshRender` accumulates all mesh data with rebased offsets
- Single `ProcessBuffer()` call per frame = 1 GPU submission
- Before: Each element = immediate `ExecuteCommandBuffer()` (stale references)
- Now: All elements batched correctly

### Event Dispatch
- Input events → `LayoutEvent` → `PostLayoutEvent` → `PreRenderEvent` → `RenderEvent`
- Input events (`MouseMoveEvent`, `MouseKeyDownEvent`, `MouseKeyUpEvent`)
- `VisualContainer.HandleEvents()` cascades to children

## Troubleshooting

### Elements not appearing
- Verify `GUIGlobals.GlobalMaterial` is set
- Check `Element.IsActive = true`
- Ensure `DesiredRect` is not zero-sized

### Colors look wrong
- Check shader blend mode: `Blend One OneMinusSrcAlpha`
- Verify color values are byte range (0-255), not float (0-1)
- Check `GUIColor` constructor usage

### Animations stuttering
- Verify `PreRenderEvent.DeltaTime` is per-frame
- Check that `ProcessBuffer()` is called once per frame
- Confirm tweens are using correct easing type

### Layout not working
- Order matters: `DockOption` modifies `RemainingRect` sequentially
- `PaddingOption` must come BEFORE child layout options
- Use `DockOption(EDockType.Fill)` as catch-all for remaining space

## Color Format Reference

```csharp
// GUIColor uses bytes (0-255)
var red = new GUIColor(255, 0, 0);        // Opaque red
var transparent = new GUIColor(0, 0, 0, 0); // Transparent black
var semi = new GUIColor(255, 255, 255, 128); // Semi-transparent white

// ElementColor from single color
var single = new ElementColor(new GUIColor(100, 150, 200)); // Expanded to 4 corners

// ElementColor from array (corners: 0=BL, 1=BR, 2=TR, 3=TL)
var gradient = new ElementColor(
    new GUIColor(255, 0, 0),   // Bottom-Left
    new GUIColor(0, 255, 0),   // Bottom-Right
    new GUIColor(0, 0, 255),   // Top-Right
    new GUIColor(255, 255, 0)  // Top-Left
);
```

## Next Steps

1. Run the example scene
2. Hover over elements to see color transitions
3. Click buttons to trigger callbacks
4. Toggle the switch to see dual-state animations
5. Explore `ExampleUI.cs` and customize colors/sizes
6. Build your own UI by combining elements + layout options
