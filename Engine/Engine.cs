﻿// <copyright file="Engine.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Service;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public class Engine
    {
        static Engine()
        {
            Engine.GameObjects = new List<GameObject.GameObject>();
            Engine.Colliders = new List<IRectangle>();
            Engine.GameObjectsToRemove = new List<GameObject.GameObject>();
            Engine.GameObjectsToAdd = new List<GameObject.GameObject>();
            Engine.Renderers = new Dictionary<RenderLayer, List<IRenderer>>();
            Engine.Services = new Dictionary<string, IService>();

            foreach (RenderLayer layer in (RenderLayer[])Enum.GetValues(typeof(RenderLayer)))
            {
                Engine.Renderers.Add(layer, new List<IRenderer>());
            }

            Engine.AddService(new CollisionService());
        }

        /// <summary>
        /// Gets a list of GameObjects in the game.
        /// </summary>
        public static List<GameObject.GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Gets the renderers.
        /// </summary>
        public static Dictionary<RenderLayer, List<IRenderer>> Renderers { get; private set; }

        /// <summary>
        /// Gets a list of the colliders in the game.
        /// </summary>
        public static List<IRectangle> Colliders { get; private set; }

        /// <summary>
        /// Gets the GameWindow the Engine runs on.
        /// </summary>
        public static OpenTK.Windowing.Desktop.GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Gets the game Camera.
        /// </summary>
        public static Camera.Camera Camera { get; private set; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        public static Dictionary<string, IService> Services { get; private set; }

        /// <summary>
        /// Gets or sets Here you can add GameObjects that should be removed Engine frame.
        /// </summary>
        private static List<GameObject.GameObject> GameObjectsToRemove { get; set; }

        /// <summary>
        /// Gets or sets Here you can add GameObjects that should be added next frame.
        /// </summary>
        private static List<GameObject.GameObject> GameObjectsToAdd { get; set; }

        /// <summary>
        /// Adds an GameObject to the list.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public static void AddGameObject(GameObject.GameObject gameObject)
        {
            Engine.GameObjectsToAdd.Add(gameObject);
        }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <param name="layer">The render order.</param>
        public static void AddRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            Engine.Renderers[layer].Add(renderer);
            renderer.OnRendererCreate();
        }

        /// <summary>
        /// Start the engine ticks.
        /// </summary>
        /// <param name="window">The GameWindow the engine will be run on.</param>
        public static void StartEngine(OpenTK.Windowing.Desktop.GameWindow window)
        {
            Engine.GameWindow = window;
            Engine.Camera = new Camera.Camera();
            Engine.AddRenderer(Engine.Camera);

            window.UpdateFrame += Engine.Update;
            Engine.GameWindow.RenderFrame += Engine.Render;
            Engine.GameWindow.Resize += Engine.Resize;
            Engine.AddRenderer(new UiMatrixRenderer(), RenderLayer.UI);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);
        }

        /// <summary>
        /// Removes an GameObject from the World and Calls it OnDestroy() func.
        /// Also checks if there are Colliders from it and delets it.
        /// </summary>
        /// <param name="gameObject">The GameObject to Destroy.</param>
        public static void RemoveGameObject(GameObject.GameObject gameObject)
        {
            Engine.GameObjectsToRemove.Add(gameObject);
        }

        /// <summary>
        /// Adds an service to the engine.
        /// </summary>
        /// <param name="service">the service.</param>
        public static void AddService(IService service)
        {
            Service.ServiceInfo serviceInfo = (Service.ServiceInfo)service.GetType().GetCustomAttributes(typeof(Service.ServiceInfo), false)[0];
            Engine.Services.Add(serviceInfo.Name, service);
            service.OnUpdatableCreate();
            Engine.AddRenderer(service, serviceInfo.RenderLayer);
        }

        /// <summary>
        /// Gets a service by name.
        /// </summary>
        /// <param name="key">the name.</param>
        /// <returns>the service.</returns>
        public static IService GetService(string key)
        {
            return Engine.Services[key];
        }

        /// <summary>
        /// Gets service by type.
        /// </summary>
        /// <typeparam name="T">the type.</typeparam>
        /// <returns>the service.</returns>
        public static T GetService<T>()
            where T : IService
        {
            Service.ServiceInfo serviceInfo = (Service.ServiceInfo)Attribute.GetCustomAttributes(typeof(T), typeof(Service.ServiceInfo))[0];
            return (T)Engine.GetService(serviceInfo.Name);
        }

        /// <summary>
        /// Removes an GameObject from the World and Calls it OnDestroy() func.
        /// Also checks if there are Colliders from it and delets it.
        /// </summary>
        /// <param name="gameObject">The GameObject to Destroy.</param>
        private static void ExcludeGameObject(GameObject.GameObject gameObject)
        {
            Engine.RemoveRenderer(gameObject.SpriteRenderer);
            gameObject.OnUpdatableDestroy();
            Engine.GameObjects.Remove(gameObject);
            if (Engine.Colliders.Contains(gameObject))
            {
                Engine.Colliders.Remove(gameObject);
            }

            // GC.Collect();
        }

        /// <summary>
        /// Adds an GameObject to the list.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        private static void ImplementGameObject(GameObject.GameObject gameObject)
        {
            Engine.GameObjects.Add(gameObject);

            if (gameObject.Sprite != null)
            {
                SpriteRenderer renderer = new SpriteRenderer(gameObject.Sprite, gameObject);
                gameObject.SpriteRenderer = renderer;
                Engine.AddRenderer(renderer);
            }

            gameObject.OnUpdatableCreate();
        }

        /// <summary>
        /// Removes an rendere.
        /// </summary>
        /// <param name="renderer">The rendere to Remove.</param>
        private static void RemoveRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            Engine.Renderers[layer].Remove(renderer);
        }

        private static void Update(FrameEventArgs args)
        {
            float elapsed = (float)MathHelper.Clamp(args.Time, 0, 0.08);
            foreach (IService service in Engine.Services.Values)
            {
                service.OnUpdate(elapsed);
            }

            foreach (GameObject.GameObject gameObject in Engine.GameObjects)
            {
                gameObject.OnUpdate(elapsed);
            }

            foreach (GameObject.GameObject g in Engine.GameObjectsToRemove)
            {
                Engine.ExcludeGameObject(g);
            }

            Engine.GameObjectsToRemove.Clear();

            foreach (GameObject.GameObject g in Engine.GameObjectsToAdd)
            {
                Engine.ImplementGameObject(g);
            }

            Engine.GameObjectsToAdd.Clear();
        }

        private static void Resize(ResizeEventArgs args)
        {
            foreach (KeyValuePair<RenderLayer, List<IRenderer>> item in Engine.Renderers)
            {
                item.Value.ForEach(renderer => renderer.Resize(args));
            }
        }

        private static void Render(FrameEventArgs args)
        {
            Engine.GameWindow.SwapBuffers();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach (KeyValuePair<RenderLayer, List<IRenderer>> item in Engine.Renderers)
            {
                item.Value.ForEach(renderer => renderer.Render(args));
            }
        }
    }
}
