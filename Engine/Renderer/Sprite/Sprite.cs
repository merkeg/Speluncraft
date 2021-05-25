// <copyright file="Sprite.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Sprite
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// The Sprite class.
    /// </summary>
    public class Sprite : ISprite
    {
        private readonly Stream resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="resource">The resource to bind as sprite.</param>
        /// <param name="flip">Set if flipped.</param>
        public Sprite(string resource, bool flip = true)
        {
            Assembly asm = Assembly.GetEntryAssembly();

            this.resource = asm.GetManifestResourceStream(resource);
            this.Handle = GL.GenTexture();

            this.TexX0 = 0;
            this.TexX1 = 1;
            this.TexY0 = 0;
            this.TexY1 = 1;

            this.BuildSprite(flip);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="resource">The resource to bind as sprite.</param>
        /// <param name="flip">Set if flipped.</param>
        public Sprite(Stream resource, bool flip = true)
        {
            this.resource = resource;
            this.Handle = GL.GenTexture();

            this.TexX0 = 0;
            this.TexX1 = 1;
            this.TexY0 = 0;
            this.TexY1 = 1;

            this.BuildSprite(flip);
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

        /// <inheritdoc/>
        public float TexX0 { get; private set; }

        /// <inheritdoc/>
        public float TexX1 { get; private set; }

        /// <inheritdoc/>
        public float TexY0 { get; private set; }

        /// <inheritdoc/>
        public float TexY1 { get; private set; }

        /// <inheritdoc/>
        public Color4? Color { get; set; }

        /// <inheritdoc/>
        public void TimeElapsed(float time)
        {
        }

        private void BuildSprite(bool flip)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(this.resource);
            if (flip)
            {
                image.Mutate(x => x.Flip(FlipMode.Vertical));
            }

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
