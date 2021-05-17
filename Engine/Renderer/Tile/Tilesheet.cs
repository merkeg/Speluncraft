// <copyright file="Tilesheet.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using global::Engine.Renderer.Sprite;
    using OpenTK.Graphics.OpenGL;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using Image = SixLabors.ImageSharp.Image;

    /// <summary>
    /// The Tileset class.
    /// </summary>
    public class Tilesheet : ITilesheet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tilesheet"/> class.
        /// </summary>
        /// <param name="resource">The resource location.</param>
        /// <param name="tileSizeX">The X size of tiles in pixels.</param>
        /// <param name="tileSizeY">The Y size of tiles in pixels.</param>
        public Tilesheet(string resource, int tileSizeX, int tileSizeY)
        {
            Assembly asm = Assembly.GetEntryAssembly();
            this.Init(asm.GetManifestResourceStream(resource), tileSizeX, tileSizeY);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tilesheet"/> class.
        /// </summary>
        /// <param name="resource">The resource location.</param>
        /// <param name="tileSizeX">The X size of tiles in pixels.</param>
        /// <param name="tileSizeY">The Y size of tiles in pixels.</param>
        [Obsolete]
        public Tilesheet(Stream resource, int tileSizeX, int tileSizeY)
        {
            this.Init(resource, tileSizeX, tileSizeY);
        }

        /// <inheritdoc/>
        public int Handle { get; private set; }

        /// <inheritdoc/>
        public int AmountTileHeight { get; private set; }

        /// <inheritdoc/>
        public int AmountTileWidth { get; private set; }

        /// <inheritdoc/>
        public int TileSizeX { get; private set; }

        /// <inheritdoc/>
        public int TileSizeY { get; private set; }

        /// <summary>
        /// Gets the Tiles with tex coordinates.
        /// </summary>
        public ISprite[] Tiles { get; private set; }

        /// <summary>
        /// Gets or sets custom sprites.
        /// </summary>
        public List<ISprite> CustomSprites { get; set; }

        /// <inheritdoc/>
        public float TileTexSizeX => 1f / this.AmountTileWidth;

        /// <inheritdoc/>
        public float TileTexSizeY => 1f / this.AmountTileHeight;

        /// <summary>
        /// Sets a custom Sprite to an Sprite id.
        /// </summary>
        /// <param name="tileId">sprite id.</param>
        /// <param name="sprite">Sprite to replace with.</param>
        public void SetCustomSprite(int tileId, ISprite sprite)
        {
            this.Tiles[tileId] = sprite;
            this.CustomSprites.Add(sprite);
        }

        private void Init(Stream resource, int tileSizeX, int tileSizeY)
        {
            this.CustomSprites = new List<ISprite>();

            // Source: https://github.com/davudk/OpenGL-TileMap-Demos
            using Image<Rgba32> image = Image.Load<Rgba32>(resource);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            byte[] data = new byte[image.Width * image.Height * 4];

            this.AmountTileWidth = image.Width / tileSizeX;
            this.AmountTileHeight = image.Height / tileSizeY;
            this.TileSizeX = tileSizeX;
            this.TileSizeY = tileSizeY;

            var i = 0;
            for (int yD = this.AmountTileHeight - 1; yD >= 0; yD--)
            {
                for (int y = yD * tileSizeY; y < (yD * tileSizeY) + tileSizeY; y++)
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
            ISprite[] tiles = new ISprite[this.AmountTileWidth * this.AmountTileHeight];
            for (int tileId = 0; tileId < this.AmountTileWidth * this.AmountTileHeight; tileId++)
            {
                float tileTexCoordX0 = (tileId % this.AmountTileWidth) * this.TileTexSizeX;
                float tileTexCoordY0 = (tileId / this.AmountTileWidth) * this.TileTexSizeY;
                float tileTexCoordX1 = tileTexCoordX0 + this.TileTexSizeX;
                float tileTexCoordY1 = tileTexCoordY0 + this.TileTexSizeY;
                tiles[tileId] = new TileSprite(this.Handle, this.TileSizeX, this.TileSizeY, tileTexCoordX0, tileTexCoordY0, tileTexCoordX1, tileTexCoordY1);
            }

            this.Tiles = tiles;
        }
    }
}
