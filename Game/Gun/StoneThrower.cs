// <copyright file="StoneThrower.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Throw Stones...
    /// </summary>
    public class StoneThrower : Engine.Component.Component, IGun
    {
        private readonly float stoneLenght = 0.5f;
        private readonly float stoneHeight = 0.5f;
        private readonly float bufferDistance = 0f;

        private readonly int damageDelayFrames = 1;

        private readonly float stoneVelocityX = 8;
        private readonly float stoneVelocityY = 2;
        private int dmg = 5;
        private float reloadTime = 0.5f;
        private float reloadCoolDown = 0;

        private bool shotFiredThisFrame;

        private ISprite stoneSprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoneThrower"/> class.
        /// </summary>
        public StoneThrower()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Engine.Renderer.Tile.Tilesheet animatedBullet = new Engine.Renderer.Tile.Tilesheet("Game.Resources.Animated.bullet.png", 32, 32);
            this.stoneSprite = new AnimatedSprite(animatedBullet, new[] { new Keyframe(0, 0, 0.5f), new Keyframe(0, 1, 0.5f) });
        }

        /// <inheritdoc/>
        public Engine.Component.Component GetAsComponent()
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
                        Ammunition.Stone b = new Ammunition.Stone(this.dmg, -this.stoneVelocityX, this.stoneVelocityY, this.GameObject.MinX - this.stoneLenght - this.bufferDistance, this.GameObject.MinY + 0.3f, this.stoneLenght, this.stoneHeight, this.stoneSprite, this.damageDelayFrames);
                        Engine.Engine.AddGameObject(b);
                    }

                    if (d.GetDirection() == ILookDirection.Right)
                    {
                        Ammunition.Stone b = new Ammunition.Stone(this.dmg, this.stoneVelocityX, this.stoneVelocityY, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.3f, this.stoneLenght, this.stoneHeight, this.stoneSprite, this.damageDelayFrames);
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
