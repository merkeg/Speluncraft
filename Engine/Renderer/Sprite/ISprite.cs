// <copyright file="ISprite.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using OpenTK.Mathematics;

    /// <summary>
    /// ISprite class.
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// Gets the OpenGL texture handle.
        /// </summary>
        public int Handle { get; }

        /// <summary>
        /// Gets the sprite width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the sprite height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the texture x0 position.
        /// </summary>
        public float TexX0 { get; }

        /// <summary>
        /// Gets the texture x1 position.
        /// </summary>
        public float TexX1 { get; }

        /// <summary>
        /// Gets the texture y0 position.
        /// </summary>
        public float TexY0 { get; }

        /// <summary>
        /// Gets the texture y1 position.
        /// </summary>
        public float TexY1 { get; }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        public Color4? Color { get; set; }

        /// <summary>
        /// Before rendering the renderer runs this with the time attached.
        /// </summary>
        /// <param name="time">The elapsed time in seconds.</param>
        public void TimeElapsed(float time);
    }
}