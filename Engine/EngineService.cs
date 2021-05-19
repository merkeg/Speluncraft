// <copyright file="EngineService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using global::Engine.Service;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public partial class Engine
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        public static Dictionary<string, IService> Services { get; private set; }

        /// <summary>
        /// Adds an service to the engine.
        /// </summary>
        /// <param name="service">the service.</param>
        public static void AddService(IService service)
        {
            Service.ServiceInfo serviceInfo = (Service.ServiceInfo)service.GetType().GetCustomAttributes(typeof(Service.ServiceInfo), false)[0];
            Engine.Services.Add(serviceInfo.Name, service);
            service.OnUpdatableCreate();
            Engine.AddRenderer(service, serviceInfo.RenderLayer);
        }

        /// <summary>
        /// Gets a service by name.
        /// </summary>
        /// <param name="key">the name.</param>
        /// <returns>the service.</returns>
        public static IService GetService(string key)
        {
            return Engine.Services[key];
        }

        /// <summary>
        /// Gets service by type.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <returns>the service.</returns>
        public static T GetService<T>()
            where T : IService
        {
            Service.ServiceInfo serviceInfo = (Service.ServiceInfo)Attribute.GetCustomAttributes(typeof(T), typeof(Service.ServiceInfo))[0];
            return (T)Engine.GetService(serviceInfo.Name);
        }
    }
}