// <copyright file="Item.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Items that the Player can Pick Up.
    /// </summary>
    public abstract class Item : Engine.GameObject.GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="sizeX">the width of the GameObject.</param>
        /// <param name="sizeY">the height of the GameObject.</param>
        /// /// <param name="sprite">The sprite.</param>
        public Item(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.AddComponent(new PickUp());
        }

        /// <summary>
        /// Gets called when Player collids.
        /// </summary>
        public abstract void OnPickUp();
    }
}