// <copyright file="Rectangle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.GameObject
{
    /// <summary>
    /// Rectangle class.
    /// </summary>
    public class Rectangle : IRectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="sizeX">the width of the GameObject.</param>
        /// <param name="sizeY">the height of the GameObject.</param>
        public Rectangle(float minX, float minY, float sizeX, float sizeY)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        /// <summary>
        /// Gets the width of the Rectangle.
        /// </summary>
        public virtual float SizeX { get; }

        /// <summary>
        /// Gets the height of the Rectangle.
        /// </summary>
        public virtual float SizeY { get; }

        /// <summary>
        /// Gets the X-Coordinate of the top right point.
        /// </summary>
        public virtual float MaxX => this.MinX + this.SizeX;

        /// <summary>
        /// Gets the Y-Coordinate of the top right point.
        /// </summary>
        public virtual float MaxY => this.MinY + this.SizeY;

        /// <summary>
        /// Gets or sets the X-Coordinate of bottom left point.
        /// </summary>
        public virtual float MinX { get; set; }

        /// <summary>
        /// Gets or sets the Y-Coordinate of bottom left point.
        /// </summary>
        public virtual float MinY { get; set; }

        /// <inheritdoc/>
        public bool Mirrored { get; set; }
    }
}
