// <copyright file="ShotGun.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Engine.Component;
    using Engine.Renderer.Sprite;
    using OpenTK.Mathematics;

    /// <summary>
    /// A shotgun.
    /// </summary>
    public class ShotGun : Engine.Component.Component, IGun
    {
        private readonly float bulletLenght = 0.3f;
        private readonly float bulletHeight = 0.3f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 12;
        private readonly float knockBackX = 5;
        private readonly float knockBackY = 1f;
        private int bulletAmount = 4;
        private int dmg = 7;
        private float reloadTime = 0.75f;
        private float reloadCoolDown = 0;

        private bool shotFiredThisFrame;

        private Random randy;

        private ISprite bulletSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShotGun"/> class.
        /// </summary>
        public ShotGun()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Engine.Renderer.Tile.Tilesheet animatedBullet = new Engine.Renderer.Tile.Tilesheet("Game.Resources.Animated.bullet.png", 32, 32);
            this.bulletSprite = new AnimatedSprite(animatedBullet, new[] { new Keyframe(0, 0, 0.5f), new Keyframe(0, 1, 0.5f) });
            this.randy = new Random();
        }

        /// <inheritdoc/>
        public Component GetAsComponent()
        {
            return this;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.reloadCoolDown -= frameTime;
        }

        /// <summary>
        /// Fires a Bullet, when trigger is pulled and is reloaded.
        /// </summary>
        public void PullTrigger()
        {
            if (this.reloadCoolDown <= 0)
            {
                this.shotFiredThisFrame = true;

                // KnockBack
                Physics p = this.GameObject.GetComponent<Physics>();
                if (p != null)
                {
                    ILookDirection d = (ILookDirection)this.GameObject;
                    if (d.GetDirection() == ILookDirection.Left)
                    {
                        p.AddVelocityX(this.knockBackX);
                        p.AddVelocitY(this.knockBackY);
                    }

                    if (d.GetDirection() == ILookDirection.Right)
                    {
                        p.AddVelocityX(-this.knockBackX);
                        p.AddVelocitY(this.knockBackY);
                    }
                }

                for (int i = 0; i < this.bulletAmount; i++)
                {
                    Vector2 vel = new Vector2(1, (float)((this.randy.NextDouble() * 0.3) - 0.1));
                    vel.Normalize();
                    vel = new Vector2(vel.X * this.bulletVelocity, vel.Y * this.bulletVelocity);

                    if (this.GameObject is ILookDirection)
                    {
                        ILookDirection d = (ILookDirection)this.GameObject;
                        if (d.GetDirection() == ILookDirection.Left)
                        {
                            Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, -vel.X, vel.Y, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                            Engine.Engine.AddGameObject(b);
                        }

                        if (d.GetDirection() == ILookDirection.Right)
                        {
                            Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, vel.X, vel.Y, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                            Engine.Engine.AddGameObject(b);
                        }
                    }
                }

                this.reloadCoolDown = this.reloadTime;
            }
            else
            {
                this.shotFiredThisFrame = false;
            }
        }

        /// <inheritdoc/>
        public bool ShotFired()
        {
            return this.shotFiredThisFrame;
        }
    }
}
