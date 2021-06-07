// <copyright file="TilemapServiceRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Drawing;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Renderer.Tile;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Tilemap service class.
    /// </summary>
    public partial class TilemapService
    {
        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.Color3(Color.White);

            Vector2[,] texCoords = new Vector2[2, 2];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    texCoords[x, y] = new Vector2(0, 0);
                }
            }

            bool flippedHorizontal;
            bool flippedVertical;
            bool flippedDiagonal;

            int tilemapHandle;

            foreach (Tilemap tilemap in this.tilemaps.Keys)
            {
                tilemapHandle = tilemap.Tilesheet.Handle;
                GL.BindTexture(TextureTarget.Texture2D, tilemapHandle);

                foreach (TilemapTileLayer layer in tilemap.TileLayers)
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
                            GL.Color4(sprite.Color ?? Color4.White);

                            texCoords[0, 0].X = sprite.TexX0;
                            texCoords[0, 0].Y = sprite.TexY0;

                            texCoords[1, 0].X = sprite.TexX1;
                            texCoords[1, 0].Y = sprite.TexY0;

                            texCoords[1, 1].X = sprite.TexX1;
                            texCoords[1, 1].Y = sprite.TexY1;

                            texCoords[0, 1].X = sprite.TexX0;
                            texCoords[0, 1].Y = sprite.TexY1;

                            if (flippedHorizontal)
                            {
                                texCoords[0, 0].X += layer.TileTexSizeX;
                                texCoords[0, 1].X += layer.TileTexSizeX;

                                texCoords[1, 0].X -= layer.TileTexSizeX;
                                texCoords[1, 1].X -= layer.TileTexSizeX;
                            }

                            if (flippedVertical)
                            {
                                texCoords[0, 0].Y += layer.TileTexSizeY;
                                texCoords[1, 0].Y += layer.TileTexSizeY;

                                texCoords[0, 1].Y -= layer.TileTexSizeY;
                                texCoords[1, 1].Y -= layer.TileTexSizeY;
                            }

                            if (flippedDiagonal)
                            {
                                Vector2 vec10 = texCoords[1, 0];
                                texCoords[1, 0] = texCoords[0, 1];
                                texCoords[0, 1] = vec10;
                            }

                            if (sprite.Handle != tilemapHandle)
                            {
                                GL.BindTexture(TextureTarget.Texture2D, sprite.Handle);
                            }

                            float overlap = 0.0001f;

                            GL.Begin(PrimitiveType.Quads);

                            GL.TexCoord2(texCoords[0, 0]);
                            GL.Vertex2(offset.X + x - overlap, offset.Y - y - overlap);

                            GL.TexCoord2(texCoords[1, 0]);
                            GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y - overlap);

                            GL.TexCoord2(texCoords[1, 1]);
                            GL.Vertex2(offset.X + x + 1 + overlap, offset.Y - y + 1 + overlap);

                            GL.TexCoord2(texCoords[0, 1]);
                            GL.Vertex2(offset.X + x - overlap, offset.Y - y + 1 + overlap);

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
    }
}