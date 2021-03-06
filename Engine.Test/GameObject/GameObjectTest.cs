namespace EngineTest
{
    using Engine.Component;
    using Engine.GameObject;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameObjectTest
    {
        [TestMethod]
        public void TestGameObjectIntersects()
        {
            GameObject go1 = new GameObject(0, 0, 5, 5, null);
            GameObject go2 = new GameObject(1, 1, 5, 5, null);
            GameObject go3 = new GameObject(10, 10, 15, 15, null);

            Assert.IsTrue(go1.Intersects(go2), "Both GameObjects should be intersecting");
            Assert.IsTrue(go2.Intersects(go1), "Both GameObjects should be intersecting");

            Assert.IsFalse(go1.Intersects(go3), "Both GameObjects should not be intersecting");
            Assert.IsFalse(go2.Intersects(go3), "Both GameObjects should not be intersecting");

            Assert.IsFalse(go1.Intersects(go1));
            go2 = go1;
            Assert.IsFalse(go1.Intersects(go2));
            IRectangle r1 = (IRectangle)go1;
            Assert.IsFalse(go1.Intersects(r1));
        }

        [TestMethod]
        public void TestGameObjectComponent()
        {
            GameObject go1 = new GameObject(0, 0, 5, 5, null);
            GameObject go2 = new GameObject(1, 1, 5, 5, null);

            go1.AddComponent(new Physics());

            Physics component = go1.GetComponent<Physics>();
            Assert.IsNotNull(component, "Component should not be null");
            go1.RemoveComponent(component);

            component = go1.GetComponent<Physics>();
            Assert.IsNull(component, "Component should be null");
        }

        [TestMethod]
        public void TestGameObjectRect()
        {
            GameObject go1 = new GameObject(0, 0, 5, 5, null);
            go1.MaxX = 2;
            go1.MaxY = 2;
            
            Assert.IsTrue(go1.SizeX == 2);
            Assert.IsTrue(go1.SizeY == 2);

            go1.Mirrored = true;
            Assert.IsTrue(go1.Mirrored == true);
        }
    }
}
