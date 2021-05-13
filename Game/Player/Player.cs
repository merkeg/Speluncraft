// <copyright file="Player.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Engine.GameObject;
    using Engine.Renderer.Sprite;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// The thing the Player COntrolls.
    /// </summary>
    public class Player : GameObject, Gun.ILookDirection
    {
        private int jumpcounter;
        private int jumpCounterMax = 1;

        private float accelaration = 18f;
        private float idealBreacking = 25f;
        private float activeBreacking = 20f;
        private float jumpPower = 10f;

        private AnimatedSprite spriteWalking;
        private Sprite spriteIdle;
        private Sprite spriteJump;
        private Sprite spriteFall;

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
            this.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());

            this.AddComponent(new Engine.Component.HealthPoints(100, 100));

            this.jumpcounter = this.jumpCounterMax;

            this.InitializeSprites();

            Engine.Engine.Colliders.Add(this);

            // For Demo 2.0
            this.AddComponent(new Engine.Component.DoDamageCollisionResponse(10, 1));

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
            OpenTK.Windowing.GraphicsLibraryFramework.KeyboardState keyboardState = Engine.Engine.GameWindow.KeyboardState;
            Engine.Component.Physics physics = this.GetComponent<Engine.Component.Physics>();

            walk(frameTime, keyboardState, physics);

            shoot(keyboardState);

            base.OnUpdate(frameTime);
            this.UpdateAnimations();

            // Check death
            if (this.GetComponent<Engine.Component.HealthPoints>().GetIsDeadFlag())
            {
                Engine.Engine.RemoveGameObject(this);
            }

            // Reset jump counter when ground was touched
            Engine.Component.UndoOverlapCollisionResponse collider = this.GetComponent<Engine.Component.UndoOverlapCollisionResponse>();
            if (collider.GetGroundTouchedFlag())
            {
                this.jumpcounter = this.jumpCounterMax;
            }

            // Jump
            if (keyboardState.IsKeyPressed(Keys.Space) && this.jumpcounter > 0)
            {
                physics.AddVelocitY(this.jumpPower);
                this.jumpcounter--;
            }
        }

        private void shoot(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.gun.PullTrigger();
            }
        }

        private void walkLeft(float frameTime, Engine.Component.Physics physics)
        {
            this.isFaceing = Gun.ILookDirection.Left;
            if (physics.GetVelocity().X > 0)
            { // Player is breaking since he is going right
                physics.AddVelocityX(-this.activeBreacking * frameTime);
            }

            physics.AddVelocityX(-this.accelaration * frameTime);
        }

        private void walkRight(float frameTime, Engine.Component.Physics physics)
        {
            this.isFaceing = Gun.ILookDirection.Right;
            if (physics.GetVelocity().X < 0)
            { // Player is breaking since he going left
                physics.AddVelocityX(this.activeBreacking * frameTime);
            }

            physics.AddVelocityX(this.accelaration * frameTime);
        }

        private void idle(float frameTime, Engine.Component.Physics physics)
        {
            this.Sprite = this.spriteIdle;
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

        private void walk(float frameTime, KeyboardState keyboardState, Engine.Component.Physics physics)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            { // Player wants to go left
                walkLeft(frameTime, physics);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            { // Player wants to go right
                walkRight(frameTime, physics);
            }
            else
            { // Player is not breaking or accelerating
                idle(frameTime, physics);
            }
        }

        private void UpdateAnimations()
        {
            Engine.Component.Physics phys = this.GetComponent<Engine.Component.Physics>();

            if (phys.GetVelocity().X < -0.1)
            {
                this.Sprite = this.spriteWalking;
                this.Mirrored = true;
            }

            if (phys.GetVelocity().X > 0.1)
            {
                this.Sprite = this.spriteWalking;
                this.Mirrored = false;
            }

            if (phys.GetVelocity().Y < -0.1)
            {
                this.Sprite = this.spriteFall;
            }

            if (phys.GetVelocity().Y > 0.5)
            {
                this.Sprite = this.spriteJump;
            }
        }

        private void InitializeSprites()
        {
            Engine.Renderer.Tile.Tilesheet walkingSheet = new Engine.Renderer.Tile.Tilesheet("Game.Resources.Player.adventurer_walking.png", 80, 110);
            this.spriteWalking = new AnimatedSprite(walkingSheet, Keyframe.RangeX(0, 1, 0, 0.1f));

            this.spriteIdle = new Sprite("Game.Resources.Player.adventurer_idle.png", false);
            this.spriteJump = new Sprite("Game.Resources.Player.adventurer_jump.png", false);
            this.spriteFall = new Sprite("Game.Resources.Player.adventurer_fall.png", false);
        }
    }
}
