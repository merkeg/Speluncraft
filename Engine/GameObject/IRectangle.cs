// <copyright file="IRectangle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.GameObject
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Rectangle definde with 2 points.
    /// </summary>
    public interface IRectangle : IMirrorable
    {
        /// <summary>
        /// Gets X-Coordinate of the left bottom point, of the Rectangle.
        /// </summary>
        public float MinX { get; }

        /// <summary>
        /// Gets Y-Coordinate of the left bottom point, of the Rectangle.
        /// </summary>
        public float MinY { get; }

        /// <summary>
        /// Gets the X-Coordinate of the top right point, of the Rectangle.
        /// </summary>
        public float MaxX { get; }

        /// <summary>
        /// Gets the X-Coordinate of the top right point, of the Rectangle.
        /// </summary>
        public float MaxY { get; }
    }
}