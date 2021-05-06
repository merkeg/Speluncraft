// <copyright file="CameraTrackingComponent.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using Engine.Camera;
    using Engine.Component;

    /// <summary>
    /// Camera tracking component class.
    /// </summary>
    public class CameraTrackingComponent : Component
    {
        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            Engine.Engine.Camera.Center = new OpenTK.Mathematics.Vector2(this.GameObject.MinX, this.GameObject.MinY);
        }
    }
}
