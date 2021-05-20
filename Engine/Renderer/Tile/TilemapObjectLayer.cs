// <copyright file="TilemapObjectLayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System.Collections.Generic;
    using global::Engine.Renderer.Tile.Parser;

    /// <summary>
    /// Tilemap object layer class.
    /// </summary>
    public class TilemapObjectLayer
    {
        private TilemapLayerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapObjectLayer"/> class.
        /// </summary>
        /// <param name="tilesheet">The tilesheet.</param>
        /// <param name="model">The model.</param>
        public TilemapObjectLayer(Tilesheet tilesheet, TilemapLayerModel model)
        {
            this.model = model;

            this.Objects = new List<TilemapLayerObject>();
            foreach (TilemapLayerObjectModel info in model.objects)
            {
                this.Objects.Add(new TilemapLayerObject(info, tilesheet));
            }
        }

        /// <summary>
        /// Gets layer Objects.
        /// </summary>
        public List<TilemapLayerObject> Objects { get; }

        /// <summary>
        /// Find Object by name.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <returns>Object or null.</returns>
        public TilemapLayerObject FindObjectByName(string name)
        {
            return this.Objects.Find(obj => obj.Name.Equals(name));
        }

        /// <summary>
        /// Find Objects by name.
        /// </summary>
        /// <param name="name">Name of the objects.</param>
        /// <returns>Objects or null.</returns>
        public List<TilemapLayerObject> FindObjectsByName(string name)
        {
            return this.Objects.FindAll(obj => obj.Name.Equals(name));
        }
    }
}