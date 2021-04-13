// <copyright file="TilemapRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using global::Engine.Renderer.Tile;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The Tilemap renderer.
    /// </summary>
    public class TilemapRenderer : IRenderer
    {
        public TilemapRenderer(Tilemap tilemap)
        {
            this.Tilemap = tilemap;
        }

        public Tilemap Tilemap { get; private set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {

            GL.BindTexture(TextureTarget.Texture2D, this.Tilemap.Tileset.Handle);
            GL.Begin(PrimitiveType.Triangles);

            for (int x = 0; x < this.Tilemap.Width; x++)
            {
                for (int y = 0; y < this.Tilemap.Height; y++)
                {

                    uint tile = this.Tilemap[x, y];
                    tile--;
                    float tileTexCoordX0 = (tile % this.Tilemap.Width) * this.Tilemap.TileTexSizeX;
                    float tileTexCoordY0 = (tile / this.Tilemap.Width) * this.Tilemap.TileTexSizeY;
                    float tileTexCoordX1 = tileTexCoordX0 + this.Tilemap.TileTexSizeX;
                    float tileTexCoordY1 = tileTexCoordY0 + this.Tilemap.TileTexSizeY;
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
            }

            GL.End();
        }
    }
}
