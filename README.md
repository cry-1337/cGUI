# Complected Graphical User Interface

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Engine: Agnostic](https://img.shields.io/badge/Engine-Agnostic-orange.svg)](https://github.com/cry-1337/cGUI)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/cry-1337/cGUI/pulls)

**Complected Graphical User Interface (cGUI)** is a high-performance, engine-agnostic GUI framework designed with a **Zero-GC** philosophy. Built for environments where every CPU cycle and byte of memory matters, cGUI provides a robust element system and rendering pipeline that operates entirely without heap allocations.

## 💡 Core Philosophy: Engine Independence

The primary goal of **cGUI** is to separate GUI logic from the rendering backend. While it provides a high-performance implementation for **Unity**, the core architecture is **engine-agnostic**. 

By leveraging an abstract rendering context (`IMeshRenderContext`), cGUI can be integrated into custom DirectX/Vulkan engines, Godot, or any other framework requiring a high-speed, allocation-free interface.

## 🔥 Key Features

* **Zero-GC Core:** No `GC.Alloc` during layout calculation, element updates, or mesh assembly.
* **Modular Element System:** A specialized architecture for building UI components (Buttons, Panels, Labels) that works independently of engine-specific objects like MonoBehaviours.
* **Abstract Rendering Context:** Easily implement your own backend by fulfilling the `IMeshRenderContext` interface.
* **Blittable Data Handling:** Optimized for high-speed memory copying (blitting) of vertex and index data, ideal for modern graphics APIs.
* **XR & Mobile Optimized:** Dramatically reduces CPU overhead and thermal throttling, ensuring a rock-solid frame rate for performance-critical applications.

## 🏗 Technical Deep-Dive

### Boxing Prevention
The core of cGUI's performance lies in its generic implementation. By using a generic render context and element system, the library avoids the common pitfall of "Interface Boxing"—where a struct-based element is cast to an interface and moved to the heap.



### Lightweight Element System
Unlike standard Unity UI elements that are heavy `MonoBehaviour` objects, cGUI elements are designed to be lightweight. They function within a managed lifecycle that minimizes overhead while providing the flexibility to build complex, nested layouts.

### Memory Efficiency
* **`ref` & `in` Parameters:** Heavy vertex and element data are passed by reference to minimize stack copying.
* **Capacity Pre-allocation:** Render contexts and element buffers are designed to be reused with pre-allocated internal arrays, preventing array resizing during critical frames.

## 📊 Performance Comparison

| Metric | Standard UI Frameworks | **cGUI** |
| :--- | :--- | :--- |
| **GC Pressure** | High (Layout/Rebuild) | **Zero (Stable)** |
| **Object Model** | Class-based (Reference Types) | **Struct-based (Value Types)** |
| **Backend** | Engine-Locked | **Engine-Agnostic** |
| **Memory Locality** | Poor (Heap Scatter) | **Excellent (Contiguous)** |
| **Boxing** | Common (Interfaces/Enums) | **Eliminated (Generics)** |

## 🎯 Ideal Use Cases

1.  **Engine-Agnostic Tools:** Libraries and tools that need to run across different game engines or custom frameworks.
2.  **High-Frequency HUDs:** Real-time tactical displays and mini-maps updating every frame.
3.  **XR Overlays:** Performance-critical interfaces for Meta Quest, Vision Pro, and other mobile XR headsets.
4.  **Professional Editor Tools:** Creating responsive, data-heavy custom editors without interface lag.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/cry-1337/cGUI/blob/main/LICENSE) file for details.
