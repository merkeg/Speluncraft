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
    using Engine.Renderer;
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
        private float reloadTime = 0.6f;
        private float reloadCoolDown = 0;

        private float repeatTime = 0.1f;
        private float repeatTimeCounter;
        private bool secondShot;

        private bool shotFiredThisFrame;

        private int shotDierection;

        private ISprite bulletSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineGun"/> class.
        /// </summary>
        public MachineGun()
        {
            this.bulletSprite = TextureAtlas.Sprites["ammunition_bullet"];
        }

        /// <inheritdoc/>
        public Component GetAsComponent()
        {
            return this;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            if (this.repeatTimeCounter <= 0)
            {
                if (this.secondShot)
                {
                    this.repeatTimeCounter = this.repeatTime;
                    this.secondShot = false;
                }
                else
                {
                    // Well after 340282300000000000000000000000000000000 Seconds this will go wrong. ( About 10790280948756976154236428209030 years )
                    this.repeatTimeCounter = float.MaxValue;
                }

                this.Shoot();
            }

            this.shotFiredThisFrame = false;
            this.reloadCoolDown -= frameTime;
            this.repeatTimeCounter -= frameTime;
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
                    this.shotDierection = ((ILookDirection)this.GameObject).GetDirection();
                    this.Shoot();

                    this.repeatTimeCounter = this.repeatTime;
                    this.secondShot = true;

                    this.reloadCoolDown = this.reloadTime;
                }
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

        /// <inheritdoc/>
        public float GetReloadTime()
        {
            return this.reloadTime;
        }

        /// <inheritdoc/>
        public float GetReloadTimeLeft()
        {
            return this.reloadCoolDown;
        }

        private void Shoot()
        {
            if (this.shotDierection == ILookDirection.Left)
            {
                Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, -this.bulletVelocity, 0, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                Engine.Engine.AddGameObject(b);
            }

            if (this.shotDierection == ILookDirection.Right)
            {
                Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, this.bulletVelocity, 0, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                Engine.Engine.AddGameObject(b);
            }
        }
    }
}
