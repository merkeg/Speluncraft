// <copyright file="GrenadeLauncher.cs" company="RWUwU">
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
    /// Launches Grenades.
    /// </summary>
    public class GrenadeLauncher : Engine.Component.Component, IGun
    {
        private readonly float bulletLenght = 0.5f;
        private readonly float bulletHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float bulletVelocity = 8;
        private float reloadTime = 1.25f;
        private float reloadCoolDown = 0;

        private bool shotFiredThisFrame;

        private ISprite grenadeSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrenadeLauncher"/> class.
        /// </summary>
        public GrenadeLauncher()
        {
            this.grenadeSprite = TextureAtlas.Sprites["ammunition_bullet"];
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
                        Ammunition.Grenade g = new Ammunition.Grenade(-this.bulletVelocity, 5, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, this.grenadeSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(g);
                    }

                    if (d.GetDirection() == ILookDirection.Right)
                    {
                        Ammunition.Grenade g = new Ammunition.Grenade(this.bulletVelocity, 5, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, this.grenadeSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(g);
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
