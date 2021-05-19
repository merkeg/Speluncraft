// <copyright file="SpriteRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using global::Engine.GameObject;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The SpriteRenderer class.
    /// </summary>
    public class SpriteRenderer : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderer"/> class.
        /// </summary>
        /// <param name="sprite">The sprite to render.</param>
        /// <param name="position">The position to render the sprite.</param>
        public SpriteRenderer(ISprite sprite, IRectangle position)
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
        public ISprite Sprite { get; internal set; }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            return;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            this.Sprite.TimeElapsed((float)args.Time);
            GL.BindTexture(TextureTarget.Texture2D, this.Sprite.Handle);
            GL.Color4(this.Sprite.Color ?? Color4.White);
            GL.Begin(PrimitiveType.Quads);

            if (this.Position.Mirrored)
            {
                GL.TexCoord2(this.Sprite.TexX1, this.Sprite.TexY0);
                GL.Vertex2(this.Position.MinX, this.Position.MinY);

                GL.TexCoord2(this.Sprite.TexX0, this.Sprite.TexY0);
                GL.Vertex2(this.Position.MaxX, this.Position.MinY);

                GL.TexCoord2(this.Sprite.TexX0, this.Sprite.TexY1);
                GL.Vertex2(this.Position.MaxX, this.Position.MaxY);

                GL.TexCoord2(this.Sprite.TexX1, this.Sprite.TexY1);
                GL.Vertex2(this.Position.MinX, this.Position.MaxY);
            }
            else
            {
                GL.TexCoord2(this.Sprite.TexX0, this.Sprite.TexY0);
                GL.Vertex2(this.Position.MinX, this.Position.MinY);

                GL.TexCoord2(this.Sprite.TexX1, this.Sprite.TexY0);
                GL.Vertex2(this.Position.MaxX, this.Position.MinY);

                GL.TexCoord2(this.Sprite.TexX1, this.Sprite.TexY1);
                GL.Vertex2(this.Position.MaxX, this.Position.MaxY);

                GL.TexCoord2(this.Sprite.TexX0, this.Sprite.TexY1);
                GL.Vertex2(this.Position.MinX, this.Position.MaxY);
            }

            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            return;
        }
    }
}
