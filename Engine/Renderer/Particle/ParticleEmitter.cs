// <copyright file="ParticleEmitter.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Particle
{
    using System;
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Renderer.UI;
    using OpenTK.Mathematics;

    /// <summary>
    /// Particle emitter class.
    /// </summary>
    public class ParticleEmitter
    {
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEmitter"/> class.
        /// </summary>
        /// <param name="particleLifetimeMin">min lifetime.</param>
        /// <param name="particleLifetimeMax">max lifetime.</param>
        /// <param name="spawnAmountMin">spawn min.</param>
        /// <param name="spawnAmountMax">spawn max.</param>
        /// <param name="velocityMin">vel min.</param>
        /// <param name="velocityMax">vel max.</param>
        /// <param name="spawnCooldown">cooldown.</param>
        /// <param name="particleSizeMin">Size min.</param>
        /// <param name="particleSizeMax">Size max.</param>
        /// <param name="gravity">Gravity in y axis.</param>
        public ParticleEmitter(float particleLifetimeMin, float particleLifetimeMax, uint spawnAmountMin, uint spawnAmountMax, Vector2 velocityMin, Vector2 velocityMax, float spawnCooldown, float particleSizeMin, float particleSizeMax, float gravity)
        {
            this.ParticleLifetimeMin = particleLifetimeMin;
            this.ParticleLifetimeMax = particleLifetimeMax;
            this.VelocityMin = velocityMin;
            this.VelocityMax = velocityMax;
            this.SpawnCooldown = spawnCooldown;
            this.SpawnAmountMin = spawnAmountMin;
            this.SpawnAmountMax = spawnAmountMax;
            this.random = new Random();
            this.Sprites = new List<ISprite>();
            this.Colours = new List<Color4>();
            this.ParticleSizeMin = particleSizeMin;
            this.ParticleSizeMax = particleSizeMax;
            this.Gravity = gravity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEmitter"/> class.
        /// </summary>
        public ParticleEmitter()
        {
            this.ParticleLifetimeMin = 1;
            this.ParticleLifetimeMax = 2;
            this.VelocityMin = new Vector2(-2, -2);
            this.VelocityMax = new Vector2(2, 2);
            this.SpawnCooldown = 0.2f;
            this.SpawnAmountMin = 2;
            this.SpawnAmountMax = 3;
            this.ParticleSizeMin = 0.1f;
            this.ParticleSizeMax = 0.3f;
            this.random = new Random();
            this.Sprites = new List<ISprite>();
            this.Colours = new List<Color4>();
            this.Gravity = 1;
        }

        /// <summary>
        /// Gets or sets the Sprites to use.
        /// </summary>
        public List<ISprite> Sprites { get; set; }

        /// <summary>
        /// Gets or sets the colours to use.
        /// </summary>
        public List<Color4> Colours { get; set; }

        /// <summary>
        /// Gets or sets the min lifetime of the particle.
        /// </summary>
        public float ParticleLifetimeMin { get; set; }

        /// <summary>
        /// Gets or sets the max lifetime of the particle.
        /// </summary>
        public float ParticleLifetimeMax { get; set; }

        /// <summary>
        /// Gets or sets the gravity in y axis.
        /// </summary>
        public float Gravity { get; set; }

        /// <summary>
        /// Gets or sets the min velocity.
        /// </summary>
        public Vector2 VelocityMin { get; set; }

        /// <summary>
        /// Gets or sets the max velocity.
        /// </summary>
        public Vector2 VelocityMax { get; set; }

        /// <summary>
        /// Gets or sets the cooldown between Particle spawns.
        /// </summary>
        public float SpawnCooldown { get; set; }

        /// <summary>
        /// Gets or sets the min spawn amount.
        /// </summary>
        public uint SpawnAmountMin { get; set; }

        /// <summary>
        /// Gets or sets the max spawn amount.
        /// </summary>
        public uint SpawnAmountMax { get; set; }

        /// <summary>
        /// Gets or sets the particle size min.
        /// </summary>
        public float ParticleSizeMin { get; set; }

        /// <summary>
        /// Gets or sets the particle size max.
        /// </summary>
        public float ParticleSizeMax { get; set; }

        /// <summary>
        /// Gets an random sprite from the list.
        /// </summary>
        /// <returns>Random sprite.</returns>
        public ISprite GetRandomSprite()
        {
            if (this.Sprites.Count == 0)
            {
                return null;
            }

            if (this.Sprites.Count == 1)
            {
                return this.Sprites[0];
            }

            return this.Sprites[this.random.Next(0, this.Sprites.Count)];
        }

        /// <summary>
        /// Gets a random colour from the list or defaults to white.
        /// </summary>
        /// <returns>Random color.</returns>
        public Color4 GetRandomColour()
        {
            if (this.Colours.Count == 0)
            {
                return Color4.White;
            }

            if (this.Colours.Count == 1)
            {
                return this.Colours[0];
            }

            return this.Colours[this.random.Next(0, this.Colours.Count)];
        }

        /// <summary>
        /// Generates a new particle.
        /// </summary>
        /// <param name="position">Position of the spawn point</param>
        /// <returns>new Particle.</returns>
        public Particle NewParticle(IRectangle position)
        {
            float lifetime = ((float)this.random.NextDouble() * (this.ParticleLifetimeMax - this.ParticleLifetimeMin)) + this.ParticleLifetimeMin;
            float x = ((this.VelocityMax.X - this.VelocityMin.X) * (float)this.random.NextDouble()) + this.VelocityMin.X;
            float y = ((this.VelocityMax.Y - this.VelocityMin.Y) * (float)this.random.NextDouble()) + this.VelocityMin.Y;
            float particleSize = ((float)this.random.NextDouble() * (this.ParticleSizeMax - this.ParticleSizeMin)) + this.ParticleSizeMin;
            return new Particle(lifetime, new Vector2(x, y), new Rectangle(position.MinX, position.MinY, particleSize, particleSize), this.GetRandomSprite(), this.GetRandomColour());
        }
    }
}