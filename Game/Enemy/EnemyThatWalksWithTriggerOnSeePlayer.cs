// <copyright file="EnemyThatWalksWithTriggerOnSeePlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Enemy that can do something when he sees the Player. ( Overrider OnSeePlayer ).
    /// </summary>
    public abstract class EnemyThatWalksWithTriggerOnSeePlayer : DummyAI
    {
        private Engine.GameObject.GameObject vision;
        private float visionHeight;
        private float visionLength = 7;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyThatWalksWithTriggerOnSeePlayer"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        protected EnemyThatWalksWithTriggerOnSeePlayer(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.visionHeight = this.SizeY / 2;
            this.vision = new Engine.GameObject.GameObject(this.MinX, this.MinY, this.visionLength, this.visionHeight, null);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);

            // Reposition vision
            if (this.GetDirection() == Gun.ILookDirection.Right)
            {
                this.vision.MinX = this.MinX + this.SizeX;
                this.vision.MinY = this.MinY + (this.SizeY / 2);
            }
            else
            {
                this.vision.MinX = this.MinX - this.visionLength;
                this.vision.MinY = this.MinY + (this.SizeY / 2);
            }

            foreach (Engine.GameObject.GameObject g in Engine.Engine.GameObjects)
            {
                if (this.vision.Intersects(g))
                {
                    if (g is Game.Player.Player)
                    {
                        this.SawPlayerThisFrame(frameTime);
                    }
                }
            }
        }

        /// <summary>
        /// Do SOmething when we see the Player.
        /// </summary>
        /// <param name="frameTime">Time pased since last frame.</param>
        public abstract void SawPlayerThisFrame(float frameTime);
    }
}
