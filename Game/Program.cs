// <copyright file="Program.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Example
{
    using System.IO;
    using System.Reflection;
    using Engine.Camera;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;
    using Game.Player;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The Main class of the game.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            GameWindow window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
            window.Size = new Vector2i(1280, 720);
            window.VSync = VSyncMode.Adaptive;
            Engine.Engine engine = Engine.Engine.Instance();
            engine.StartEngine(window);

            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream tilesheet = assembly.GetManifestResourceStream("Game.Resources.tilesheet.png");
            using Stream tilemapStream = assembly.GetManifestResourceStream("Game.Resources.jumpNrun.json");

            Tileset tileset = new Tileset(tilesheet, 16);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tileset, model);

            TilemapRenderer renderer = new TilemapRenderer(tilemap, 0, 0);
            engine.AddRenderer(renderer);

            using Stream spriteStream = assembly.GetManifestResourceStream("Game.Resources.player.png");
            Sprite sprite = new Sprite(spriteStream);

            Player player = new Player(3, -5, 1, 1, sprite);
            player.AddComponent(new CameraTrackingComponent());
            engine.AddGameObject(player);

            Camera cam = engine.Camera;
            cam.Scale = 5f;
            window.Run();
        }
    }
}