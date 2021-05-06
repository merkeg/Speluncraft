// <copyright file="TileSprite.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using global::Engine.Renderer.Sprite;

    /// <summary>
    /// TileSprite class.
    /// </summary>
    public class TileSprite : ISprite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSprite"/> class.
        /// </summary>
        /// <param name="handle">Handle.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="texX0">TexX0.</param>
        /// <param name="texY0">TexY0.</param>
        /// <param name="texX1">TexX1.</param>
        /// <param name="texY1">TexY1.</param>
        public TileSprite(int handle, int width, int height, float texX0, float texY0, float texX1, float texY1)
        {
            this.Handle = handle;
            this.Width = width;
            this.Height = height;
            this.TexX0 = texX0;
            this.TexX1 = texX1;
            this.TexY0 = texY0;
            this.TexY1 = texY1;
        }

        /// <inheritdoc/>
        public int Handle { get; }

        /// <inheritdoc/>
        public int Width { get; }

        /// <inheritdoc/>
        public int Height { get; }

        /// <inheritdoc/>
        public float TexX0 { get; }

        /// <inheritdoc/>
        public float TexX1 { get; }

        /// <inheritdoc/>
        public float TexY0 { get; }

        /// <inheritdoc/>
        public float TexY1 { get; }

        /// <inheritdoc/>
        public void TimeElapsed(float time)
        {
        }
    }
}