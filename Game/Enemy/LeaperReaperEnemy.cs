// <copyright file="LeaperReaperEnemy.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

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
        }

        /// <inheritdoc/>
        public override void SawPlayerThisFrame()
        {
            Console.WriteLine("I SEEEEEEEEEEE YOU");
        }
    }
}
