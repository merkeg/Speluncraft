// <copyright file="DummyAI.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;
    using OpenTK.Mathematics;

    /// <summary>
    /// Lets the enemy walk from one side of the platform to the other side.
    /// </summary>
    public class DummyAI : Enemy
    {
        private readonly float movementSpeed = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyAI"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        public DummyAI(float minX, float minY, float sizeX, float sizeY, Sprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            Engine.Component.Physics enemyPhysics = this.GetComponent<Engine.Component.Physics>();
            enemyPhysics.AddVelocityX(this.movementSpeed);
        }


        private void CheckLedge()
        {
            Engine.GameObject.GameObject checkLeft = new Engine.GameObject.GameObject(this.MinX - 0.3f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);
            Engine.GameObject.GameObject checkRight = new Engine.GameObject.GameObject(this.MinX + this.SizeX + 0.1f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);
            bool hasFloorLeft = false;
            bool hasFloorRight = false;
            Engine.Component.Physics phys = this.GetComponent<Engine.Component.Physics>();

            foreach (Engine.GameObject.IRectangle r in Engine.Engine.Instance().Colliders)
            {
                if (checkLeft.Intersects(r))
                {
                    hasFloorLeft = true;
                }

                if (checkRight.Intersects(r))
                {
                    hasFloorRight = true;
                }
            }

            if (!hasFloorRight && !hasFloorLeft)
            {
                phys.SetVelocity(0, phys.GetVelocity().Y);
                return;
            }

            if (!hasFloorRight)
            {
                phys.SetVelocity(-this.movementSpeed, 0);
                return;
            }

            if (!hasFloorLeft)
            {
                phys.SetVelocity(this.movementSpeed, 0);
                return;
            }

            if (phys.GetVelocity().X == 0)
            {
                phys.SetVelocity(this.movementSpeed, 0);
            }
            
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.CheckLedge();
            base.OnUpdate(frameTime);
        }
    }
}
