using Engine.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Scene
{
    [TestClass]
    public class BundleTest
    {

        [TestMethod]
        public void TestBundle()
        {
            Bundle bundle = new Bundle();
            bundle.Add("number", 1);
            bundle.Add("string", "Hello");
            
            Assert.IsTrue(bundle.Get("number", 0) == 1);
            Assert.IsTrue(bundle.Get("string", "not hello").Equals("Hello"));
            Assert.IsTrue(bundle.Get("anything", true));
        }
    }
}