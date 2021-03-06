// <copyright file="Sniper.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Engine.Component;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// A Sniper with high damage but a long reload time.
    /// </summary>
    public class Sniper : Engine.Component.Component, IGun
    {
        private readonly float bulletLenght = 0.7f;
        private readonly float bulletHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 15;
        private int dmg = 50;
        private float reloadTime = 1.2f;
        private float reloadCoolDown = 0;

        private bool shotFiredThisFrame;

        private ISprite bulletSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sniper"/> class.
        /// </summary>
        public Sniper()
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
            this.shotFiredThisFrame = false;
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
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, -this.bulletVelocity, 0, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(b);
                    }

                    if (d.GetDirection() == ILookDirection.Right)
                    {
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, this.bulletVelocity, 0, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.4f, this.bulletLenght, this.bulletHeight, this.bulletSprite, this.damageDelayFrames);
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
    }
}
