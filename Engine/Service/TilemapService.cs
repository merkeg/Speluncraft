// <copyright file="TilemapService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using global::Engine.Component;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Renderer.Tile;
    using global::Engine.Renderer.Tile.Parser;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Tilemap service class.
    /// </summary>
    [ServiceInfo("renderer_tilemap", RenderLayer.GAME)]
    public partial class TilemapService : IService
    {
        private const uint BitFlippedHorizontal = 0x80000000;
        private const uint BitFlippedVertical = 0x40000000;
        private const uint BitFlippedDiagonal = 0x20000000;

        private Dictionary<Tilemap, Vector2i> tilemaps;
        private List<Tilesheet> tilesheets;

        private IRectangle optimizationPoint;
        private int renderRange;

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapService"/> class.
        /// </summary>
        public TilemapService()
        {
            this.tilemaps = new Dictionary<Tilemap, Vector2i>();
            this.tilesheets = new List<Tilesheet>();
            this.renderRange = 15;
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }

        /// <inheritdoc/>
        public void OnUpdate(float frameTime)
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
        }

        /// <inheritdoc/>
        public void SceneChangeCleanup()
        {
            this.tilemaps.Clear();
            this.tilesheets.Clear();
            this.optimizationPoint = null;
        }

        /// <summary>
        /// Add Tilemap to the renderer.
        /// </summary>
        /// <param name="tilemap">Tilemap.</param>
        /// <param name="offset">Offset.</param>
        public void AddTilemap(Tilemap tilemap, Vector2i offset)
        {
            this.tilemaps.Add(tilemap, offset);
            this.tilesheets.Add(tilemap.Tilesheet);
            Engine.BackgroundColor = ColorTranslator.FromHtml(tilemap.TilemapModel.backgroundcolor);
            foreach (TilemapTileLayer layer in tilemap.TileLayers)
            {
                if (layer.TilemapModel.properties == null)
                {
                    continue;
                }

                foreach (CustomPropertyModel prop in layer.TilemapModel.properties)
                {
                    if (prop.name.Contains("collision"))
                    {
                        if (((bool)prop.value) == true)
                        {
                            Engine.Colliders.AddRange(TilemapParser.GenerateCollisionMap(layer.TilemapModel, offset.X, offset.Y));
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the optimization point.
        /// </summary>
        /// <param name="rectangle">optimization point.</param>
        public void SetOptimizationPoint(IRectangle rectangle)
        {
            this.optimizationPoint = rectangle;
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}