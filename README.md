# AR Guardian System (Unity ARFoundation)

## Overview
This project demonstrates a stable AR-anchored virtual object with a proximity-based Guardian system built using Unity and ARFoundation. A virtual cube is placed on a detected real-world surface and remains pinned in place while dynamically responding to the user’s distance with visual and UI feedback.

---

## Features
- Horizontal plane detection and tap-to-place interaction  
- Stable world anchoring using ARFoundation anchors  
- Single placement logic (one Guardian cube per session)  
- Custom Shader Graph material with a holographic / fresnel-style look  
- Proximity-based Guardian State Machine:
  - **Safe Zone (> 0.5m):** Cyan/Blue visuals, *SYSTEM ARMED*  
  - **Warning Zone (< 0.5m):** Pulsing yellow visuals, *WARNING: RESTRICTED AREA*  
  - **Breach Zone (< 0.2m):** Flashing red visuals, *CRITICAL HALT // BACK AWAY*  
- World-space floating UI panel that always faces the user  
- Clean AR presentation with plane visuals disabled  

---

## Tech Stack
- **Engine:** Unity  
- **Language:** C#  
- **AR:** ARFoundation (ARCore / ARKit)  
- **UI:** TextMeshPro (World Space)  
- **Rendering:** Shader Graph (Unlit, Fresnel / holographic style)  

---

## Platform
- Android (ARCore-supported devices)  
- Architecture compatible with iOS (ARKit)

---

## How to Run
1. Open the project in Unity  
2. Ensure ARFoundation and ARCore/ARKit packages are installed  
3. Build and run on an AR-supported mobile device  
4. Scan a flat surface and tap to place the Guardian cube  
5. Move closer to observe Guardian state transitions  

---

## Notes
This project focuses on AR stability, spatial UI clarity, and state-driven behavior. Visual effects and transitions were intentionally kept clear and readable to align with real-world Mixed Reality product standards.
