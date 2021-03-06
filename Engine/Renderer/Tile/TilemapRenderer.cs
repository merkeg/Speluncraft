// <copyright file="TilemapRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System;
    using System.Drawing;
    using global::Engine.Renderer.Tile;
    using global::Engine.Renderer.Tile.Parser;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Tilemap renderer.
    /// </summary>
    [Obsolete]
    public class TilemapRenderer : IRenderer
    {
        private const uint BitFlippedHorizontal = 0x80000000;
        private const uint BitFlippedVertical = 0x40000000;
        private const uint BitFlippedDiagonal = 0x20000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapRenderer"/> class.
        /// </summary>
        /// <param name="tilemap">The tilemap the renderer renders.</param>
        /// <param name="originX">X origin position.</param>
        /// <param name="originY">Y origin position.</param>
        public TilemapRenderer(Tilemap tilemap, int originX, int originY)
        {
            this.Tilemap = tilemap;
            this.OriginX = originX;
            this.OriginY = originY;
        }

        /// <summary>
        /// Gets the tilemap object.
        /// </summary>
        public Tilemap Tilemap { get; private set; }

        /// <summary>
        /// Gets the originx.
        /// </summary>
        public int OriginX { get; private set; }

        /// <summary>
        /// Gets the origin y.
        /// </summary>
        public int OriginY { get; private set; }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            foreach (TilemapTileLayer layer in this.Tilemap.TileLayers)
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
                            Engine.Colliders.AddRange(TilemapParser.GenerateCollisionMap(layer.TilemapModel, this.OriginX, this.OriginY));
                        }

                        break;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, this.Tilemap.Tilesheet.Handle);
            GL.Color3(Color.White);
            foreach (TilemapTileLayer tilemap in this.Tilemap.TileLayers)
            {
                for (int x = 0; x < tilemap.Width; x++)
                {
                    for (int y = 0; y < tilemap.Height; y++)
                    {
                        uint tile = tilemap[x, y];
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

                        float tileTexCoordX0 = (tile % this.Tilemap.Tilesheet.AmountTileWidth) * tilemap.TileTexSizeX;
                        float tileTexCoordY0 = tile / this.Tilemap.Tilesheet.AmountTileWidth * tilemap.TileTexSizeY;
                        float tileTexCoordX1 = tileTexCoordX0 + tilemap.TileTexSizeX;
                        float tileTexCoordY1 = tileTexCoordY0 + tilemap.TileTexSizeY;

                        if (flipped_horizontal)
                        {
                            tileTexCoordY0 += tilemap.TileTexSizeY;
                            tileTexCoordY1 -= tilemap.TileTexSizeY;
                        }

                        if (flipped_vertical)
                        {
                            tileTexCoordX0 += tilemap.TileTexSizeX;
                            tileTexCoordX1 -= tilemap.TileTexSizeX;
                        }

                        GL.Begin(PrimitiveType.Triangles);
                        if (!flipped_diagonal)
                        {
                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y + 1 + 0.0001);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y + 1 + 0.0001);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y + 1 + 0.0001);
                        }
                        else
                        {
                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y + 1 + 0.0001);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y - 0.0001);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(this.OriginX + x - 0.0001, this.OriginY - y + 1 + 0.0001);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                            GL.Vertex2(this.OriginX + x + 1 + 0.0001, this.OriginY - y + 1 + 0.0001);
                        }

                        GL.End();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            return;
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}
