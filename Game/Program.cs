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
            using Stream tilesheetStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.tilesheet.png");
            using Stream tilemapStream = this.assembly.GetManifestResourceStream("Game.Resources.Level.getUp.json");

            Tilesheet tilesheet = new Tilesheet(tilesheetStream, 16);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tilesheet, model);

            TilemapRenderer renderer = new TilemapRenderer(tilemap, 0, 0);
            Engine.Engine.AddRenderer(renderer);
        }

        private void AddPlayer()
        {
            using Stream tilesheetStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.tilesheet.png");
            Tilesheet tilesheet = new Tilesheet(tilesheetStream, 16);
            AnimatedSprite sprite = new AnimatedSprite(tilesheet, new[] { new Keyframe(9, 0), new Keyframe(10, 0), new Keyframe(12, 0) });

            Player.Player player = new Player.Player(7, -27, 1, 1, sprite);
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
            DummyAI testEnemy = new DummyAI(2, -25, 1, 1, enemySprite, 10);
            Engine.Engine.AddGameObject(testEnemy);

            using Stream enemyGunSpriteStream = this.assembly.GetManifestResourceStream("Game.Resources.enemyGun.png");
            Sprite enemyGunSprite = new Sprite(enemyGunSpriteStream);
            EnemyPistol enemyWithPistol = new EnemyPistol(5, -20, 1, 1, enemyGunSprite, 5);
            Engine.Engine.AddGameObject(enemyWithPistol);
        }
    }
}