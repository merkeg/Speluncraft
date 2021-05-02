// <copyright file="RelativeRectangle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI
{
    using global::Engine.GameObject;

    /// <summary>
    /// Relative rectangle class.
    /// </summary>
    public class RelativeRectangle : Rectangle
    {
        private readonly Rectangle absolute;
        private float relX;
        private float relY;
        private float relSizeX;
        private float relSizeY;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeRectangle"/> class.
        /// </summary>
        /// <param name="absolute">Absolute position.</param>
        /// <param name="minX">Relative minx.</param>
        /// <param name="minY">Relative miny.</param>
        /// <param name="sizeX">Relative sizex.</param>
        /// <param name="sizeY">Relative sizey.</param>
        public RelativeRectangle(Rectangle absolute, float minX, float minY, float sizeX, float sizeY)
            : base(minX, minY, sizeX, sizeY)
        {
            this.absolute = absolute;
            this.relX = minX;
            this.relY = minY;
            this.relSizeX = sizeX;
            this.relSizeY = sizeY;
        }

        /// <inheritdoc/>
        public override float SizeX => this.relSizeX;

        /// <inheritdoc/>
        public override float SizeY => this.relSizeY;

        /// <inheritdoc/>
        public override float MaxX => this.MinX + this.relSizeX;

        /// <inheritdoc/>
        public override float MaxY => this.MinY + this.relSizeY;

        /// <inheritdoc/>
        public override float MinX
        {
            get => this.absolute.MinX + this.relX;
            set => this.relX = value;
        }

        /// <inheritdoc/>
        public override float MinY
        {
            get => this.absolute.MinY + this.relY;
            set => this.relY = value;
        }
    }
}