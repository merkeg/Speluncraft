// <copyright file="StartMenu.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer.Text;
    using Engine.Renderer.UI;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Contains the Starmenu. Some call it Main Menu.
    /// </summary>
    public class StartMenu : UiElement
    {
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
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public override void OnRendererCreate()
        {
            this.AddSprite(new Engine.Renderer.Sprite.Sprite("Game.Resources.Sprite.startmenu.png", false), new RelativeRectangle(this.AbsoluteBounds, 0, 0, 2560, 1057));
            this.AddText("Start", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, -250, 0, 0, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom), 1);
            this.AddText("Quit", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 100, -150, 0, 0, RelativeRectangleXAlignment.Left, RelativeRectangleYAlignment.Bottom), 1);
        }
    }
}
