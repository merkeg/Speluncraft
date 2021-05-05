// <copyright file="AnimatedSprite.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using global::Engine.Renderer.Tile;

    /// <summary>
    /// Animated sprite class.
    /// </summary>
    public class AnimatedSprite : ISprite
    {
        private int currentIndex;
        private float elapsed;
        private ITilesheet tilesheet;
        private Keyframe[] keyframes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedSprite"/> class.
        /// </summary>
        /// <param name="tilesheet">The tilesheet.</param>
        /// <param name="keyframes">Keyframes.</param>
        public AnimatedSprite(ITilesheet tilesheet, Keyframe[] keyframes)
        {
            this.tilesheet = tilesheet;
            this.keyframes = keyframes;
        }

        /// <inheritdoc/>
        public int Handle => this.tilesheet.Handle;

        /// <inheritdoc/>
        public int Width => this.tilesheet.TileSize;

        /// <inheritdoc/>
        public int Height => this.tilesheet.TileSize;

        /// <inheritdoc/>
        public float TexX0 => this.tilesheet.TileTexSizeX * this[this.currentIndex].X;

        /// <inheritdoc/>
        public float TexX1 => this.TexX0 + this.tilesheet.TileTexSizeX;

        /// <inheritdoc/>
        public float TexY0 => this.tilesheet.TileTexSizeY * this[this.currentIndex].Y;

        /// <inheritdoc/>
        public float TexY1 => this.TexY0 + this.tilesheet.TileTexSizeY;

        /// <summary>
        /// Reference to this.
        /// </summary>
        /// <param name="keyframe">The keyframe.</param>
        public ref Keyframe this[int keyframe] => ref this.keyframes[keyframe];

        /// <inheritdoc/>
        public void TimeElapsed(float time)
        {
            this.elapsed += time;
            if (this.elapsed >= this[this.currentIndex].Time)
            {
                this.elapsed = 0;
                this.currentIndex = ++this.currentIndex % this.keyframes.Length;
            }
        }
    }
}