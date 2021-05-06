// <copyright file="Program.cs" company="RWUwU">
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
            Engine.Engine.AddService(new TestService());
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
            using Stream tilesheetStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.tilesheetMC.png");
            using Stream tilemapStream = this.assembly.GetManifestResourceStream("Game.Resources.Level.mapalla.json");

            Tilesheet tilesheet = new Tilesheet(tilesheetStream, 32);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tilesheet, model);

            TilemapRenderer renderer = new TilemapRenderer(tilemap, 0, 0);
            Engine.Engine.AddRenderer(renderer);
        }

        private void AddPlayer()
        {
            using Stream tilesheetStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.tilesheetMC.png");
            Tilesheet tilesheet = new Tilesheet(tilesheetStream, 32);
            AnimatedSprite sprite = new AnimatedSprite(tilesheet, new[] { new Keyframe(1, 0), new Keyframe(2, 0), new Keyframe(3, 0) });

            Player.Player player = new Player.Player(96, -33, 1, 1, sprite);
            player.AddComponent(new CameraTrackingComponent());
            player.Mirrored = true;
            Engine.Engine.AddGameObject(player);

            // make sure to initialize healthbar after the player
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            Engine.Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            using Stream fontModelStream = this.assembly.GetManifestResourceStream("Game.Resources.Font.hack.font.fnt");
            using Stream fontStream = this.assembly.GetManifestResourceStream("Game.Resources.Font.hack.font.png");
            FontModel fontModel = FontModel.Parse(fontModelStream);
            Sprite fontSprite = new Sprite(fontStream);
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
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

            using Stream fireStream = this.assembly.GetManifestResourceStream("Game.Resources.Animated.fire.png");
            Tilesheet fireTilesheet = new Tilesheet(fireStream, 32);
            float delay = 0.041f;
            ISprite fireSprite = new AnimatedSprite(fireTilesheet, new[] { new Keyframe(0, 0, delay), new Keyframe(0, 1, delay), new Keyframe(0, 2, delay), new Keyframe(0, 3, delay), new Keyframe(0, 4, delay), new Keyframe(0, 5, delay), new Keyframe(0, 6, delay), new Keyframe(0, 7, delay), new Keyframe(0, 8, delay), new Keyframe(0, 9, delay), new Keyframe(0, 10, delay), new Keyframe(0, 11, delay), new Keyframe(0, 12, delay), new Keyframe(0, 13, delay), new Keyframe(0, 14, delay), new Keyframe(0, 15, delay), new Keyframe(0, 16, delay), new Keyframe(0, 17, delay), new Keyframe(0, 18, delay), new Keyframe(0, 19, delay), new Keyframe(0, 20, delay), new Keyframe(0, 21, delay), new Keyframe(0, 22, delay), new Keyframe(0, 23, delay) });
            Enemy.Enemy fire = new Enemy.Enemy(100, -44, 1, 1, fireSprite, 25);
            Engine.Engine.AddGameObject(fire);
        }
    }
}