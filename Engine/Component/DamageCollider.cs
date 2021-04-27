// <copyright file="DamageCollider.cs" company="RWUwU">
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
    public class DamageCollider : Component
    {
        private bool collided = false;
        private int dmg;
        private float dmgCooldown;
        private float dmgCooldownCounter = 0;
        private List<GameObject.IRectangle> collisionList = new List<GameObject.IRectangle>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageCollider"/> class.
        /// </summary>
        /// <param name="dmg">How Much DMG it will deal on Contact.</param>
        /// <param name="dmgCD">How long before this can deal dmg again.</param>
        public DamageCollider(int dmg, int dmgCD)
        {
            this.dmg = dmg;
            this.dmgCooldown = dmgCD;
        }

        /// <summary>
        /// Add a Collision that was undone, but still needs to be checked for DMG.
        /// </summary>
        /// <param name="r">The Rectangle we collided with.</param>
        public void AddToCollisionList(GameObject.IRectangle r)
        {
            this.collisionList.Add(r);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.dmgCooldownCounter -= frameTime;
            if (this.dmgCooldownCounter > 0)
            {
                this.collisionList = new List<GameObject.IRectangle>();
                return;
            }

            foreach (GameObject.IRectangle r in this.collisionList)
            {
                if (r is GameObject.GameObject)
                {
                    GameObject.GameObject g = (GameObject.GameObject)r;
                    if (g.GetComponent<HealthPoints>() != null)
                    {
                        g.GetComponent<HealthPoints>().AddHP(-this.dmg);
                        this.dmgCooldownCounter = this.dmgCooldown;
                    }
                }
            }

            if (this.GetGameObject().GetComponent<Collider>() != null && this.GetGameObject().GetComponent<Collider>().GetCollided().Count != 0)
            {
                this.collided = true;

                // We already collided with something and the overlap has been undone.
                foreach (GameObject.IRectangle r in this.GetGameObject().GetComponent<Collider>().GetCollided())
                {
                    if (r is GameObject.GameObject)
                    {
                        GameObject.GameObject g = (GameObject.GameObject)r;
                        if (g.GetComponent<HealthPoints>() != null)
                        {
                            g.GetComponent<HealthPoints>().AddHP(-this.dmg);
                            this.dmgCooldownCounter = this.dmgCooldown;
                        }
                    }
                }

                this.collisionList = new List<GameObject.IRectangle>();
                return;
            }

            // If the Collider Component allready checked, then we dont need to check anymore. ( If perfomance problems )
            foreach (GameObject.IRectangle r in Engine.Instance().Colliders)
            {
                if (this.GameObject.Intersects(r))
                {
                    this.collided = true;

                    if (r is GameObject.GameObject)
                    {
                        GameObject.GameObject g = (GameObject.GameObject)r;
                        if (g.GetComponent<HealthPoints>() != null)
                        {
                            g.GetComponent<HealthPoints>().AddHP(-this.dmg);
                            this.dmgCooldownCounter = this.dmgCooldown;
                        }
                    }
                }
            }

            this.collisionList = new List<GameObject.IRectangle>();
        }
    }
}
