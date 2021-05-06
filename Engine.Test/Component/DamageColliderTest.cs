﻿using System;
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
            Engine.Engine.Colliders.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.AddGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.DamageCollider(10, 1));

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);

            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90, "Got:"+ takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP());
        }

        [TestMethod]
        public void TestDealingDMGWithCollider()
        {
            Engine.Engine.Colliders.Clear();
            Engine.Engine.GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.AddGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

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
            Engine.Engine.Colliders.Clear();
            Engine.Engine.GameObjects.Clear();

            GameObject takeingDMG = new GameObject(0, 0, 1, 1, null);
            takeingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            takeingDMG.AddComponent(new Engine.Component.DamageCollider(10,1));
            takeingDMG.AddComponent(new Engine.Component.Collider());
            Engine.Engine.AddGameObject(takeingDMG);
            Engine.Engine.Colliders.Add(takeingDMG);

            GameObject doingDMG = new GameObject(0, 0, 1, 1, null);
            doingDMG.AddComponent(new Engine.Component.Collider());
            doingDMG.AddComponent(new Engine.Component.DamageCollider(10, 1));
            doingDMG.AddComponent(new Engine.Component.HealthPoints(100, 100));
            Engine.Engine.AddGameObject(doingDMG);
            Engine.Engine.Colliders.Add(doingDMG);

            doingDMG.OnUpdate(0.1f);
            takeingDMG.OnUpdate(0.1f);
            Assert.IsTrue(takeingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
            Assert.IsTrue(doingDMG.GetComponent<Engine.Component.HealthPoints>().GetCurrHP() == 90);
        }
    }
}
