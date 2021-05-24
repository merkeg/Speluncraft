// <copyright file="PauseMenu.cs" company="RWUwU">
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
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// PauseMenu.
    /// </summary>
    public class PauseMenu : UiElement
    {
        private RelativeRectangle selectedMenuResume;
        private RelativeRectangle selectedMenuMainMenu;
        private QuadRenderer quadRenderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        /// <param name="bounds">bounds.</param>
        /// <param name="backgroundColor">backgroundColor.</param>
        /// <param name="font">font.</param>
        /// <param name="alignment">alignment.</param>
        /// <param name="fitToViewport">fitToViewport.</param>
        public PauseMenu(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left, bool fitToViewport = false)
            : base(bounds, backgroundColor, font, alignment, fitToViewport)
        {
            this.selectedMenuResume = new RelativeRectangle(this.AbsoluteBounds, 0, 85, 600, 85);
            this.selectedMenuMainMenu = new RelativeRectangle(this.AbsoluteBounds, 0, 185, 600, 85);

            this.quadRenderer = this.AddQuad(this.selectedMenuResume, new Color4(80, 80, 80, 0.25f));
            this.AddText("Resume", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, 70, 0, 0), 1);
            this.AddText("Main Menu", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, 170, 0, 0), 1);
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public override void OnRendererCreate()
        {
            Engine.Engine.GetService<Engine.Service.InputService>().Subscribe(new[] { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Down, Keys.Up, Keys.Left, Keys.Right }, this.ButtonChange);
            Engine.Engine.GetService<Engine.Service.InputService>().Subscribe(new[] { Keys.Space, Keys.Enter }, this.ExecuteSelectedMenu);
        }

        /// <inheritdoc/>
        public override void OnRendererDelete()
        {
            Engine.Engine.GetService<Engine.Service.InputService>().Unsubscribe(new[] { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Down, Keys.Up, Keys.Left, Keys.Right }, this.ButtonChange);
            Engine.Engine.GetService<Engine.Service.InputService>().Unsubscribe(new[] { Keys.Space, Keys.Enter }, this.ExecuteSelectedMenu);
        }

        private void ButtonChange()
        {
            this.quadRenderer.Bounds = this.quadRenderer.Bounds == this.selectedMenuResume ? this.selectedMenuMainMenu : this.selectedMenuResume;
        }

        private void ExecuteSelectedMenu()
        {
            if (this.quadRenderer.Bounds == this.selectedMenuMainMenu)
            {
                GameManager.UpdatesPaused = false;
                Engine.Engine.ChangeScene(GameManager.SceneStart);
            }
        }
    }
}
