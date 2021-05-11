// <copyright file="TilemapService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
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
    public class TilemapService : IService
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
        public void Render(FrameEventArgs args)
        {
            GL.Color3(Color.White);
            float tileTexCoordX0;
            float tileTexCoordY0;
            float tileTexCoordX1;
            float tileTexCoordY1;

            bool flippedHorizontal;
            bool flippedVertical;
            bool flippedDiagonal;

            int tilemapHandle;

            foreach (Tilemap tilemap in this.tilemaps.Keys)
            {
                tilemapHandle = tilemap.Tilesheet.Handle;
                GL.BindTexture(TextureTarget.Texture2D, tilemapHandle);

                foreach (TilemapLayer layer in tilemap.Layers)
                {
                    Vector2 offset = this.tilemaps[tilemap];
                    for (int x = 0; x < layer.Width; x++)
                    {
                        if (Math.Abs(this.optimizationPoint.MinX - offset.X - x) > this.renderRange)
                        {
                            continue;
                        }

                        for (int y = 0; y < layer.Height; y++)
                        {
                            if (Math.Abs(this.optimizationPoint.MinY - (offset.Y - y)) > this.renderRange)
                            {
                                continue;
                            }

                            uint tile = layer[x, y];
                            if (tile == 0)
                            {
                                continue;
                            }

                            // https://doc.mapeditor.org/en/stable/reference/tmx-map-format/#tile-flipping
                            flippedHorizontal = (tile & BitFlippedHorizontal) > 0;
                            flippedVertical = (tile & BitFlippedVertical) > 0;
                            flippedDiagonal = (tile & BitFlippedDiagonal) > 0;

                            tile &= ~(BitFlippedHorizontal | BitFlippedVertical | BitFlippedDiagonal);
                            tile--;
                            ISprite sprite = tilemap.Tilesheet.Tiles[tile];

                            tileTexCoordX0 = sprite.TexX0;
                            tileTexCoordY0 = sprite.TexY0;
                            tileTexCoordX1 = sprite.TexX1;
                            tileTexCoordY1 = sprite.TexY1;

                            if (flippedHorizontal)
                            {
                                tileTexCoordY0 += layer.TileTexSizeY;
                                tileTexCoordY1 -= layer.TileTexSizeY;
                            }

                            if (flippedVertical)
                            {
                                tileTexCoordX0 += layer.TileTexSizeX;
                                tileTexCoordX1 -= layer.TileTexSizeX;
                            }

                            if (sprite.Handle != tilemapHandle)
                            {
                                GL.BindTexture(TextureTarget.Texture2D, sprite.Handle);
                            }

                            float overlap = 0.0001f;

                            GL.Begin(PrimitiveType.Quads);
                            if (!flippedDiagonal)
                            {
                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);
                            }
                            else
                            {
                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);
                            }

                            GL.End();
                            if (sprite.Handle != tilemapHandle)
                            {
                                GL.BindTexture(TextureTarget.Texture2D, tilemap.Tilesheet.Handle);
                            }
                        }
                    }
                }
            }

            foreach (Tilesheet sheet in this.tilesheets)
            {
                foreach (ISprite sprite in sheet.CustomSprites)
                {
                    sprite.TimeElapsed((float)args.Time);
                }
            }
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
            foreach (TilemapLayer layer in tilemap.Layers)
            {
                if (layer.TilemapModel.properties == null)
                {
                    continue;
                }

                foreach (TilemapLayerPropertiesModel prop in layer.TilemapModel.properties)
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
    }
}