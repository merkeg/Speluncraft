// <copyright file="IService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using global::Engine.GameObject;
    using global::Engine.Renderer;

    /// <summary>
    /// IService class.
    /// </summary>
    public interface IService : IRenderer, IUpdatable
    {
    }
}