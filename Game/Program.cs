namespace Example
{
    using Engine.Camera;
    using Engine.Renderer;
    using Engine.Renderer.Tile;
    using Engine.Renderer.Tile.Parser;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;
    using System;
    using System.IO;
    using System.Reflection;

    internal class Program
    {
        private static void Main()
        {
            GameWindow window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            window.Size = new Vector2i(800, 600);

            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream tilesheet = assembly.GetManifestResourceStream("Game.Resources.tilesheet.png");
            using Stream tilemapStream = assembly.GetManifestResourceStream("Game.Resources.all.json");

            Tileset tileset = new Tileset(tilesheet);
            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(tileset, model.layers[0], 16);
            tilemap.TileTexSizeX = 1f / 48;
            tilemap.TileTexSizeY = 1f / 22;

            TilemapRenderer renderer = new TilemapRenderer(tilemap);
            Camera cam = new Camera();

            window.RenderFrame += renderer.Render;
            window.RenderFrame += _ => cam.Draw();
            window.RenderFrame += _ => window.SwapBuffers();
            window.Resize += _ => cam.Resize(_.Width, _.Height);

            window.UpdateFrame += a =>
            {
                KeyboardState state = window.KeyboardState;
                var axisX = state.IsKeyDown(Keys.E) ? -1f : state.IsKeyDown(Keys.Q) ? 1f : 0f;

                var zoom = cam.Scale * (1 + a.Time * axisX);
                zoom = MathHelper.Clamp(zoom, 5f, 20f);
                cam.Scale = (float) zoom;

                float axisLeftRight = state.IsKeyDown(Keys.A) ? -1.0f : state.IsKeyDown(Keys.D) ? 1.0f : 0.0f;
                float axisUpDown = state.IsKeyDown(Keys.S) ? -1.0f : state.IsKeyDown(Keys.W) ? 1.0f : 0.0f;
                var movement = ((float)a.Time) * new Vector2(axisLeftRight, axisUpDown);
                cam.Center += movement.TransformDirection(cam.CameraMatrix.Inverted());
            };
            window.Run();
        }
    }
}