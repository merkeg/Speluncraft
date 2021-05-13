// <copyright file="IPhysicsServiceSubscriber.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Componts must implemt this to get Updates from Physics Service.
    /// </summary>
    public interface IPhysicsServiceSubscriber
    {
        /// <summary>
        /// Methode the PhysicsService calls, to update the positon of the GameObject.
        /// </summary>
        /// <param name="frameTime">Time since last frame.</param>
        public void UpdateForService(float frameTime);
    }
}
