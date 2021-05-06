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

        /// <summary>
        /// Function to get a keyframe array with a set range.
        /// </summary>
        /// <param name="minX">min x.</param>
        /// <param name="maxX">max x.</param>
        /// <param name="y">y.</param>
        /// <param name="time">time in seconds.</param>
        /// <returns>an array.</returns>
        public static Keyframe[] RangeX(int minX, int maxX, int y, float time)
        {
            Keyframe[] keyframes = new Keyframe[maxX - minX];
            for (int x = minX; x <= maxX; x++)
            {
                keyframes[x - minX] = new Keyframe(x, y, time);
            }

            return keyframes;
        }

        /// <summary>
        /// Function to get a keyframe array with a set range.
        /// </summary>
        /// <param name="x">x.</param>
        /// <param name="minY">min y.</param>
        /// <param name="maxY">max y.</param>
        /// <param name="time">time in seconds.</param>
        /// <returns>an array.</returns>
        public static Keyframe[] RangeY(int x, int minY, int maxY, float time)
        {
            Keyframe[] keyframes = new Keyframe[maxY - minY];
            for (int y = minY; y <= maxY; y++)
            {
                keyframes[y - minY] = new Keyframe(x, y, time);
            }

            return keyframes;
        }
    }
}