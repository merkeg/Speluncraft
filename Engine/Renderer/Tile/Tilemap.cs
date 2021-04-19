﻿// <copyright file="Tilemap.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
#pragma warning disable SA1135 // Using directives should be qualified
    using Tile.Parser;
#pragma warning restore SA1135 // Using directives should be qualified

    /// <summary>
    /// The Tilemap class.
    /// </summary>
    public class Tilemap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tilemap"/> class.
        /// </summary>
        /// <param name="tileset">The tileset the tilemap will use.</param>
        /// <param name="model">The model which the tilemap will be built.</param>
        public Tilemap(Tileset tileset, TilemapModel model)
        {
            this.Layers = new TilemapLayer[model.layers.Count];
            this.Tileset = tileset;
            foreach (TilemapLayerModel layer in model.layers)
            {
                this.Layers[layer.id - 1] = new TilemapLayer(tileset, layer);
            }
        }

        /// <summary>
        /// Gets the Layers.
        /// </summary>
        public TilemapLayer[] Layers { get; private set; }

        /// <summary>
        /// Gets the Tileset the tilemap is using.
        /// </summary>
        public Tileset Tileset { get; private set; }

        /// <summary>
        /// Reference to this.
        /// </summary>
        /// <param name="layer">The layer to access to.</param>
        /// <returns>The specified Tilemap layer.</returns>
        public ref TilemapLayer this[int layer] => ref this.Layers[layer];
    }
}