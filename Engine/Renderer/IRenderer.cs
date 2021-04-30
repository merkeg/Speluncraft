// <copyright file="IRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Rendering layers.
    /// </summary>
    public enum RenderLayer
    {
        /// <summary>
        /// Game Elements.
        /// </summary>
        GAME,

        /// <summary>
        /// UI elements.
        /// </summary>
        UI,
    }

    /// <summary>
    /// Renderer base class.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// The render function.
        /// </summary>
        /// <param name="args">Render update arguments.</param>
        public void Render(FrameEventArgs args);

        /// <summary>
        /// The resize function.
        /// </summary>
        /// <param name="args">resize arguments.</param>
        public void Resize(ResizeEventArgs args);

        /// <summary>
        /// Run when the renderer is added.
        /// </summary>
        public void OnCreate();
    }
}
