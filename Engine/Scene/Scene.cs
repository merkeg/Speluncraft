// <copyright file="Scene.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Scene
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Sprite;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Scene class.
    /// </summary>
    public abstract class Scene : IScene
    {
        static Scene()
        {
            if (Scene.Current == null)
            {
                Engine.ChangeScene(new DefaultScene());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this.Renderers = new Dictionary<RenderLayer, List<IRenderer>>();
            this.GameObjects = new List<GameObject>();
            this.Colliders = new List<IRectangle>();

            foreach (RenderLayer layer in (RenderLayer[])Enum.GetValues(typeof(RenderLayer)))
            {
                this.Renderers.Add(layer, new List<IRenderer>());
            }
        }

        /// <summary>
        /// Gets or sets the current Scene.
        /// </summary>
        public static Scene Current { get; set; }

        /// <inheritdoc/>
        public List<GameObject> GameObjects { get; set; }

        /// <inheritdoc/>
        public Dictionary<RenderLayer, List<IRenderer>> Renderers { get; set; }

        /// <inheritdoc/>
        public List<IRectangle> Colliders { get; set; }

        /// <inheritdoc/>
        public abstract void OnSceneLoad();

        /// <inheritdoc/>
        public virtual void OnSceneUnload()
        {
            foreach (GameObject gO in this.GameObjects.ToList())
            {
                this.RemoveGameObject(gO);
            }

            foreach (RenderLayer layer in this.Renderers.Keys.ToList())
            {
                this.Renderers[layer].ToList().ForEach(renderer => Engine.RemoveRenderer(renderer, layer));
                this.Renderers[layer].Clear();
            }

            this.GameObjects.Clear();
            this.Colliders.Clear();
        }

        /// <summary>
        /// Add gameObject.
        /// </summary>
        /// <param name="gameObject">the gO to add.</param>
        public void AddGameObject(GameObject gameObject)
        {
            this.GameObjects.Add(gameObject);
            if (gameObject.Sprite != null)
            {
                SpriteRenderer renderer = new SpriteRenderer(gameObject.Sprite, gameObject);
                gameObject.SpriteRenderer = renderer;
                this.AddRenderer(renderer);
            }

            gameObject.OnUpdatableCreate();
        }

        /// <summary>
        /// Removes an GameObject from the World and Calls it OnDestroy() func.
        /// Also checks if there are Colliders from it and delets it.
        /// </summary>
        /// <param name="gameObject">The GameObject to Destroy.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject.SpriteRenderer != null)
            {
                this.RemoveRenderer(gameObject.SpriteRenderer);
            }

            gameObject.OnUpdatableDestroy();
            this.GameObjects.Remove(gameObject);
            if (this.Colliders.Contains(gameObject))
            {
                this.Colliders.Remove(gameObject);
            }
        }

        /// <summary>
        /// Adds an renderer to the list.
        /// </summary>
        /// <param name="renderer">The renderer to add.</param>
        /// <param name="layer">The render order.</param>
        public void AddRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            this.Renderers[layer].Add(renderer);
            renderer.OnRendererCreate();
            if (Engine.GameWindow != null)
            {
                renderer.Resize(new ResizeEventArgs(Engine.GameWindow.Size));
            }
        }

        /// <summary>
        /// Removes an renderer.
        /// </summary>
        /// <param name="renderer">The renderer to Remove.</param>
        /// <param name="layer">Render layer.</param>
        public void RemoveRenderer(IRenderer renderer, RenderLayer layer = RenderLayer.GAME)
        {
            renderer.OnRendererDelete();
            if (this.Renderers[layer].Contains(renderer))
            {
                this.Renderers[layer].Remove(renderer);
            }
        }
    }
}