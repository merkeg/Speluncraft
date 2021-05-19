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

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public static Gun.Player GamePlayer { get; set; }

        private static void Main()
        {
            new Program();
        }

        private void InitializeRenderers()
        {
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheetMC.png", 32, 32);
            TilemapModel model = TilemapParser.ParseTilemap("Game.Resources.Level.mapalla.json");
            Tilemap tilemap = new Tilemap(tilesheet, model);
            this.AnimateTiles(tilesheet);
            Engine.Engine.GetService<TilemapService>().AddTilemap(tilemap, Vector2i.Zero);
        }

        private void AddPlayer()
        {
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheetMC.png", 32, 32);
            Sprite sprite = new Sprite("Game.Resources.Player.adventurer_idle.png", false);

            Gun.Player player = new Gun.Player(96, -33, 1, 1.375f, sprite);
            player.AddComponent(new CameraTrackingComponent());
            Engine.Engine.AddGameObject(player);
            Program.GamePlayer = player;

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
            using Stream enemyStream = this.assembly.GetManifestResourceStream("Game.Resources.enemy.png");
            Sprite enemySprite = new Sprite(enemyStream);
            DummyAI testEnemy = new DummyAI(75, -25, 1, 1, enemySprite, 10);

            // Engine.Engine.AddGameObject(testEnemy);
            using Stream enemyGunSpriteStream = this.assembly.GetManifestResourceStream("Game.Resources.enemyGun.png");
            Sprite enemyGunSprite = new Sprite(enemyGunSpriteStream);
            EnemyPistol enemyWithPistol = new EnemyPistol(81, -43, 1, 1, enemyGunSprite, 5);
            Engine.Engine.AddGameObject(enemyWithPistol);

            Tilesheet fireTilesheet = new Tilesheet("Game.Resources.Animated.fire.png", 32, 32);
            float delay = 0.041f;
            ISprite fireSprite = new AnimatedSprite(fireTilesheet, Keyframe.RangeY(0, 0, 23, delay));
            Enemy.Enemy fire = new Enemy.Enemy(100, -44, 1, 1, fireSprite, 25);
            Engine.Engine.AddGameObject(fire);
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