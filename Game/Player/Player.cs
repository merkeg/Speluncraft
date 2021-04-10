// <copyright file="Player.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.GameObject;

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
            this.AddComponent(new Engine.Component.Physics());
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            OpenTK.Windowing.GraphicsLibraryFramework.KeyboardState keyboardState = Engine.Engine.Instance().GameWindow.KeyboardState;


            base.OnUpdate(frameTime);
        }

    }
}
