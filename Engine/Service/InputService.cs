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
                    this.subscribers[key]();
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
        /// Unsubscribe from an keydown event.
        /// </summary>
        /// <param name="key">key.</param>
        /// <param name="action">action.</param>
        public void Unsubscribe(Keys key, Handler action)
        {
            this.subscribers[key] -= action;
        }
    }
}