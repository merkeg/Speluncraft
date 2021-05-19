using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Player.Items
{
    public class PickUp : Engine.Component.CollisionResponse
    {
        public override void OnUpdate(float frameTime)
        {
            foreach (Player p in Engine.Engine.GetService<Engine.Service.CollisionService>().GetCollosions(this.GameObject))
            {
                if (this.GameObject is Item)
                {
                    ((Item)this.GameObject).OnPickUp();
                }
            }
        }
    }
}
