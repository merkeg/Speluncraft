// <copyright file="Level1.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using System;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;
    using Engine.Renderer.UI;
    using Engine.Scene;
    using Engine.Service;
    using Game.Enemy;
    using Game.Gun;
    using Game.UI;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Game scene class.
    /// </summary>
    public class Level1 : Scene
    {
        private Tilemap tilemap;
        private Menu.PauseMenu pauseMenu;

        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            this.pauseMenu = new Menu.PauseMenu(new Rectangle(0, 0, 0, 0), new Color4(0, 0, 0, 0.5f), TextureAtlas.Fonts["defaultFont"], UiAlignment.Left, true);
            this.InitializeRenderers();
            this.AddPlayer();
            this.AddObjects();
        }

        private void InitializeRenderers()
        {
            this.tilemap = TextureAtlas.Tilemaps["level01"];
            Engine.Engine.GetService<TilemapService>().AddTilemap(this.tilemap, Vector2i.Zero);
        }

        private void AddPlayer()
        {
            TilemapLayerObject spawnPos = this.tilemap.FindObjectByName("spawn");
            Player.Player player = new Player.Player(spawnPos.X, -spawnPos.Y + 1, 1, 1.375f, TextureAtlas.Sprites["adventurer_idle"]);
            player.AddComponent(new CameraTrackingComponent());
            Engine.Engine.AddGameObject(player);

            // make sure to initialize UI after the player
            UILoader.Initialize_UI(player);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), TextureAtlas.Fonts["debugFont"], player, UiAlignment.Right);
            debugRenderer.Hidden = true;
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
            Engine.Engine.GetService<InputService>().Subscribe(Keys.F3, () => debugRenderer.Hidden = !debugRenderer.Hidden);
            Engine.Engine.GetService<InputService>().Subscribe(Keys.Escape, this.DisplayPauseMenu);

            Engine.Engine.GetService<TilemapService>().SetOptimizationPoint(player);

            Engine.Engine.AddGameObject(new InteractableElement(96, -30, 0.3f, 0.3f, TextureAtlas.Fonts["debugFont"], "Press [A] and [D] to walk.", Color4.White, Color4.White, 4, false));
            Engine.Engine.AddGameObject(new InteractableElement(84, -32, 0.3f, 0.3f, TextureAtlas.Fonts["debugFont"], "Press [Left] and [Right] to shoot.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(74, -42, 0.3f, 0.3f, TextureAtlas.Fonts["debugFont"], "Press [Space] to jump.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(50, -34, 0.3f, 0.3f, TextureAtlas.Fonts["debugFont"], "Lava will kill you", Color4.Coral, Color4.Coral, 6, false));

            TilemapLayerObject exitPos = this.tilemap.FindObjectByName("exit");
            InteractableElement el = new InteractableElement(exitPos.X, -exitPos.Y + 1, 0.3f, 0.3f, TextureAtlas.Fonts["debugFont"], "Press [W] to exit.", Color4.White, Color4.White, 4, false);
            Engine.Engine.AddGameObject(el);
            el.Interact += () =>
            {
                Engine.Engine.ChangeScene(new Level2());
            };
        }

        private void AddObjects()
        {
            ISprite enemySprite = TextureAtlas.Sprites["zombie_walking"];
            ISprite heartSprite = TextureAtlas.Sprites["heart"];

            this.tilemap.FindObjectsByName("HealthPickup").ForEach(obj =>
            {
                Player.Items.HealthPickUp heart = new Player.Items.HealthPickUp(obj.X, -obj.Y + 1, 1, 1, heartSprite, 30);
                Engine.Engine.AddGameObject(heart);
            });

            this.tilemap.FindObjectsByName("DummyAI").ForEach(obj =>
            {
                LeaperReaperEnemy enemy = new LeaperReaperEnemy(obj.X, -obj.Y + 1, 1, 1.375f, enemySprite, 10);
                Engine.Engine.AddGameObject(enemy);
            });

            this.tilemap.FindObjectsByName("EnemyPistol").ForEach(obj =>
            {
                EnemyWithWeapon enemy = new EnemyWithWeapon(obj.X, -obj.Y + 1, 1, 1.375f, enemySprite, 10, new StoneThrower());
                Engine.Engine.AddGameObject(enemy);
            });
        }

        private void DisplayPauseMenu()
        {
            if (GameManager.UpdatesPaused)
            {
                GameManager.UpdatesPaused = false;
                Engine.Engine.RemoveRenderer(this.pauseMenu, RenderLayer.UI);
                return;
            }

            GameManager.UpdatesPaused = true;
            Engine.Engine.AddRenderer(this.pauseMenu, RenderLayer.UI);
        }
    }
}