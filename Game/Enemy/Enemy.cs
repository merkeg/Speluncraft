// <copyright file="Enemy.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer.Sprite;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Basic Enemy that can be shot at.
    /// </summary>
    public class Enemy : Engine.GameObject.GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="minX">X-Coordinate of bottom leftPoint.</param>
        /// <param name="minY">Y-Coordinate of bottom leftPoint.</param>
        /// <param name="sizeX">Size in X axsis.</param>
        /// <param name="sizeY">Size in Y axsis.</param>
        /// <param name="sprite">Enemy Sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public Enemy(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            Engine.Component.Physics physics = new Engine.Component.Physics();
            physics.SetIsAffectedByGravity(true);
            physics.SetGravityMultiplier(3);
            this.AddComponent(physics);
            this.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());

            this.AddComponent(new Engine.Component.HealthPoints(50, 50));

            // this.AddComponent(new Engine.Component.DoDamageCollisionResponse(damage, 1));
            this.AddComponent(new Engine.Component.DoDamageWithKnockbackCollisionResponse(damage, 0.2f, 10, 5));

            // HitBox of Enemy needs to be in CollideList.
            Engine.Engine.Colliders.Add(this);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            if (this.GetComponent<Engine.Component.HealthPoints>() != null)
            {
                if (this.GetComponent<Engine.Component.HealthPoints>().GetIsDeadFlag())
                {
                    Engine.Engine.RemoveGameObject(this);
                }
            }
        }
    }
}
