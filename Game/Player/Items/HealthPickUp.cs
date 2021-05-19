using Engine.Renderer.Sprite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Player.Items
{
    public class HealthPickUp : Item
    {
        private int heal;
        public HealthPickUp(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int heal) : base(minX, minY, sizeX, sizeY, sprite)
        {
            this.heal = heal;
            Engine.Component.Physics p = new Engine.Component.Physics();
            p.SetIsAffectedByGravity(true);
            this.AddComponent(p);
        }

        public override void OnPickUp()
        {
            foreach (Player p in Engine.Engine.GameObjects)
            {
                p.GetComponent<Engine.Component.HealthPoints>().AddHP(this.heal);
                Engine.Engine.RemoveGameObject(this);
            }
        }
    }
}
