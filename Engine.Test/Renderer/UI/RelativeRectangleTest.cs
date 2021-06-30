using Engine.GameObject;
using Engine.Renderer.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Renderer.UI
{
    [TestClass]
    public class RelativeRectangleTest
    {

        [TestMethod]
        public void TestRelativeRectangle()
        {
            Rectangle rectangle = new Rectangle(1, 1, 1, 1);
            RelativeRectangle relativeRectangle = new RelativeRectangle(rectangle, 1, 1, 2, 2);
            
            Assert.IsTrue(relativeRectangle.MinX == 2);
            Assert.IsTrue(relativeRectangle.MinY == 2);
            
            Assert.IsTrue(relativeRectangle.MaxX == 4);
            Assert.IsTrue(relativeRectangle.MaxY == 4);
            
            Assert.IsTrue(relativeRectangle.SizeX == 2);
            Assert.IsTrue(relativeRectangle.SizeY == 2);
            
        }
        
        [TestMethod]
        public void TestRelativeRectangleOther()
        {
            Rectangle rectangle = new Rectangle(1, 1, 4, 4);
            RelativeRectangle relativeRectangle = new RelativeRectangle(rectangle, 1, 1, 1, 1, RelativeRectangleXAlignment.Right, RelativeRectangleYAlignment.Bottom);
            
            Assert.IsTrue(relativeRectangle.MinX == 6);
            Assert.IsTrue(relativeRectangle.MinY == 6);
            
            Assert.IsTrue(relativeRectangle.MaxX == 7);
            Assert.IsTrue(relativeRectangle.MaxY == 7);
        }
    }
}