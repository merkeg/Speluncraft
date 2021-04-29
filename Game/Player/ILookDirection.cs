// <copyright file="ILookDirection.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interfaces that lets you get the direction an GameObject is faceing.
    /// </summary>
    public interface ILookDirection
    {
        /// <summary>
        /// The value getDirection must return, when GameObject is faceing left.
        /// </summary>
        public static readonly int Left = -1;

        /// <summary>
        /// The value getDirection must return, when GameObject is faceing right.
        /// </summary>
        public static readonly int Right = 1;

        /// <summary>
        /// Get the direction the GameObject is faceing.
        /// </summary>
        /// <returns>-1 for left, 1 for right.</returns>
        public int GetDirection();
    }
}
