// <copyright file="Grenade.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun.Ammunition
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// A Grenade that explodes on Impact.
    /// </summary>
    public class Grenade : Engine.GameObject.GameObject
    {
        private readonly int exRadiusX = 1;
        private readonly int exRadiusY = 1;
        private int damageDelay = 0;
        private bool isExing = false;

        private float exCounter = 0.15f;
        private Engine.GameObject.GameObject ex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grenade"/> class.
        /// </summary>
        /// <param name="velocityX">How fast it is flying straight.</param>
        /// <param name="velocityY">How fast it is flying up.</param>
        /// <param name="minX">Where it will spawn on X-Axsis.</param>
        /// <param name="minY">Where it will spawn on Y-Axsis.</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y.</param>
        /// <param name="sprite">Its sprite.</param>
        /// <param name="damageDelayFrames">How many frames to wait, before this bullet will do Damage.</param>
        public Grenade(float velocityX, float velocityY, float minX, float minY, float sizeX, float sizeY, Engine.Renderer.Sprite.ISprite sprite, int damageDelayFrames)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.damageDelay = damageDelayFrames;
            Engine.Component.Physics p = new Engine.Component.Physics();
            p.SetVelocity(velocityX, velocityY);
            p.SetMaxVelocity(Math.Abs(velocityX), Math.Abs(velocityY) + 20);
            p.SetGravityMultiplier(2f);
            p.SetGravity(-10f);
            p.SetIsAffectedByGravity(true);
            this.AddComponent(p);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            // When / after Exploding
            if (this.isExing)
            {
                base.OnUpdate(frameTime);
                this.exCounter -= frameTime;
                if (this.exCounter <= 0)
                {
                    Engine.Engine.RemoveGameObject(this.ex);
                    Engine.Engine.RemoveGameObject(this);
                }

                return;
            }

            if (this.damageDelay == 0)
            {
                this.AddComponent(new Engine.Component.DoDamageCollisionResponse(0, 10));
            }

            base.OnUpdate(frameTime);
            if (this.damageDelay < 0)
            {
                if (this.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetIsCollided())
                {
                    this.Explode();
                    this.isExing = true;
                    this.RemoveComponent(this.GetComponent<Engine.Component.Physics>());
                }
            }

            this.damageDelay--;
        }

        private void Explode()
        {
            Sprite sprite = new Sprite("Game.Resources.Floppa.png", false);
            this.ex = new Engine.GameObject.GameObject(this.MinX - this.exRadiusX, this.MinY - this.exRadiusY, this.SizeX + (2 * this.exRadiusX), this.SizeY + (2 * this.exRadiusY), sprite);
            this.ex.AddComponent(new Engine.Component.DoDamageWithKnockbackCollisionResponse(99, 10, 20, 5));
            Engine.Engine.AddGameObject(this.ex);
        }
    }
}
