// <copyright file="TilemapService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System.Collections.Generic;
    using System.Drawing;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapService"/> class.
        /// </summary>
        public TilemapService()
        {
            this.tilemaps = new Dictionary<Tilemap, Vector2i>();
            this.tilesheets = new List<Tilesheet>();
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.Color3(Color.White);
            foreach (Tilemap tilemap in this.tilemaps.Keys)
            {
                foreach (TilemapLayer layer in tilemap.Layers)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        for (int y = 0; y < layer.Height; y++)
                        {
                            uint tile = layer[x, y];
                            if (tile == 0)
                            {
                                continue;
                            }

                            // https://doc.mapeditor.org/en/stable/reference/tmx-map-format/#tile-flipping
                            bool flipped_horizontal = (tile & BitFlippedHorizontal) > 0;
                            bool flipped_vertical = (tile & BitFlippedVertical) > 0;
                            bool flipped_diagonal = (tile & BitFlippedDiagonal) > 0;

                            tile &= ~(BitFlippedHorizontal | BitFlippedVertical | BitFlippedDiagonal);
                            tile--;
                            ISprite sprite = tilemap.Tilesheet.Tiles[tile];

                            float tileTexCoordX0 = sprite.TexX0;
                            float tileTexCoordY0 = sprite.TexY0;
                            float tileTexCoordX1 = sprite.TexX1;
                            float tileTexCoordY1 = sprite.TexY1;

                            if (flipped_horizontal)
                            {
                                tileTexCoordY0 += layer.TileTexSizeY;
                                tileTexCoordY1 -= layer.TileTexSizeY;
                            }

                            if (flipped_vertical)
                            {
                                tileTexCoordX0 += layer.TileTexSizeX;
                                tileTexCoordX1 -= layer.TileTexSizeX;
                            }

                            Vector2 offset = this.tilemaps[tilemap];
                            GL.BindTexture(TextureTarget.Texture2D, sprite.Handle);
                            float overlap = 0.0001f;

                            GL.Begin(PrimitiveType.Triangles);
                            if (!flipped_diagonal)
                            {
                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y + 1 + overlap);
                            }
                            else
                            {
                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                                GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

                                GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                                GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y + 1 + overlap);
                            }

                            GL.End();
                        }
                    }
                }
            }

            foreach (Tilesheet sheet in this.tilesheets)
            {
                foreach (ISprite sprite in sheet.Tiles)
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
    }
}