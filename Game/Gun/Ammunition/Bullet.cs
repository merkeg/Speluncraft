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
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="dmg">How much dmg it will do on impact.</param>
        /// <param name="velocityX">How fast it is flying.</param>
        /// <param name="minX">Where it will spawn on X-Axsis.</param>
        /// <param name="minY">Where it will spawn on Y-Axsis.</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y.</param>
        /// <param name="sprite">Its sprite.</param>
        public Bullet(int dmg, float velocityX, float minX, float minY, float sizeX, float sizeY, Engine.Renderer.Sprite.ISprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.AddComponent(new Engine.Component.DoDamageCollisionResponse(dmg, 10));
            Engine.Component.Physics p = new Engine.Component.Physics();
            p.SetVelocity(velocityX, 0);
            p.SetMaxVelocity(Math.Abs(velocityX), 0);
            p.SetIsAffectedByGravity(false);
            this.AddComponent(p);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            if (this.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetIsCollided())
            {
                Engine.Engine.RemoveGameObject(this);
            }
        }
    }
}
