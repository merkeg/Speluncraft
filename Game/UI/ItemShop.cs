// <copyright file="ItemShop.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Particle;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Tile;
    using Engine.Renderer.UI;
    using Engine.Service;
    using Game.Gun;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Class for Item Shop ingame.
    /// </summary>
    public class ItemShop : IRenderer
    {
        private static bool shopActive = true;
        private static Vector2 screenCenter;

        // Button1 = LeftClick, Button2 = RightClick, Middle = MiddleClick.
        private static Vector2 windowsMousePosition;


        /// <summary>
        /// Gets called when Mouse is Moved.
        /// </summary>
        /// <param name="args">MouseMove Args</param>
        public void MouseMove(MouseMoveEventArgs args)
        {
            windowsMousePosition.X = args.X;
            windowsMousePosition.Y = args.Y;
            return;
        }

        /// <summary>
        /// Gets Called if some Mousebutton is pressed down.
        /// </summary>
        /// <param name="args">MouseDown Args</param>
        public void MouseDown(MouseButtonEventArgs args)
        {
            Debug.WriteLine(args.Button);
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            Engine.Engine.GameWindow.MouseMove += this.MouseMove;
            Engine.Engine.GameWindow.MouseDown += this.MouseDown;
            return;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            // render shop
            if (shopActive)
            {
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.Color4(new Color4(1.0f / 255 * 104, 1.0f / 255 * 167, 1.0f / 255 * 220, 0.5f));
                GL.Begin(PrimitiveType.Quads);

                GL.Vertex2(screenCenter.X - (250 / 2), screenCenter.Y - (250 / 2));
                GL.Vertex2(screenCenter.X + (250 / 2), screenCenter.Y - (250 / 2));
                GL.Vertex2(screenCenter.X + (250 / 2), screenCenter.Y + (250 / 2));
                GL.Vertex2(screenCenter.X - (250 / 2), screenCenter.Y + (250 / 2));

                GL.End();

                return;
            }
            else
            {
                return;
            }
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            screenCenter.X = args.Size.X / 2f;
            screenCenter.Y = args.Size.Y / 2f;
            return;
        }
    }
}
