// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System.Diagnostics;
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

        private static float xOffset = 50;
        private static float yOffset = 50;
        private static float weaponHeight = 2.5f; // Changes the weapon scale.
        private static float healthbarHeight = 2.0f; // Change float value for healtbar scale.

        private static float healthbarHeightValue;
        private static float weaponSpriteSizeValue;
        private static Vector3 healthbarSpriteAspect = new Vector3(220, 18, 220f / 18f);
        private static Vector3 weaponSpriteAspect = new Vector3(64, 64, 64f / 64f);
        private static Vector2 screenSize;

        private static float reloadTime;
        private static float reloadTimeLeft;
        private static float reloadScale;

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

            reloadTime = player.GetGun().GetReloadTime();
        }

        /// <summary>
        /// Render Healthbar.
        /// </summary>
        /// <param name="args">.</param>
        public void Render(FrameEventArgs args)
        {
            currentHP = this.player.GetComponent<HealthPoints>().GetCurrHP();
            this.hTexX0 = currentHP / (float)this.player.GetComponent<HealthPoints>().GetMaxHP();

            reloadTimeLeft = this.player.GetGun().GetReloadTimeLeft();
            reloadScale = reloadTimeLeft / reloadTime;
            if (reloadTimeLeft < 0)
            {
                reloadScale = 0;
            }

            this.RenderIndicators(args);
            this.RenderWeaponSlot(args);
        }

        /// <summary>
        /// function to render current weapon.
        /// renders weapon on the right side and dependent on the healthbar currently.
        /// </summary>
        /// /// <param name="args">.</param>
        public void RenderWeaponSlot(FrameEventArgs args)
        {
            // weapon
            GL.BindTexture(TextureTarget.Texture2D, GunType.GunTypeArray[ItemShop.CurrentWeaponIndex].GunSprite.Handle);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue, screenSize.Y - yOffset);

            GL.TexCoord2(1, 0);
            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue + (weaponSpriteAspect.Z * weaponSpriteSizeValue), screenSize.Y - yOffset);

            GL.TexCoord2(1, 1);
            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue + (weaponSpriteAspect.Z * weaponSpriteSizeValue), screenSize.Y - (yOffset + weaponSpriteSizeValue));

            GL.TexCoord2(0, 1);
            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue, screenSize.Y - (yOffset + weaponSpriteSizeValue));

            GL.End();

            // reload alpha. only shown when weapon is reloading
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 0.3f));
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue, screenSize.Y - yOffset - (weaponSpriteSizeValue * reloadScale));

            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue + (weaponSpriteAspect.Z * weaponSpriteSizeValue), screenSize.Y - yOffset - (weaponSpriteSizeValue * reloadScale));

            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue + (weaponSpriteAspect.Z * weaponSpriteSizeValue), screenSize.Y - yOffset);

            GL.Vertex2(screenSize.X - xOffset - weaponSpriteSizeValue, screenSize.Y - yOffset);

            GL.End();
        }

        /// <summary>
        /// function to render indicators of healthbar.
        /// </summary>
        /// <param name="args">.</param>
        public void RenderIndicators(FrameEventArgs args) // needs cleanup or shortening for code metrics.
        {
            // outline
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 95, 1.0f / 255 * 84, 1.0f / 255 * 68, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f * 2f);
            GL.Vertex2(xOffset, screenSize.Y - yOffset);

            GL.TexCoord2(1, 1f / 20f * 2f);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * healthbarHeightValue), screenSize.Y - yOffset);

            GL.TexCoord2(1, 1f / 20f);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * healthbarHeightValue), screenSize.Y - yOffset - healthbarHeightValue);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, screenSize.Y - yOffset - healthbarHeightValue);

            GL.End();

            // inlay
            GL.BindTexture(TextureTarget.Texture2D, this.indicators.Handle);
            GL.Color4(new Color4(1.0f / 255 * 200, 0.0f, 0.0f, 1.0f));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1f / 20f);
            GL.Vertex2(xOffset, screenSize.Y - yOffset);

            GL.TexCoord2(this.hTexX0, 1f / 20f);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * healthbarHeightValue * this.hTexX0), screenSize.Y - yOffset);

            GL.TexCoord2(this.hTexX0, 0);
            GL.Vertex2(xOffset + (healthbarSpriteAspect.Z * healthbarHeightValue * this.hTexX0), screenSize.Y - yOffset - healthbarHeightValue);

            GL.TexCoord2(0, 0);
            GL.Vertex2(xOffset, screenSize.Y - yOffset - healthbarHeightValue);

            GL.End();
        }

        /// <summary>
        /// Resize function.
        /// </summary>
        /// <param name="args">args.</param>
        public void Resize(ResizeEventArgs args)
        {
            screenSize.X = args.Size.X;
            screenSize.Y = args.Size.Y;

            healthbarHeightValue = (screenSize.Y / 720) * healthbarSpriteAspect.Y * healthbarHeight;
            weaponSpriteSizeValue = (screenSize.Y / 720) * weaponSpriteAspect.Y * weaponHeight;

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
