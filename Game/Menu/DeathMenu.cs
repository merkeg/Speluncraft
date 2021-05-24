// <copyright file="DeathMenu.cs" company="RWUwU">
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
    public class DeathMenu : UiElement
    {
        private RelativeRectangle selectedMenuRespawn;
        private RelativeRectangle selectedMenuMainMenu;
        private QuadRenderer quadRenderer;

        private RelativeRectangle textGitgud;
        private RelativeRectangle buttonRespawn;
        private RelativeRectangle buttonMainMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathMenu"/> class.
        /// </summary>
        /// <param name="bounds">bounds.</param>
        /// <param name="backgroundColor">backgroundColor.</param>
        /// <param name="font">font.</param>
        /// <param name="alignment">alignment.</param>
        /// <param name="fitToViewport">fitToViewport.</param>
        public DeathMenu(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left, bool fitToViewport = false)
            : base(bounds, backgroundColor, font, alignment, fitToViewport)
        {
            this.textGitgud = new RelativeRectangle(this.AbsoluteBounds, (Engine.Engine.GameWindow.Size.X / 2) - 477, (Engine.Engine.GameWindow.Size.Y / 2) - 44, 955, 88);
            this.buttonRespawn = new RelativeRectangle(this.AbsoluteBounds, (Engine.Engine.GameWindow.Size.X / 2) - 294, (Engine.Engine.GameWindow.Size.Y / 2) + 150, 0, 0);
            this.buttonMainMenu = new RelativeRectangle(this.AbsoluteBounds, (Engine.Engine.GameWindow.Size.X / 2) + 100, (Engine.Engine.GameWindow.Size.Y / 2) + 150, 0, 0);
            this.selectedMenuRespawn = new RelativeRectangle(this.AbsoluteBounds, (Engine.Engine.GameWindow.Size.X / 2) - 335, (Engine.Engine.GameWindow.Size.Y / 2) + 150, 250, 55);
            this.selectedMenuMainMenu = new RelativeRectangle(this.AbsoluteBounds, (Engine.Engine.GameWindow.Size.X / 2) + 72, (Engine.Engine.GameWindow.Size.Y / 2) + 150, 250, 55);

            this.quadRenderer = this.AddQuad(this.selectedMenuRespawn, new Color4(80, 80, 80, 0.25f));
            this.AddSprite(TextureAtlas.Sprites["gitgudcasul"], this.textGitgud);
            this.AddText("Respawn", Color4.DarkGray, this.buttonRespawn, 0.5f);
            this.AddText("Main Menu", Color4.DarkGray, this.buttonMainMenu, 0.5f);
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public override void Resize(ResizeEventArgs args)
        {
            base.Resize(args);
            this.textGitgud.MinX = (Engine.Engine.GameWindow.Size.X / 2) - 477;
            this.textGitgud.MinY = (Engine.Engine.GameWindow.Size.Y / 2) - 44;

            this.buttonRespawn.MinX = (Engine.Engine.GameWindow.Size.X / 2) - 294;
            this.buttonRespawn.MinY = (Engine.Engine.GameWindow.Size.Y / 2) + 150;
            this.buttonMainMenu.MinX = (Engine.Engine.GameWindow.Size.X / 2) + 100;
            this.buttonMainMenu.MinY = (Engine.Engine.GameWindow.Size.Y / 2) + 150;
            this.selectedMenuRespawn.MinX = (Engine.Engine.GameWindow.Size.X / 2) - 335;
            this.selectedMenuRespawn.MinY = (Engine.Engine.GameWindow.Size.Y / 2) + 150;
            this.selectedMenuMainMenu.MinX = (Engine.Engine.GameWindow.Size.X / 2) + 72;
            this.selectedMenuMainMenu.MinY = (Engine.Engine.GameWindow.Size.Y / 2) + 150;
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
            this.quadRenderer.Bounds = this.quadRenderer.Bounds == this.selectedMenuRespawn ? this.selectedMenuMainMenu : this.selectedMenuRespawn;
        }

        private void ExecuteSelectedMenu()
        {
            if (this.quadRenderer.Bounds == this.selectedMenuMainMenu)
            {
                GameManager.UpdatesPaused = false;
                Engine.Engine.ChangeScene(GameManager.SceneStart);
                return;
            }

            GameManager.UpdatesPaused = false;
            Engine.Engine.ChangeScene(GameManager.SceneGame);
        }
    }
}
