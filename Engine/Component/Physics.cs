﻿// <copyright file="Physics.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenTK.Mathematics;

    /// <summary>
    /// The physics component.
    /// </summary>
    public class Physics : Component
    {
        /// <summary>
        /// Gets or sets The Velocity at wich the Object will move OnUpdate.
        /// </summary>
        private Vector2 velocity;

        /// <summary>
        /// Gets or sets The Max Velocity at wich the Object will move OnUpdate.
        /// </summary>
        private Vector2 maxVelocity;

        /// <summary>
        /// The Gravity ( How much an Object gets pulled down ).
        /// </summary>
        private float gravity;

        /// <summary>
        /// If the Object should get Gravity.
        /// </summary>
        private bool isAffectedByGravity;

        /// <summary>
        /// How often should Gravity be added to accelerate teh Drop. When Velocity Y. < 0.
        /// </summary>
        private float gravityMultiplier;

        /// <summary>
        /// Add to the x Velocity.
        /// </summary>
        /// <param name="x">How much to add.</param>
        public void AddVelocityX(float x)
        {
            this.velocity.X += x;
        }

        /// <summary>
        /// Add to the y Velocity.
        /// </summary>
        /// <param name="y">How much to add.</param>
        public void AddVelocitY(float y)
        {
            this.velocity.Y += y;
        }

        /// <summary>
        /// Set the Gravity.
        /// </summary>
        /// <param name="gravity">The paramref name="gravity". Normally a value. < 0</param>
        public void SetGravity(float gravity)
        {
            this.gravity = gravity;
        }

        /// <summary>
        /// Set if the Object should get Gravity.
        /// </summary>
        /// <param name="isAffectedByGravity">Yes or no.</param>
        public void SetIsAffectedByGravity(bool isAffectedByGravity)
        {
            this.isAffectedByGravity = isAffectedByGravity;
        }

        /// <summary>
        /// Set how much gravity should be added when Y. < 0 to accelert the drop.
        /// </summary>
        /// <param name="multi"> how much gravity should be added when Y. < 0 to accelert the drop.</param>
        public void SetGravityMultiplier(float multi)
        {
            this.gravityMultiplier = multi;
        }

        /// <summary>
        /// Set the curretn Velocity.
        /// </summary>
        /// <param name="x">X Part of the Velocity Vector.</param>
        /// <param name="y">Y Part of the Velocity Vector.</param>
        public void SetVelocity(float x, float y)
        {
            this.velocity.X = x;
            this.velocity.Y = y;
        }

        /// <summary>
        /// Get the Current Velocity.
        /// </summary>
        /// <returns>The Current Velocity.</returns>
        public Vector2 GetVelocity()
        {
            return this.velocity;
        }

        /// <summary>
        /// Set the MaxVelocity.
        /// </summary>
        /// <param name="x">X Part of the MaxVelocity Vector.</param>
        /// <param name="y">Y Part of the MaxVelocity Vector.</param>
        public void SetMaxVelocity(float x, float y)
        {
            this.maxVelocity.X = x;
            this.maxVelocity.Y = y;
        }

        /// <summary>
        /// Sets some Default values.
        /// </summary>
        public override void OnCreated()
        {
            this.velocity = new Vector2(0, 0);
            this.maxVelocity = new Vector2(10, 20);
            this.gravity = -0.5f;
            this.isAffectedByGravity = false;
            this.gravityMultiplier = 1;
            return;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.AddGravity(frameTime);

            this.CheckIfToFast();
            this.GetGameObject().MinX += frameTime * this.velocity.X;
            this.GetGameObject().MinY += frameTime * this.velocity.Y;
        }

        private void AddGravity(float frameTime)
        {
            if (!this.isAffectedByGravity)
            {
                return;
            }

            if (this.velocity.Y > 0)
            {
                this.velocity.Y += this.gravity * frameTime;
            }
            else
            {
                this.velocity.Y += this.gravity * this.gravityMultiplier * frameTime;
            }
        }

        private void CheckIfToFast()
        {
            if (Math.Abs(this.velocity.X) > this.maxVelocity.X)
            {
                if (this.velocity.X > 0)
                {
                    this.velocity.X = this.maxVelocity.X;
                }
                else
                {
                    this.velocity.X = -this.maxVelocity.X;
                }
            }

            if (Math.Abs(this.velocity.Y) > this.maxVelocity.Y)
            {
                if (this.velocity.Y > 0)
                {
                    this.velocity.Y = this.maxVelocity.Y;
                }
                else
                {
                    this.velocity.Y = -this.maxVelocity.Y;
                }
            }
        }
    }
}
