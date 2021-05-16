﻿// <copyright file="Program.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game
{
    using System.IO;
    using System.Reflection;
    using Engine.Camera;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;
    using Engine.Renderer.UI;
    using Engine.Service;
    using Game.Enemy;
    using Game.Player;
    using Game.UI;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

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

            Player.Player player = new Player.Player(96, -33, 1, 1.375f, sprite);
            player.AddComponent(new CameraTrackingComponent());
            Engine.Engine.AddGameObject(player);

            // make sure to initialize healthbar after the player
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            Engine.Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            FontModel fontModel = FontModel.Parse("Game.Resources.Font.hack.font.fnt");
            Sprite fontSprite = new Sprite("Game.Resources.Font.hack.font.png");
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
            Engine.Engine.GetService<TilemapService>().SetOptimizationPoint(player);
        }

        private void AddEnemies()
        {
            Tilesheet walkingSheet = new Tilesheet("Game.Resources.Enemy.zombie_walking.png", 80, 110);
            AnimatedSprite spriteWalking = new AnimatedSprite(walkingSheet, Keyframe.RangeX(0, 1, 0, 0.1f));
            EnemyPistol enemyWithPistol = new EnemyPistol(81, -43, 1, 1.375f, spriteWalking, 5);
            Engine.Engine.AddGameObject(enemyWithPistol);

            // Spam map with Enemys (TEMPORARY)
            Engine.Engine.AddGameObject(new DummyAI(42, -35, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(81, -56, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(81, -73, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(68, -60, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(93, -75, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(77, -88, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(94, -95, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(32, -98, 1, 1.375f, spriteWalking, 5));
            Engine.Engine.AddGameObject(new DummyAI(31, -108, 1, 1.375f, spriteWalking, 5));

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