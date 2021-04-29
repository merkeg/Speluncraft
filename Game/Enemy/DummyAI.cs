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
    public class DummyAI : Enemy, Player.ILookDirection
    {
        private readonly float movementSpeed = 2;
        private Engine.Component.Physics phys;
        private int lookingDirection = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyAI"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public DummyAI(float minX, float minY, float sizeX, float sizeY, Sprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.phys = this.GetComponent<Engine.Component.Physics>();
            this.phys.AddVelocityX(this.movementSpeed);
        }

        /// <inheritdoc/>
        public int GetDirection()
        {
            return this.lookingDirection;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.CheckLedge();
            base.OnUpdate(frameTime);
            this.CheckWall();
        }

        private void CheckLedge()
        {
            Engine.GameObject.GameObject checkLeft = new Engine.GameObject.GameObject(this.MinX - 0.3f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);
            Engine.GameObject.GameObject checkRight = new Engine.GameObject.GameObject(this.MinX + this.SizeX + 0.1f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);
            bool hasFloorLeft = false;
            bool hasFloorRight = false;

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
                this.phys.SetVelocity(0, this.phys.GetVelocity().Y);
                return;
            }

            if (!hasFloorRight)
            {
                this.phys.SetVelocity(-this.movementSpeed, 0);
                this.lookingDirection = Player.ILookDirection.Left;
                return;
            }

            if (!hasFloorLeft)
            {
                this.phys.SetVelocity(this.movementSpeed, 0);
                this.lookingDirection = Player.ILookDirection.Right;
                return;
            }

            if (this.phys.GetVelocity().X == 0)
            {
                this.phys.SetVelocity(this.movementSpeed, 0);
            }
        }

        private void CheckWall()
        {
            if (this.phys.GetVelocity().X == 0 && this.phys.GetVelocity().Y == 0)
            {
                if (this.lookingDirection == Player.ILookDirection.Left)
                {
                    this.phys.SetVelocity(this.movementSpeed, 0);
                    this.lookingDirection = Player.ILookDirection.Right;
                }

                if (this.lookingDirection == Player.ILookDirection.Right)
                {
                    this.phys.SetVelocity(-this.movementSpeed, 0);
                    this.lookingDirection = Player.ILookDirection.Left;
                }
            }
        }
    }
}
