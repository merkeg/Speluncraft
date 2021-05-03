using Engine.Renderer.UI.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace EngineTest.Renderer.UI
{
    [TestClass]
    public class GraphTest
    {

        [TestMethod]
        public void TestGraphDataSet()
        {
            GraphDataSet dataSet = new GraphDataSet(10, Color4.Aqua);
            dataSet.AddData(1);
            Assert.IsTrue((int)dataSet.Data[^1] == 1, "Wrong value");
        }
        
        [TestMethod]
        public void TestGraphDataSetBulk()
        {
            GraphDataSet dataSet = new GraphDataSet(10, Color4.Aqua);
            for (int i = 0; i < 10; i++)
            {
                dataSet.AddData(1);
            }

            int calc = 0;
            foreach (float f in dataSet.Data)
            {
                calc += (int) f;
            }
            
            Assert.IsTrue(calc == 10, "Shifted out?");
        }
    }
}