// <copyright file="MachineGun.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Engine.Component;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// A Pistol that shoots normal bullets. With Medium DMG and Medium reload time.
    /// </summary>
    public class MachineGun : Engine.Component.Component, IGun
    {
        private readonly float bulletLenght = 0.5f;
        private readonly float bulletHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 12;
        private int dmg = 7;
        private float reloadTime = 0.25f;
        private float reloadCoolDown = 0;

        private bool shotFiredThisFrame;

        private ISprite bulletSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineGun"/> class.
        /// </summary>
        public MachineGun()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Engine.Renderer.Tile.Tilesheet animatedBullet = new Engine.Renderer.Tile.Tilesheet("Game.Resources.Animated.bullet.png", 32, 32);
            this.bulletSprite = new AnimatedSprite(animatedBullet, new[] { new Keyframe(0, 0, 0.5f), new Keyframe(0, 1, 0.5f) });
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
                if (this.GameObject is ILookDirection)
                {
                    ILookDirection d = (ILookDirection)this.GameObject;
                    if (d.GetDirection() == ILookDirection.Left)
                    {
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, -this.bulletVelocity, 0, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(b);
                    }

                    if (d.GetDirection() == ILookDirection.Right)
                    {
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, this.bulletVelocity, 0, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(b);
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
