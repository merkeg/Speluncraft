// <copyright file="CollisionRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using System;
    using global::Engine.GameObject;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The collision renderer class.
    /// </summary>
    public class CollisionRenderer : IRenderer
    {
        /// <inheritdoc/>
        public void OnCreate()
        {
            return;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.LineWidth(2);
            GL.Color3(System.Drawing.Color.Cyan);
            foreach (IRectangle rec in Engine.Colliders)
            {
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(rec.MinX, rec.MinY);
                GL.Vertex2(rec.MinX, rec.MaxY);
                GL.Vertex2(rec.MaxX, rec.MaxY);
                GL.Vertex2(rec.MaxX, rec.MinY);
                GL.End();
            }
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            return;
        }
    }
}
