using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineTest.Component
{
    [TestClass]
    public class ColliderTest
    {
        [TestMethod]
        public void TestColliderUp()
        {
            Engine.Engine.Instance().Colliders.Clear();
            GameObject g = new GameObject(0.25f, 0.75f, 0.5f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Instance().Colliders.Add(r);
            Engine.Engine.Instance().AddGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.Collider());
            g.AddComponent(p);

            p.AddVelocitY(0.1f);

            Assert.IsTrue(g.Intersects(r));

            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().Y == 0);
            Assert.IsTrue(g.MinY == r.MaxY);
            Assert.IsTrue(g.GetComponent<Engine.Component.Collider>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderDown()
        {
            Engine.Engine.Instance().Colliders.Clear();
            GameObject g = new GameObject(0.2f, -0.5f, 0.5f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Instance().Colliders.Add(r);
            Engine.Engine.Instance().AddGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.Collider());
            g.AddComponent(p);

            p.AddVelocitY(0.1f);

            Assert.IsTrue(g.Intersects(r));

            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().Y == 0);
            Assert.IsTrue(g.MinY == r.MinY - g.SizeY);
            Assert.IsFalse(g.GetComponent<Engine.Component.Collider>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderLeft()
        {
            Engine.Engine.Instance().Colliders.Clear();

            GameObject g = new GameObject(-0.5f, 0, 1f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Instance().Colliders.Add(r);
            Engine.Engine.Instance().AddGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.Collider());
            g.AddComponent(p);

            p.AddVelocityX(0.1f);

            Assert.IsTrue(g.Intersects(r));

            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().X == 0,"Expeceted 0, got: " + p.GetVelocity().X + "|" + g.MinX +" " +g.MinY);
            Assert.IsTrue(g.MinX == r.MinX - g.SizeX);
            Assert.IsFalse(g.GetComponent<Engine.Component.Collider>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderRight()
        {
            Engine.Engine.Instance().Colliders.Clear();
            GameObject g = new GameObject(0.7f, 0, 1f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Instance().Colliders.Add(r);
            Engine.Engine.Instance().AddGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.Collider());
            g.AddComponent(p);

            p.AddVelocityX(0.1f);

            Assert.IsTrue(g.Intersects(r));

            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().X == 0);
            Assert.IsTrue(g.MinX == r.MaxX);
            Assert.IsFalse(g.GetComponent<Engine.Component.Collider>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestCollidedList()
        {
            Engine.Engine.Instance().Colliders.Clear();
            GameObject g1 = new GameObject(0, 0, 1, 1, null);
            Rectangle r1 = new Rectangle(0, -0.5f, 1, 1);

            g1.AddComponent(new Engine.Component.Collider());

            Engine.Engine.Instance().Colliders.Add(r1);
            Engine.Engine.Instance().AddGameObject(g1);

            g1.OnUpdate(0.1f);

            Assert.IsTrue(g1.GetComponent<Engine.Component.Collider>().GetCollided()[0] == r1);

            g1.OnUpdate(0.1f);
            Assert.IsTrue(g1.GetComponent<Engine.Component.Collider>().GetCollided().Count == 0);

        }
    }
}
