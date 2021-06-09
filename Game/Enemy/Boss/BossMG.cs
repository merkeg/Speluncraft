// <copyright file="BossMG.cs" company="RWUwU">
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
    /// The Bosses MG that shoots fast.
    /// </summary>
    public class BossMG : Engine.GameObject.GameObject
    {
        private readonly float bulletLenght = 0.5f;
        private readonly float bulletHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 10;
        private int dmg = 5;
        private float reloadTime = 2f;
        private float reloadCoolDown = 0;

        private float repeatTime = 0.1f;
        private float repeatTimeCounter;
        private bool secondShot;

        private ISprite bulletSprite;

        private Random randy;

        /// <summary>
        /// Initializes a new instance of the <see cref="BossMG"/> class.
        /// </summary>
        /// <param name="minX">X-Coordinate of bottom leftPoint.</param>
        /// <param name="minY">Y-Coordinate of bottom leftPoint.</param>
        /// <param name="sizeX">Size in X axsis.</param>
        /// <param name="sizeY">Size in Y axsis.</param>
        /// <param name="sprite">The Sprite.</param>
        public BossMG(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.bulletSprite = TextureAtlas.Sprites["ammunition_bullet"];
            this.randy = new Random();
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            if (!Engine.Engine.GameIsRunning)
            {
                return;
            }

            base.OnUpdate(frameTime);

            if (this.reloadCoolDown <= 0)
            {
                this.Shoot();
                this.repeatTimeCounter = this.repeatTime;
                this.secondShot = true;
                this.reloadCoolDown = this.reloadTime;
            }

            if (this.repeatTimeCounter <= 0)
            {
                if (this.secondShot)
                {
                    this.repeatTimeCounter = this.repeatTime;
                    this.secondShot = false;
                }
                else
                {
                    // Well after 340282300000000000000000000000000000000 Seconds this will go wrong. ( About 10790280948756976154236428209030 years )
                    this.repeatTimeCounter = float.MaxValue;
                }

                this.Shoot();
            }

            this.reloadCoolDown -= frameTime;
            this.repeatTimeCounter -= frameTime;
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

            float xOffset = (float)this.randy.NextDouble() - 0.5f;
            xOffset = xOffset * 5;

            Vector2 lineToPlayer = new Vector2((player.MinX + (player.SizeX / 2)) - (this.MinX + this.SizeX), (player.MinY + (player.SizeY / 2)) - (this.MinY + (this.SizeY / 2)) + xOffset);
            lineToPlayer.Normalize();

            Gun.Ammunition.Bullet b = new Gun.Ammunition.Bullet(this.dmg, lineToPlayer.X * this.bulletVelocity, lineToPlayer.Y * this.bulletVelocity, this.MinX + this.SizeX + this.bufferDistance, this.MinY + (this.SizeY / 2), this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames, 0.15f, 0.15f);
            Engine.Engine.AddGameObject(b);
        }
    }
}
