// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using Engine.Component;
    using Engine.Renderer;
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

        private static float xOffset = 25;
        private static float yOffset = 25;
        private static float healthbarHeight = 18 * 2.0f; // only change float value for healtbar scale

        private static float heightValue;
        private static Vector3 healthbarSpriteAspect = new Vector3(220, 18, 220f / 18f);
        private static Vector2 screenSize;

        private Player.Player player;
        private ISprite indicators;
        private float hTexX0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthbarPlayer"/> class.
        /// </summary>
        /// <param name="player">player.</param>
        public HealthbarPlayer(Player.Player player)
        {
            this.player = player;

            this.indicators = TextureAtlas.Sprites["healthbar_hearts"];
        }

        /// <summary>
        /// Render Healthbar.
        /// </summary>
        /// <param name="args">.</param>
        public void Render(FrameEventArgs args)
        {
            this.RenderWeaponSlot();

            currentHP = this.player.GetComponent<HealthPoints>().GetCurrHP();
            this.hTexX0 = currentHP / 100f;

            this.RenderIndicators();
        }

        /// <summary>
        /// function to render healthbar background.
        /// </summary>
        public void RenderWeaponSlot()
        {
            GL.BindTexture(TextureTarget.Texture2D, GunType.GunTypeArray[ItemShop.CurrentWeaponIndex].GunSprite.Handle);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1);
            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(64, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex2(64, 64);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 64);

            GL.End();
        }

        /// <summary>
        /// function to render indicators of healthbar.
        /// </summary>
        public void RenderIndicators() // needs cleanup or shortening for code metrics.
        {
            // outline
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 95, 1.0f / 255 * 84, 1.0f / 255 * 68, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, screenSize.Y - yOffset);

            GL.TexCoord2(1, 1f / 20f);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * heightValue), screenSize.Y - yOffset);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * heightValue), screenSize.Y - yOffset - heightValue);

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset, screenSize.Y - yOffset - heightValue);

            GL.End();

            // inlay
            /*
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 200, 0.0f, 0.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(xOffset + 10, screenY - yOffset - (indicatorsYsize * uiScale));

            GL.TexCoord2(this.hTexX0, 0);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * this.hTexX0 * uiScale), screenY - yOffset - (indicatorsYsize * uiScale));

            GL.TexCoord2(this.hTexX0, 1f / 20f);
            GL.Vertex2(xOffset + 10 + (indicatorsXsize * this.hTexX0 * uiScale), screenY - yOffset);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset + 10, screenY - yOffset);

            GL.End();
            */
        }

        /// <summary>
        /// Resize function.
        /// </summary>
        /// <param name="args">args.</param>
        public void Resize(ResizeEventArgs args)
        {
            // Debug.WriteLine(args.Size);
            screenSize.X = args.Size.X;
            screenSize.Y = args.Size.Y;

            heightValue = (screenSize.Y / 720) * healthbarHeight;

            return;
        }

        /// <summary>
        /// OnCreate function.
        /// </summary>
        public void OnRendererCreate()
        {
            return;
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }
    }
}
