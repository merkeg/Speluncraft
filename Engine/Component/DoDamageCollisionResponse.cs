// <copyright file="DoDamageCollisionResponse.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A Kind of Collider, that Checks, if it Intersects with anything in Engine.Colliders. If yes it will deal DMG, if there is an HP Component.
    /// But it wont deal DMG to the GameObject it belongs to.
    /// </summary>
    public class DoDamageCollisionResponse : CollisionResponse
    {
        private bool collided = false;
        private int dmg;
        private float dmgCooldown;
        private float dmgCooldownCounter = 0;
        private bool didDMGthisFrame = false;
        private List<GameObject.GameObject> didDmgOnThose = new List<GameObject.GameObject>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DoDamageCollisionResponse"/> class.
        /// </summary>
        /// <param name="dmg">How Much DMG it will deal on Contact.</param>
        /// <param name="dmgCD">How long before this can deal dmg again.</param>
        public DoDamageCollisionResponse(int dmg, float dmgCD)
        {
            this.dmg = dmg;
            this.dmgCooldown = dmgCD;
        }

        /// <summary>
        /// Returns, if the DMG Collider already collided once. Usefull for bullets.
        /// </summary>
        /// <returns>If we allready Collided with something.</returns>
        public bool GetIsCollided()
        {
            return this.collided;
        }

        /// <summary>
        /// Get if this did dmg this frame.
        /// </summary>
        /// <returns>True if this did dmg this frame.</returns>
        public bool GetDidDMGthisFrame()
        {
            return this.didDMGthisFrame;
        }

        /// <summary>
        /// Get a List of GameObject we did DMG on those.
        /// </summary>
        /// <returns>A List with GameObjects we did DMG on.</returns>
        public List<GameObject.GameObject> GetDidDmgToThose()
        {
            return this.didDmgOnThose;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.didDMGthisFrame = false;
            this.didDmgOnThose.Clear();
            this.dmgCooldownCounter -= frameTime;
            if (this.dmgCooldownCounter > 0)
            {
                return;
            }

            foreach (GameObject.IRectangle r in Engine.GetService<Service.CollisionService>().GetCollosions(this.GameObject))
            {
                this.collided = true;
                if (r is GameObject.GameObject)
                {
                    GameObject.GameObject g = (GameObject.GameObject)r;
                    if (g.GetComponent<HealthPoints>() != null)
                    {
                        g.GetComponent<HealthPoints>().AddHP(-this.dmg);
                        this.didDMGthisFrame = true;
                        this.didDmgOnThose.Add(g);
                        this.dmgCooldownCounter = this.dmgCooldown;
                    }
                }
            }
        }
    }
}
