// <copyright file="StartScene.cs" company="RWUwU">
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
    public class StartScene : Scene
    {
        /// <inheritdoc/>
        public override void OnSceneLoad()
        {
            Menu.StartMenu startMenu = new Menu.StartMenu(new Rectangle(0, 0, 0, 0), Color4.Black, new Font(FontModel.Parse("Game.Resources.Font.semicondensed.font.fnt"), new Sprite("Game.Resources.Font.semicondensed.font.png")), UiAlignment.Left, true);
            Engine.Engine.AddRenderer(startMenu, RenderLayer.UI);
        }
    }
}