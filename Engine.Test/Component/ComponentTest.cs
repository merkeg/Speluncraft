using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Component
{
    [TestClass]
    public class ComponentTest
    {

        [TestMethod]
        public void TestComponent()
        {
            Engine.Component.Component component = new ComponentStub();
            component.OnCreated();
            component.OnDestroy();
            
            Assert.IsNull(component.GetGameObject());
        }
    }
}