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

            Menu.StartMenu startMenu = new Menu.StartMenu(new Rectangle(0, 0, 0, 0), Color4.Black, new Font(FontModel.Parse("Game.Resources.Font.semicondensed.font.fnt"), new Sprite("Game.Resources.Font.semicondensed.font.png")), UiAlignment.Left, true);
            Engine.Engine.AddRenderer(startMenu, RenderLayer.UI);
            Camera cam = Engine.Engine.Camera;
            cam.Scale = 5f;
            window.Run();
        }

        private static void Main()
        {
            new Program();
        }
    }
}