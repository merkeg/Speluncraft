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

        /// <summary>
        /// Gets How long this Gun need to reload.
        /// </summary>
        /// <returns>How long this Gun need to reload.</returns>
        public float GetReloadTime();

        /// <summary>
        /// Gets how much time is left before this gun can fiere again.
        /// </summary>
        /// <returns>How much time is left before this gun can fiere again ( in Seconds ).</returns>
        public float GetReloadTimeLeft();
    }
}
