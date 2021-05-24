// <copyright file="TextureLoader.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.Tile;

    /// <summary>
    /// Texture loader class.
    /// </summary>
    public class TextureLoader
    {
        /// <summary>
        /// Load textures.
        /// </summary>
        public static void LoadTextures()
        {
            // Fonts
            TextureAtlas.Set("defaultFont", new Font(FontModel.Parse("Game.Resources.Font.semicondensed.font.fnt"), new Sprite("Game.Resources.Font.semicondensed.font.png")));

            // Tilesheets
            TextureAtlas.Set("defaultTilesheet", new Tilesheet("Game.Resources.Sprite.tilesheetMC.png", 32, 32));
        }
    }
}