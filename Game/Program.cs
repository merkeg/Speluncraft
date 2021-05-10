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
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheet.png", 16, 16);

            AnimatedSprite animatedSprite =
                new AnimatedSprite(tilesheet, new[] { new Keyframe(0, 1, .3f), new Keyframe(1, 1, .3f) });
            tilesheet.SetCustomSprite(0, animatedSprite);
            TilemapModel model = TilemapParser.ParseTilemap("Game.Resources.Level.getUp.json");
            Tilemap tilemap = new Tilemap(tilesheet, model);

            Engine.Engine.GetService<TilemapService>().AddTilemap(tilemap, Vector2i.Zero);
        }

        private void AddPlayer()
        {
            Tilesheet tilesheet = new Tilesheet("Game.Resources.Sprite.tilesheet.png", 16, 16);
            AnimatedSprite sprite = new AnimatedSprite(tilesheet, new[] { new Keyframe(9, 0), new Keyframe(10, 0), new Keyframe(12, 0) });

            Player.Player player = new Player.Player(7, -27, 1, 1, sprite);
            player.AddComponent(new CameraTrackingComponent());
            player.Mirrored = true;
            Engine.Engine.AddGameObject(player);

            // make sure to initialize healthbar after the player
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            Engine.Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            FontModel fontModel = FontModel.Parse("Game.Resources.Font.hack.font.fnt");
            Sprite fontSprite = new Sprite("Game.Resources.Font.hack.font.png");
            Font font = new Font(fontModel, fontSprite);

            DebugRenderer debugRenderer = new DebugRenderer(new Rectangle(5, 5, 300, 325), new Color4(0, 0, 0, 0.3f), font, player, UiAlignment.Right);
            Engine.Engine.AddRenderer(debugRenderer, RenderLayer.UI);
        }

        private void AddEnemies()
        {
            Sprite enemySprite = new Sprite("Game.Resources.enemy.png");
            DummyAI testEnemy = new DummyAI(2, -25, 1, 1, enemySprite, 10);
            Engine.Engine.AddGameObject(testEnemy);

            Sprite enemyGunSprite = new Sprite("Game.Resources.enemyGun.png");
            EnemyPistol enemyWithPistol = new EnemyPistol(5, -20, 1, 1, enemyGunSprite, 5);
            Engine.Engine.AddGameObject(enemyWithPistol);
        }
    }
}