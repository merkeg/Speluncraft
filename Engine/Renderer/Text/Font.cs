// <copyright file="Font.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Text
{
    using global::Engine.Renderer.Text.Parser;

    /// <summary>
    /// Font class.
    /// </summary>
    public class Font
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="model">Font model.</param>
        /// <param name="fontSheet">Spritesheet with handle of the font.</param>
        public Font(FontModel model, Sprite.Sprite fontSheet)
        {
            this.Model = model;
            this.FontSheet = fontSheet;
        }

        /// <summary>
        /// Gets the font model.
        /// </summary>
        public FontModel Model { get; private set; }

        /// <summary>
        /// Gets the font sheet.
        /// </summary>
        public Sprite.Sprite FontSheet { get; private set; }
    }
}