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
    using Game.Enemy;
    using Game.Player;
    using Game.UI;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using Game.UI;

    /// <summary>
    /// The Main class of the game.
    /// </summary>
    internal class Program
    {
        private Engine.Engine engine;
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
            this.engine = Engine.Engine.Instance();
            this.engine.StartEngine(window);
            this.assembly = Assembly.GetExecutingAssembly();

            this.InitializeRenderers();
            this.AddPlayer();
            this.AddEnemies();

            Camera cam = this.engine.Camera;
            cam.Scale = 5f;
            window.Run();
        }

        private static void Main()
        {
            new Program();
        }

        private void InitializeRenderers()
        {
            using Stream tilesheet = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.tilesheet.png");
            using Stream tilemapStream = this.assembly.GetManifestResourceStream("Game.Resources.Level.getUp.json");

            Tileset tileset = new Tileset(tilesheet, 16);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tileset, model);

            TilemapRenderer renderer = new TilemapRenderer(tilemap, 0, 0);
            this.engine.AddRenderer(renderer);
        }

        private void AddPlayer()
        {
            using Stream spriteStream = this.assembly.GetManifestResourceStream("Game.Resources.Floppa.png");
            Sprite sprite = new Sprite(spriteStream);
            Player.Player player = new Player.Player(7, -27, 1, 1, sprite);
            player.AddComponent(new CameraTrackingComponent());
            this.engine.AddGameObject(player);

            // make sure to initialize UI after the player
            UILoader.Initialize_UI(this.engine);

            using Stream fontModelStream = this.assembly.GetManifestResourceStream("Game.Resources.Font.hack.font.fnt");
            using Stream fontStream = this.assembly.GetManifestResourceStream("Game.Resources.Font.hack.font.png");
            FontModel fontModel = FontModel.Parse(fontModelStream);
            Sprite fontSprite = new Sprite(fontStream);
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            this.engine.AddRenderer(debugRenderer, RenderLayer.UI);
        }

        private void AddEnemies()
        {
            using Stream enemyStream = this.assembly.GetManifestResourceStream("Game.Resources.enemy.png");
            Sprite enemySprite = new Sprite(enemyStream);
            DummyAI testEnemy = new DummyAI(2, -25, 1, 1, enemySprite, 10);
            this.engine.AddGameObject(testEnemy);

            using Stream enemyGunSpriteStream = this.assembly.GetManifestResourceStream("Game.Resources.enemyGun.png");
            Sprite enemyGunSprite = new Sprite(enemyGunSpriteStream);
            EnemyPistol enemyWithPistol = new EnemyPistol(5, -20, 1, 1, enemyGunSprite, 5);
            this.engine.AddGameObject(enemyWithPistol);
        }
    }
}