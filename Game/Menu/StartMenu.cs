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
        public StartMenu(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left)
            : base(bounds, backgroundColor, font, alignment)
        {
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public override void OnRendererCreate()
        {
            this.AddText("Start", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 20, this.AbsoluteBounds.MaxY - 120, 0, 0), 1.25f);
            this.AddText("Quit", Color4.Azure, new RelativeRectangle(this.AbsoluteBounds, 20, this.AbsoluteBounds.MaxY - 80, 0, 0), 1.25f);
        }
    }
}
