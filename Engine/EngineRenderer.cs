// <copyright file="EngineRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System.Collections.Generic;
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
        /// <summary>
        /// Gets the renderers.
        /// </summary>
        public static Dictionary<RenderLayer, List<IRenderer>> Renderers { get; private set; }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <param name="layer">The render order.</param>
        public static void AddRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            Engine.Renderers[layer].Add(renderer);
            renderer.OnRendererCreate();
            if (Engine.GameWindow != null)
            {
                renderer.Resize(new ResizeEventArgs(Engine.GameWindow.Size));
            }
        }

        /// <summary>
        /// Removes an rendere.
        /// </summary>
        /// <param name="renderer">The rendere to Remove.</param>
        /// <param name="layer">Render layer.</param>
        public static void RemoveRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            renderer.OnRendererDelete();
            Engine.Renderers[layer].Remove(renderer);
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
            foreach (List<IRenderer> renderers in Engine.Renderers.Values.ToList())
            {
                renderers.ForEach(renderer => renderer.Resize(args));
            }
        }

        private static void Render(FrameEventArgs args)
        {
            Engine.GameWindow.SwapBuffers();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach (List<IRenderer> renderers in Engine.Renderers.Values.ToList())
            {
                renderers.ForEach(renderer => renderer.Render(args));
            }
        }
    }
}