// <copyright file="UiMatrixRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Internal Renderer.
    /// </summary>
    public class UiMatrixRenderer : IRenderer
    {
        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            Vector2i windowSize = Engine.GameWindow.Size;
            GL.LoadIdentity();
            GL.Ortho(0f, windowSize.X, windowSize.Y, 0f, -1f, 1f);
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
            throw new System.NotImplementedException();
        }
    }
}