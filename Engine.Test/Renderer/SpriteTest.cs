using Engine.Renderer.Sprite;
using Engine.Renderer.Tile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

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
            
            Assert.IsTrue(sprite.Width == stud.TileSizeX);
            Assert.IsTrue(sprite.Height == stud.TileSizeY);
            
        }
        
        [TestMethod]
        public void TestAnimatedSpriteTimings()
        {
            ITilesheet stud = new TilesheetStud();
            Keyframe kf1 = new Keyframe(0, 0, 1);
            Keyframe kf2 = new Keyframe(1, 0, 1);
            AnimatedSprite sprite = new AnimatedSprite(stud, new[] {kf1, kf2});
            
            Assert.IsTrue(sprite.TexX0 == 0);
            Assert.IsTrue(sprite.TexX1 == stud.TileTexSizeX);
            Assert.IsTrue(sprite.TexY0 == 0);
            Assert.IsTrue(sprite.TexY1 == stud.TileTexSizeY);
            Assert.IsTrue(sprite.State == 0);
            
            sprite.TimeElapsed(5);
            Assert.IsTrue(sprite.TexX0 > 0);
            sprite.TimeElapsed(5);
            Assert.IsTrue(sprite.TexX0 == 0);
            
            sprite.Color = Color4.Aqua;
            Assert.IsTrue(sprite.Color == Color4.Aqua);
            
            sprite.SetState(1);
            Assert.IsTrue(sprite.State == 1);

            sprite.SetState(0);
            sprite.Paused = true;
            sprite.TimeElapsed(5);
            Assert.IsTrue(sprite.TexX0 == 0);
        }

        [TestMethod]
        public void TestKeyframes()
        {
            Keyframe[] frames = Keyframe.RangeX(0, 2, 0, 1);
            Assert.IsTrue(frames.Length == 3);
            
            frames = Keyframe.RangeY(0, 0, 2, 1);
            Assert.IsTrue(frames.Length == 3);
        }
    }
}