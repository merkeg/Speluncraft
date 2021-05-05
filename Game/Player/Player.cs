// <copyright file="Player.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer.Sprite;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The thing the Player COntrolls.
    /// </summary>
    public class Player : GameObject, ILookDirection
    {
        private int jumpcounter;
        private int jumpCounterMax = 1;

        private float accelaration = 18f;
        private float idealBreacking = 25f;
        private float activeBreacking = 20f;
        private float jumpPower = 10f;

        private int isFaceing;
        private Gun.IGun gun;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the Player.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the Player.</param>
        /// <param name="sizeX">Player width.</param>
        /// <param name="sizeY">Player height.</param>
        /// <param name="sprite">Player sprite.</param>
        public Player(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
            : base(minX, minY, sizeX, sizeY, sprite)
        {
            Engine.Component.Physics physics = new Engine.Component.Physics();
            physics.SetIsAffectedByGravity(true);
            physics.SetGravityMultiplier(2);
            this.AddComponent(physics);
            this.AddComponent(new Engine.Component.Collider());

            this.AddComponent(new Engine.Component.HealthPoints(100, 100));

            this.jumpcounter = this.jumpCounterMax;

            Engine.Engine.Instance().Colliders.Add(this);

            // For Demo 2.0
            this.AddComponent(new Engine.Component.DamageCollider(10, 1));

            this.gun = new Gun.Pistol();
            this.AddComponent(this.gun.GetAsComponent());
        }

        /// <inheritdoc/>
        public int GetDirection()
        {
            return this.isFaceing;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            OpenTK.Windowing.GraphicsLibraryFramework.KeyboardState keyboardState = Engine.Engine.Instance().GameWindow.KeyboardState;
            Engine.Component.Physics physics = this.GetComponent<Engine.Component.Physics>();

            if (keyboardState.IsKeyDown(Keys.A))
            { // Player wants to go left
                this.isFaceing = ILookDirection.Left;
                if (physics.GetVelocity().X > 0)
                { // Player is breaking since he is going right
                    physics.AddVelocityX(-this.activeBreacking * frameTime);
                }

                physics.AddVelocityX(-this.accelaration * frameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            { // Player wants to go right
                this.isFaceing = ILookDirection.Right;
                if (physics.GetVelocity().X < 0)
                { // Player is breaking since he going left
                    physics.AddVelocityX(this.activeBreacking * frameTime);
                }

                physics.AddVelocityX(this.accelaration * frameTime);
            }
            else
            { // Player is not breaking or accelerating
                if (physics.GetVelocity().X > 0)
                { // Player is going right
                    physics.AddVelocityX(-this.idealBreacking * frameTime);
                }
                else
                {
                    physics.AddVelocityX(this.idealBreacking * frameTime);
                }

                if (Math.Abs(physics.GetVelocity().X) <= this.idealBreacking * frameTime)
                {
                    physics.SetVelocity(0f, physics.GetVelocity().Y);
                }
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.gun.PullTrigger();
            }

            base.OnUpdate(frameTime);

            if (this.GetComponent<Engine.Component.HealthPoints>().GetIsDeadFlag())
            {
                Engine.Engine.Instance().GameObjectsToRemove.Add(this);
            }

            Engine.Component.Collider collider = this.GetComponent<Engine.Component.Collider>();
            if (collider.GetGroundTouchedFlag())
            {
                this.jumpcounter = this.jumpCounterMax;
            }

            if (keyboardState.IsKeyPressed(Keys.Space) && this.jumpcounter > 0)
            {
                physics.AddVelocitY(this.jumpPower);
                this.jumpcounter--;
            }
        }
    }
}
