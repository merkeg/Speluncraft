using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Engine.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Assert.IsFalse(false, "Richtig sollte falsch sein :^)");
        }
    }
}
