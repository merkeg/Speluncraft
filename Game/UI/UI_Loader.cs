using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Engine.Renderer;

namespace Game.UI
{
    /// <summary>
    /// Loader Class to initialize the whole UI of the Game. (hopefully).
    /// </summary>
    internal class UI_Loader
    {
        /// <summary>
        /// Initializes the UI.
        /// </summary>
        /// <param name="engine">reference to engine for adding the renderes.</param>
        public static void Initialize_UI(Engine.Engine engine)
        {
            HealthbarPlayer playerhealthbar = new HealthbarPlayer();
            engine.AddRenderer(playerhealthbar, RenderLayer.UI);
        }
    }
}
