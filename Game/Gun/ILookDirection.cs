// <copyright file="ILookDirection.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interfaces that lets you get the direction an GameObject is facing.
    /// </summary>
    public interface ILookDirection
    {
        /// <summary>
        /// The value getDirection must return, when GameObject is facing left.
        /// </summary>
        public static readonly int Left = -1;

        /// <summary>
        /// The value getDirection must return, when GameObject is facing right.
        /// </summary>
        public static readonly int Right = 1;

        /// <summary>
        /// Get the direction the GameObject is facing.
        /// </summary>
        /// <returns>-1 for left, 1 for right.</returns>
        public int GetDirection();
    }
}
