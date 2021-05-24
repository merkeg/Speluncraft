// <copyright file="Engine.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Scene;
    using global::Engine.Service;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public partial class Engine
    {
        static Engine()
        {
            Engine.Services = new Dictionary<string, IService>();
            Engine.ServiceRenderers = new Dictionary<RenderLayer, List<IRenderer>>();
            foreach (RenderLayer layer in (RenderLayer[])Enum.GetValues(typeof(RenderLayer)))
            {
                Engine.ServiceRenderers.Add(layer, new List<IRenderer>());
            }

            Engine.AddService(new PhysicsService());
            Engine.AddService(new CollisionService());
        }

        /// <summary>
        /// Gets a list of the colliders in the game.
        /// </summary>
        public static List<IRectangle> Colliders => Scene.Scene.Current.Colliders;

        /// <summary>
        /// Gets the GameWindow the Engine runs on.
        /// </summary>
        public static OpenTK.Windowing.Desktop.GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Gets the game Camera.
        /// </summary>
        public static Camera.Camera Camera { get; private set; }

        /// <summary>
        /// Start the engine ticks.
        /// </summary>
        /// <param name="window">The GameWindow the engine will be run on.</param>
        public static void StartEngine(OpenTK.Windowing.Desktop.GameWindow window)
        {
            Engine.GameWindow = window;
            Engine.Camera = new Camera.Camera();
            Engine.ServiceRenderers[RenderLayer.GAME].Add(Engine.Camera);

            window.UpdateFrame += Engine.Update;
            Engine.GameWindow.RenderFrame += Engine.Render;
            Engine.GameWindow.Resize += Engine.Resize;
            Engine.uiMatrixRenderer = new UiMatrixRenderer();

            // Services
            Engine.AddService(new TilemapService());
            Engine.AddService(new ParticleService());
            Engine.AddService(new InputService());

            // OpenGL capabilities
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);
        }

        /// <summary>
        /// Change the Scene.
        /// </summary>
        /// <param name="scene">Scene to change to.</param>
        public static void ChangeScene(Scene.Scene scene)
        {
            if (Scene.Scene.Current != null)
            {
                Scene.Scene.Current.OnSceneUnload();
            }

            Scene.Scene.Current = scene;
            Scene.Scene.Current.OnSceneLoad();
            GC.Collect();
        }
    }
}
