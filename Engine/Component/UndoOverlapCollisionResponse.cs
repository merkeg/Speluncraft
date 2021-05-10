// <copyright file="UndoOverlapCollisionResponse.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Component
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Adds a Collider to a GameObject that undoes overlaps with other Colliders.
    /// </summary>
    public class UndoOverlapCollisionResponse : CollisionResponse
    {
        /// <summary>
        /// 0 = Up in distance array.
        /// </summary>
        public const int Up = 0;

        /// <summary>
        /// 1 = Down in distance array.
        /// </summary>
        public const int Down = 1;

        /// <summary>
        /// 2 = Down in distance array.
        /// </summary>
        public const int Left = 2;

        /// <summary>
        /// 3 = Down in distance array.
        /// </summary>
        public const int Right = 3;

        /// <summary>
        /// Const for X Axsis.
        /// </summary>
        public const bool X = true;

        /// <summary>
        /// Const for Y Axsis.
        /// </summary>
        public const bool Y = false;

        /// <summary>
        /// Flag is set, when Groud was touched, needed for player jump reset.
        /// </summary>
        private bool touchedGround;

        /// <summary>
        /// Get the Flag, if ground was touched on a collsion.
        /// </summary>
        /// <returns>The Flag if ground was touced.</returns>
        public bool GetGroundTouchedFlag()
        {
            return this.touchedGround;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.touchedGround = false;

            List<GameObject.IRectangle> sideCollisionRight = new List<GameObject.IRectangle>(); // Those overlaps will only be undone after up and down collisions.
            List<GameObject.IRectangle> sideCollisionsLeft = new List<GameObject.IRectangle>();

            foreach (GameObject.IRectangle r in Engine.GetService<Service.CollisionService>().GetCollosions(this.GameObject))
            {
                if (this.GameObject.Intersects(r))
                {
                    float[] distance = new float[4];
                    distance[Up] = Math.Abs(r.MaxY - this.GameObject.MinY);
                    distance[Down] = Math.Abs(r.MinY - this.GameObject.MaxY);
                    distance[Left] = Math.Abs(r.MinX - this.GameObject.MaxX);
                    distance[Right] = Math.Abs(r.MaxX - this.GameObject.MinX);

                    float minDistance = distance[0];
                    int indexOfMinDistance = 0;

                    for (int i = 1; i < distance.Length; i++)
                    {
                        if (distance[i] < minDistance)
                        {
                            minDistance = distance[i];
                            indexOfMinDistance = i;
                        }
                    }

                    switch (indexOfMinDistance)
                    {
                        case Up:
                            this.GameObject.MinY = r.MaxY;
                            this.touchedGround = true;
                            this.ResetVelocity(Y);
                            break;
                        case Down:
                            this.GameObject.MinY = r.MinY - this.GameObject.SizeY;
                            this.ResetVelocity(Y);
                            break;
                        case Left:
                            sideCollisionsLeft.Add(r);
                            break;
                        case Right:
                            sideCollisionRight.Add(r);
                            break;
                    }
                }
            }

            // After the Up and Down Collisions have been checked, look if other collsion are still there.
            foreach (GameObject.IRectangle r in sideCollisionsLeft)
            {
                if (this.GameObject.Intersects(r))
                {
                    this.GameObject.MinX = r.MinX - this.GameObject.SizeX;
                    this.ResetVelocity(X);
                }
            }

            foreach (GameObject.IRectangle r in sideCollisionRight)
            {
                if (this.GameObject.Intersects(r))
                {
                    this.GameObject.MinX = r.MaxX;
                    this.ResetVelocity(X);
                }
            }
        }

        private void ResetVelocity(bool axsis)
        {
            Physics physics = this.GameObject.GetComponent<Physics>();
            if (physics != null)
            {
                if (axsis == X)
                {
                    physics.SetVelocity(0, physics.GetVelocity().Y);
                }
                else
                {
                    physics.SetVelocity(physics.GetVelocity().X, 0);
                }
            }
        }
    }
}
