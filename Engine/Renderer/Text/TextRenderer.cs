// <copyright file="TextRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using global::Engine.Renderer.Text.Parser;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The text renderer class.
    /// </summary>
    public class TextRenderer : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextRenderer"/> class.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="font">The font to render it with.</param>
        /// <param name="color">The color to render it with.</param>
        /// <param name="position">Position of the text.</param>
        /// <param name="fontScale">Scale of the text.</param>
        public TextRenderer(string text, Font font, Color4 color, GameObject.Rectangle position, float fontScale = 1.0f)
        {
            this.Text = text;
            this.Font = font;
            this.Color = color;
            this.Position = position;
            this.FontScale = fontScale;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color4 Color { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public GameObject.Rectangle Position { get; set; }

        /// <summary>
        /// Gets or sets the font scale.
        /// </summary>
        public float FontScale { get; set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            double x = this.Position.MinX;
            double y = this.Position.MinY;

            GL.BindTexture(TextureTarget.Texture2D, this.Font.FontSheet.Handle);
            GL.Color4(this.Color);
            GL.Begin(PrimitiveType.Quads);

            foreach (char character in this.Text.ToCharArray())
            {
                FontModel.FontModelCharacter data = this.Font.Model.Characters[character];
                float calc = 1.0f / this.Font.FontSheet.Width;

                float tex_X0 = calc * data.X;
                float tex_X1 = tex_X0 + (calc * data.Width);

                float tex_Y0 = 1 - (calc * data.Y);
                float tex_Y1 = tex_Y0 - (calc * data.Height);

                double x0 = x + (data.XOffset * this.FontScale);
                double x1 = x0 + (data.Width * this.FontScale);

                double y0 = y + (data.YOffset * this.FontScale);
                double y1 = y0 + (data.Height * this.FontScale);

                GL.TexCoord2(tex_X0, tex_Y0);
                GL.Vertex2(x0, y0);

                GL.TexCoord2(tex_X1, tex_Y0);
                GL.Vertex2(x1, y0);

                GL.TexCoord2(tex_X1, tex_Y1);
                GL.Vertex2(x1, y1);

                GL.TexCoord2(tex_X0, tex_Y1);
                GL.Vertex2(x0, y1);
                x += data.XAdvance * this.FontScale;
            }

            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }
    }
}
