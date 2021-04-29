namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenTK.Graphics.OpenGL;
    using global::Engine.Renderer;
    using OpenTK.Windowing.Common;
    using OpenTK.Mathematics;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static HealthbarPlayer instance;
        private static OpenTK.Windowing.Desktop.GameWindow gamewindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthbarPlayer"/> class.
        /// </summary>
        public HealthbarPlayer()
        {
            gamewindow = Engine.Engine.Instance().GameWindow;
        }

        /// <summary>
        /// Function so player can retrieve always the same healthbar instance (Singleton).
        /// </summary>
        /// <returns>HealtbarPlayer instance</returns>
        public static HealthbarPlayer Instance()
        {
            if (instance == null)
            {
                instance = new HealthbarPlayer();
            }

            return instance;
        }

        public void Render(FrameEventArgs args)
        {
            GL.LineWidth(3);
            GL.Color3(System.Drawing.Color.Cyan);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(3.0, -8.0);
            GL.Vertex2(3.0, -5.0);
            GL.Vertex2(5.0, -5.0);
            GL.Vertex2(5.0, -8.0);
            GL.End();

            Vector2i size = gamewindow.ClientSize;
            Console.WriteLine(size);
        }

        public void Resize(ResizeEventArgs args)
        {
            return;
        }

        public void OnCreate()
        {
            return;
        }
    }
}
