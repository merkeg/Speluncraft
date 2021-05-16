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
    using Game.Gun;
    using Engine.Renderer.Sprite;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static int currentHP = 0;
        private static float uiScale = 1.8f;

        private static float xOffset = 25;
        private static float yOffset = 25;
        private static int screenX;
        private static int screenY;

        private static int backgroundXSize = 300;
        private static int backgroundYSize = 60;
        private static int indicatorsXsize = 220;
        private static int indicatorsYsize = 18; // Height of only one indicator (one heart)

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

            this.RenderIndicators();
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
            GL.Vertex2(xOffset, screenY - yOffset - (backgroundYSize * uiScale));

            GL.TexCoord2(1, 0);
            GL.Vertex2(backgroundXSize * uiScale, screenY - yOffset - (backgroundYSize * uiScale));

            GL.TexCoord2(1, 1);
            GL.Vertex2(backgroundXSize * uiScale, screenY - yOffset);

            GL.TexCoord2(0, 1);
            GL.Vertex2(xOffset, screenY - yOffset);

            GL.End();
        }

        /// <summary>
        /// function to render indicators of healthbar.
        /// </summary>
        public void RenderIndicators() // needs cleanup or shortening for code metrics.
        {
            float relpos = 32 * uiScale;

            // outline
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 95, 1.0f / 255 * 84, 1.0f / 255 * 68, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * uiScale), screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * uiScale), screenY - yOffset - relpos);

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - relpos);

            GL.End();

            // inlay
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 200, 0.0f, 0.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(xOffset + 10, screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(this.hTexX0, 0);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * this.hTexX0 * uiScale), screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(this.hTexX0, 1f / 20f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * this.hTexX0 * uiScale), screenY - yOffset - relpos);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - relpos);

            GL.End();

            relpos = 15 + (15 * (float)(uiScale * 0.07)); // set offset for next indicatorbar

            // outline
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 95, 1.0f / 255 * 84, 1.0f / 255 * 68, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f * 3f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f * 3f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * uiScale), screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f * 4f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * uiScale), screenY - yOffset - relpos);

            GL.TexCoord2(0, 1f / 20f * 4f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - relpos);

            GL.End();

            // inlay
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 104, 1.0f / 255 * 167, 1.0f / 255 * 220, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * 1 * uiScale), screenY - yOffset - (indicatorsYsize * uiScale) - relpos);

            GL.TexCoord2(1, 1f / 20f * 3f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * 1 * uiScale), screenY - yOffset - relpos);

            GL.TexCoord2(0, 1f / 20f * 3f);
            GL.Vertex2(xOffset + 10, screenY - yOffset - relpos);

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
