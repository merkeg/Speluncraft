// <copyright file="Tilemap.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using global::Engine.Renderer.Tile.Parser;

    /// <summary>
    /// The Tilemap class.
    /// </summary>
    public class Tilemap
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Tilemap"/> class.
        /// </summary>
        /// <param name="tileset"> The tileset the tilemap is using.</param>
        /// <param name="model"> The model the tilemap bases on.</param>
        /// <param name="tileSize"> Size of the tiles in pixels</param>
        public Tilemap(Tileset tileset, TilemapLayerModel model, int tileSize)
        {
            this.Tileset = tileset;
            this.TilemapModel = model;
            this.Tiles = model.data;
            this.TileSize = tileSize;
        }

        /// <summary>
        /// Gets the tile amount in length.
        /// </summary>
        public int Width
        {
            get
            {
                return this.TilemapModel.width;
            }
        }

        /// <summary>
        /// Gets the tile amount in height.
        /// </summary>
        public int Height
        {
            get
            {
                return this.TilemapModel.height;
            }
        }

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        public uint[] Tiles { get; private set; }

        /// <summary>
        /// Gets or sets the tileset.
        /// </summary>
        public Tileset Tileset { get; set; }

        /// <summary>
        /// Gets the Tilemap model.
        /// </summary>
        public TilemapLayerModel TilemapModel { get; private set; }

        /// <summary>
        /// Gets the tile size.
        /// </summary>
        public int TileSize { get; private set; }

        /// <summary>
        /// Gets the Tile texture size X.
        /// </summary>
        public float TileTexSizeX { get; set; }

        /// <summary>
        /// Gets the Tile texture size Y.
        /// </summary>
        public float TileTexSizeY { get; set; }

        /// <summary>
        /// Makes content accessible.
        /// </summary>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <returns>The handle for the tile.</returns>
        public ref uint this[int x, int y] => ref this.Tiles[x + (y * this.Width)];
    }
}
