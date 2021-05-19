// <copyright file="PickUp.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// PickUp Componet will only work on Items.
    /// </summary>
    public class PickUp : Engine.Component.CollisionResponse
    {
        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            foreach (Engine.GameObject.IRectangle r in Engine.Engine.GetService<Engine.Service.CollisionService>().GetCollosions(this.GameObject))
            {
                if (r is Player)
                {
                    if (this.GameObject is Item)
                    {
                        ((Item)this.GameObject).OnPickUp();
                    }
                }
            }
        }
    }
}
