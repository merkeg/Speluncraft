using Engine.GameObject;
using Engine.Renderer.UI;
using Engine.Renderer.UI.Primitive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace EngineTest.Renderer.UI
{
    [TestClass]
    public class UiElementTest
    {

        [TestMethod]
        public void TestUiElementsText()
        {
            UiElementStud uiElement = new UiElementStud(new Rectangle(0, 0, 1, 1), Color4.Aqua);
            uiElement.AddText("test", Color4.Aqua, new Rectangle(0, 0, 1, 1), 0.1f);
            Assert.IsTrue(uiElement.Renderers.Count == 2, "Should be 2.");
        }
        
        [TestMethod]
        public void TestUiElementsGraph()
        {
            UiElementStud uiElement = new UiElementStud(new Rectangle(0, 0, 1, 1), Color4.Aqua);
            uiElement.AddGraph("Title", new Rectangle(0, 0, 1, 1), 0, 10);
            Assert.IsTrue(uiElement.Renderers.Count == 2, "Should be 2.");
        }
        
        [TestMethod]
        public void TestUiElementsQuad()
        {
            UiElementStud uiElement = new UiElementStud(new Rectangle(0, 0, 1, 1), Color4.Aqua);
            uiElement.AddQuad(new Rectangle(0, 0, 1, 1), Color4.Aqua);
            Assert.IsTrue(uiElement.Renderers.Count == 2, "Should be 2.");
        }

        [TestMethod]
        public void TestParams()
        {
            UiElementStud uiElement = new UiElementStud(new Rectangle(0, 0, 1, 1), Color4.Aqua);
            Assert.IsTrue(uiElement.Bounds.SizeX == 1);
            
            Assert.IsTrue(uiElement.BackgroundColor == Color4.Aqua);
            
            Assert.IsFalse(uiElement.Hidden);
        }
    }
}