// <copyright file="Component.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;

    /// <summary>
    /// The base class for components in GameObjects.
    /// </summary>
    public abstract class Component : GameObject.IComponent
    {
        /// <summary>
        /// Gets the GameObject the component is in.
        /// </summary>
        public GameObject.GameObject GameObject { get; private set; }

        /// <summary>
        /// Called if the GameObject is created.
        /// </summary>
        public virtual void OnCreated()
        {
        }

        /// <summary>
        /// Called every gametick.
        /// </summary>
        /// <param name="frameTime">Time between the frame.</param>
        public abstract void OnUpdate(float frameTime);

        /// <summary>
        /// Called if the GameObject is destroyed.
        /// </summary>
        public virtual void OnDestroy()
        {
        }

        /// <summary>
        /// The internal function to set the parent.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        public void SetGameObject(GameObject.GameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        /// <summary>
        /// Gets the GameObject.
        /// </summary>
        /// <returns>the parent GameObject.</returns>
        public GameObject.GameObject GetGameObject()
        {
            return this.GameObject;
        }
    }
}