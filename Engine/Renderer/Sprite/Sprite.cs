// <copyright file="Sprite.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using OpenTK.Graphics.OpenGL;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The Sprite class.
    /// </summary>
    public class Sprite
    {
        private Stream resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="resource">The resource to bind as sprite.</param>
        public Sprite(Stream resource)
        {
            this.resource = resource;
            this.Handle = GL.GenTexture();

            this.BuildSprite();
        }

        /// <summary>
        /// Gets the OpenGL texture handle.
        /// </summary>
        public int Handle { get; private set; }

        /// <summary>
        /// Gets the sprite width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the sprite height.
        /// </summary>
        public int Height { get; private set; }

        private void BuildSprite()
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(this.resource);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            byte[] data = new byte[image.Width * image.Height * 4];

            this.Width = image.Width;
            this.Height = image.Height;

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

            GL.BindTexture(TextureTarget.Texture2D, this.Handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        }
    }
}
