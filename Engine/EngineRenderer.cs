// <copyright file="EngineRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using global::Engine.Renderer;
    using global::Engine.Service;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public partial class Engine
    {
        private static UiMatrixRenderer uiMatrixRenderer;

        /// <summary>
        /// Gets or sets a Backgroundcolor.
        /// </summary>
        public static Color BackgroundColor { get; set; } = Color.Black;

        /// <summary>
        /// Gets the renderers.
        /// </summary>
        public static Dictionary<RenderLayer, List<IRenderer>> Renderers => Scene.Scene.Current.Renderers;

        /// <summary>
        /// Gets or sets the scene renderers.
        /// </summary>
        public static Dictionary<RenderLayer, List<IRenderer>> ServiceRenderers { get; set; }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <param name="layer">The render order.</param>
        public static void AddRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            Scene.Scene.Current.AddRenderer(renderer, layer);
        }

        /// <summary>
        /// Removes an rendere.
        /// </summary>
        /// <param name="renderer">The renderer to Remove.</param>
        /// <param name="layer">Render layer.</param>
        public static void RemoveRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            Scene.Scene.Current.RemoveRenderer(renderer, layer);
        }

        private static void Update(FrameEventArgs args)
        {
            float elapsed = (float)MathHelper.Clamp(args.Time, 0, 0.02);
            foreach (IService service in Engine.Services.Values.ToList())
            {
                service.OnUpdate(elapsed);
            }

            foreach (GameObject.GameObject gameObject in Engine.GameObjects.ToList())
            {
                gameObject.OnUpdate(elapsed);
            }
        }

        private static void Resize(ResizeEventArgs args)
        {
            foreach (RenderLayer layer in Engine.Renderers.Keys.ToList())
            {
                Engine.ServiceRenderers[layer].ForEach(renderer => renderer.Resize(args));
                Engine.Renderers[layer].ForEach(renderer => renderer.Resize(args));
            }
        }

        private static void Render(FrameEventArgs args)
        {
            Engine.GameWindow.SwapBuffers();
            GL.ClearColor(BackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (RenderLayer layer in Engine.Renderers.Keys.ToList())
            {
                if (layer == RenderLayer.UI)
                {
                    Engine.uiMatrixRenderer.Render(args);
                }

                Engine.ServiceRenderers[layer].ToList().ForEach(renderer => renderer.Render(args));
                Engine.Renderers[layer].ToList().ForEach(renderer => renderer.Render(args));
            }
        }
    }
}