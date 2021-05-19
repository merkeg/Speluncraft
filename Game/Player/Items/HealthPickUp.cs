// <copyright file="HealthPickUp.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Item that can be picked up and restores hp to the Player.
    /// </summary>
    public class HealthPickUp : Item
    {
        private int heal;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPickUp"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="sizeX">the width of the GameObject.</param>
        /// <param name="sizeY">the height of the GameObject.</param>
        /// /// <param name="sprite">The sprite.</param>
        /// <param name="heal">The amount this pickup will heal the Player.</param>
        public HealthPickUp(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int heal)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.heal = heal;
        }

        /// <inheritdoc/>
        public override void OnPickUp()
        {
            foreach (Engine.GameObject.GameObject g in Engine.Engine.GameObjects)
            {
                if (g is Player)
                {
                    g.GetComponent<Engine.Component.HealthPoints>().AddHP(this.heal);
                    Engine.Engine.RemoveGameObject(this);
                }
            }
        }
    }
}
