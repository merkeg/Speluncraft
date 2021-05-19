// <copyright file="EngineGameObject.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine
{
    using System.Collections.Generic;
    using global::Engine.Renderer.Sprite;

    /// <summary>
    /// The Engine instance to bind with OpenGL.
    /// </summary>
    public partial class Engine
    {
        /// <summary>
        /// Gets a list of GameObjects in the game.
        /// </summary>
        public static List<GameObject.GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Adds an GameObject to the list.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public static void AddGameObject(GameObject.GameObject gameObject)
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
        /// Removes an GameObject from the World and Calls it OnDestroy() func.
        /// Also checks if there are Colliders from it and delets it.
        /// </summary>
        /// <param name="gameObject">The GameObject to Destroy.</param>
        public static void RemoveGameObject(GameObject.GameObject gameObject)
        {
            Engine.RemoveRenderer(gameObject.SpriteRenderer);
            gameObject.OnUpdatableDestroy();
            Engine.GameObjects.Remove(gameObject);
            if (Engine.Colliders.Contains(gameObject))
            {
                Engine.Colliders.Remove(gameObject);
            }
        }
    }
}