// <copyright file="Program.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game
{
    using System;
    using System.IO;
    using System.Reflection;
    using Engine.Camera;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Particle;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;
    using Engine.Renderer.UI;
    using Engine.Service;
    using Game.Enemy;
    using Game.Gun;
    using Game.UI;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The Main class of the game.
    /// </summary>
    internal class Program
    {
        private Assembly assembly;
        private Tilemap tilemap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        public Program()
        {
            GameWindow window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
            window.RenderFrequency = 60;

            window.Size = new Vector2i(1280, 720);
            window.VSync = VSyncMode.Adaptive;
            window.Title = "Speluncraft";
            Engine.Engine.StartEngine(window);
            this.assembly = Assembly.GetExecutingAssembly();
            this.InitializeRenderers();
            this.AddPlayer();
            this.AddEnemies();

            Camera cam = Engine.Engine.Camera;
            cam.Scale = 5f;
            window.Run();
        }

        private static void Main()
        {
            new Program();
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
            UILoader.Initialize_UI();

            FontModel fontModel = FontModel.Parse("Game.Resources.Font.hack.font.fnt");
            Sprite fontSprite = new Sprite("Game.Resources.Font.hack.font.png");
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            debugRenderer.Hidden = true;
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
            Engine.Engine.GetService<InputService>().Subscribe(Keys.F3, () => debugRenderer.Hidden = !debugRenderer.Hidden);

            Engine.Engine.GetService<TilemapService>().SetOptimizationPoint(player);

            InteractableElement el = new InteractableElement(96, -30, 0.3f, 0.3f, font, "Press [A] and [D] to walk.", Color4.White, Color4.White, 4, false);
            Engine.Engine.AddGameObject(el);
            el.Interact += () => Console.WriteLine("Test");

            Engine.Engine.AddGameObject(new InteractableElement(84, -32, 0.3f, 0.3f, font, "Press [Left] and [Right] to shoot.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(74, -42, 0.3f, 0.3f, font, "Press [Space] to jump.", Color4.White, Color4.White, 6, false));
            Engine.Engine.AddGameObject(new InteractableElement(50, -34, 0.3f, 0.3f, font, "Lava will kill you", Color4.Coral, Color4.Coral, 6, false));
        }

        private void AddEnemies()
        {
            Sprite enemySprite = new Sprite("Game.Resources.enemy.png");
            Sprite enemyGunSprite = new Sprite("Game.Resources.enemyGun.png");

            Engine.Engine.AddGameObject(new Player.Items.HealthPickUp(81, -43, 1, 1, enemySprite, 30));

            // Tilesheet fireTilesheet = new Tilesheet("Game.Resources.Animated.fire.png", 32, 32);
            // float delay = 0.041f;
            // ISprite fireSprite = new AnimatedSprite(fireTilesheet, Keyframe.RangeY(0, 0, 23, delay));
            // Enemy.Enemy fire = new Enemy.Enemy(100, -44, 1, 1, fireSprite, 25);
            // Engine.Engine.AddGameObject(fire);
            this.tilemap.FindObjectsByName("DummyAI").ForEach(obj =>
            {
                DummyAI enemy = new DummyAI(obj.X, -obj.Y + 1, 1, 1.375f, enemySprite, 10);
                Engine.Engine.AddGameObject(enemy);
            });

            this.tilemap.FindObjectsByName("EnemyPistol").ForEach(obj =>
            {
                EnemyPistol enemy = new EnemyPistol(obj.X, -obj.Y + 1, 1, 1.375f, enemyGunSprite, 10);
                Engine.Engine.AddGameObject(enemy);
            });
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
        }
    }
}