using Engine.GameObject;
using Engine.Renderer.Particle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace EngineTest.Renderer.Particle
{
    [TestClass]
    public class ParticleEmitterTest
    {
        private ParticleEmitter emitter;
        private float particleLifetimeMin;
        private float particleLifetimeMax;
        private uint spawnAmountMin;
        private uint spawnAmountMax;
        private Vector2 velocityMin;
        private Vector2 velocityMax;
        private float spawnCooldown;
        private float particleSizeMin;
        private float particleSizeMax;
        private float gravity;

        [TestInitialize]
        public void Init()
        {
            particleLifetimeMin = 1;
            particleLifetimeMax = 2;
            spawnAmountMin = 1;
            spawnAmountMax = 2;
            velocityMin = new Vector2(-2, -2);
            velocityMax = new Vector2(2, 2);
            spawnCooldown = 1;
            particleSizeMin = 0.1f;
            particleSizeMax = 0.3f;
            gravity = 1;
            this.emitter = new ParticleEmitter(particleLifetimeMin, particleLifetimeMax, spawnAmountMin, spawnAmountMax,
                velocityMin, velocityMax, spawnCooldown, particleSizeMin, particleSizeMax, gravity);
        }

        [TestMethod]
        public void TestGetters()
        {
            Assert.IsTrue(this.emitter.Gravity == this.gravity);
            Assert.IsTrue(this.emitter.SpawnCooldown == this.spawnCooldown);
            Assert.IsTrue(this.emitter.ParticleLifetimeMax == this.particleLifetimeMax);
            Assert.IsTrue(this.emitter.ParticleLifetimeMin == this.particleLifetimeMin);
            Assert.IsTrue(this.emitter.ParticleSizeMax == this.particleSizeMax);
            Assert.IsTrue(this.emitter.ParticleSizeMin == this.particleSizeMin);
            Assert.IsTrue(this.emitter.SpawnAmountMax == this.spawnAmountMax);
            Assert.IsTrue(this.emitter.SpawnAmountMin == this.spawnAmountMin);
            
            Assert.IsTrue(this.emitter.VelocityMax == this.velocityMax);
            Assert.IsTrue(this.emitter.VelocityMin == this.velocityMin);
            
            Assert.IsTrue(this.emitter.Colours.Count == 0);
            Assert.IsTrue(this.emitter.Sprites.Count == 0);
        }
        
        [TestMethod]
        public void TestNewParticleInVelocityBounds()
        {
            Rectangle rectangle = new Rectangle(0, 0, 1, 1);
            Engine.Renderer.Particle.Particle particle = this.emitter.NewParticle(rectangle);

            Vector2 velocity = particle.Velocity;
            
            // Test if Velocity is in bounds
            Assert.IsTrue(velocity.X >= this.emitter.VelocityMin.X);
            Assert.IsTrue(velocity.Y >= this.emitter.VelocityMin.Y);
            
            Assert.IsTrue(velocity.X <= this.emitter.VelocityMax.X);
            Assert.IsTrue(velocity.Y <= this.emitter.VelocityMax.Y);
        }
        
        [TestMethod]
        public void TestNewParticleInSizeBounds()
        {
            Rectangle rectangle = new Rectangle(0, 0, 1, 1);
            Engine.Renderer.Particle.Particle particle = this.emitter.NewParticle(rectangle);

            IRectangle bounds = particle.Bounds;
            
            // Test if Velocity is in bounds
            Assert.IsTrue(bounds.MaxX - bounds.MinX >= this.emitter.ParticleSizeMin);
            Assert.IsTrue(bounds.MaxY - bounds.MinY >= this.emitter.ParticleSizeMin);
            
            Assert.IsTrue(bounds.MaxX - bounds.MinX <= this.emitter.ParticleSizeMax);
            Assert.IsTrue(bounds.MaxY - bounds.MinY <= this.emitter.ParticleSizeMax);
        }
        
    }
}