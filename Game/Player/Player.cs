// <copyright file="Player.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The thing the Player COntrolls.
    /// </summary>
    public class Player : GameObject
    {
        private int jumpCounterMax = 1;
        private int jumpcounter;

        private float accelaration = 0.3f;
        private float idealBreacking = 0.1f;
        private float activeBreacking = 0.2f;
        private float jumpPower = 2f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the Player.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the Player.</param>
        /// <param name="sizeX">Player width</param>
        /// <param name="sizeY">Player height</param>
        public Player(float minX, float minY, float sizeX, float sizeY)
            : base(minX, minY, sizeX, sizeY)
        {
            Engine.Component.Physics physics = new Engine.Component.Physics();
            physics.SetIsAffectedByGravity(true);
            physics.SetGravityMultiplier(3);
            this.AddComponent(physics);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            OpenTK.Windowing.GraphicsLibraryFramework.KeyboardState keyboardState = Engine.Engine.Instance().GameWindow.KeyboardState;
            Engine.Component.Physics physics = this.GetComponent<Engine.Component.Physics>();

            if (keyboardState.IsKeyDown(Keys.A))
            { // Player wants to go left
                if (physics.GetVelocity().X > 0)
                { // Player is breaking since he is going right
                    physics.AddVelocityX(-this.activeBreacking);
                }

                physics.AddVelocityX(-this.accelaration);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            { // Player wants to go right
                if (physics.GetVelocity().X < 0)
                { // Player is breaking since he going left
                    physics.AddVelocityX(this.activeBreacking);
                }

                physics.AddVelocityX(this.accelaration);
            }
            else
            { // Player is not breaking or accelerating
                if (physics.GetVelocity().X > 0)
                { // Player is going right
                    physics.AddVelocityX(-this.idealBreacking);
                }
                else
                {
                    physics.AddVelocityX(this.idealBreacking);
                }

                if (Math.Abs(physics.GetVelocity().X) <= this.idealBreacking)
                {
                    physics.SetVelocity(0f, physics.GetVelocity().Y);
                }
            }

            if (keyboardState.IsKeyPressed(Keys.Space) && this.jumpcounter > 0)
            {
                physics.AddVelocitY(this.jumpPower);
                this.jumpcounter--;
            }

            base.OnUpdate(frameTime);
        }

    }
}
