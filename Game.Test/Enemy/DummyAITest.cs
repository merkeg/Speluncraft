using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest.Enemy
{
    [TestClass]
    public class DummyAITest
    {
        [TestMethod]
        public void TestDummy()
        {
            Engine.Engine.Instance().GameObjects.Clear();
            Engine.Engine.Instance().Colliders.Clear();

            Engine.GameObject.Rectangle floor = new Engine.GameObject.Rectangle(0, 0, 5, 1);
            Engine.Engine.Instance().Colliders.Add(floor);

            Game.Enemy.DummyAI ai = new Game.Enemy.DummyAI(0, 1, 1, 1, null, 5);
            Engine.Engine.Instance().GameObjects.Add(ai);

            for (int i = 0; i < 100; i++)
            {
                ai.OnUpdate(0.1f);
            }

            Assert.IsTrue(ai.MinY == 1);
        }
    }
}
