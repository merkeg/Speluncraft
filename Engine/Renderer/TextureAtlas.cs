// <copyright file="TextureAtlas.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using System.Collections.Generic;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Renderer.Text;
    using global::Engine.Renderer.Tile;

    /// <summary>
    /// Texture atlas class.
    /// </summary>
    public class TextureAtlas
    {
        static TextureAtlas()
        {
            Fonts = new Dictionary<string, Font>();
            Sprites = new Dictionary<string, ISprite>();
            Tilesheets = new Dictionary<string, Tilesheet>();
        }

        /// <summary>
        /// Gets the fonts.
        /// </summary>
        public static Dictionary<string, Font> Fonts { get; private set; }

        /// <summary>
        /// Gets the Sprites.
        /// </summary>
        public static Dictionary<string, ISprite> Sprites { get; private set; }

        /// <summary>
        /// Gets the Tilesheets.
        /// </summary>
        public static Dictionary<string, Tilesheet> Tilesheets { get; private set; }

        /// <summary>
        /// Add a new Texture.
        /// </summary>
        /// <param name="name">Name of the texture.</param>
        /// <param name="value">Value of the Texture.</param>
        public static void Set(string name, Font value)
        {
            Fonts.Add(name, value);
        }

        /// <summary>
        /// Add a new Texture.
        /// </summary>
        /// <param name="name">Name of the texture.</param>
        /// <param name="value">Value of the Texture.</param>
        public static void Set(string name, ISprite value)
        {
            Sprites.Add(name, value);
        }

        /// <summary>
        /// Add a new Texture.
        /// </summary>
        /// <param name="name">Name of the texture.</param>
        /// <param name="value">Value of the Texture.</param>
        public static void Set(string name, Tilesheet value)
        {
            Tilesheets.Add(name, value);
        }
    }
}