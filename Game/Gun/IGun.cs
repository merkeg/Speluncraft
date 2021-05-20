// <copyright file="IGun.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Gun
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for Guns, provides pullTrigger() Methode.
    /// </summary>
    public interface IGun
    {
        /// <summary>
        /// Pull the trigger of the gun. This should/will cause the gun to shoot in most cases.
        /// </summary>
        public void PullTrigger();

        /// <summary>
        /// Retunrs the gun as Component.
        /// </summary>
        /// <returns>The Gun as a Component.</returns>
        public Engine.Component.Component GetAsComponent();

        /// <summary>
        /// If a shoot was fired this Frame.
        /// </summary>
        /// <returns>True if a shoot was fired this Frame.</returns>
        public bool ShotFired();
    }
}
