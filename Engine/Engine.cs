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
            this.Colliders = new List<Rectangle>();
            this.Renderers = new List<Renderer.IRenderer>();
        }

        /// <summary>
        /// Gets a list of GameObjects in the game.
        /// </summary>
        public List<GameObject.GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Gets a list of Renderers in the game.
        /// </summary>
        public List<Renderer.IRenderer> Renderers { get; private set; }

        /// <summary>
        /// Gets a list of the colliders in the game.
        /// </summary>
        public List<Rectangle> Colliders { get; private set; }

        /// <summary>
        /// Gets the GameWindow the Engine runs on.
        /// </summary>
        public OpenTK.Windowing.Desktop.GameWindow GameWindow { get; private set; }

        /// <summary>
        /// Gets the game Camera.
        /// </summary>
        public Camera.Camera Camera { get; private set; }

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
            this.Renderers.Add(renderer);
            renderer.OnCreate();

            // Set the SwapBuffer to the last position.
            this.GameWindow.RenderFrame += renderer.Render;

            this.GameWindow.Resize += renderer.Resize;
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
            this.GameWindow.RenderFrame += this.SwapBuffers;
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
        }

        private void Update(OpenTK.Windowing.Common.FrameEventArgs args)
        {
            this.GameObjects.ForEach(gameObject => gameObject.OnUpdate((float)args.Time));
        }

        private void SwapBuffers(FrameEventArgs args)
        {
            this.GameWindow.SwapBuffers();
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
