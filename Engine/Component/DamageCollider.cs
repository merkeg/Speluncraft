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
        private float dmgCoolDonw;
        private float dmgCoolDonwCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageCollider"/> class.
        /// </summary>
        /// <param name="dmg">How Much DMG it will deal on Contact.</param>
        /// <param name="dmgCD">How long before this can deal dmg again.</param>
        public DamageCollider(int dmg, int dmgCD)
        {
            this.dmg = dmg;
            this.dmgCoolDonw = dmgCD;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.dmgCoolDonwCounter -= frameTime;
            if (this.dmgCoolDonwCounter > 0)
            {
                return;
            }

            if (this.GetGameObject().GetComponent<Collider>().GetCollided().Count != 0)
            {
                this.collided = true;

                // We already collided with something and the overlap has been undown.
                foreach (GameObject.IRectangle r in this.GetGameObject().GetComponent<Collider>().GetCollided())
                {
                    if (r is GameObject.GameObject)
                    {
                        GameObject.GameObject g = (GameObject.GameObject)r;
                        if (g.GetComponent<HealthPoints>() != null)
                        {
                            g.GetComponent<HealthPoints>().AddHP(-this.dmg);
                            this.dmgCoolDonwCounter = this.dmgCoolDonw;
                        }
                    }
                }

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
                            this.dmgCoolDonwCounter = this.dmgCoolDonw;
                        }
                    }
                }
            }
        }
    }
}
