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
        private readonly string text;
        private readonly Font font;
        private readonly Color4 color;
        private readonly Vector2d position;
        private readonly float fontScale;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRenderer"/> class.
        /// </summary>
        /// <param name="text">The text to show.</param>
        /// <param name="font">The font to render it with.</param>
        /// <param name="color">The color to render it with.</param>
        /// <param name="position">Position of the text.</param>
        public TextRenderer(string text, Font font, Color4 color, Vector2d position, float fontScale = 1.0f)
        {
            this.text = text;
            this.font = font;
            this.color = color;
            this.position = position;
            this.fontScale = fontScale;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            double x = this.position.X;
            double y = this.position.Y;

            GL.BindTexture(TextureTarget.Texture2D, this.font.FontSheet.Handle);
            GL.Color4(this.color);
            Vector2i windowSize = Engine.Instance().GameWindow.Size;
            GL.LoadIdentity();
            GL.Ortho(0f, windowSize.X, windowSize.Y, 0f, -1f, 1f);
            GL.Begin(PrimitiveType.Quads);

            foreach (char character in this.text.ToCharArray())
            {
                FontModel.FontModelCharacter data = this.font.Model.Characters[character];
                float calc = 1.0f / this.font.FontSheet.Width;

                float tex_X0 = calc * data.X;
                float tex_X1 = tex_X0 + (calc * data.Width);

                float tex_Y0 = 1 - (calc * data.Y);
                float tex_Y1 = tex_Y0 - (calc * data.Height);

                double x0 = x + (data.XOffset * this.fontScale);
                double x1 = x0 + (data.Width * this.fontScale);

                double y0 = y + (data.YOffset * this.fontScale);
                double y1 = y0 + (data.Height * this.fontScale);

                GL.TexCoord2(tex_X0, tex_Y0);
                GL.Vertex2(x0, y0);

                GL.TexCoord2(tex_X1, tex_Y0);
                GL.Vertex2(x1, y0);

                GL.TexCoord2(tex_X1, tex_Y1);
                GL.Vertex2(x1, y1);

                GL.TexCoord2(tex_X0, tex_Y1);
                GL.Vertex2(x0, y1);
                x += data.XAdvance * this.fontScale;
            }

            GL.End();
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnCreate()
        {
        }
    }
}
