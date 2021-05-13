// <copyright file="CollisionService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System.Collections.Generic;
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
                if (g.GetComponent<Component.CollisionResponse>() != null)
                {
                    List<GameObject.IRectangle> collisionThisGameObjectHas = new List<GameObject.IRectangle>();
                    foreach (GameObject.IRectangle r in Engine.Colliders)
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
            try
            {
               output = this.collision[gameObject];
            }
            catch
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
    }
}