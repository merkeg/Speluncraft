using System;
using System.Collections.Generic;
using System.Text;
using Engine.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Component
{
    [TestClass]
    public class HealtPointsTest
    {
        [TestMethod]
        public void testHP()
        {
            HealthPoints hp = new HealthPoints(100, 50);

            Assert.IsFalse(hp.GetIsDeadFlag());
            Assert.IsTrue(hp.GetCurrHP() == 50);
            Assert.IsTrue(hp.GetMaxHP() == 100);

            hp.SetHP(75);
            Assert.IsFalse(hp.GetIsDeadFlag());
            Assert.IsTrue(hp.GetCurrHP() == 75);
            Assert.IsTrue(hp.GetMaxHP() == 100);

            hp.SetMaxHP(60);
            Assert.IsFalse(hp.GetIsDeadFlag());
            Assert.IsTrue(hp.GetCurrHP() == 60);
            Assert.IsTrue(hp.GetMaxHP() == 60);

            hp.AddHP(-50);
            Assert.IsFalse(hp.GetIsDeadFlag());
            Assert.IsTrue(hp.GetCurrHP() == 10);
            Assert.IsTrue(hp.GetMaxHP() == 60);

        }
    }
}
