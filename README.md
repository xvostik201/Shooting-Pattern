# 🛰️ Tactical Weapon System (Modular FPS Framework)

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Unity](https://img.shields.io/badge/Unity-2022.3-black?logo=unity)
![Zenject](https://img.shields.io/badge/DI-Zenject-green?logo=unity)
![DOTween](https://img.shields.io/badge/Animation-DOTween-red?logo=unity)

A high-fidelity modular weapon framework for Unity. This system focuses on decoupled architecture, data-driven recoil patterns, and procedural visual feedback, providing a professional-grade "game feel" for tactical simulations.

🎥 **Preview**

![Recoil Demo](https://github.com/xvostik201/Shooting-Pattern/raw/main/Assets/Documentation/Gif1.gif)

✈️ **Overview**

This framework demonstrates:
* **Decoupled Architecture:** Subsystems are strictly separated using **Zenject** (Input → Weapon Logic → Camera → Combat).
* **Dual-Raycast Targeting:** Sophisticated shooting logic that compensates for parallax between the camera lens (operator view) and the weapon muzzle.
* **Procedural Recoil Engine:** Uses **DOTween** for inertial "Punch" animations, eliminating heavy `Update` calculations for weapon kickback.
* **Interface-based Damage System:** Unified interaction via `IDamagable`, allowing the weapon to damage any registered entity without hard dependencies.

📸 **Screenshots**

| Recoil Pattern Analysis | Configuration Interface |
| :--- | :--- |
| ![33 Hits](https://github.com/xvostik201/Shooting-Pattern/raw/main/Assets/Documentation/33Hits.png) | ![Weapon Data](https://github.com/xvostik201/Shooting-Pattern/raw/main/Assets/Documentation/M4Data.png) |
| *33-round burst precision report* | *ScriptableObject-based tuning (Curves/Arrays)* |

| Debug & Verification | Maximum Spread |
| :--- | :--- |
| ![0 Hits](https://github.com/xvostik201/Shooting-Pattern/raw/main/Assets/Documentation/0Hits.png) | ![50 Hits](https://github.com/xvostik201/Shooting-Pattern/raw/main/Assets/Documentation/50Hits.png) |
| *Visual debug cubes for hit verification* | *Extended fire cycle dispersion* |

🛠️ **Key Features**

🎯 **Combat Logic**
* **Dynamic Spread:** Supports both legacy **Array-based** patterns and modern **Animation Curve** procedural recoil.
* **Smart Raycasting:** Calculates trajectories from the muzzle to the world-point aimed at by the camera.
* **Random Noise:** Per-shot min/max spread variance for realistic dispersion.

🎛️ **Architecture**
* **Dependency Injection:** Powered by **Zenject** to maintain a clean, singleton-free codebase.
* **Scriptable Armory:** The `WeaponRegistry` allows for instant weapon swapping and global asset management.
* **Custom Editor:** Tailor-made Inspector that dynamically hides/shows fields based on the selected recoil type.

### 🧠 Implementation Spotlight: Advanced Ballistics Logic

The following snippet from `M4.cs` demonstrates the core shooting pipeline: resolving camera-to-world coordinates, injecting procedural recoil data, and processing damage via interfaces.

```csharp
// 1. Resolve target point from Camera center (Parallax Compensation)
Ray targetRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
Vector3 targetPoint = Physics.Raycast(targetRay, out RaycastHit hit, 100f, 
    Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)
    ? hit.point
    : targetRay.GetPoint(100f);

// 2. Fetch Recoil Data (Supports both fixed Patterns and Dynamic Curves)
float spreadX, spreadY;
if (_weaponData.RecoilType == WeaponRecoilType.Array && _weaponData.SpreadPattern.Length > 0)
{
    int index = Mathf.Min(_currentBullet, _weaponData.SpreadPattern.Length - 1);
    spreadX = _weaponData.SpreadPattern[index].x;
    spreadY = _weaponData.SpreadPattern[index].y;
}
else
{
    spreadX = _weaponData.SpreadCurveX.Evaluate(_currentBullet);
    spreadY = _weaponData.SpreadCurveY.Evaluate(_currentBullet);
}

// 3. Calculate final trajectory with Muzzle-to-Target routing
Vector3 spreadOffset = new Vector3(
    spreadX + Random.Range(_weaponData.MinXRandomSpread, _weaponData.MaxXRandomSpread),
    spreadY + Random.Range(_weaponData.MinYRandomSpread, _weaponData.MaxYRandomSpread), 
    0);

Vector3 direction = (targetPoint - _shootPoint.position).normalized;
Vector3 finalDir = (_currentBullet > 0) ? (direction + spreadOffset).normalized : direction;

// 4. Execute physical hit detection and Damage processing
if (Physics.Raycast(_shootPoint.position, finalDir, out RaycastHit finalHit, 100f))
{
    // Decoupled interaction via Interface
    IDamagable damagable = finalHit.transform.GetComponentInParent<IDamagable>() 
                           ?? finalHit.transform.GetComponentInChildren<IDamagable>();

    damagable?.TakeDamage(_weaponData.WeaponDamage);
    DebugCube(finalHit.point);
}
```

🎮 **Controls**

| Action | Input |
| :--- | :--- |
| **Shoot** | Left Mouse Button |
| **Aim / Look** | Mouse Movement |
| **Movement** | WASD |

▶ **How to Build**

1. Unity **2022.3 LTS** or newer.
2. Clone repository.
3. Import **Zenject** and **DOTween**.
4. Open the project and launch the main scene.

📜 **License**

This project is licensed under the **MIT License**.

📮 **Contact**

📧 Email: zkostyutkin2004@gmail.com
🧩 LinkedIn: [Click](https://www.linkedin.com/in/zakhar-kostyutkin-b2740b393/)]

📄 **Developer Note**

This project was created as a technical demonstration of clean software architecture and procedural animation systems in Unity, showcasing how SOLID principles can be applied to core gameplay mechanics.

---
**xvostik201 by Zakhar Kostyuktin**
