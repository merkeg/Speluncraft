// <copyright file="DummyAI.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Enemy
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using OpenTK.Mathematics;

    /// <summary>
    /// Lets the enemy walk from one side of the platform to the other side.
    /// </summary>
    public class DummyAI : Enemy, Gun.ILookDirection
    {
        private readonly float movementSpeed = 2;
        private Engine.Component.Physics phys;
        private int lookingDirection = 1;

        private Engine.GameObject.GameObject checkLeft;
        private Engine.GameObject.GameObject checkRight;

        private GameComponents.AnimationScheduler animationScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyAI"/> class.
        /// </summary>
        /// <param name="minX">minX.</param>
        /// <param name="minY">minY.</param>
        /// <param name="sizeX">sizeX.</param>
        /// <param name="sizeY">sizeY.</param>
        /// <param name="sprite">sprite.</param>
        /// <param name="damage">Damage dealt by touching the enemy.</param>
        public DummyAI(float minX, float minY, float sizeX, float sizeY, ISprite sprite, int damage)
            : base(minX, minY, sizeX, sizeY, sprite, damage)
        {
            this.phys = this.GetComponent<Engine.Component.Physics>();
            this.phys.AddVelocityX(this.movementSpeed);

            if (sprite != null)
            {
                this.InitializeSprites();
            }

            this.checkLeft = new Engine.GameObject.GameObject(this.MinX - 0.3f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);
            this.checkRight = new Engine.GameObject.GameObject(this.MinX + this.SizeX + 0.1f, this.MinY - 0.3f, 0.2f, 0.1f, this.Sprite);

            this.animationScheduler = new GameComponents.AnimationScheduler();
            this.AddComponent(this.animationScheduler);
        }

        /// <summary>
        /// Gets or sets a walking sprite.
        /// </summary>
        public ISprite SpriteWalking { get; set; }

        /// <summary>
        /// Gets or sets a hurt sprite.
        /// </summary>
        public ISprite SpriteHurt { get; set; }

        /// <summary>
        /// Gets or sets an attack sprite.
        /// </summary>
        public ISprite SpriteAttack { get; set; }

        /// <summary>
        /// Gets the Movespeed.
        /// </summary>
        /// <returns>The Movespeed.</returns>
        public float GetMoveSpeed()
        {
            return this.movementSpeed;
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
            this.UpdateAnimations();
            base.OnUpdate(frameTime);
            this.CheckWall();
        }

        /// <summary>
        /// Calls the OnUpdate of Enenemy ( without the AI ).
        /// </summary>
        /// <param name="frameTime">Time passed since last frame.</param>
        public void EnemyOnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
        }

        private void CheckLedge()
        {
            this.checkLeft.MinX = this.MinX - 0.3f;
            this.checkLeft.MinY = this.MinY - 0.3f;
            this.checkRight.MinX = this.MinX + this.SizeX + 0.1f;
            this.checkRight.MinY = this.MinY - 0.3f;

            bool hasFloorLeft = false;
            bool hasFloorRight = false;

            foreach (Engine.GameObject.IRectangle r in Engine.Engine.Colliders)
            {
                if (this.checkLeft.Intersects(r))
                {
                    if (!(r is Player.Player))
                    {
                        hasFloorLeft = true;
                    }
                }

                if (this.checkRight.Intersects(r))
                {
                    if (!(r is Player.Player))
                    {
                        hasFloorRight = true;
                    }
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
                this.lookingDirection = Gun.ILookDirection.Left;
                this.Mirrored = true;
                return;
            }

            if (!hasFloorLeft)
            {
                this.phys.SetVelocity(this.movementSpeed, 0);
                this.lookingDirection = Gun.ILookDirection.Right;
                this.Mirrored = false;
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
                if (this.lookingDirection == Gun.ILookDirection.Left)
                {
                    this.phys.SetVelocity(this.movementSpeed, 0);
                    this.lookingDirection = Gun.ILookDirection.Right;
                    this.Mirrored = false;
                    return;
                }

                if (this.lookingDirection == Gun.ILookDirection.Right)
                {
                    this.phys.SetVelocity(-this.movementSpeed, 0);
                    this.lookingDirection = Gun.ILookDirection.Left;
                    this.Mirrored = true;
                    return;
                }
            }
        }

        private void UpdateAnimations()
        {
            Engine.Component.Physics phys = this.GetComponent<Engine.Component.Physics>();
            if (phys == null)
            {
                return;
            }

            if (phys.GetVelocity().X < -0.1)
            {
                this.animationScheduler.AddAnimation(10, 0.0001f, this.SpriteWalking, true);
                this.lookingDirection = Gun.ILookDirection.Left;
                this.Mirrored = true;
            }

            if (phys.GetVelocity().X > 0.1)
            {
                this.animationScheduler.AddAnimation(10, 0.0001f, this.SpriteWalking, false);
                this.lookingDirection = Gun.ILookDirection.Right;
                this.Mirrored = false;
            }

            if (this.GetComponent<Engine.Component.HealthPoints>().GetTookDmgThisFrame())
            {
                this.animationScheduler.AddAnimation(7, 0.3f, this.SpriteHurt, this.Mirrored);
            }

            if (this.GetComponent<Engine.Component.DoDamageWithKnockbackCollisionResponse>().GetDidDMGthisFrame())
            {
                this.animationScheduler.AddAnimation(4, 0.3f, this.SpriteAttack, this.Mirrored);
            }
        }

        private void InitializeSprites()
        {
            this.SpriteAttack = TextureAtlas.Sprites["zombie_attack"];
            this.SpriteHurt = TextureAtlas.Sprites["zombie_hurt"];
            this.SpriteWalking = TextureAtlas.Sprites["zombie_walking"];
        }
    }
}
