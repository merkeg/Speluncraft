using Engine.GameObject;
using Engine.Renderer.Particle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Renderer.Particle
{
    [TestClass]
    public class ParticleEmitterDataTest
    {

        [TestMethod]
        public void TestClass()
        {
            Rectangle pos = new Rectangle(0, 0, 1, 1);
            float lifetime = 1;
            ParticleEmitterData data = new ParticleEmitterData(null, pos, lifetime);
            
            Assert.IsTrue(data.Particles.Count == 0);
            Assert.IsTrue(data.EmitterLifetime == lifetime);
            Assert.IsTrue(data.EmitterLived == 0);
            Assert.IsTrue(data.Position == pos);
        }
    }
}