// <copyright file="IUpdatable.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.GameObject
{
    /// <summary>
    /// IUpdatable class.
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Called every tick.
        /// </summary>
        /// <param name="frameTime">Time between the frame.</param>
        public void OnUpdate(float frameTime);

        /// <summary>
        /// Called if created.
        /// </summary>
        public void OnUpdatableDestroy();

        /// <summary>
        /// Called if destroyed.
        /// </summary>
        public void OnUpdatableCreate();
    }
}