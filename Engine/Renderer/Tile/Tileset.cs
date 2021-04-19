// <copyright file="Tileset.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System.IO;
    using OpenTK.Graphics.OpenGL;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using Image = SixLabors.ImageSharp.Image;

    /// <summary>
    /// The Tileset class.
    /// </summary>
    public class Tileset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        /// <param name="resource">The resource location.</param>
        /// <param name="tileSize">The size of tiles in pixels.</param>
        public Tileset(Stream resource, int tileSize)
        {
            // Source: https://github.com/davudk/OpenGL-TileMap-Demos
            using Image<Rgba32> image = Image.Load<Rgba32>(resource);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            byte[] data = new byte[image.Width * image.Height * 4];

            this.AmountTileWidth = image.Width / tileSize;
            this.AmountTileHeight = image.Height / tileSize;
            this.TileSize = tileSize;

            var i = 0;
            for (int yD = this.AmountTileHeight - 1; yD >= 0; yD--)
            {
                for (int y = yD * tileSize; y < (yD * tileSize) + tileSize; y++)
                {
                    foreach (var p in image.GetPixelRowSpan(y))
                    {
                        data[i++] = p.R;
                        data[i++] = p.G;
                        data[i++] = p.B;
                        data[i++] = p.A;
                    }
                }
            }

            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            this.Handle = handle;
        }

        /// <summary>
        /// Gets the handle id for the specifit Tileset.
        /// </summary>
        public int Handle { get; private set; }

        /// <summary>
        /// Gets the amount in height.
        /// </summary>
        public int AmountTileHeight { get; private set; }

        /// <summary>
        /// Gets the amount in width.
        /// </summary>
        public int AmountTileWidth { get; private set; }

        /// <summary>
        /// Gets the size of the tile in pixels.
        /// </summary>
        public int TileSize { get; private set; }
    }
}
