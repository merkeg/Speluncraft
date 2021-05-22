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
        /// <param name="font">usable font.</param>
        public static void Initialize_UI(Font font)
        {
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            ItemShop itemShop = new ItemShop(font);
            Engine.AddRenderer(itemShop, RenderLayer.UI);
        }
    }
}
