// <copyright file="Stone.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun.Ammunition
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A Stone, that flys slow and has gravity.
    /// </summary>
    public class Stone : Engine.GameObject.GameObject
    {
        private int damageDelay = 0;
        private int dmg;

        private Engine.GameObject.GameObject graphic;
        private float graphicOffsetX = 0.2f;
        private float graphicOffsetY = 0.2f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stone"/> class.
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
        public Stone(int dmg, float velocityX, float velocityY, float minX, float minY, float sizeX, float sizeY, Engine.Renderer.Sprite.ISprite sprite, int damageDelayFrames)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.damageDelay = damageDelayFrames;
            this.dmg = dmg;
            Engine.Component.Physics p = new Engine.Component.Physics();
            p.SetVelocity(velocityX, velocityY);
            p.SetMaxVelocity(Math.Abs(velocityX), 20);
            p.SetIsAffectedByGravity(true);
            p.SetGravity(-7);
            p.SetGravityMultiplier(2);
            this.AddComponent(p);

            this.graphic = new Engine.GameObject.GameObject(this.MinX - (this.graphicOffsetX / 2), this.MinY - (this.graphicOffsetY / 2), this.SizeX + this.graphicOffsetX, this.SizeY + this.graphicOffsetY, sprite);
            this.Sprite = null;
            Engine.Engine.AddGameObject(this.graphic);
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

            this.graphic.MinX = this.MinX - (this.graphicOffsetX / 2);
            this.graphic.MinY = this.MinY - (this.graphicOffsetY / 2);

            this.damageDelay--;
        }

        /// <inheritdoc/>
        public override void OnUpdatableDestroy()
        {
            Engine.Engine.RemoveGameObject(this.graphic);
            base.OnUpdatableDestroy();
        }
    }
}
