// <copyright file="CollisionResponse.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface that Components must implemen, in order to get info about with what they collid.
    /// </summary>
    public abstract class CollisionResponse : Component
    {
        /// <summary>
        /// Gets called by the Collosion service, when you start intersecting with something.
        /// </summary>
        /// <param name="rectangle">The Rectanlge, we are now colliding with.</param>
        public abstract void OnTriggerEnter(GameObject.IRectangle rectangle);

        /// <summary>
        /// Gets called by the Collosion service, when you stop intersecting with something.
        /// </summary>
        /// <param name="rectangle">The Rectangle, we no longer collid with.</param>
        public abstract void OnTriggerExit(GameObject.IRectangle rectangle);
    }
}
