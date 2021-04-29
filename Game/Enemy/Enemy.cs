﻿// <copyright file="Enemy.cs" company="RWUwU">
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
        public Enemy(float minX, float minY, float sizeX, float sizeY, Sprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            Engine.Component.Physics physics = new Engine.Component.Physics();
            physics.SetIsAffectedByGravity(true);
            physics.SetGravityMultiplier(3);
            this.AddComponent(physics);
            this.AddComponent(new Engine.Component.Collider());

            this.AddComponent(new Engine.Component.HealthPoints(100, 100));

            this.AddComponent(new Engine.Component.DamageCollider(5, 1));

            // HitBox of Enemy needs to be in CollideList.
            Engine.Engine.Instance().Colliders.Add(this);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            if (this.GetComponent<Engine.Component.HealthPoints>().GetIsDeadFlag())
            {
                Engine.Engine.Instance().GameObjectsToRemove.Add(this);
            }

            // Console.WriteLine("Enemy: " + this.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
        }
    }
}
