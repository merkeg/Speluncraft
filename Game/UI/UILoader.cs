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

    /// <summary>
    /// Loader Class to initialize the whole UI of the Game. (hopefully).
    /// </summary>
    internal class UILoader
    {
        /// <summary>
        /// Initializes the UI.
        /// </summary>
        public static void Initialize_UI()
        {
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            Engine.AddRenderer(playerhealthbar, RenderLayer.UI);

            ItemShop itemShop = new ItemShop();
            Engine.AddRenderer(itemShop, RenderLayer.UI);
        }
    }
}
