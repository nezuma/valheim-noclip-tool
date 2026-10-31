# Noclip Tool

A lightweight client‑side **noclip / fly mode** for Valheim.  
Fly through walls, adjust your speed on the fly, and toggle everything from an in‑game GUI.

![Noclip Tool Menu](https://raw.githubusercontent.com/nezuma/ValheimNoclipTool/main/screenshot.png)  
_Screenshot placeholder – replace with a real image URL._

---

## Features

- **True noclip** – disables gravity and collision, allowing free flight through terrain and structures.
- **Camera‑relative movement** – fly in the direction you are looking (WASD + Space / Ctrl for vertical).
- **Adjustable speed** – change flight speed with the mouse wheel or via the GUI slider (0.5× – 10×).
- **In‑game menu** – toggle noclip and fine‑tune speed without leaving the game (default key: **F7**).
- **Safe state handling** – saves and restores your original gravity and god‑mode state when noclip is turned off.
- **Client‑side only** – works in single‑player and on servers (does not require server installation).

---

## Installation

### Thunderstore / r2modman (recommended)

1. Install **BepInExPack Valheim** from Thunderstore.
2. Install **Noclip Tool** via your mod manager (r2modman, Gale, etc.).
3. Launch the game – the mod loads automatically.

### Manual installation

1. Make sure **BepInExPack Valheim** is installed in your game folder.
2. Download `NoclipTool.dll` from the latest release.
3. Place `NoclipTool.dll` into `Valheim/BepInEx/plugins/NoclipTool/`.
4. Start the game.

---

## Usage

| Action                        | Default Key                |
| ----------------------------- | -------------------------- |
| Toggle noclip ON / OFF        | **F5**                     |
| Open / close the in‑game menu | **F7**                     |
| Move (camera relative)        | **W A S D**                |
| Move up                       | **Space**                  |
| Move down                     | **Left Ctrl / Right Ctrl** |
| Change flight speed (in‑game) | **Mouse Wheel**            |

All keybinds can be changed in the BepInEx config file:  
`BepInEx/config/ru.nezuma.valheim.nocliptool.cfg`

---

## Configuration

The mod creates a config file with the following entries:

| Section | Key               | Default | Description                              |
| ------- | ----------------- | ------- | ---------------------------------------- |
| General | `ToggleNoclipKey` | `F5`    | Key to enable / disable noclip.          |
| General | `ToggleMenuKey`   | `F7`    | Key to show / hide the GUI.              |
| General | `SpeedMultiplier` | `2.0`   | Base flight speed multiplier (0.5 – 10). |

> The in‑game slider and mouse‑wheel adjustments override the config value for the current session.  
> The config value is restored on game restart.

---

## Compatibility

- **Valheim** – compatible with the latest game version (tested on patch 0.217.46+).
- **BepInEx** – requires BepInEx 5.4.23xx or newer (provided by **BepInExPack Valheim**).
- **Multiplayer** – client‑side only. Other players will see you teleporting / flying, but the mod does not affect server logic.
- **Other mods** – no known conflicts. The mod uses Harmony to patch `Player.FixedUpdate` and `Player.Update`.

---

## Known Issues

- Very high speed values (above 15×) may cause you to clip through the world border.
- The menu window position is not saved between sessions (it resets to the top‑left corner).

---

## Changelog

### 1.0.0

- Initial release.
- Noclip toggle, camera‑relative movement, vertical controls, speed adjustment, in‑game GUI.
- Safe restoration of gravity and god‑mode state.

---

## License

This mod is released under the **MIT License**.  
See the [LICENSE](LICENSE) file for details.

---

## Credits

- **Author:** nezuma
- **BepInEx** and **HarmonyX** – for making Valheim modding possible.
- **Valheim modding community** – for documentation and inspiration.
