// <copyright file="LeaperReaperEnemy.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

using Engine.Renderer;

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Enemy that jumps at at Player when he sees him.
    /// </summary>
    public class LeaperReaperEnemy : EnemyThatWalksWithTriggerOnSeePlayer
    {
        private float leapCoolDown = 2;
        private float leapCoolDownCounter;

        private float leapSpeedX = 10;
        private float leapSpeedY = 7;

        private float leapingBreakSpeedX = 5;

        private ISprite leapingSpirite = TextureAtlas.Sprites["leaper_jump"];
        private ISprite slidingSprite = TextureAtlas.Sprites["leaper_slide"];

        private Game.GameComponents.AnimationScheduler animationScheduler;

        private Engine.Component.Physics physics;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaperReaperEnemy"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public LeaperReaperEnemy(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.physics = this.GetComponent<Engine.Component.Physics>();
            this.animationScheduler = this.GetComponent<Game.GameComponents.AnimationScheduler>();
            this.SpriteAttack = TextureAtlas.Sprites["leaper_attack"];
            this.SpriteHurt = TextureAtlas.Sprites["leaper_hurt"];
            this.SpriteWalking = TextureAtlas.Sprites["leaper_walking"];
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            // Are we leaping?
            if (Math.Abs(this.GetMoveSpeed()) < Math.Abs(this.physics.GetVelocity().X))
            {
                if (Math.Abs(this.physics.GetVelocity().Y) < 1f)
                {
                    this.animationScheduler.AddAnimation(9, 0.2f, this.slidingSprite, this.Mirrored);
                }

                if (this.GetDirection() == Gun.ILookDirection.Right)
                {
                    this.physics.AddVelocityX(-frameTime * this.leapingBreakSpeedX);
                }
                else
                {
                    this.physics.AddVelocityX(frameTime * this.leapingBreakSpeedX);
                }

                this.EnemyOnUpdate(frameTime);

                // this.Components.ForEach(component => component.OnUpdate(frameTime));
            }
            else
            {
                base.OnUpdate(frameTime);
            }

            if (this.GetComponent<Engine.Component.DoDamageWithKnockbackCollisionResponse>() == null)
            {
                return;
            }

            if (this.GetComponent<Engine.Component.DoDamageWithKnockbackCollisionResponse>().GetDidDMGthisFrame())
            {
                this.physics.SetVelocity(0, this.physics.GetVelocity().Y);
            }

            this.leapCoolDownCounter -= frameTime;
        }

        /// <inheritdoc/>
        public override void SawPlayerThisFrame(float frameTime)
        {
            if (this.leapCoolDownCounter <= 0)
            {
                this.animationScheduler.AddAnimation(20, 5f, this.leapingSpirite, this.Mirrored);
                if (this.GetDirection() == Gun.ILookDirection.Right)
                {
                    this.physics.SetMaxVelocity(this.leapSpeedX, this.physics.GetMaxVelocity().Y);
                    this.physics.SetVelocity(this.leapSpeedX, this.leapSpeedY);
                }
                else
                {
                    this.physics.SetMaxVelocity(this.leapSpeedX, this.physics.GetMaxVelocity().Y);
                    this.physics.SetVelocity(-this.leapSpeedX, this.leapSpeedY);
                }

                this.leapCoolDownCounter = this.leapCoolDown;
            }
        }
    }
}
