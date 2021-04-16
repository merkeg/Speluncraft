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
            window.Size = new Vector2i(800, 600);
            window.VSync = VSyncMode.Adaptive;
            Engine.Engine engine = Engine.Engine.Instance();
            engine.StartEngine(window);

            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream tilesheet = assembly.GetManifestResourceStream("Game.Resources.tilesheet.png");
            using Stream tilemapStream = assembly.GetManifestResourceStream("Game.Resources.lol.json");

            Tileset tileset = new Tileset(tilesheet, 16);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tileset, model);

            TilemapRenderer renderer = new TilemapRenderer(tilemap, 0, 0);
            engine.AddRenderer(renderer);

            CollisionRenderer colRenderer = new CollisionRenderer();
            engine.AddRenderer(colRenderer);

            using Stream spriteStream = assembly.GetManifestResourceStream("Game.Resources.player.png");
            Sprite sprite = new Sprite(spriteStream);

            GameObject player = new GameObject(-3, -3, 1, 1, sprite);
            engine.AddGameObject(player);

            Camera cam = engine.Camera;
            window.UpdateFrame += a =>
            {
                KeyboardState state = window.KeyboardState;
                var axisX = state.IsKeyDown(Keys.End) ? -1f : state.IsKeyDown(Keys.Delete) ? 1f : 0f;

                var zoom = cam.Scale * (1 + (a.Time * axisX));
                zoom = MathHelper.Clamp(zoom, 5f, 10f);
                cam.Scale = (float)zoom;

                float axisLeftRight = state.IsKeyDown(Keys.Left) ? -1.0f : state.IsKeyDown(Keys.Right) ? 1.0f : 0.0f;
                float axisUpDown = state.IsKeyDown(Keys.Down) ? -1.0f : state.IsKeyDown(Keys.Up) ? 1.0f : 0.0f;
                var movement = ((float)a.Time) * new Vector2(axisLeftRight, axisUpDown);
                cam.Center += movement.TransformDirection(cam.CameraMatrix.Inverted());
            };

            window.UpdateFrame += a =>
            {
                KeyboardState state = window.KeyboardState;
                float axisLeftRight = state.IsKeyDown(Keys.A) ? -1.0f : state.IsKeyDown(Keys.D) ? 1.0f : 0.0f;
                float axisUpDown = state.IsKeyDown(Keys.S) ? -1.0f : state.IsKeyDown(Keys.W) ? 1.0f : 0.0f;

                player.MinX += (float)a.Time * axisLeftRight * 3;
                player.MinY += (float)a.Time * axisUpDown * 3;
            };

            window.Run();
        }
    }
}