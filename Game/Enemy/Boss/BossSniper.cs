// <copyright file="BossSniper.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy.Boss
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using OpenTK.Mathematics;

    /// <summary>
    /// The Sniper weapon of the Boss.
    /// </summary>
    public class BossSniper : Engine.GameObject.GameObject
    {
        private readonly float bulletLenght = 0.5f;
        private readonly float bulletHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 10;
        private int dmg = 10;
        private float reloadTime = 3f;
        private float reloadCoolDown = 0;

        private ISprite bulletSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="BossSniper"/> class.
        /// </summary>
        /// <param name="minX">X-Coordinate of bottom leftPoint.</param>
        /// <param name="minY">Y-Coordinate of bottom leftPoint.</param>
        /// <param name="sizeX">Size in X axsis.</param>
        /// <param name="sizeY">Size in Y axsis.</param>
        /// <param name="sprite">The Sprite.</param>
        public BossSniper(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
            : base(minX,  minY,  sizeX,  sizeY,  sprite)
        {
            this.bulletSprite = TextureAtlas.Sprites["ammunition_bullet"];
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            if (this.reloadCoolDown < 0)
            {
                this.Shoot();
                this.reloadCoolDown = this.reloadTime;
            }

            this.reloadCoolDown -= frameTime;
        }

        private void Shoot()
        {
            Engine.GameObject.GameObject player = null;
            foreach (Engine.GameObject.GameObject g in Engine.Engine.GameObjects)
            {
                if (g is Game.Player.Player)
                {
                    player = g;
                }
            }

            if (player == null)
            {
                return;
            }

            Vector2 lineToPlayer = new Vector2((player.MinX + (player.SizeX / 2)) - (this.MinX + this.SizeX), (player.MinY + (player.SizeY / 2)) - (this.MinY + (this.SizeY / 2)));
            lineToPlayer.Normalize();

            Gun.Ammunition.Bullet b = new Gun.Ammunition.Bullet(this.dmg, lineToPlayer.X * this.bulletVelocity, lineToPlayer.Y * this.bulletVelocity, this.MinX + this.SizeX + this.bufferDistance, this.MinY + (this.SizeY / 2), this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
            Engine.Engine.AddGameObject(b);
        }
    }
}
