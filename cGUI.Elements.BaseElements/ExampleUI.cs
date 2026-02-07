using cGUI.Abstraction.Structs;
using cGUI.Elements.BaseElements;
using cGUI.Elements.Models;
using cGUI.Layout.Abstraction;
using cGUI.Layout.Options;
using cGUI.Visual;
using UnityEngine;

namespace cGUI.Examples;

/// <summary>
/// Example demonstrating the new element system with animated color transitions:
/// - SimpleElement: Static colored rectangles
/// - HoverableElement: Color transition on mouse hover
/// - ButtonElement: Click feedback with press color + callback
/// - ToggleElement: On/off state with dual color sets
/// - PanelElement: Container with background + padding for child layout
/// </summary>
public class ExampleUI
{
    private PanelElement m_RootPanel = null!;
    private ButtonElement m_IncrementButton = null!;
    private ButtonElement m_DecrementButton = null!;
    private ToggleElement m_ToggleSwitch = null!;
    private HoverableElement m_HoverBox = null!;

    private int m_Counter = 0;

    /// <summary>
    /// Creates and returns the root panel with all UI elements.
    /// Call from UIManager.Start() to initialize the UI.
    /// </summary>
    public VisualElement CreateRootPanel()
    {
        CreateUI();
        return m_RootPanel;
    }

    private void CreateUI()
    {
        // Root panel: Main container with padding
        m_RootPanel = new PanelElement(
            "RootPanel",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(50, 50, Screen.width - 100, Screen.height - 100),
                Color = new ElementColor(new GUIColor(40, 40, 50)), // Dark background
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Fill),
                    new PaddingOption(20) // 20px padding on all sides
                }
            });

        // Section 1: Title/Label
        var hoverLabel = new SimpleElement(
            "HoverLabel",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 0, 30),
                Color = new ElementColor(new GUIColor(100, 150, 255)), // Blue
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Top),
                    new MarginOption(0, 0, 0, 10)
                }
            });
        m_RootPanel.Add(hoverLabel);

        // Section 2: Hover Demo
        m_HoverBox = new HoverableElement(
            "HoverBox",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 0, 60),
                Color = new ElementColor(new GUIColor(100, 150, 200)), // Light blue
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Top),
                    new MarginOption(0, 0, 0, 20)
                }
            },
            new GUIColor[] { new(150, 200, 255), new(150, 200, 255), new(150, 200, 255), new(150, 200, 255) }, // Lighter on hover
            new ElementTweenOptions
            {
                HoverInDuration = 0.2f,
                HoverOutDuration = 0.15f,
                HoverEasing = cGUI.Animations.EaseType.SmoothStep
            });
        m_RootPanel.Add(m_HoverBox);

        // Section 3: Button Demo Label
        var buttonLabel = new SimpleElement(
            "ButtonLabel",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 0, 30),
                Color = new ElementColor(new GUIColor(100, 255, 150)), // Green
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Top),
                    new MarginOption(0, 0, 0, 10)
                }
            });
        m_RootPanel.Add(buttonLabel);

        // Increment button
        m_IncrementButton = new ButtonElement(
            "IncrementButton",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 120, 50),
                Color = new ElementColor(new GUIColor(100, 200, 100)), // Light green
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Left),
                    new MarginOption(0, 0, 10, 15)
                }
            },
            new GUIColor[] { new(150, 255, 150), new(150, 255, 150), new(150, 255, 150), new(150, 255, 150) }, // Hover
            new GUIColor[] { new(80, 180, 80), new(80, 180, 80), new(80, 180, 80), new(80, 180, 80) }, // Press
            onClick: OnIncrementClick,
            tweenOptions: new ElementTweenOptions
            {
                HoverInDuration = 0.15f,
                HoverOutDuration = 0.1f,
                PressInDuration = 0.05f,
                PressOutDuration = 0.1f,
                HoverEasing = cGUI.Animations.EaseType.SmoothStep,
                PressEasing = cGUI.Animations.EaseType.OutQuad
            });
        m_RootPanel.Add(m_IncrementButton);

        // Decrement button
        m_DecrementButton = new ButtonElement(
            "DecrementButton",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 120, 50),
                Color = new ElementColor(new GUIColor(200, 100, 100)), // Light red
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Left),
                    new MarginOption(0, 0, 0, 15)
                }
            },
            new GUIColor[] { new(255, 150, 150), new(255, 150, 150), new(255, 150, 150), new(255, 150, 150) }, // Hover
            new GUIColor[] { new(180, 80, 80), new(180, 80, 80), new(180, 80, 80), new(180, 80, 80) }, // Press
            onClick: OnDecrementClick,
            tweenOptions: new ElementTweenOptions
            {
                HoverInDuration = 0.15f,
                HoverOutDuration = 0.1f,
                PressInDuration = 0.05f,
                PressOutDuration = 0.1f,
                HoverEasing = cGUI.Animations.EaseType.SmoothStep,
                PressEasing = cGUI.Animations.EaseType.OutQuad
            });
        m_RootPanel.Add(m_DecrementButton);

        // Section 4: Toggle Demo Label
        var toggleLabel = new SimpleElement(
            "ToggleLabel",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 0, 30),
                Color = new ElementColor(new GUIColor(255, 200, 100)), // Orange
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Top),
                    new MarginOption(0, 10, 0, 10)
                }
            });
        m_RootPanel.Add(toggleLabel);

        // Toggle switch
        m_ToggleSwitch = new ToggleElement(
            "ToggleSwitch",
            new ElementOption
            {
                DesiredRect = new GUIRectangle(0, 0, 0, 60),
                Color = new ElementColor(new GUIColor(150, 100, 80)), // Off color (brownish)
                LayoutOptions = new ILayoutOption[]
                {
                    new DockOption(EDockType.Top)
                }
            },
            hoveredColor: new GUIColor[] { new(180, 130, 110), new(180, 130, 110), new(180, 130, 110), new(180, 130, 110) }, // Off hover
            pressedColor: new GUIColor[] { new(120, 70, 50), new(120, 70, 50), new(120, 70, 50), new(120, 70, 50) }, // Off press
            onColor: new GUIColor[] { new(100, 200, 100), new(100, 200, 100), new(100, 200, 100), new(100, 200, 100) }, // On color (green)
            onHoveredColor: new GUIColor[] { new(150, 255, 150), new(150, 255, 150), new(150, 255, 150), new(150, 255, 150) }, // On hover
            onPressedColor: new GUIColor[] { new(80, 180, 80), new(80, 180, 80), new(80, 180, 80), new(80, 180, 80) }, // On press
            onToggle: OnToggleChanged,
            initialState: false,
            tweenOptions: new ElementTweenOptions
            {
                HoverInDuration = 0.15f,
                HoverOutDuration = 0.1f,
                PressInDuration = 0.05f,
                PressOutDuration = 0.1f,
                ToggleDuration = 0.3f,
                HoverEasing = cGUI.Animations.EaseType.SmoothStep,
                PressEasing = cGUI.Animations.EaseType.OutQuad,
                ToggleEasing = cGUI.Animations.EaseType.SmoothStep
            });
        m_RootPanel.Add(m_ToggleSwitch);
    }

    private void OnIncrementClick()
    {
        m_Counter++;
        Debug.Log($"Counter incremented: {m_Counter}");
    }

    private void OnDecrementClick()
    {
        m_Counter--;
        Debug.Log($"Counter decremented: {m_Counter}");
    }

    private void OnToggleChanged(bool isOn)
    {
        Debug.Log($"Toggle switched: {(isOn ? "ON" : "OFF")}");
    }

}
