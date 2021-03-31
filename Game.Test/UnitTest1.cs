using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.IsFalse(false, "Sollte durchlaufen");
        }
    }
}
