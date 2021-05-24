// <copyright file="DeathScene.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.UI;
    using Engine.Scene;
    using OpenTK.Mathematics;

    /// <summary>
    /// Start scene class.
    /// </summary>
    public class DeathScene : Scene
    {
        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            Menu.DeathMenu deathMenu = new Menu.DeathMenu(new Rectangle(0, 0, 0, 0), Color4.Black, TextureAtlas.Fonts["defaultFont"], UiAlignment.Left, true);
            Engine.Engine.AddRenderer(deathMenu, RenderLayer.UI);
        }
    }
}