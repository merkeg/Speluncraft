// <copyright file="ParticleService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Particle;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Particle service.
    /// </summary>
    [ServiceInfo("particle", RenderLayer.PARTICLE)]
    public class ParticleService : IService
    {
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleService"/> class.
        /// </summary>
        public ParticleService()
        {
            this.Emitters = new List<ParticleEmitterData>();
            this.random = new Random();
        }

        /// <summary>
        /// Gets the emitters.
        /// </summary>
        public List<ParticleEmitterData> Emitters { get; private set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            foreach (ParticleEmitterData particleEmitter in this.Emitters.ToList())
            {
                foreach (Particle particle in particleEmitter.Particles)
                {
                    if (particle.Sprite == null)
                    {
                        GL.BindTexture(TextureTarget.Texture2D, 0);
                        GL.Color4(particle.Color);
                        GL.Begin(PrimitiveType.Quads);
                        GL.Vertex2(particle.Bounds.MinX, particle.Bounds.MinY);
                        GL.Vertex2(particle.Bounds.MaxX, particle.Bounds.MinY);
                        GL.Vertex2(particle.Bounds.MaxX, particle.Bounds.MaxY);
                        GL.Vertex2(particle.Bounds.MinX, particle.Bounds.MaxY);
                        GL.End();
                    }
                    else
                    {
                        GL.BindTexture(TextureTarget.Texture2D, particle.Sprite.Handle);
                        GL.Color4(particle.Color);
                        GL.Begin(PrimitiveType.Quads);
                        GL.TexCoord2(particle.Sprite.TexX0, particle.Sprite.TexY0);
                        GL.Vertex2(particle.Bounds.MinX, particle.Bounds.MinY);
                        GL.TexCoord2(particle.Sprite.TexX1, particle.Sprite.TexY0);
                        GL.Vertex2(particle.Bounds.MaxX, particle.Bounds.MinY);
                        GL.TexCoord2(particle.Sprite.TexX1, particle.Sprite.TexY1);
                        GL.Vertex2(particle.Bounds.MaxX, particle.Bounds.MaxY);
                        GL.TexCoord2(particle.Sprite.TexX0, particle.Sprite.TexY1);
                        GL.Vertex2(particle.Bounds.MinX, particle.Bounds.MaxY);
                        GL.End();
                    }
                }
            }
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
            foreach (ParticleEmitterData particleEmitterData in this.Emitters.ToList())
            {
                ParticleEmitter particleEmitter = particleEmitterData.Emitter;
                particleEmitterData.EmitterLived += frameTime;
                particleEmitterData.EmitterCooldown += frameTime;
                if (particleEmitterData.EmitterLifetime > 0 && particleEmitterData.EmitterLived >= particleEmitterData.EmitterLifetime)
                {
                    this.Emitters.Remove(particleEmitterData);
                    continue;
                }

                if (particleEmitterData.EmitterCooldown >= particleEmitter.SpawnCooldown)
                {
                    particleEmitterData.EmitterCooldown = 0;
                    for (int i = 0; i < this.random.Next((int)particleEmitter.SpawnAmountMin, (int)particleEmitter.SpawnAmountMax + 1); i++)
                    {
                        particleEmitterData.Particles.Add(particleEmitter.NewParticle(particleEmitterData.Position));
                    }
                }

                for (int i = 0; i < particleEmitterData.Particles.Count; i++)
                {
                    Particle particle = particleEmitterData.Particles[i];
                    particle.Lived += frameTime;

                    if (particle.Lived >= particle.Lifetime)
                    {
                        particleEmitterData.Particles.Remove(particle);
                        continue;
                    }

                    particle.Bounds.MinX += frameTime * particle.Velocity.X;
                    particle.Bounds.MinY += (frameTime * particle.Velocity.Y) - (particle.Lived * particleEmitter.Gravity * frameTime);
                }
            }
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
            this.Emitters.Clear();
        }

        /// <summary>
        /// Add an emitter.
        /// </summary>
        /// <param name="emitter">The emitter.</param>
        /// <param name="position">Position.</param>
        /// <param name="lifetime">The lifetime of the emitter. Set to 0 to disable deletion.</param>
        /// <returns>ParticleEmitterData.</returns>
        public ParticleEmitterData Emit(ParticleEmitter emitter, IRectangle position, float lifetime)
        {
            ParticleEmitterData data = new ParticleEmitterData(emitter, position, lifetime);
            this.Emitters.Add(data);
            return data;
        }

        /// <summary>
        /// Remove an emitter.
        /// </summary>
        /// <param name="data">The data you got from the emit method.</param>
        public void Remove(ParticleEmitterData data)
        {
            this.Emitters.Remove(data);
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}