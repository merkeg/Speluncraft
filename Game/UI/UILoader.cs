// <copyright file="UILoader.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine;
    using Engine.Renderer;
    using Engine.Renderer.Text;

    /// <summary>
    /// Loader Class to initialize the whole UI of the Game. (hopefully).
    /// </summary>
    internal class UILoader
    {
        /// <summary>
        /// Initializes the UI.
        /// </summary>
        /// /// <param name="player">player.</param>
        public static void Initialize_UI(Player.Player player)
        {
            HealthbarPlayer playerhealthbar = new HealthbarPlayer(player);
            Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            ItemShop itemShop = new ItemShop(TextureAtlas.Fonts["debugFont"]);
            Engine.AddRenderer(itemShop, RenderLayer.UI);
        }
    }
}
