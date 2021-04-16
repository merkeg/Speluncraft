// <copyright file="CameraTrackingComponent.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.Player
{
    using Engine.Camera;
    using Engine.Component;

    public class CameraTrackingComponent : Component
    {

        public override void OnUpdate(float frameTime)
        {
            Engine.Engine.Instance().Camera.Center = new OpenTK.Mathematics.Vector2(this.GameObject.MinX, this.GameObject.MinY);
        }
    }
}
