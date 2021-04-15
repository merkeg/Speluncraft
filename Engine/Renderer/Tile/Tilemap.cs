using Engine.Renderer.Tile.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Renderer.Tile
{
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
        /// <returns>The specified Tilemap layer</returns>
        public ref TilemapLayer this[int layer] => ref this.Layers[layer];
    }
}
