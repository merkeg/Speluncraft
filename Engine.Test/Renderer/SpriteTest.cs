using Engine.Renderer.Sprite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Renderer
{
    [TestClass]
    public class SpriteTest
    {

        [TestMethod]
        public void TestAnimatedSprite()
        {
            TilesheetStud stud = new TilesheetStud();
            Keyframe kf1 = new Keyframe(0, 0, 1);
            Keyframe kf2 = new Keyframe(1, 0, 1);
            AnimatedSprite sprite = new AnimatedSprite(stud, new[] {kf1, kf2});
            
            Assert.IsTrue(sprite.Handle == stud.Handle, "Handle not identical to stud");
            Assert.IsTrue(sprite[0].X == kf1.X);
            Assert.IsTrue(sprite[0].Y == kf1.Y);
            
            Assert.IsTrue(sprite.Width == stud.TileSize);
            Assert.IsTrue(sprite.Height == stud.TileSize);
            
        }
        
        [TestMethod]
        public void TestAnimatedSpriteTimings()
        {
            TilesheetStud stud = new TilesheetStud();
            Keyframe kf1 = new Keyframe(0, 0, 1);
            Keyframe kf2 = new Keyframe(1, 0, 1);
            AnimatedSprite sprite = new AnimatedSprite(stud, new[] {kf1, kf2});
            
            Assert.IsTrue(sprite.TexX0 == 0);
            sprite.TimeElapsed(5);
            Assert.IsTrue(sprite.TexX0 > 0);
            sprite.TimeElapsed(5);
            Assert.IsTrue(sprite.TexX0 == 0);
            
        }
    }
}