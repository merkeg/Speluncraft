using System;
using System.Collections.Generic;
using System.Text;
using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Component
{
    [TestClass]
    public class DoDamageCollisionResponseTest
    {
        [TestMethod]
        public void TestDealingDMG()
        {
            Engine.Engine.Colliders.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.ImplementGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            Engine.Engine.ImplementGameObject(doingDMG);
            doingDMG.AddComponent(new Engine.Component.DoDamageCollisionResponse(10, 1));

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.1f);
            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);

            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:"+ takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDMGthisFrame());
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDmgToThose().Contains(takeingDMG));

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:" + takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
            Assert.IsFalse(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDMGthisFrame());
            Assert.IsFalse(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDmgToThose().Contains(takeingDMG));
        }

        [TestMethod]
        public void TestDealingDMGWithCollider()
        {
            Engine.Engine.Colliders.Clear();
            Engine.Engine.GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.ImplementGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            doingDMG.AddComponent(new Engine.Component.DoDamageCollisionResponse(10, 1));
            Engine.Engine.ImplementGameObject(doingDMG);

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.1f);
            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);

            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDMGthisFrame());
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:" + takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDmgToThose().Contains(takeingDMG));
        }

        [TestMethod]
        public void TestDoingDMGWithTwoColliders()
        {
            Engine.Engine.Colliders.Clear();
            Engine.Engine.GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            takeingDMG.AddComponent(new Engine.Component.DoDamageCollisionResponse(10,1));
            takeingDMG.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            Engine.Engine.ImplementGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            doingDMG.AddComponent(new Engine.Component.DoDamageCollisionResponse(10, 1));
            doingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.ImplementGameObject(doingDMG);
            Engine.Engine.Colliders.Add(doingDMG);

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.1f);
            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDMGthisFrame());
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDMGthisFrame());
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetIsCollided());
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetIsCollided());
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDmgToThose().Contains(takeingDMG));
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.DoDamageCollisionResponse>().GetDidDmgToThose().Contains(doingDMG));
        }
    }
}
