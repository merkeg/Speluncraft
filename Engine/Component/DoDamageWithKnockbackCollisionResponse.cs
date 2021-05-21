// <copyright file="DoDamageWithKnockbackCollisionResponse.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenTK.Mathematics;

    /// <summary>
    /// DoDamageWithKnockbackCollisionResponse.
    /// </summary>
    public class DoDamageWithKnockbackCollisionResponse : DoDamageCollisionResponse
    {
        private float knockbackX;
        private float knockbackY;

        private bool didDmgThisFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoDamageWithKnockbackCollisionResponse"/> class.
        /// </summary>
        /// <param name="dmg">How much DMG is dealed on hit.</param>
        /// <param name="dmgCD">How long we have to wait, to do dmg again.</param>
        /// <param name="knockbackX">X Velocity to add for knockback ( will be inverted, if we yeet to the left ).</param>
        /// <param name="knockbackY">Y Velocity to add for knockback ( Will not be inverted).</param>
        public DoDamageWithKnockbackCollisionResponse(int dmg, float dmgCD, float knockbackX, float knockbackY)
            : base(dmg, dmgCD)
        {
            this.knockbackX = knockbackX;
            this.knockbackY = knockbackY;
        }

        /// <summary>
        /// If we did dmg this frame.
        /// </summary>
        /// <returns>If we have done dmg this frame.</returns>
        public bool GetDidDmgThisGrame()
        {
            return this.didDmgThisFrame;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);

            this.didDmgThisFrame = false;

            foreach (GameObject.GameObject g in this.GetDidDmgToThose())
            {
                this.didDmgThisFrame = true;
                Physics p = g.GetComponent<Physics>();
                if (p == null)
                {
                    break;
                }

                bool gameObjectIsOnTheRightSide = false;
                if (this.GameObject.MinX < g.MinX)
                {
                    gameObjectIsOnTheRightSide = true;
                }

                p.AddVelocitY(this.knockbackY);

                if (gameObjectIsOnTheRightSide)
                {
                    p.AddVelocityX(this.knockbackX);
                }
                else
                {
                    p.AddVelocityX(-this.knockbackX);
                }
            }
        }
    }
}
