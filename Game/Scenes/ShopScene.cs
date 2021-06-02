// <copyright file="ShopScene.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using Engine.Renderer;
    using Engine.Scene;
    using Game.UI;

    /// <summary>
    /// Start shop scene.
    /// </summary>
    public class ShopScene : Scene
    {
        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            ItemShop itemShop = new ItemShop(TextureAtlas.Fonts["defaultFont"]);
            Engine.Engine.AddRenderer(itemShop, RenderLayer.UI);
        }
    }
}