// <copyright file="Tileset.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile
{
    using System.IO;
    using System.Reflection;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common.Input;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using Image = SixLabors.ImageSharp.Image;
    using System;
    /// <summary>
    /// The Tileset class.
    /// </summary>
    public class Tileset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tileset"/> class.
        /// </summary>
        /// <param name="resource">The resource location.</param>
        public Tileset(Stream resource)
        {
            // Source: https://github.com/davudk/OpenGL-TileMap-Demos
            using Image<Rgba32> image = Image.Load<Rgba32>(resource);

            byte[] data = new byte[image.Width * image.Height * 4];
            var i = 0;
            for (var y = 0; y < image.Height; y++)
            {
                foreach (var p in image.GetPixelRowSpan(y))
                {
                    data[i++] = p.R;
                    data[i++] = p.G;
                    data[i++] = p.B;
                    data[i++] = p.A;
                }
            }

            int handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            this.Handle = handle;
        }

        /// <summary>
        /// Gets the handle id for the specifit Tileset.
        /// </summary>
        public int Handle { get; private set; }
    }
}
