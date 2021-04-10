namespace Engine.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameObjectTest
    {
        [TestMethod]
        public void TestGameObjectIntersects()
        {
            GameObject.GameObject go1 = new GameObject.GameObject(0, 0, 5, 5);
            GameObject.GameObject go2 = new GameObject.GameObject(1, 1, 5, 5);
            GameObject.GameObject go3 = new GameObject.GameObject(10, 10, 15, 15);

            Assert.IsTrue(go1.Intersects(go2), "Both GameObjects should be intersecting");
            Assert.IsTrue(go2.Intersects(go1), "Both GameObjects should be intersecting");

            Assert.IsFalse(go1.Intersects(go3), "Both GameObjects should not be intersecting");
            Assert.IsFalse(go2.Intersects(go3), "Both GameObjects should not be intersecting");
        }

        [TestMethod]
        public void TestGameObjectComponents()
        {
            GameObject.GameObject go1 = new GameObject.GameObject(0, 0, 5, 5);
            GameObject.GameObject go2 = new GameObject.GameObject(1, 1, 5, 5);

            go1.AddComponent(new Component.Physics());

            Component.Physics component = go1.GetComponent<Component.Physics>();
            Assert.IsNotNull(component, "Component should not be null");
            go1.RemoveComponent(component);

            component = go1.GetComponent<Component.Physics>();
            Assert.IsNull(component, "Component should be null");
        }
    }
}
