// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Engine;
    using Engine.Component;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Sprite;
    using Game.Player;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static int currentHP = 0;
        private static float uiScale = 1.0f;

        private static float xOffset = 25;
        private static float yOffset = 25;
        private static int screenX;
        private static int screenY;

        private static int backgroundXSize = 300;
        private static int backgroundYSize = 60;

        private static HealthbarPlayer instance;
        private static Player player;
        private Assembly assembly;
        private Sprite indicators;
        private Sprite background;
        private float hTexX0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthbarPlayer"/> class.
        /// </summary>
        public HealthbarPlayer()
        {
            instance = this;

            foreach (IRectangle r in Engine.Colliders)
            {
                if (r is Player)
                {
                    GameObject g = (GameObject)r;
                    player = (Player)g;
                }
            }

            this.assembly = Assembly.GetExecutingAssembly();
            using Stream indicatorSpriteStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.Healthbar.heartsheet_new.png");
            this.indicators = new Sprite(indicatorSpriteStream, false);

            using Stream backgroundSpriteStream = this.assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.Healthbar.background2.png");
            this.background = new Sprite(backgroundSpriteStream, false);
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

            this.RenderBackground();

            currentHP = player.GetComponent<HealthPoints>().GetCurrHP();
            this.hTexX0 = currentHP / 100f;

            // heart outline
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, yOffset);

            GL.TexCoord2(1, 1f / 20f);
            GL.Vertex2(xOffset + (220 * uiScale), yOffset);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + (220 * uiScale), yOffset + (18 * uiScale));

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset, yOffset + (18 * uiScale));

            GL.End();

            // heart inlay
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f, 0f, 0f, 1.0f)); // change color of inlay here
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(xOffset, yOffset);

            GL.TexCoord2(this.hTexX0, 0);
            GL.Vertex2(xOffset + ((220 * uiScale) * this.hTexX0), yOffset);

            GL.TexCoord2(this.hTexX0, 1f / 20f);
            GL.Vertex2(xOffset + ((220 * uiScale) * this.hTexX0), yOffset + (18 * uiScale));

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, yOffset + (18 * uiScale));

            GL.End();

            // Debug.WriteLine("Y0: " + this.hTexY0 + "hTexY1: " + this.hTexY1);
        }

        /// <summary>
        /// function to render healthbar background.
        /// </summary>
        public void RenderBackground()
        {
            // healthbar background
            GL.BindTexture(TextureTarget.Texture2D, this.background.Handle);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(xOffset * uiScale, screenY - yOffset - (60 * uiScale));

            GL.TexCoord2(1, 0);
            GL.Vertex2(backgroundXSize * uiScale, screenY - yOffset - (60 * uiScale));

            GL.TexCoord2(1, 1);
            GL.Vertex2(backgroundXSize * uiScale, screenY - yOffset);

            GL.TexCoord2(0, 1);
            GL.Vertex2(xOffset, screenY - yOffset);

            GL.End();
        }

        /// <summary>
        /// function to render indicators of healthbar.
        /// </summary>
        public void RenderIndicators(Color4 color)
        {
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, yOffset);

            GL.TexCoord2(1, 1f / 20f);
            GL.Vertex2(xOffset + (220 * uiScale), yOffset);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + (220 * uiScale), yOffset + (18 * uiScale));

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset, yOffset + (18 * uiScale));

            GL.End();
        }

        /// <summary>
        /// Resize function.
        /// </summary>
        /// <param name="args">args.</param>
        public void Resize(ResizeEventArgs args)
        {
            // Debug.WriteLine(args.Size);
            screenX = args.Size.X;
            screenY = args.Size.Y;
            return;
        }

        /// <summary>
        /// OnCreate function.
        /// </summary>
        public void OnRendererCreate()
        {
            return;
        }
    }
}
