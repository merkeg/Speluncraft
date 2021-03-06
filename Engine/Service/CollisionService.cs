// <copyright file="CollisionService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// TestService class.
    /// </summary>
    [ServiceInfo("collisionService")]
    public class CollisionService : IService
    {
        private Dictionary<GameObject.GameObject, List<GameObject.IRectangle>> collision = new Dictionary<GameObject.GameObject, List<GameObject.IRectangle>>();

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
            this.collision = new Dictionary<GameObject.GameObject, List<GameObject.IRectangle>>();
            foreach (GameObject.GameObject g in Engine.GameObjects)
            {
                bool hasSubbed = false;
                foreach (GameObject.IComponent c in g.GetComponents().ToList())
                {
                    if (c is ICollosionServiceSubscriber)
                    {
                        hasSubbed = true;
                        break;
                    }
                }

                if (hasSubbed)
                {
                    List<GameObject.IRectangle> collisionThisGameObjectHas = new List<GameObject.IRectangle>();
                    foreach (GameObject.IRectangle r in Engine.Colliders.ToList())
                    {
                        if (g.Intersects(r))
                        {
                            collisionThisGameObjectHas.Add(r);
                        }
                    }

                    this.collision.Add(g, collisionThisGameObjectHas);
                }
            }
        }

        /// <summary>
        /// Get all the IRectangle a GameObject collided with, this frame.
        /// </summary>
        /// <param name="gameObject">The GameObject, you want the collisions for.</param>
        /// <returns>A List of the Collisions.</returns>
        public List<GameObject.IRectangle> GetCollosions(GameObject.GameObject gameObject)
        {
            List<GameObject.IRectangle> output;
            if (this.collision.ContainsKey(gameObject))
            {
                output = this.collision[gameObject];
            }
            else
            {
                output = new List<GameObject.IRectangle>();
            }

            return output;
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
        }

        /// <inheritdoc/>
        public void SceneChangeCleanup()
        {
            this.collision = new Dictionary<GameObject.GameObject, List<GameObject.IRectangle>>();
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}