# Weekly_3a
# Laser Shooter Game

The **Laser Shooter Game** is a Unity project showcasing an advanced weapon system with multiple modes. Players can shoot lasers or bombs, toggle combo modes, and earn points dynamically based on their actions.

---
## video demo game
![Gameplay demo ](https://github.com/VGD-TEAM/Weekly_3a/blob/main/Recording%20shooter%20game.mp4)

---

## Features
- **Dynamic Weapon System**:
  - Laser weapon for precision shooting.
  - Bombs for explosive attacks.
  - Toggle between weapons using keybindings.

- **Combo Shooting**:
  - Activate combo mode to fire a series of lasers with customizable delays.
  - Perfect for taking down multiple targets.

- **Score Management**:
  - Earn points by hitting targets.
  - Dynamically updates the score in the UI.

---

## Controls
| **Action**               | **Default Key**   |
|---------------------------|-------------------|
| Switch to Laser           | `1`              |
| Switch to Bomb            | `2`              |
| Toggle Combo Mode         | `Enter`          |
| Shoot (Laser or Bomb)     | `Space`          |

---

## How It Works
1. **Weapon Switching**:
   - Players can switch between Laser and Bomb weapons using `1` and `2` keys.
   - Each weapon has unique behaviors, such as precise laser targeting or explosive bomb attacks.

2. **Combo Mode**:
   - Enable combo mode using `Enter` to fire multiple lasers in a sequence.
   - Customize the number of lasers (`lasersInCombo`) and the delay (`delayBetweenLasers`) in Unity's Inspector.

3. **Shooting Mechanic**:
   - Press `Space` to shoot the active weapon.
   - The game spawns appropriate projectiles and calculates scores dynamically.

---

## Code Overview
The **LaserShooter** script inherits from `ClickSpawner` and manages:
- Weapon switching (Laser, Bomb).
- Combo mode functionality using coroutines.
- Score tracking with the `NumberField` component.
- Input handling with Unity's **Input System**.

### Key Methods:
- **SwitchToLaser()**: Activates the laser weapon.
- **SwitchToBomb()**: Activates the bomb weapon.
- **ToggleComboMode()**: Toggles combo mode on or off.
- **PerformShoot()**: Handles shooting based on the active weapon and combo state.
- **ShootCombo()**: Implements combo shooting using coroutines.

---

## Customization
### Inspector Settings:
- `pointsToAdd`: Points gained for hitting a target.
- `lasersInCombo`: Number of lasers fired in combo mode (default: 3).
- `delayBetweenLasers`: Delay between lasers in combo mode (default: 0.2 seconds).

### Input Bindings:
You can modify the input bindings in the script or the Unity **Input System** to suit your preferences.

---

## How to Play
1. Open the game in Unity.
2. Start the game and use the controls to switch weapons, toggle combo mode, and shoot.
3. Earn points by successfully hitting targets.
4. Enjoy the dynamic gameplay experience!

---

## Requirements
- Unity 2021.3 or newer.
- **Unity Input System** package installed.

---

## Future Improvements
- Add more weapon types (e.g., curved projectiles).
- Integrate visual effects and sound for a richer experience.
- Include advanced scoring logic with multipliers.

---

Happy Shooting! 🎮
