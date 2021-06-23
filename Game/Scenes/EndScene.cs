// <copyright file="EndScene.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Scenes
{
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.UI;
    using Engine.Scene;
    using OpenTK.Mathematics;

    /// <summary>
    /// Start scene class.
    /// </summary>
    public class EndScene : Scene
    {
        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            Menu.EndMenu endMenu = new Menu.EndMenu(new Rectangle(0, 0, 0, 0), Color4.Black, TextureAtlas.Fonts["defaultFont"], UiAlignment.Left, true);
            Engine.Engine.AddRenderer(endMenu, RenderLayer.UI);
        }
    }
}