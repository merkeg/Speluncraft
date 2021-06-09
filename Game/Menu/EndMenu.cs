// <copyright file="EndMenu.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Menu
{
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Renderer.UI;
    using Engine.Renderer.UI.Primitive;
    using Game.Scenes;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// PauseMenu.
    /// </summary>
    public class EndMenu : UiElement
    {
        private RelativeRectangle textGitgud;
        private RelativeRectangle buttonMainMenu;

        private bool previousMouseButtonState;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndMenu"/> class.
        /// </summary>
        /// <param name="bounds">bounds.</param>
        /// <param name="backgroundColor">backgroundColor.</param>
        /// <param name="font">font.</param>
        /// <param name="alignment">alignment.</param>
        /// <param name="fitToViewport">fitToViewport.</param>
        public EndMenu(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left, bool fitToViewport = false)
            : base(bounds, backgroundColor, font, alignment, fitToViewport)
        {
            this.textGitgud = new RelativeRectangle(this.AbsoluteBounds, 0, 0, 955, 88);
            this.buttonMainMenu = new RelativeRectangle(this.AbsoluteBounds, 0, 0, 0, 0);

            this.AddSprite(TextureAtlas.Sprites["congratulaejuns"], this.textGitgud);
            this.AddText("Press space to continue", Color4.DarkGray, this.buttonMainMenu, 0.25f);
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
            MouseState mouseState = Engine.Engine.GameWindow.MouseState;
            if (mouseState.IsAnyButtonDown)
            {
                this.OpenStartMenu();
            }
        }

        /// <inheritdoc/>
        public override void Resize(ResizeEventArgs args)
        {
            base.Resize(args);
            this.textGitgud.MinX = (Engine.Engine.GameWindow.Size.X / 2) - 477;
            this.textGitgud.MinY = (Engine.Engine.GameWindow.Size.Y / 2) - 48;

            this.buttonMainMenu.MinX = (Engine.Engine.GameWindow.Size.X / 2) - 114;
            this.buttonMainMenu.MinY = (Engine.Engine.GameWindow.Size.Y / 2) + 48;
        }

        /// <inheritdoc/>
        public override void OnRendererCreate()
        {
            Engine.Engine.GetService<Engine.Service.InputService>().Subscribe(new[] { Keys.Space, Keys.Enter }, this.OpenStartMenu);
        }

        /// <inheritdoc/>
        public override void OnRendererDelete()
        {
            Engine.Engine.GetService<Engine.Service.InputService>().Unsubscribe(new[] { Keys.Space, Keys.Enter }, this.OpenStartMenu);
        }

        private void OpenStartMenu()
        {
            GameManager.UpdatesPaused = false;
            Engine.Engine.ChangeScene(GameManager.SceneStart);
        }
    }
}
