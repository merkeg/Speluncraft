using Engine.GameObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineTest.Component
{
    [TestClass]
    public class UndoOverlapCollisionResponseTest
    {
        [TestMethod]
        public void TestColliderUp()
        {
            Engine.Engine.Colliders.Clear();
            GameObject g = new GameObject(0.25f, 0.75f, 0.5f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Colliders.Add(r);
            Engine.Engine.ImplementGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            g.AddComponent(p);

            p.AddVelocitY(0.1f);

            Assert.IsTrue(g.Intersects(r));

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.01f);
            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().Y == 0);
            Assert.IsTrue(g.MinY == r.MaxY);
            Assert.IsTrue(g.GetComponent<Engine.Component.UndoOverlapCollisionResponse>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderDown()
        {
            Engine.Engine.Colliders.Clear();
            GameObject g = new GameObject(0.2f, -0.5f, 0.5f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Colliders.Add(r);
            Engine.Engine.ImplementGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            g.AddComponent(p);

            p.AddVelocitY(0.1f);

            Assert.IsTrue(g.Intersects(r));

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.01f);
            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().Y == 0);
            Assert.IsTrue(g.MinY == r.MinY - g.SizeY);
            Assert.IsFalse(g.GetComponent<Engine.Component.UndoOverlapCollisionResponse>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderLeft()
        {
            Engine.Engine.Colliders.Clear();

            GameObject g = new GameObject(-0.5f, 0, 1f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Colliders.Add(r);
            Engine.Engine.ImplementGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            g.AddComponent(p);

            p.AddVelocityX(0.1f);

            Assert.IsTrue(g.Intersects(r));

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.01f);
            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().X == 0,"Expeceted 0, got: " + p.GetVelocity().X + "|" + g.MinX +" " +g.MinY);
            Assert.IsTrue(g.MinX == r.MinX - g.SizeX);
            Assert.IsFalse(g.GetComponent<Engine.Component.UndoOverlapCollisionResponse>().GetGroundTouchedFlag());
        }

        [TestMethod]
        public void TestColliderRight()
        {
            Engine.Engine.Colliders.Clear();
            GameObject g = new GameObject(0.7f, 0, 1f, 1, null);
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Engine.Engine.Colliders.Add(r);
            Engine.Engine.ImplementGameObject(g);
            Engine.Component.Physics p = new Engine.Component.Physics();

            g.AddComponent(new Engine.Component.UndoOverlapCollisionResponse());
            g.AddComponent(p);

            p.AddVelocityX(0.1f);

            Assert.IsTrue(g.Intersects(r));

            Engine.Engine.GetService<Engine.Service.CollisionService>().OnUpdate(0.01f);
            g.OnUpdate(0.01f);

            Assert.IsFalse(g.Intersects(r));
            Assert.IsTrue(p.GetVelocity().X == 0);
            Assert.IsTrue(g.MinX == r.MaxX);
            Assert.IsFalse(g.GetComponent<Engine.Component.UndoOverlapCollisionResponse>().GetGroundTouchedFlag());
        }
    }
}
