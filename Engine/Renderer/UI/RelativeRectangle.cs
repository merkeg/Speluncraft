// <copyright file="RelativeRectangle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI
{
    using global::Engine.GameObject;

    /// <summary>
    /// Where the Relative Rectangle gets aligned on the X-axis.
    /// </summary>
    public enum RelativeRectangleXAlignment
    {
        /// <summary>
        /// Left alignment.
        /// </summary>
        Left,

        /// <summary>
        /// Right alignment.
        /// </summary>
        Right,
    }

    /// <summary>
    /// Where the Relative Rectangle gets aligned on the Y-axis.
    /// </summary>
    public enum RelativeRectangleYAlignment
    {
        /// <summary>
        /// Top alignment.
        /// </summary>
        Top,

        /// <summary>
        /// Bottom alignment.
        /// </summary>
        Bottom,
    }

    /// <summary>
    /// Relative rectangle class.
    /// </summary>
    public class RelativeRectangle : Rectangle
    {
        private readonly IRectangle absolute;
        private float relX;
        private float relY;
        private float relSizeX;
        private float relSizeY;
        private RelativeRectangleXAlignment xAlignment;
        private RelativeRectangleYAlignment yAlignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeRectangle"/> class.
        /// </summary>
        /// <param name="absolute">Absolute position.</param>
        /// <param name="minX">Relative minx.</param>
        /// <param name="minY">Relative miny.</param>
        /// <param name="sizeX">Relative sizex.</param>
        /// <param name="sizeY">Relative sizey.</param>
        /// <param name="xAlignment">X Alignment of the Rectangle to the parent.</param>
        /// <param name="yAlignment">Y Alignment of the Rectangle to the parent.</param>
        public RelativeRectangle(IRectangle absolute, float minX, float minY, float sizeX, float sizeY, RelativeRectangleXAlignment xAlignment = RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment yAlignment = RelativeRectangleYAlignment.Top)
            : base(minX, minY, sizeX, sizeY)
        {
            this.absolute = absolute;
            this.relX = minX;
            this.relY = minY;
            this.relSizeX = sizeX;
            this.relSizeY = sizeY;
            this.xAlignment = xAlignment;
            this.yAlignment = yAlignment;
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
            get
            {
                if (this.xAlignment == RelativeRectangleXAlignment.Left)
                {
                    return this.absolute.MinX + this.relX;
                }
                else
                {
                    return this.absolute.MaxX + this.relX;
                }
            }
            set => this.relX = value;
        }

        /// <inheritdoc/>
        public override float MinY
        {
            get
            {
                if (this.yAlignment == RelativeRectangleYAlignment.Top)
                {
                    return this.absolute.MinY + this.relY;
                }
                else
                {
                    return this.absolute.MaxY + this.relY;
                }
            }
            set => this.relY = value;
        }
    }
}