// <copyright file="PhysicsService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Moves every Component with a phsics Component.
    /// </summary>
    [ServiceInfo("physicsService")]
    public class PhysicsService : IService
    {
        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdate(float frameTime)
        {
            foreach (GameObject.GameObject g in Engine.GameObjects)
            {
                if (g.GetComponent<Component.Physics>() != null)
                {
                    g.GetComponent<Component.Physics>().UpdateForService(frameTime);
                }
            }
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }
    }
}
