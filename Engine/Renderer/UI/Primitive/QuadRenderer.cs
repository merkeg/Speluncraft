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
        private IRectangle bounds;
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
            this.bounds = bounds;
            this.color = color;
            this.filled = filled;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Color4(this.color);
            GL.Begin(this.filled ? PrimitiveType.Quads : PrimitiveType.LineStrip);
            GL.Vertex2(this.bounds.MinX, this.bounds.MinY);
            GL.Vertex2(this.bounds.MaxX, this.bounds.MinY);
            GL.Vertex2(this.bounds.MaxX, this.bounds.MaxY);
            GL.Vertex2(this.bounds.MinX, this.bounds.MaxY);
            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnCreate()
        {
        }
    }
}