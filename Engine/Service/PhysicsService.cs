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
        public void SceneChangeCleanup()
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
                foreach (Component.Component c in g.GetComponents())
                {
                    if (c is IPhysicsServiceSubscriber)
                    {
                        IPhysicsServiceSubscriber c1 = (IPhysicsServiceSubscriber)c;
                        c1.UpdateForService(frameTime);
                        continue;
                    }
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

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}
