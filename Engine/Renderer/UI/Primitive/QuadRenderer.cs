// <copyright file="QuadRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI.Primitive
{
    using global::Engine.GameObject;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Quad Renderer class.
    /// </summary>
    public class QuadRenderer : IRenderer
    {
        private bool filled;
        private Color4 color;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadRenderer"/> class.
        /// </summary>
        /// <param name="bounds">Bounds of quad.</param>
        /// <param name="color">Color of quad.</param>
        /// <param name="filled">Quad filled.</param>
        public QuadRenderer(IRectangle bounds, Color4 color, bool filled = true)
        {
            this.Bounds = bounds;
            this.color = color;
            this.filled = filled;
        }

        /// <summary>
        /// Gets or sets bounds.
        /// </summary>
        public IRectangle Bounds { get; set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Color4(this.color);
            GL.Begin(this.filled ? PrimitiveType.Quads : PrimitiveType.LineStrip);
            GL.Vertex2(this.Bounds.MinX, this.Bounds.MinY);
            GL.Vertex2(this.Bounds.MaxX, this.Bounds.MinY);
            GL.Vertex2(this.Bounds.MaxX, this.Bounds.MaxY);
            GL.Vertex2(this.Bounds.MinX, this.Bounds.MaxY);
            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}