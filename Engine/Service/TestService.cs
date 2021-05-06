// <copyright file="TestService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using OpenTK.Windowing.Common;

    /// <summary>
    /// TestService class.
    /// </summary>
    [ServiceInfo("test")]
    public class TestService : IService
    {
        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }

        /// <inheritdoc/>
        public void OnUpdate(float frameTime)
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
        }
    }
}