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
        private Dictionary<Tilemap, List<GameObject>> gameObjects;
        private List<Tilesheet> tilesheets;

        private IRectangle optimizationPoint;
        private int renderRange;

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapService"/> class.
        /// </summary>
        public TilemapService()
        {
            this.tilemaps = new Dictionary<Tilemap, Vector2i>();
            this.gameObjects = new Dictionary<Tilemap, List<GameObject>>();
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

        /// <summary>
        /// Add Tilemap to the renderer.
        /// </summary>
        /// <param name="tilemap">Tilemap.</param>
        /// <param name="offset">Offset.</param>
        public void AddTilemap(Tilemap tilemap, Vector2i offset)
        {
            this.tilemaps.Add(tilemap, offset);
            this.tilesheets.Add(tilemap.Tilesheet);
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

            List<GameObject> damageObjects = new List<GameObject>();
            tilemap.ObjectLayers.ForEach(objectLayer =>
            {
                objectLayer.FindObjectsByName("damage").ForEach(obj =>
                {
                    CustomPropertyModel prop = obj.GetProperty("damage");
                    if (prop == null)
                    {
                        return;
                    }

                    GameObject gO = new GameObject(obj.X, offset.Y - obj.Y - obj.Height + 1, obj.Width, obj.Height, null);
                    gO.AddComponent(new DoDamageCollisionResponse((int)(long)prop.value, 1));
                    damageObjects.Add(gO);
                    Engine.AddGameObject(gO);
                });
            });
            this.gameObjects.Add(tilemap, damageObjects);
        }

        /// <summary>
        /// Sets the optimization point.
        /// </summary>
        /// <param name="rectangle">optimization point.</param>
        public void SetOptimizationPoint(IRectangle rectangle)
        {
            this.optimizationPoint = rectangle;
        }
    }
}