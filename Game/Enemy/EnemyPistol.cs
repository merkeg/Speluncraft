// <copyright file="EnemyPistol.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// DummyAI with cool Pistol that shoots.
    /// </summary>
    public class EnemyPistol : DummyAI
    {
        private Gun.Pistol pistol;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyPistol"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public EnemyPistol(float minX, float minY, float sizeX, float sizeY, Sprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.pistol = new Gun.Pistol();
            this.AddComponent(this.pistol);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.pistol.PullTrigger();
            base.OnUpdate(frameTime);
        }
    }
}
