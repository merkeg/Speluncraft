// <copyright file="Keyframe.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    /// <summary>
    /// Keyframe struct.
    /// </summary>
    public readonly struct Keyframe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keyframe"/> struct.
        /// </summary>
        /// <param name="x">the X position of the sprite in the tilesheet.</param>
        /// <param name="y">the Y position of the sprite in the tilesheet.</param>
        /// <param name="time">time it stays at this sprite.</param>
        public Keyframe(int x, int y, float time = 1)
        {
            this.X = x;
            this.Y = y;
            this.Time = time;
        }

        /// <summary>
        /// Gets the X position of the sprite in the tilesheet.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y position of the sprite in the tilesheet.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets the time it stays at this sprite.
        /// </summary>
        public float Time { get; }
    }
}