// <copyright file="ITilesheet.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    /// <summary>
    /// ITilesheet class.
    /// </summary>
    public interface ITilesheet
    {
        /// <summary>
        /// Gets the handle id for the specific Tileset.
        /// </summary>
        public int Handle { get; }

        /// <summary>
        /// Gets the amount in height.
        /// </summary>
        public int AmountTileHeight { get; }

        /// <summary>
        /// Gets the amount in width.
        /// </summary>
        public int AmountTileWidth { get; }

        /// <summary>
        /// Gets the size of the tile in pixels.
        /// </summary>
        public int TileSize { get; }

        /// <summary>
        /// Gets the Tile texture size X.
        /// </summary>
        public float TileTexSizeX => 1f / this.AmountTileWidth;

        /// <summary>
        /// Gets the Tile texture size Y.
        /// </summary>
        public float TileTexSizeY => 1f / this.AmountTileHeight;
    }
}