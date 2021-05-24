// <copyright file="IScene.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Scene
{
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer;

    /// <summary>
    /// IScene class.
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// Gets or sets the scene loaded gameobjects.
        /// </summary>
        public List<GameObject> GameObjects { get; set; }

        /// <summary>
        /// Gets or sets the scene loaded renderers.
        /// </summary>
        public Dictionary<RenderLayer, List<IRenderer>> Renderers { get; set; }

        /// <summary>
        /// Gets or sets the colliders.
        /// </summary>
        public List<IRectangle> Colliders { get; set; }

        /// <summary>
        /// Runs when the Scene loads.
        /// </summary>
        public void OnSceneLoad();

        /// <summary>
        /// Runs when the Scene unloads.
        /// </summary>
        public void OnSceneUnload();
    }
}