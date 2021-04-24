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

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            foreach (GameObject.Rectangle r in Engine.Instance().Colliders)
            {
                if (this.GameObject.Intersects(r))
                {
                    this.collided = true;

                    // Do Damage.
                }
            }
        }
    }
}
