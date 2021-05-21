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
        private static Vector3 shopSpriteAspect = new Vector3(1280, 720, 720f / 1280f); // width, height and original aspect-ratio (I hate scaling :()
        private static Vector2 screenCenter;
        private static Vector2 screenSize;
        private static float shopWidth;
        private static float shopWindowBorderIndent = 300;
        private static Vector2 shopOrigin;

        private static Vector2 windowsMousePosition; // Button1 = LeftClick, Button2 = RightClick, Middle = MiddleClick.

        private static Assembly assembly;
        private static Sprite shopBackground;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemShop"/> class.
        /// </summary>
        /// <param name="itemcount">Amount of items in shop.</param>
        public ItemShop(int itemcount = 4)
        {
            this.ItemCount = itemcount;
        }

        /// <summary>
        /// Gets or Sets a value indicating whether ItemShop should be rendered or not.
        /// </summary>
        public bool ShopActive { get; set; }

        /// <summary>
        /// Gets or Sets avalue indicating whether many Items should be in the Shop.
        /// </summary>
        public int ItemCount { get; set; }

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
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            this.ShopActive = true; // remove after done
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
            if (this.ShopActive)
            {
                // render shop background
                GL.BindTexture(TextureTarget.Texture2D, shopBackground.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2(screenCenter.X - (shopWidth / 2f), screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(1, 1);
                GL.Vertex2(screenCenter.X + (shopWidth / 2f), screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(1, 0);
                GL.Vertex2(screenCenter.X + (shopWidth / 2f), screenCenter.Y + (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(0, 0);
                GL.Vertex2(screenCenter.X - (shopWidth / 2f), screenCenter.Y + (shopSpriteAspect.Z * shopWidth / 2));

                GL.End();

                this.RenderItems(args);

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
            shopWidth = screenSize.X - shopWindowBorderIndent;

            shopOrigin.X = screenCenter.X - (shopWidth / 2f);
            shopOrigin.Y = screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2);
            return;
        }

        /// <summary>
        /// render itemslots onto shopbackground.
        /// </summary>
        private void RenderItems(FrameEventArgs args)
        {
            for (int i = 1; i <= this.ItemCount; i++)
            {
                // render itemslots
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                // GL.TexCoord2(0, 1);
                GL.Vertex2(shopOrigin.X, shopOrigin.Y);

                // GL.TexCoord2(1, 1);
                GL.Vertex2(shopOrigin.X + (50 * i), shopOrigin.Y);

                // GL.TexCoord2(1, 0);
                GL.Vertex2(shopOrigin.X + (50 * i), shopOrigin.Y + 50);

                // GL.TexCoord2(0, 0);
                GL.Vertex2(shopOrigin.X, shopOrigin.Y + 50);

                GL.End();
            }
        }
    }
}
