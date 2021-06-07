// <copyright file="ParticleEmitterData.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Particle
{
    using System.Collections.Generic;
    using global::Engine.GameObject;

    /// <summary>
    /// Data class.
    /// </summary>
    public class ParticleEmitterData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEmitterData"/> class.
        /// </summary>
        /// <param name="emitter">Particle emitter.</param>
        /// <param name="position">Position.</param>
        /// <param name="emitterLifetime">Lifetime of the emitter.</param>
        public ParticleEmitterData(ParticleEmitter emitter, IRectangle position, float emitterLifetime)
        {
            this.Position = position;
            this.EmitterLifetime = emitterLifetime;
            this.EmitterLived = 0;
            this.EmitterCooldown = 0;
            this.Particles = new List<Particle>();
            this.Emitter = emitter;
        }

        /// <summary>
        /// Gets or sets the list of particles.
        /// </summary>
        public List<Particle> Particles { get; set; }

        /// <summary>
        /// Gets or sets the Emitter lifetime.
        /// </summary>
        public float EmitterLifetime { get; set; }

        /// <summary>
        /// Gets or sets the lived time.
        /// </summary>
        public float EmitterLived { get; set; }

        /// <summary>
        /// Gets or sets the cooldown.
        /// </summary>
        public float EmitterCooldown { get; set; }

        /// <summary>
        /// Gets or sets position.
        /// </summary>
        public IRectangle Position { get; set; }

        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public ParticleEmitter Emitter { get; set; }
    }
}