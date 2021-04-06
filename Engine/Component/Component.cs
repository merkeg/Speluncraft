// <copyright file="Component.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    /// <summary>
    /// The base class for components in GameObjects.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// The GameObject the component is in.
        /// </summary>
        private GameObject.GameObject gameObject;

        /// <summary>
        /// Called if the GameObject is created.
        /// </summary>
        internal void OnCreated()
        {
            return;
        }

        /// <summary>
        /// Called every gametick.
        /// </summary>
        /// <param name="frameTime">Time between the frame.</param>
        internal abstract void OnUpdate(float frameTime);

        /// <summary>
        /// Called if the GameObject is destroyed.
        /// </summary>
        internal void OnDestroy()
        {
            return;
        }

        /// <summary>
        /// The internal function to set the parent.
        /// </summary>
        /// <param name="gameObject">The parent GameObject.</param>
        internal void SetGameObject(GameObject.GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}