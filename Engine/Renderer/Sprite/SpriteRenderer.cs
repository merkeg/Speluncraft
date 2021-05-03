// <copyright file="SpriteRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using global::Engine.GameObject;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// The SpriteRenderer class.
    /// </summary>
    public class SpriteRenderer : IRenderer, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderer"/> class.
        /// </summary>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="position">The position to render the sprite.</param>
        public SpriteRenderer(Sprite sprite, IRectangle position)
        {
            this.Position = position;
            this.Sprite = sprite;
        }

        /// <summary>
        /// Gets the position to render the sprite.
        /// </summary>
        public IRectangle Position { get; private set; }

        /// <summary>
        /// Gets the sprite to render.
        /// </summary>
        public Sprite Sprite { get; internal set; }

        /// <summary>
        /// Yeet it away.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public void OnCreate()
        {
            return;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, this.Sprite.Handle);
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(this.Position.MinX, this.Position.MinY);

            GL.TexCoord2(1, 0);
            GL.Vertex2(this.Position.MaxX, this.Position.MinY);

            GL.TexCoord2(1, 1);
            GL.Vertex2(this.Position.MaxX, this.Position.MaxY);

            GL.TexCoord2(0, 1);
            GL.Vertex2(this.Position.MinX, this.Position.MaxY);

            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            return;
        }
    }
}
