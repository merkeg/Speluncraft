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
            GL.LineWidth(2);
            GL.Color3(System.Drawing.Color.Cyan);
            foreach (Rectangle rec in Engine.Instance().Colliders)
            {
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(rec.MinX, rec.MinY);
                GL.Vertex2(rec.MinX, rec.MinY + rec.SizeY);
                GL.Vertex2(rec.MinX + rec.SizeX, rec.MinY + rec.SizeY);
                GL.Vertex2(rec.MinX + rec.SizeX, rec.MinY);
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
