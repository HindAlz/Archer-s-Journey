# Archer's Journey

**A 2D side scrolling platformer built with Unity and C#, featuring archery combat, food collection, and a temporary fire arrow power-up.**

Guide an archer through a pixel art level, jump across platforms, deal with patrolling enemies, and collect food along the way. Reach the finish to see your collection total, or restart after losing all health.

The project builds on Unity's **2D Platformer Microgame** foundation, with archery mechanics, collectible food, chest triggered power ups, and game specific interface elements.

## Gameplay

- **Platforming:** move left and right, jump, and navigate the level's terrain and hazards.
- **Archery combat:** fire arrows in the direction the character is facing.
- **Fire-arrow power-up:** enter a chest's trigger area to open it and temporarily switch to fire arrows. The interface displays an indicator while the effect is active.
- **Enemy encounters:** enemies patrol assigned paths and react to projectile hits.
- **Food collection:** pick up food items and track the total in the interface.
- **Health and replay:** monitor the health bar and use the game over screen to restart.
- **Level completion:** reaching the victory zone triggers feedback and a collection summary.

## Controls

| Action | Input |
| --- | --- |
| Move left | **A** or **Left Arrow** |
| Move right | **D** or **Right Arrow** |
| Jump | **Space** |
| Shoot an arrow | **E** |
| Toggle the pause/menu interface | **Escape** |
| Restart after game over | Click **Play Again?** |

The attack key remains **E** while the fire-arrow effect is active. Movement and jump bindings are defined in [`ProjectSettings/InputManager.asset`](ProjectSettings/InputManager.asset); the attack key is handled in `PlayerController.cs`.

## Technology

| Component | Technology |
| --- | --- |
| Engine | **Unity 2022.3.49f1** |
| Language | C# |
| Rendering | Universal Render Pipeline 14.0.11 |
| Camera | Cinemachine 2.10.1 |
| Interface | Unity UI Toolkit, UXML, and USS |
| Gameplay systems | 2D physics, sprite animation, tilemaps, and prefab-based projectiles |


## Open and play in Unity

1. Install **Unity Hub** and **Unity 2022.3.49f1**.
2. Clone the repository:

   ```bash
   git clone https://github.com/HindAlz/Archer-s-Journey.git
   ```

3. In Unity Hub, add the cloned project folder containing `Assets`, `Packages`, and `ProjectSettings`.
4. Open it with the matching editor and allow Unity to import assets and resolve packages.
5. Open **[`Assets/Scenes/SampleScene.unity`](Assets/Scenes/SampleScene.unity)**.
6. Press **Play**, then click inside the Game view so it receives keyboard input.

`SampleScene.unity` is the main game scene and the only enabled scene in the committed build settings. To create a standalone build, install the relevant platform support module, open **File → Build Settings**, confirm this scene is included, and select **Build**.

## Project navigation

| Location | Purpose |
| --- | --- |
| [`Assets/Scenes/`](Assets/Scenes/) | Main level scene. |
| [`Assets/Scripts/Mechanics/`](Assets/Scripts/Mechanics/) | Player movement, enemies, health, collectibles, and victory triggers. |
| [`Assets/Scripts/Gameplay/`](Assets/Scripts/Gameplay/) | Gameplay events such as jumping, collisions, spawning, and victory. |
| [`Assets/Scripts/UI/`](Assets/Scripts/UI/) | Health and collection display, power-up state, menus, and result screens. |
| [`Assets/UIStuff/`](Assets/UIStuff/) | UXML layouts for the gameplay, game-over, and summary interfaces. |
| [`Assets/Prefabs/`](Assets/Prefabs/) | Reusable characters, food items, and fire-arrow objects. |
| [`Assets/My stuff/`](Assets/My%20stuff/) | Additional project art and prefabs. |

## Implementation highlights

- [`PlayerController.cs`](Assets/Scripts/Mechanics/PlayerController.cs) combines movement and jump states with directional projectile spawning, attack animation, and a firing cooldown.
- [`EnemyController.cs`](Assets/Scripts/Mechanics/EnemyController.cs) handles patrol movement and different reactions to ordinary and fire-arrow hits.
- [`Collectible.cs`](Assets/Collectible.cs) updates the food counter and removes collected objects after audio feedback.
- [`GameUIHandler.cs`](Assets/Scripts/UI/GameUIHandler.cs) updates the health display and item counter and manages the temporary fire-arrow state.
- [`Chest.cs`](Assets/Cainos/Pixel%20Art%20Platformer%20-%20Village%20Props/Script/Chest.cs) opens a chest and activates the power-up when the player enters its trigger.
- [`SummaryUI.cs`](Assets/Scripts/UI/SummaryUI.cs) displays the collected-item total when the player reaches the end of the level.

## Demo
full video demo: https://drive.google.com/file/d/1IHg1v9fb4BVlh-qFVm8Lk9z9kWeSi4tM/view?usp=sharing
<img width="1676" height="922" alt="image" src="https://github.com/user-attachments/assets/43eac7d4-42ef-4a87-bce4-b5b676fb111c" />
<img width="1678" height="938" alt="image" src="https://github.com/user-attachments/assets/665e7238-398c-42ea-8280-b58f43eb72cb" />


## Foundation and asset credits

The project uses Unity's **2D Platformer Microgame** as its starting point. The original template documentation is retained in [`Assets/PlatformerMicrogame_README.txt`](Assets/PlatformerMicrogame_README.txt).

The repository also includes third-party art and resources, including **Cainos Pixel Art Platformer – Village Props**. See the included asset documentation and [`Assets/ThirdPartyNotice.txt`](Assets/ThirdPartyNotice.txt) for existing notices.
