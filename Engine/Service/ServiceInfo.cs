// <copyright file="ServiceInfo.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using global::Engine.Renderer;

    /// <summary>
    /// Service Info class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceInfo : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfo"/> class.
        /// </summary>
        /// <param name="name">Name of the service.</param>
        /// <param name="renderLayer">Render layer of the service.</param>
        public ServiceInfo(string name, RenderLayer renderLayer = RenderLayer.GAME)
        {
            this.Name = name;
            this.RenderLayer = renderLayer;
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Render layer the service wants to render on.
        /// </summary>
        public RenderLayer RenderLayer { get; }
    }
}