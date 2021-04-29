// <copyright file="Pistol.cs" company="RWUwU">
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
    public class Pistol : Engine.Component.Component, IGun
    {
        private readonly float bulletLenght = 0.25f;
        private readonly float bulletHeight = 0.2f;
        private readonly float bufferDistance = 0.37f;

        private readonly float bulletVelocity = 10;
        private int dmg = 10;
        private float reloadTime = 0.5f;
        private float reloadCoolDown = 0;

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
                if (this.GameObject is Player.ILookDirection)
                {
                    Player.ILookDirection d = (Player.ILookDirection)this.GameObject;
                    if (d.GetDirection() == Player.ILookDirection.Left)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        using Stream spriteStream = assembly.GetManifestResourceStream("Game.Resources.player.png");
                        Sprite sprite = new Sprite(spriteStream);
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, -this.bulletVelocity, this.GameObject.MinX - this.bulletLenght - this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, sprite);
                        Engine.Engine.Instance().GameObjectsToAdd.Add(b);
                    }

                    if (d.GetDirection() == Player.ILookDirection.Right)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        using Stream spriteStream = assembly.GetManifestResourceStream("Game.Resources.player.png");
                        Sprite sprite = new Sprite(spriteStream);
                        Ammunition.Bullet b = new Ammunition.Bullet(this.dmg, this.bulletVelocity, this.GameObject.MinX + this.GameObject.SizeX + this.bufferDistance, this.GameObject.MinY + 0.5f, this.bulletLenght, this.bulletHeight, sprite);
                        Engine.Engine.Instance().GameObjectsToAdd.Add(b);
                    }
                }

                this.reloadCoolDown = this.reloadTime;
            }
        }
    }
}
