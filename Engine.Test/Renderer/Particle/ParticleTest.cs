using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace EngineTest.Renderer.Particle
{
    [TestClass]
    public class ParticleTest
    {

        [TestMethod]
        public void TestParticleClassCreation()
        {
            float lifetime = 1;
            Vector2 velocity = new Vector2(1, 1);
            Rectangle rectangle = new Rectangle(0, 0, 1, 1);
            Color4 color = Color4.Aqua;

            Engine.Renderer.Particle.Particle particle =
                new Engine.Renderer.Particle.Particle(lifetime, velocity, rectangle, null, color);
            
            Assert.IsTrue(particle.Bounds == rectangle);
            Assert.IsTrue(particle.Lifetime == lifetime);
            Assert.IsTrue(particle.Velocity == velocity);
            Assert.IsTrue(particle.Sprite == null);
            Assert.IsTrue(particle.Color == color);
            Assert.IsTrue(particle.Lived == 0);
        }
    }
}