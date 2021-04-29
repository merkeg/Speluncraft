// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using Engine;
    using Engine.Component;
    using Engine.GameObject;
    using Engine.Renderer;
    using Game.Player;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static HealthbarPlayer instance;
        private static Player player;
        private static Engine engine;

        private static int currentHP = 0;

        private static Vector2d origin;
        private static float xMinOffset = -5.8f; // -6.0 Top
        private static float yMinOffset = -0.1f; // 0.0 Left
        private float width = 3.5f;
        private float height = 0.3f;
        private int health;
        private float hpScale;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthbarPlayer"/> class.
        /// </summary>
        public HealthbarPlayer()
        {
            engine = Engine.Instance();
            instance = this;

            foreach (IRectangle r in engine.Colliders)
            {
                if (r is Player)
                {
                    GameObject g = (GameObject)r;
                    player = (Player)g;
                }
            }

            origin = new Vector2d(player.MinX, player.MinY);

            // Scale for healthbar calc
            this.health = player.GetComponent<HealthPoints>().GetMaxHP();
            this.hpScale = this.width / this.health;
        }

        /// <summary>
        /// Function so player can retrieve always the same healthbar instance (Singleton).
        /// </summary>
        /// <returns>HealtbarPlayer instance.</returns>
        public static HealthbarPlayer Instance()
        {
            if (instance == null)
            {
                instance = new HealthbarPlayer();
            }

            return instance;
        }

        /// <summary>
        /// Render Healthbar.
        /// </summary>
        /// <param name="args">.</param>
        public void Render(FrameEventArgs args)
        {
            currentHP = player.GetComponent<HealthPoints>().GetCurrHP();

            /* remove only for debugging
            // Console.WriteLine("Current HP player: " + currentHP + "\nPosition: " + player.MinX + " | " + player.MinY);
            */

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(new Color4(1.0f, 0, 0, 0.5f));
            GL.Vertex2(player.MinX - origin.X + xMinOffset, player.MinY - origin.Y + yMinOffset);
            GL.Vertex2(player.MinX - origin.X + xMinOffset + (this.hpScale * currentHP), player.MinY - origin.Y + yMinOffset);
            GL.Vertex2(player.MinX - origin.X + xMinOffset + (this.hpScale * currentHP), player.MinY - origin.Y + yMinOffset - this.height);
            GL.Vertex2(player.MinX - origin.X + xMinOffset, player.MinY - origin.Y + yMinOffset - this.height);
            GL.End();
        }

        /// <summary>
        /// Resize function.
        /// </summary>
        /// <param name="args">args.</param>
        public void Resize(ResizeEventArgs args)
        {
            return;
        }

        /// <summary>
        /// OnCreate function.
        /// </summary>
        public void OnCreate()
        {
            return;
        }
    }
}
