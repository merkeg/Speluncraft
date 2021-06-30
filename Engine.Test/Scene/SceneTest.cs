using System;
using Engine.GameObject;
using Engine.Renderer;
using Engine.Scene;
using EngineTest.Renderer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Scene
{
    [TestClass]
    public class SceneTest
    {

        [TestMethod]
        public void TestScene()
        {
            DefaultScene scene = new DefaultScene();
            
            Assert.IsTrue(scene.Bundle == null);
            Assert.IsTrue(scene.Colliders.Count == 0);
            Assert.IsTrue(scene.Renderers.Count == Enum.GetValues(typeof(RenderLayer)).Length);
            Assert.IsTrue(scene.GameObjects.Count == 0);
        }

        [TestMethod]
        public void TestGameObject()
        {
            DefaultScene scene = new DefaultScene();
            GameObject gO = new GameObject(0, 0, 1, 1, null);
            
            scene.AddGameObject(gO);
            scene.Colliders.Add(gO);
            
            Assert.IsTrue(scene.GameObjects.Count == 1);
            Assert.IsTrue(scene.Colliders.Count == 1);
            
            scene.RemoveGameObject(gO);
            Assert.IsTrue(scene.GameObjects.Count == 0);
            Assert.IsTrue(scene.Colliders.Count == 0);
        }
        
        [TestMethod]
        public void TestRenderer()
        {
            DefaultScene scene = new DefaultScene();

            IRenderer renderer = new RendererStud();
            
            scene.AddRenderer(renderer);
            Assert.IsTrue(scene.Renderers[RenderLayer.GAME].Count == 1);
            
            scene.RemoveRenderer(renderer);
            Assert.IsTrue(scene.Renderers[RenderLayer.GAME].Count == 0);
        }
        
        [TestMethod]
        public void TestSceneUnload()
        {
            DefaultScene scene = new DefaultScene();

            GameObject gO = new GameObject(0, 0, 1, 1, null);
            
            scene.AddGameObject(gO);
            Assert.IsTrue(scene.GameObjects.Count == 1);
            
            scene.OnSceneUnload();
            
            Assert.IsTrue(scene.GameObjects.Count == 0);
        }
    }
}