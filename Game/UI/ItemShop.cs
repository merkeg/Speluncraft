// <copyright file="ItemShop.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
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

        private static Vector3 shopSpriteAspect = new Vector3(1280, 720, 720f / 1280f); // width, height and original aspect-ratio (I hate scaling :()
        private static Vector2 screenCenter;
        private static Vector2 screenSize;
        private static float shopWindowBorderIndent = 300;

        private static Vector2 windowsMousePosition; // Button1 = LeftClick, Button2 = RightClick, Middle = MiddleClick.

        private static Assembly assembly;
        private static Sprite shopBackground;

        /// <summary>
        /// Gets called when Mouse is Moved.
        /// </summary>
        /// <param name="args">MouseMove Args.</param>
        public void MouseMove(MouseMoveEventArgs args)
        {
            windowsMousePosition.X = args.X;
            windowsMousePosition.Y = args.Y;
            return;
        }

        /// <summary>
        /// Gets Called if some Mousebutton is pressed down.
        /// </summary>
        /// <param name="args">MouseDown Args.</param>
        public void MouseDown(MouseButtonEventArgs args)
        {
            Debug.WriteLine(args.Button);
            Debug.WriteLine(shopSpriteAspect.Z);
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            Engine.Engine.GameWindow.MouseMove += this.MouseMove;
            Engine.Engine.GameWindow.MouseDown += this.MouseDown;

            assembly = Assembly.GetExecutingAssembly();
            using Stream shopBackgroundSpriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.itemshop.png");
            shopBackground = new Sprite(shopBackgroundSpriteStream, false);
            return;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            // render shop
            if (shopActive)
            {
                GL.BindTexture(TextureTarget.Texture2D, shopBackground.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2(screenCenter.X - (screenSize.X / 2f), screenCenter.Y - (shopSpriteAspect.Z * screenSize.X / 2));

                GL.TexCoord2(1, 1);
                GL.Vertex2(screenCenter.X + (screenSize.X / 2f), screenCenter.Y - (shopSpriteAspect.Z * screenSize.X / 2));

                GL.TexCoord2(1, 0);
                GL.Vertex2(screenCenter.X + (screenSize.X / 2f), screenCenter.Y + (shopSpriteAspect.Z * screenSize.X / 2));

                GL.TexCoord2(0, 0);
                GL.Vertex2(screenCenter.X - (screenSize.X / 2f), screenCenter.Y + (shopSpriteAspect.Z * screenSize.X / 2));

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
            screenSize.X = args.Size.X;
            screenSize.Y = args.Size.Y;
            screenSize.X = screenSize.X - shopWindowBorderIndent;
            return;
        }
    }
}
