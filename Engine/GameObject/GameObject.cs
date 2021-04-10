﻿// <copyright file="GameObject.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>
namespace Engine.GameObject
{
    using System.Collections.Generic;

    /// <summary>
    /// The GameObject class which active game elements are deriving from.
    /// </summary>
    public class GameObject : IRectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="sizeX">the width of the GameObject.</param>
        /// <param name="sizeY">the height of the GameObject.</param>
        public GameObject(float minX, float minY, float sizeX, float sizeY)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.Components = new List<Component.Component>();
        }

        /// <summary>
        /// Gets the width of the GameObject.
        /// </summary>
        public float SizeX { get; }

        /// <summary>
        /// Gets the hieght of the GameObject.
        /// </summary>
        public float SizeY { get; }

        /// <summary>
        /// Gets the X-Coordinate of the top right point, of the GameObject.
        /// </summary>
        public float MaxX => this.MinX + this.SizeX;

        /// <summary>
        /// Gets the Y-Coordinate of the top right point, of the GameObject.
        /// </summary>
        public float MaxY => this.MinY + this.SizeY;

        /// <summary>
        /// Gets the X-Coordinate of bottom left point, of the GameObject.
        /// </summary>
        public float MinX { get; internal set; }

        /// <summary>
        /// Gets the Y-Coordinate of bottom left point, of the GameObject.
        /// </summary>
        public float MinY { get; internal set; }

        /// <summary>
        /// Gets or sets Components.
        /// </summary>
        public List<Component.Component> Components { get; protected set; }

        /// <summary>
        /// Checks if the other element intersects with this GameObject.
        /// </summary>
        /// <param name="rectangle">The other element to check at.</param>
        /// <returns>the value if the other element intersects with the gameobject.</returns>
        public bool Intersects(IRectangle rectangle)
        {
            bool result = false;
            bool noXintersect = (this.MaxX <= rectangle.MinX) || (this.MinX >= rectangle.MaxX);
            bool noYintersect = (this.MaxY <= rectangle.MinY) || (this.MinY >= rectangle.MaxY);
            result = !(noXintersect || noYintersect);
            return result;
        }

        /// <summary>
        /// Adds a component from the GameObject.
        /// </summary>
        /// <param name="component">The component to add.</param>
        public void AddComponent(Component.Component component)
        {
            this.Components.Add(component);
            component.SetGameObject(this);
        }

        /// <summary>
        /// Removes a component from the GameObject.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        public void RemoveComponent(Component.Component component)
        {
            this.Components.Remove(component);
            component.SetGameObject(null);
        }

        /// <summary>
        /// Gets the component from the GameObject.
        /// </summary>
        /// <typeparam name="T">Component.</typeparam>
        /// <returns>The component or null if not existant.</returns>
        public T GetComponent<T>()
            where T : Component.Component
        {
            foreach (Component.Component component in this.Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        /// <summary>
        /// Called if the GameObject is created.
        /// </summary>
        public virtual void OnCreated()
        {
            this.Components.ForEach(component => component.OnCreated());
        }

        /// <summary>
        /// Called every gametick.
        /// </summary>
        /// <param name="frameTime">Time between the frame.</param>
        public virtual void OnUpdate(float frameTime)
        {
            this.Components.ForEach(component => component.OnUpdate(frameTime));
        }

        /// <summary>
        /// Called if the GameObject is destroyed.
        /// </summary>
        public virtual void OnDestroy()
        {
            this.Components.ForEach(component => component.OnDestroy());
        }
    }
}