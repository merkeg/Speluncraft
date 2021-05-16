// <copyright file="IComponent.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.GameObject
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for GameObjects, to use Components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// The internal function to set the parent.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        public void SetGameObject(GameObject gameObject);

        /// <summary>
        /// Called if the GameObject is created.
        /// </summary>
        public void OnCreated();

        /// <summary>
        /// Called every gametick.
        /// </summary>
        /// <param name="frameTime">Time between the frame.</param>
        public void OnUpdate(float frameTime);

        /// <summary>
        /// Called if the GameObject is destroyed.
        /// </summary>
        public void OnDestroy();
    }
}
