// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System.IO;
    using Engine.Renderer.Sprite;
    using System.Reflection;
    using Engine;
    using Engine.Component;
    using Engine.GameObject;
    using Engine.Renderer;
    using Game.Player;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using System.Diagnostics;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static HealthbarPlayer instance;
        private static Player player;
        private static Engine engine;

        private static int currentHP = 0;
        private static float uiScale = 0.5f;
        private static float xOffset = 20;
        private static float yOffset = 20;
        private float hTexY0;
        private float hTexY1;

        private Assembly assembly;
        private Sprite sprite;

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


            this.assembly = Assembly.GetExecutingAssembly();
            using Stream spriteStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.Healthbar.heart_sheet.png");
            this.sprite = new Sprite(spriteStream, false);
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

            this.hTexY0 = 1 - (currentHP / 5f * (1f / 20f));
            this.hTexY1 = this.hTexY0 + (1f / 20f);

            // Debug.WriteLine("Y0: " + this.hTexY0 + "hTexY1: " + this.hTexY1);

            GL.BindTexture(TextureTarget.Texture2D, this.sprite.Handle);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, this.hTexY0);
            GL.Vertex2(xOffset, yOffset);

            GL.TexCoord2(1, this.hTexY0);
            GL.Vertex2(xOffset + (640 * uiScale), yOffset);

            GL.TexCoord2(1, this.hTexY1);
            GL.Vertex2(xOffset + (640 * uiScale), yOffset + (64 * uiScale));

            GL.TexCoord2(0, this.hTexY1);
            GL.Vertex2(xOffset, yOffset + (64 * uiScale));

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
