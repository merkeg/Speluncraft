using Engine.GameObject;
using Engine.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Scene
{
    [TestClass]
    public class RectangleTest
    {

        [TestMethod]
        public void TestRectangle()
        {
            Rectangle rect = new Rectangle(0, 0, 1, 1);

            rect.MaxX = 2;
            rect.MaxY = 2;
            
            Assert.IsTrue(rect.SizeX == 2);
            Assert.IsTrue(rect.SizeY == 2);

            rect.Mirrored = true;
            
            Assert.IsTrue(rect.Mirrored == true);
        }
    }
}