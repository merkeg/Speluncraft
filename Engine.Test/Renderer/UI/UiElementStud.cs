using Engine.GameObject;
using Engine.Renderer.Text;
using Engine.Renderer.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace EngineTest.Renderer.UI
{
    public class UiElementStud : UiElement
    {
        public UiElementStud(Rectangle bounds, Color4 backgroundColor) : base(bounds, backgroundColor, null)
        {
        }

        public override void OnRendererCreate()
        {
        }

        public override void OnRender(FrameEventArgs args)
        {
        }
    }
}