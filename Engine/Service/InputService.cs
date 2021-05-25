// <copyright file="InputService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Engine.Renderer;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Input service class.
    /// </summary>
    [ServiceInfo("input")]
    public class InputService : IService
    {
        private Dictionary<Keys, Handler> subscribers;

        /// <summary>
        /// The handler.
        /// </summary>
        public delegate void Handler();

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
            KeyboardState state = Engine.GameWindow.KeyboardState;
            if (!state.IsAnyKeyDown)
            {
                return;
            }

            foreach (Keys key in this.subscribers.Keys.ToList())
            {
                if (state.IsKeyDown(key) && !state.WasKeyDown(key))
                {
                    if (this.subscribers[key] != null)
                    {
                        this.subscribers[key]();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
            this.subscribers = new Dictionary<Keys, Handler>();
        }

        /// <inheritdoc/>
        public void SceneChangeCleanup()
        {
            this.subscribers = new Dictionary<Keys, Handler>();
        }

        /// <summary>
        /// Subscribe to an keydown event.
        /// </summary>
        /// <param name="key">key.</param>
        /// <param name="action">action.</param>
        public void Subscribe(Keys key, Handler action)
        {
            if (!this.subscribers.ContainsKey(key))
            {
                this.subscribers.Add(key, null);
            }

            this.subscribers[key] += action;
        }

        /// <summary>
        /// Subscribe to an keydown event. Accepting multiple keys.
        /// </summary>
        /// <param name="keys">keys.</param>
        /// <param name="action">action.</param>
        public void Subscribe(Keys[] keys, Handler action)
        {
            foreach (Keys key in keys)
            {
                if (!this.subscribers.ContainsKey(key))
                {
                    this.subscribers.Add(key, null);
                }

                this.subscribers[key] += action;
            }
        }

        /// <summary>
        /// Unsubscribe from an keydown event.
        /// </summary>
        /// <param name="key">key.</param>
        /// <param name="action">action.</param>
        public void Unsubscribe(Keys key, Handler action)
        {
            this.subscribers[key] -= action;
        }

        /// <summary>
        /// Unsubscribe from an keydown event. Accepts multiple keys.
        /// </summary>
        /// <param name="keys">keys.</param>
        /// <param name="action">action.</param>
        public void Unsubscribe(Keys[] keys, Handler action)
        {
            foreach (Keys key in keys)
            {
                this.subscribers[key] -= action;
            }
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}