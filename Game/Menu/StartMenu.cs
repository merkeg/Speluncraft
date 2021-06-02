// <copyright file="StartMenu.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Text;
    using Engine.Renderer.UI;
    using Engine.Renderer.UI.Primitive;
    using Game.Scenes;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Contains the Starmenu. Some call it Main Menu.
    /// </summary>
    public class StartMenu : UiElement
    {
        private RelativeRectangle selectedMenuStart;
        private RelativeRectangle selectedMenuQuit;
        private QuadRenderer quadRenderer;

        private bool previousMouseButtonState;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMenu"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="bounds">bounds.</param>
        /// <param name="backgroundColor">backgroundColor.</param>
        /// <param name="font">font.</param>
        /// <param name="alignment">alignment.</param>
        /// <param name="fitToViewport">Set if fit to viewport.</param>
        public StartMenu(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left, bool fitToViewport = false)
            : base(bounds, backgroundColor, font, alignment, fitToViewport)
        {
            this.selectedMenuStart = new RelativeRectangle(this.AbsoluteBounds, 0, -235, 600, 85, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom);
            this.selectedMenuQuit = new RelativeRectangle(this.AbsoluteBounds, 0, -135, 600, 85, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom);

            this.AddSprite(TextureAtlas.Sprites["startscreen"], new RelativeRectangle(this.AbsoluteBounds, 0, 0, 2560, 1057));
            this.quadRenderer = this.AddQuad(this.selectedMenuStart, new Color4(80, 80, 80, 0.25f));
            this.AddText("Start", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, -250, 0, 0, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom), 1);
            this.AddText("Quit", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, -150, 0, 0, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom), 1);
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
            MouseState mouseState = Engine.Engine.GameWindow.MouseState;
            if (mouseState.IsButtonDown(MouseButton.Button1) && this.previousMouseButtonState)
            {
                this.ExecuteSelectedMenu();
            }

            if (mouseState.Y != mouseState.PreviousY)
            {
                this.quadRenderer.Bounds = mouseState.Position.Y > this.selectedMenuQuit.MinY ? this.selectedMenuQuit : this.selectedMenuStart;
            }

            this.previousMouseButtonState = !mouseState.IsButtonDown(MouseButton.Button1);
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
            this.quadRenderer.Bounds = this.quadRenderer.Bounds == this.selectedMenuStart ? this.selectedMenuQuit : this.selectedMenuStart;
        }

        private void ExecuteSelectedMenu()
        {
            if (this.quadRenderer.Bounds == this.selectedMenuQuit)
            {
                Environment.Exit(0);
            }

            Engine.Engine.ChangeScene(new ShopScene());
        }
    }
}
