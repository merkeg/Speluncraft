// <copyright file="Boss.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy.Boss
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// The Boss.
    /// </summary>
    public class Boss : Enemy
    {
        private BossSniper sniper;
        private ISprite sniperSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="Boss"/> class.
        /// </summary>
        /// <param name="minX">X-Coordinate of bottom leftPoint.</param>
        /// <param name="minY">Y-Coordinate of bottom leftPoint.</param>
        /// <param name="sizeX">Size in X axsis.</param>
        /// <param name="sizeY">Size in Y axsis.</param>
        /// <param name="sprite">Enemy Sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public Boss(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.SizeX = 2;
            this.SizeY = 4;

            this.sniperSprite = TextureAtlas.Sprites["ammunition_bullet"];
            this.sniper = new BossSniper(this.MinX, this.MinY, this.SizeX, this.SizeY / 2, this.sniperSprite);
            Engine.Engine.AddGameObject(this.sniper);
        }
    }
}
