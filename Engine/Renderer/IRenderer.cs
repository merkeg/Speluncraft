// <copyright file="Renderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using OpenTK.Windowing.Common;

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
    }
}
