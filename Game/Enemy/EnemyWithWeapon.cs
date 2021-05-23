﻿// <copyright file="EnemyWithWeapon.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// An Enemy that throws rocks when he sees the Player.
    /// </summary>
    public class EnemyWithWeapon : EnemyThatWalksWithTriggerOnSeePlayer
    {
        private Gun.IGun gun;
        private Sprite shootingSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyWithWeapon"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        /// <param name="gun">The Gun the Enemy uses.</param>
        public EnemyWithWeapon(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage, Gun.IGun gun)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.gun = gun;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            if (this.gun.ShotFired())
            {
                this.GetComponent<Game.GameComponents.AnimationScheduler>().AddAnimation(2, 0.2f, this.shootingSprite, this.Mirrored);
            }
        }

        /// <inheritdoc/>
        public override void SawPlayerThisFrame(float frameTime)
        {
            this.gun.PullTrigger();
        }
    }
}
