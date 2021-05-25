// <copyright file="Bullet.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun.Ammunition
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A bullet that flies straight untill it hits something, then does DMG and despwans.
    /// </summary>
    public class Bullet : Engine.GameObject.GameObject
    {
        private int damageDelay = 0;
        private int dmg;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="dmg">How much dmg it will do on impact.</param>
        /// <param name="velocityX">How fast it is flying straight.</param>
        /// <param name="velocityY">How fast it is flying up.</param>
        /// <param name="minX">Where it will spawn on X-Axsis.</param>
        /// <param name="minY">Where it will spawn on Y-Axsis.</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y.</param>
        /// <param name="sprite">Its sprite.</param>
        /// <param name="damageDelayFrames">How many frames to wait, before this bullet will do Damage.</param>
        public Bullet(int dmg, float velocityX, float velocityY, float minX, float minY, float sizeX, float sizeY, Engine.Renderer.Sprite.ISprite sprite, int damageDelayFrames)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.damageDelay = damageDelayFrames;
            this.dmg = dmg;
            Engine.Component.Physics p = new Engine.Component.Physics();
            p.SetVelocity(velocityX, velocityY);
            p.SetMaxVelocity(Math.Abs(velocityX), velocityY);
            p.SetIsAffectedByGravity(false);
            this.AddComponent(p);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            if (this.damageDelay == 0)
            {
                this.AddComponent(new Engine.Component.DoDamageCollisionResponse(this.dmg, 10));
            }

            if (this.damageDelay < 0)
            {
                if (this.GetComponent<Engine.Component.DoDamageCollisionResponse>() != null)
                {
                    if (this.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetIsCollided())
                    {
                        Engine.Engine.RemoveGameObject(this);
                    }
                }
            }

            base.OnUpdate(frameTime);

            this.damageDelay--;
        }
    }
}
