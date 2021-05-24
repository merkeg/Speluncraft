// <copyright file="GameScene.cs" company="RWUwU">
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
    public class GameScene : Scene
    {
        private Tilemap tilemap;
        private Menu.PauseMenu pauseMenu;

        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            this.pauseMenu = new Menu.PauseMenu(new Rectangle(0, 0, 0, 0), new Color4(0, 0, 0, 0.5f), new Font(FontModel.Parse("Game.Resources.Font.semicondensed.font.fnt"), new Sprite("Game.Resources.Font.semicondensed.font.png")), UiAlignment.Left, true);
            this.InitializeRenderers();
            this.AddPlayer();
            this.AddObjects();
        }

        private void InitializeRenderers()
        {
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheetMC.png", 32, 32);
            TilemapModel model = TilemapParser.ParseTilemap("Game.Resources.Level.mapalla.json");
            this.tilemap = new Tilemap(tilesheet, model);
            this.AnimateTiles(tilesheet);
            Engine.Engine.GetService<TilemapService>().AddTilemap(this.tilemap, Vector2i.Zero);
        }

        private void AddPlayer()
        {
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheetMC.png", 32, 32);
            Sprite sprite = new Sprite("Game.Resources.Player.adventurer_idle.png", false);

            TilemapLayerObject spawnPos = this.tilemap.FindObjectByName("spawn");
            Player.Player player = new Player.Player(spawnPos.X, -spawnPos.Y + 1, 1, 1.375f, sprite);
            player.AddComponent(new CameraTrackingComponent());
            Engine.Engine.AddGameObject(player);

            // make sure to initialize UI after the player
            UILoader.Initialize_UI(player);

            FontModel fontModel = FontModel.Parse("Game.Resources.Font.hack.font.fnt");
            Sprite fontSprite = new Sprite("Game.Resources.Font.hack.font.png");
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            debugRenderer.Hidden = true;
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
            Engine.Engine.GetService<InputService>().Subscribe(Keys.F3, () => debugRenderer.Hidden = !debugRenderer.Hidden);
            Engine.Engine.GetService<InputService>().Subscribe(Keys.Escape, this.DisplayPauseMenu);

            Engine.Engine.GetService<TilemapService>().SetOptimizationPoint(player);

            InteractableElement el = new InteractableElement(96, -30, 0.3f, 0.3f, font, "Press [A] and [D] to walk.", Color4.White, Color4.White, 4, false);
            Engine.Engine.AddGameObject(el);
            el.Interact += () => Console.WriteLine("Test");

            Engine.Engine.AddGameObject(new InteractableElement(84, -32, 0.3f, 0.3f, font, "Press [Left] and [Right] to shoot.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(74, -42, 0.3f, 0.3f, font, "Press [Space] to jump.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(50, -34, 0.3f, 0.3f, font, "Lava will kill you", Color4.Coral, Color4.Coral, 6, false));
        }

        private void AddObjects()
        {
            AnimatedSprite enemySprite = new AnimatedSprite(new Tilesheet("Game.Resources.Enemy.zombie_walking.png", 80, 110), Keyframe.RangeX(0, 1, 0, 0.1f));
            AnimatedSprite heartSprite = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.heart.png", 16, 16), Keyframe.RangeX(0, 23, 0, 0.1f));

            this.tilemap.FindObjectsByName("HealthPickup").ForEach(obj =>
            {
                Player.Items.HealthPickUp heart = new Player.Items.HealthPickUp(obj.X, -obj.Y + 1, 1, 1, heartSprite, 30);
                Engine.Engine.AddGameObject(heart);
            });

            this.tilemap.FindObjectsByName("DummyAI").ForEach(obj =>
            {
                DummyAI enemy = new DummyAI(obj.X, -obj.Y + 1, 1, 1.375f, enemySprite, 10);
                Engine.Engine.AddGameObject(enemy);
            });

            this.tilemap.FindObjectsByName("EnemyPistol").ForEach(obj =>
            {
                EnemyPistol enemy = new EnemyPistol(obj.X, -obj.Y + 1, 1, 1.375f, enemySprite, 10);
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

        private void AnimateTiles(Tilesheet sheet)
        {
            AnimatedSprite netherPortal = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.nether_portal.png", 32, 32), Keyframe.RangeY(0, 0, 31, 0.03f));
            sheet.SetCustomSprite(36, netherPortal);
            AnimatedSprite waterFlow = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_flow.png", 32, 32), Keyframe.RangeY(0, 0, 35, 0.1f));
            sheet.SetCustomSprite(81, waterFlow);
            AnimatedSprite waterStill = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_still.png", 32, 32), Keyframe.RangeY(0, 0, 50, 0.05f));
            sheet.SetCustomSprite(91, waterStill);
            AnimatedSprite waterCut = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.water_still_cut.png", 32, 32), Keyframe.RangeY(0, 0, 50, 0.05f));
            sheet.SetCustomSprite(92, waterCut);
            AnimatedSprite lavaFlow = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_flow.png", 32, 32), Keyframe.RangeY(0, 0, 245, 0.03f));
            sheet.SetCustomSprite(83, lavaFlow);
            AnimatedSprite lavaStill = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_still.png", 32, 32), Keyframe.RangeY(0, 0, 122, 0.03f));
            sheet.SetCustomSprite(93, lavaStill);
            AnimatedSprite lavaCut = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.lava_still_cut.png", 32, 32), Keyframe.RangeY(0, 0, 122, 0.03f));
            sheet.SetCustomSprite(94, lavaCut);
            AnimatedSprite fire = new AnimatedSprite(new Tilesheet("Game.Resources.Animated.fire.png", 32, 32), Keyframe.RangeY(0, 0, 23, 0.04f));
            sheet.SetCustomSprite(85, fire);
        }
    }
}