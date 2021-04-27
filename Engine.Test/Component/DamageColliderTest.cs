using System;
using System.Collections.Generic;
using System.Text;
using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Component
{
    [TestClass]
    public class DamageColliderTest
    {
        [TestMethod]
        public void TestDealingDMG()
        {
            Engine.Engine.Instance().Colliders.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.Instance().AddGameObject(takeingDMG);
            Engine.Engine.Instance().Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.DamageCollider(10, 1));

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);

            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:"+ takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
        }

        [TestMethod]
        public void TestDealingDMGWithCollider()
        {
            Engine.Engine.Instance().Colliders.Clear();
            Engine.Engine.Instance().GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.Instance().AddGameObject(takeingDMG);
            Engine.Engine.Instance().Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.Collider());
            doingDMG.AddComponent(new Engine.Component.DamageCollider(10, 1));

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);

            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:" + takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
        }

        [TestMethod]
        public void TestDoingDMGWithTwoColliders()
        {
            Engine.Engine.Instance().Colliders.Clear();
            Engine.Engine.Instance().GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            takeingDMG.AddComponent(new Engine.Component.DamageCollider(10,1));
            takeingDMG.AddComponent(new Engine.Component.Collider());
            Engine.Engine.Instance().AddGameObject(takeingDMG);
            Engine.Engine.Instance().Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.Collider());
            doingDMG.AddComponent(new Engine.Component.DamageCollider(10, 1));
            doingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.Instance().AddGameObject(doingDMG);
            Engine.Engine.Instance().Colliders.Add(doingDMG);

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
        }
    }
}
