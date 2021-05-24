// <copyright file="GameObject.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>
namespace Engine.GameObject
{
    using System;
    using System.Collections.Generic;
    using global::Engine.Renderer.Sprite;

    /// <summary>
    /// The GameObject class which active game elements are deriving from.
    /// </summary>
    public class GameObject : IRectangle, IUpdatable
    {
        /// <summary>
        /// The sprite the GameObject is drawn with.
        /// </summary>
        private ISprite sprite;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="minX">the X-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="minY">the Y-Coordinate of bottom left point, of the GameObject.</param>
        /// <param name="sizeX">the width of the GameObject.</param>
        /// <param name="sizeY">the height of the GameObject.</param>
        /// /// <param name="sprite">The sprite.</param>
        public GameObject(float minX, float minY, float sizeX, float sizeY, ISprite sprite)
        {
            this.MinX = minX;
            this.MinY = minY;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.Components = new List<IComponent>();
            this.sprite = sprite;
        }

        /// <summary>
        /// Gets or sets the width of the GameObject.
        /// </summary>
        public float SizeX { get; set; }

        /// <summary>
        /// Gets or sets the height of the GameObject.
        /// </summary>
        public float SizeY { get; set; }

        /// <summary>
        /// Gets or sets the X-Coordinate of the top right point.
        /// </summary>
        public virtual float MaxX
        {
            get => this.MinX + this.SizeX;
            set => this.SizeX = value - this.MinX;
        }

        /// <summary>
        /// Gets or sets the Y-Coordinate of the top right point.
        /// </summary>
        public virtual float MaxY
        {
            get => this.MinY + this.SizeY;
            set => this.SizeY = value - this.MinY;
        }

        /// <summary>
        /// Gets or sets the X-Coordinate of bottom left point, of the GameObject.
        /// </summary>
        public float MinX { get; set; }

        /// <summary>
        /// Gets or sets the Y-Coordinate of bottom left point, of the GameObject.
        /// </summary>
        public float MinY { get; set; }

        /// <summary>
        /// Gets or sets the Components.
        /// </summary>
        public List<IComponent> Components { get; protected set; }

        /// <summary>
        /// Gets or sets the Sprite of the GameObject.
        /// </summary>
        public ISprite Sprite
        {
        get => this.sprite;

        set
            {
                if (this.SpriteRenderer != null)
                {
                    this.SpriteRenderer.Sprite = value;
                }

                this.sprite = value;
            }
        }

        /// <inheritdoc/>
        public bool Mirrored { get; set; }

        /// <summary>
        /// Gets or sets the Sprite renderer.
        /// </summary>
        internal SpriteRenderer SpriteRenderer { get; set; }

        /// <summary>
        /// Checks if the other element intersects with this GameObject.
        /// </summary>
        /// <param name="rectangle">The other element to check at.</param>
        /// <returns>the value if the other element intersects with the gameobject.</returns>
        public bool Intersects(IRectangle rectangle)
        {
            if (rectangle == this)
            {
                return false;
            }

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
        public void AddComponent(IComponent component)
        {
            this.Components.Add(component);
            component.SetGameObject(this);
        }

        /// <summary>
        /// Removes a component from the GameObject.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        public void RemoveComponent(IComponent component)
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
        /// Get all components.
        /// </summary>
        /// <returns>All Componts of this GameObject, as a List.</returns>
        public List<IComponent> GetComponents()
        {
            return this.Components;
        }

        /// <summary>
        /// Called if the GameObject is created.
        /// </summary>
        public virtual void OnUpdatableCreate()
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
        public virtual void OnUpdatableDestroy()
        {
            this.Components.ForEach(component => component.OnDestroy());
            this.Components.Clear();
        }

        // OnUpdateCleanUp wird nach dem Update in jeden Frame ausgeführt.
    }
}