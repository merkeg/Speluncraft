using System.Drawing;
using Engine.GameObject;
using Engine.Renderer;
using Engine.Scene;
using EngineTest.Renderer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest
{
    [TestClass]
    public class EngineTester
    {

        [TestMethod]
        public void TestEngineParams()
        {
            Engine.Engine.BackgroundColor = Color.Aqua;
            Assert.IsTrue(Engine.Engine.BackgroundColor == Color.Aqua);
            
            Engine.Engine.ChangeScene(new DefaultScene());
            Assert.IsTrue(Engine.Engine.Renderers.Count == 3, "Got " + Engine.Engine.Renderers.Count);

            Engine.Engine.GameIsRunning = true;
            Assert.IsTrue(Engine.Engine.GameIsRunning);

            Engine.Engine.OnSceneChange += SceneChangeStub;
            
            GameObject go1 = new GameObject(0, 0, 5, 5, null);
            Engine.Engine.AddGameObject(go1);
            Assert.IsTrue(Engine.Engine.GameObjects.Count == 1);

            Engine.Engine.RemoveGameObject(go1);
            Assert.IsTrue(Engine.Engine.GameObjects.Count == 0);
            
        }

        [TestMethod]
        public void TestEngineGameObjects()
        {
            Engine.Engine.ChangeScene(new DefaultScene());
            GameObject go1 = new GameObject(0, 0, 5, 5, null);
            Engine.Engine.AddGameObject(go1);
            Assert.IsTrue(Engine.Engine.GameObjects.Count == 1);

            Engine.Engine.RemoveGameObject(go1);
            Assert.IsTrue(Engine.Engine.GameObjects.Count == 0);
        }
        
        [TestMethod]
        public void TestEngineRenderers()
        {
            Engine.Engine.ChangeScene(new DefaultScene());

            IRenderer renderer = new RendererStud();
            
            Engine.Engine.AddRenderer(renderer);
            int count = Engine.Engine.Renderers[RenderLayer.GAME].Count;

            Engine.Engine.RemoveRenderer(renderer);
            Assert.IsTrue(Engine.Engine.Renderers[RenderLayer.GAME].Count == (count - 1));
        }

        [TestMethod]
        public void TestEngineCamera()
        {
            Camera camera = new Camera();
            Engine.Engine.Camera = camera;
            
            Assert.IsNotNull(camera);
        }
        
        private void SceneChangeStub(Engine.Scene.Scene oldScene, Engine.Scene.Scene newScene)
        {
            
        }
    }
}