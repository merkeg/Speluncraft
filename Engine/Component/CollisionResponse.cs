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
    public abstract class CollisionResponse : Component, Service.ICollosionServiceSubscriber
    {
    }
}
