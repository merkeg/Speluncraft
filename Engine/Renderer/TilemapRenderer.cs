// <copyright file="TilemapRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using global::Engine.Renderer.Tile;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Tilemap renderer.
    /// </summary>
    public class TilemapRenderer : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapRenderer"/> class.
        /// </summary>
        /// <param name="tilemap">The tilemap the renderer renders.</param>
        public TilemapRenderer(Tilemap tilemap)
        {
            this.Tilemap = tilemap;
        }

        /// <summary>
        /// Gets the tilemap object.
        /// </summary>
        public Tilemap Tilemap { get; private set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.BindTexture(TextureTarget.Texture2D, this.Tilemap.Tileset.Handle);

            foreach (TilemapLayer tilemap in this.Tilemap.Layers)
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
                        bool flipped_horizontal = (tile & 0x80000000) > 0;
                        bool flipped_vertical = (tile & 0x40000000) > 0;
                        bool flipped_diagonal = (tile & 0x20000000) > 0;

                        tile &= ~(0x80000000 | 0x40000000 | 0x20000000);
                        tile--;

                        float tileTexCoordX0 = (tile % this.Tilemap.Tileset.AmountTileWidth) * tilemap.TileTexSizeX;
                        float tileTexCoordY0 = (tile / this.Tilemap.Tileset.AmountTileWidth) * tilemap.TileTexSizeY;
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
                            GL.Vertex2(x, y);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(x + 1, y);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(x, y + 1);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(x + 1, y);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(x, y + 1);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                            GL.Vertex2(x + 1, y + 1);
                        }
                        else
                        {
                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY0);
                            GL.Vertex2(x, y);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(x, y + 1);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(x + 1, y);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY0);
                            GL.Vertex2(x, y + 1);

                            GL.TexCoord2(tileTexCoordX0, tileTexCoordY1);
                            GL.Vertex2(x + 1, y);

                            GL.TexCoord2(tileTexCoordX1, tileTexCoordY1);
                            GL.Vertex2(x + 1, y + 1);
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
    }
}
