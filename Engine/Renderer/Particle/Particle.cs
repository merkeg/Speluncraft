// <copyright file="Particle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Particle
{
    using global::Engine.GameObject;
    using OpenTK.Mathematics;

    /// <summary>
    /// The particle struct.
    /// </summary>
    public class Particle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        /// <param name="lifetime">Lifetime of particle.</param>
        /// <param name="velocity">Velocity of particle.</param>
        /// <param name="bounds">Bounds of particle.</param>
        /// <param name="sprite">Sprite of particle.</param>
        /// <param name="color">Color of the particle.</param>
        public Particle(float lifetime, Vector2 velocity, IRectangle bounds, Sprite.ISprite sprite, Color4 color)
        {
            this.Lifetime = lifetime;
            this.Velocity = velocity;
            this.Bounds = bounds;
            this.Sprite = sprite;
            this.Lived = 0;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the time already lived.
        /// </summary>
        public float Lived { get; set; }

        /// <summary>
        /// Gets or sets the lifetime
        /// </summary>
        public float Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the bounds
        /// </summary>
        public IRectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets the Sprite.
        /// </summary>
        public Sprite.ISprite Sprite { get; set; }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        public Color4 Color { get; set; }
    }
}