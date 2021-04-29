// <copyright file="Engine.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System;
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Sprite;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public class Engine
    {
        private static Engine instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            this.GameObjects = new List<GameObject.GameObject>();
            this.Colliders = new List<IRectangle>();
            this.GameObjectsToRemove = new List<GameObject.GameObject>();
            this.GameObjectsToAdd = new List<GameObject.GameObject>();
            this.Renderers = new Dictionary<Renderer.RenderLayer, List<Renderer.IRenderer>>();

            foreach (Renderer.RenderLayer layer in (Renderer.RenderLayer[])Enum.GetValues(typeof(Renderer.RenderLayer)))
            {
                this.Renderers.Add(layer, new List<Renderer.IRenderer>());
            }
        }

        /// <summary>
        /// Gets a list of GameObjects in the game.
        /// </summary>
        public List<GameObject.GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Gets the renderers.
        /// </summary>
        public Dictionary<Renderer.RenderLayer, List<Renderer.IRenderer>> Renderers { get; private set; }

        /// <summary>
        /// Gets a list of the colliders in the game.
        /// </summary>
        public List<IRectangle> Colliders { get; private set; }

        /// <summary>
        /// Gets the GameWindow the Engine runs on.
        /// </summary>
        public OpenTK.Windowing.Desktop.GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Gets the game Camera.
        /// </summary>
        public Camera.Camera Camera { get; private set; }

        /// <summary>
        /// Gets Here you can add GameObjects that should be removed this frame.
        /// </summary>
        public List<GameObject.GameObject> GameObjectsToRemove { get; private set; }

        /// <summary>
        /// Gets Here you can add GameObjects that should be added next frame.
        /// </summary>
        public List<GameObject.GameObject> GameObjectsToAdd { get; private set; }

        /// <summary>
        /// Method to get the engine instance.
        /// </summary>
        /// <returns>Engine instance.</returns>
        public static Engine Instance()
        {
            if (instance == null)
            {
                instance = new Engine();
            }

            return instance;
        }

        /// <summary>
        /// Adds an GameObject to the list.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public void AddGameObject(GameObject.GameObject gameObject)
        {
            this.GameObjects.Add(gameObject);

            if (gameObject.Sprite != null)
            {
                SpriteRenderer renderer = new SpriteRenderer(gameObject.Sprite, gameObject);
                gameObject.SpriteRenderer = renderer;
                this.AddRenderer(renderer);
            }

            gameObject.OnCreated();
        }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        public void AddRenderer(Renderer.IRenderer renderer)
        {
            this.AddRenderer(renderer, Renderer.RenderLayer.GAME);
        }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <param name="layer">The render order.</param>
        public void AddRenderer(Renderer.IRenderer renderer, Renderer.RenderLayer layer)
        {
            this.Renderers[layer].Add(renderer);
            renderer.OnCreate();
        }

        /// <summary>
        /// Start the engine ticks.
        /// </summary>
        /// <param name="window">The GameWindow the engine will be run on.</param>
        public void StartEngine(OpenTK.Windowing.Desktop.GameWindow window)
        {
            this.GameWindow = window;
            this.Camera = new Camera.Camera();
            this.AddRenderer(this.Camera);

            window.UpdateFrame += this.Update;
            this.GameWindow.RenderFrame += this.Render;
            this.GameWindow.Resize += this.Resize;
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
        }

        /// <summary>
        /// Removes an GameObject from the World and Calls it OnDestroy() func.
        /// Also checks if there are Colliders from it and delets it.
        /// </summary>
        /// <param name="gameObject">The GameObject to Destroy.</param>
        private void RemoveGameObject(GameObject.GameObject gameObject)
        {
            this.RemoveRenderer(gameObject.SpriteRenderer);
            gameObject.OnDestroy();
            this.GameObjects.Remove(gameObject);
            if (this.Colliders.Contains(gameObject))
            {
                this.Colliders.Remove(gameObject);
            }
        }

        /// <summary>
        /// Removes an rendere.
        /// </summary>
        /// <param name="renderer">The rendere to Remove.</param>
        private void RemoveRenderer(Renderer.IRenderer renderer)
        {
            this.Renderers.Remove(renderer);
            this.GameWindow.RenderFrame -= renderer.Render;

            this.GameWindow.Resize -= renderer.Resize;
        }

        private void Update(OpenTK.Windowing.Common.FrameEventArgs args)
        {
            float elapsed = (float)MathHelper.Clamp(args.Time, 0, 0.08);
            this.GameObjects.ForEach(gameObject => gameObject.OnUpdate(elapsed));

            foreach (GameObject.GameObject g in this.GameObjectsToRemove)
            {
                this.RemoveGameObject(g);
            }

            this.GameObjectsToRemove.Clear();

            foreach (GameObject.GameObject g in this.GameObjectsToAdd)
            {
                this.AddGameObject(g);
            }

            this.GameObjectsToAdd.Clear();
        }

        private void Resize(ResizeEventArgs args)
        {
            foreach (KeyValuePair<Renderer.RenderLayer, List<Renderer.IRenderer>> item in this.Renderers)
            {
                item.Value.ForEach(renderer => renderer.Resize(args));
            }
        }

        private void Render(FrameEventArgs args)
        {
            this.GameWindow.SwapBuffers();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach (KeyValuePair<Renderer.RenderLayer, List<Renderer.IRenderer>> item in this.Renderers)
            {
                item.Value.ForEach(renderer => renderer.Render(args));
            }
        }
    }
}
