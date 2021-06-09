// <copyright file="TilemapLayerObject.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System;
    using System.Linq;
    using global::Engine.Renderer.Tile.Parser;

    /// <summary>
    /// Tilemap layer object class.
    /// </summary>
    public class TilemapLayerObject
    {
        private TilemapLayerObjectModel model;
        private Tilesheet tilesheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapLayerObject"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="sheet">Tilesheet.</param>
        public TilemapLayerObject(TilemapLayerObjectModel model, Tilesheet sheet)
        {
            this.model = model;
            this.tilesheet = sheet;
        }

        /// <summary>
        /// Gets the X.
        /// </summary>
        public float X => this.model.x / (float)this.tilesheet.TileSizeX;

        /// <summary>
        /// Gets the y.
        /// </summary>
        public float Y => this.model.y / (float)this.tilesheet.TileSizeY;

        /// <summary>
        /// Gets the width.
        /// </summary>
        public float Width => this.model.width / (float)this.tilesheet.TileSizeX;

        /// <summary>
        /// Gets the height.
        /// </summary>
        public float Height => this.model.height / (float)this.tilesheet.TileSizeY;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.model.name;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public CustomPropertyModel[] Properties => this.model.properties;

        /// <summary>
        /// Gets the type.
        /// </summary>
        public string Type => this.model.type;

        /// <summary>
        /// Gets a property, returns null if not found.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <returns>Property or null.</returns>
        public CustomPropertyModel GetProperty(string name)
        {
            try
            {
                return this.Properties.ToList().Find(prop => prop.name.Equals(name));
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}