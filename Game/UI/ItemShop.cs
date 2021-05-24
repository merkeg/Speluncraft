// <copyright file="ItemShop.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Particle;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Text.Parser;
    using Engine.Service;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Class for Item Shop ingame.
    /// </summary>
    public class ItemShop : IRenderer
    {
        private static Player.Player player;

        private static Vector3 shopSpriteAspect = new Vector3(1280, 720, 1280f / 720f); // width, height and original aspect-ratio (I hate scaling :()
        private static Vector2 screenCenter;
        private static Vector2 screenSize;
        private static float shopHeight;
        private static float shopWindowBorderIndent = 100;
        private static Vector2 shopOrigin;

        private static float itemSpacing;
        private static int itemFrameYOffset = 120;
        private static float itemFrameSize = 128;
        private static Vector4[] itemHitboxList;

        private static Vector2 windowMousePosition; // Button1 = LeftClick, Button2 = RightClick, Middle = MiddleClick.

        private static Assembly assembly;
        private static Sprite shopBackground;
        private static Sprite shopItemFrame;
        private static Font font;

        private static int currentWeaponIndex = 0;
        private static TextRenderer shopHeader;
        private static TextRenderer weaponName;
        private static TextRenderer weaponPrice;
        private static TextRenderer weaponDamage;
        private static TextRenderer weaponInfo;
        private static TextRenderer helpText;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemShop"/> class.
        /// </summary>
        /// <param name="itemcount">Amount of items in shop.</param>
        /// <param name="font_">usable font.</param>
        public ItemShop(Font font_, int itemcount = 5)
        {
            this.ItemCount = itemcount;
            this.ShopActive = true; // remove after done
            itemHitboxList = new Vector4[this.ItemCount];

            // Fill gunType Array (manually for now).
            GunType.InitGunArray();

            font = font_;

            foreach (IRectangle r in Engine.Engine.Colliders)
            {
                if (r is Player.Player)
                {
                    GameObject g = (GameObject)r;
                    player = (Player.Player)g;
                }
            }

            // replace
            Engine.Engine.GetService<InputService>().Subscribe(Keys.Enter, () => this.HideShop(!this.ShopActive));
        }

        /// <summary>
        /// Gets or Sets a value indicating whether ItemShop should be rendered or not.
        /// </summary>
        public bool ShopActive { get; set; }

        /// <summary>
        /// Gets or Sets avalue indicating whether many Items should be in the Shop.
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// Gets called when Mouse is Moved.
        /// </summary>
        /// <param name="args">MouseMove Args.</param>
        public void MouseMove(MouseMoveEventArgs args)
        {
            windowMousePosition.X = args.X;
            windowMousePosition.Y = args.Y;
            return;
        }

        /// <summary>
        /// Gets Called if some Mousebutton is pressed down.
        /// </summary>
        /// <param name="args">MouseDown Args.</param>
        public void MouseDown(MouseButtonEventArgs args)
        {
            // Debug.WriteLine(args.Button + " | " + windowMousePosition.X + " | " + windowMousePosition.Y);
            int oldIndex = currentWeaponIndex; // fix for shop crashing BUG 294
            currentWeaponIndex = 0;

            // check if mouse is clicked inside of itemFrame
            foreach (Vector4 hitbox in itemHitboxList)
            {
                // Debug.WriteLine(hitbox.X + " | " + hitbox.Y + " | " + hitbox.Z + " | " + hitbox.W);
                if (windowMousePosition.X > hitbox.X && windowMousePosition.X < hitbox.Z && windowMousePosition.Y > hitbox.Y && windowMousePosition.Y < hitbox.W)
                {
                    Debug.WriteLine("You Chose: " + GunType.GunTypeArray[currentWeaponIndex].GunName);
                    player.ChangeGun(GunType.GunTypeArray[currentWeaponIndex].Gun);
                    return;
                }

                currentWeaponIndex++;
            }

            currentWeaponIndex = oldIndex;
            Debug.WriteLine("here");
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
            Engine.Engine.GameWindow.MouseMove += this.MouseMove;
            Engine.Engine.GameWindow.MouseDown += this.MouseDown;

            assembly = Assembly.GetExecutingAssembly();
            using Stream shopBackgroundSpriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.itemshop.png");
            shopBackground = new Sprite(shopBackgroundSpriteStream, true);

            using Stream shopItemFrameSpriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.item_frame_front.png");
            shopItemFrame = new Sprite(shopItemFrameSpriteStream, true);

            // TODO so many seperate textrenderes not a good solution probably, maybe ask egor!!!
            shopHeader = new TextRenderer("Speluncos Weapon Shop", font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.4f, false);
            Engine.Engine.AddRenderer(shopHeader, RenderLayer.UI);

            weaponDamage = new TextRenderer(string.Empty, font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.3f, false);
            Engine.Engine.AddRenderer(weaponDamage, RenderLayer.UI);

            weaponName = new TextRenderer(string.Empty, font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.3f, false);
            Engine.Engine.AddRenderer(weaponName, RenderLayer.UI);

            weaponPrice = new TextRenderer(string.Empty, font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.3f, false);
            Engine.Engine.AddRenderer(weaponPrice, RenderLayer.UI);

            weaponInfo = new TextRenderer(string.Empty, font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.3f, false);
            Engine.Engine.AddRenderer(weaponInfo, RenderLayer.UI);

            helpText = new TextRenderer("HINT: Click onto weapon to select it. Hit [Enter] to start Level.", font, new Color4(1.0f, 1.0f, 1.0f, 1.0f), new Rectangle(0, 0, 0, 0), 0.2f, false);
            Engine.Engine.AddRenderer(helpText, RenderLayer.UI);

            return;
        }

        /// <summary>
        /// sets Text render as visible or not.
        /// </summary>
        /// <param name="hide">contains if text is hidden or not.</param>
        public void HideShop(bool hide)
        {
            shopHeader.Hidden = !hide;
            weaponDamage.Hidden = !hide;
            weaponInfo.Hidden = !hide;
            weaponName.Hidden = !hide;
            weaponPrice.Hidden = !hide;
            helpText.Hidden = !hide;

            this.ShopActive = hide;
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            if (this.ShopActive)
            {
                // render shop background
                GL.BindTexture(TextureTarget.Texture2D, shopBackground.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2(screenCenter.X - (shopSpriteAspect.Z * shopHeight / 2f), screenCenter.Y - (shopHeight / 2));

                GL.TexCoord2(1, 1);
                GL.Vertex2(screenCenter.X + (shopSpriteAspect.Z * shopHeight / 2f), screenCenter.Y - (shopHeight / 2));

                GL.TexCoord2(1, 0);
                GL.Vertex2(screenCenter.X + (shopSpriteAspect.Z * shopHeight / 2f), screenCenter.Y + (shopHeight / 2));

                GL.TexCoord2(0, 0);
                GL.Vertex2(screenCenter.X - (shopSpriteAspect.Z * shopHeight / 2f), screenCenter.Y + (shopHeight / 2));

                GL.End();

                shopHeader.Position.MinX = screenCenter.X - 250;
                shopHeader.Position.MinY = screenCenter.Y - (shopHeight / 2) + 50;

                this.RenderItems(args);
                this.RenderItemStats(args);

                helpText.Position.MinX = shopOrigin.X + itemSpacing - (itemFrameSize / 2);
                helpText.Position.MinY = shopOrigin.Y + shopHeight - 60;
                helpText.FontScale = 0.2f / (720 - shopWindowBorderIndent) * shopHeight;

                return;
            }
            else
            {
                return;
            }
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            screenCenter.X = args.Size.X / 2f;
            screenCenter.Y = args.Size.Y / 2f;
            screenSize.X = args.Size.X;
            screenSize.Y = args.Size.Y;
            shopHeight = screenSize.Y - shopWindowBorderIndent;

            shopOrigin.X = screenCenter.X - (shopSpriteAspect.Z * shopHeight / 2f);
            shopOrigin.Y = screenCenter.Y - (shopHeight / 2);

            itemSpacing = shopSpriteAspect.Z * shopHeight / (this.ItemCount + 1);

            itemFrameSize = 128f / 620f * screenSize.Y;
            return;
        }

        /// <summary>
        /// render itemslots onto shopbackground.
        /// </summary>
        private void RenderItems(FrameEventArgs args)
        {
            for (int i = 1; i <= this.ItemCount; i++)
            {
                // render itemslots
                GL.BindTexture(TextureTarget.Texture2D, shopItemFrame.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);

                // top left corner
                itemHitboxList[i - 1].X = (itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2);
                itemHitboxList[i - 1].Y = shopOrigin.Y + itemFrameYOffset;

                GL.TexCoord2(1, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);

                GL.TexCoord2(1, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);

                // bottom right corner
                itemHitboxList[i - 1].Z = (itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2);
                itemHitboxList[i - 1].W = shopOrigin.Y + itemFrameYOffset + itemFrameSize;

                GL.TexCoord2(0, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);

                GL.End();

                // render actual item inside itemframe
                GL.BindTexture(TextureTarget.Texture2D, GunType.GunTypeArray[i - 1].GunSprite.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);

                GL.TexCoord2(1, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);

                GL.TexCoord2(1, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);

                GL.TexCoord2(0, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);

                GL.End();

                // render currently selected weapon as highlighted
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.Color4(new Color4(1.0f / 255 * 219, 1.0f / 255 * 161, 1.0f / 255 * 0, 1.0f));
                GL.LineWidth(2f);
                GL.Begin(PrimitiveType.LineLoop);

                GL.Vertex2(itemSpacing + (itemSpacing * currentWeaponIndex) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);
                GL.Vertex2(itemSpacing + (itemSpacing * currentWeaponIndex) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset);
                GL.Vertex2(itemSpacing + (itemSpacing * currentWeaponIndex) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);
                GL.Vertex2(itemSpacing + (itemSpacing * currentWeaponIndex) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + itemFrameYOffset + itemFrameSize);

                GL.End();
            }
        }

        /// <summary>
        /// Render stats as text into shop.
        /// </summary>
        /// <param name="args">FrameEventArgs.</param>
        private void RenderItemStats(FrameEventArgs args)
        {
            weaponName.Text = "Weapon: " + GunType.GunTypeArray[currentWeaponIndex].GunName;
            weaponName.Position.MinX = shopOrigin.X + itemSpacing - (itemFrameSize / 2);
            weaponName.Position.MinY = shopOrigin.Y + itemFrameYOffset + itemFrameSize + (50 / (720 - shopWindowBorderIndent) * shopHeight);
            weaponName.FontScale = 0.3f / (720 - shopWindowBorderIndent) * shopHeight;

            weaponDamage.Text = "Damage: " + GunType.GunTypeArray[currentWeaponIndex].GunDMG.ToString() + " Healthpoints";
            weaponDamage.Position.MinX = shopOrigin.X + itemSpacing - (itemFrameSize / 2);
            weaponDamage.Position.MinY = shopOrigin.Y + itemFrameYOffset + itemFrameSize + (100 / (720 - shopWindowBorderIndent) * shopHeight);
            weaponDamage.FontScale = 0.3f / (720 - shopWindowBorderIndent) * shopHeight;

            weaponPrice.Text = "Price: " + GunType.GunTypeArray[currentWeaponIndex].GunPrice.ToString() + " / " + (player.GetComponent<Engine.Component.HealthPoints>().GetMaxHP() / 10).ToString() + " Healthpoints";
            weaponPrice.Position.MinX = shopOrigin.X + itemSpacing - (itemFrameSize / 2);
            weaponPrice.Position.MinY = shopOrigin.Y + itemFrameYOffset + itemFrameSize + (150 / (720 - shopWindowBorderIndent) * shopHeight);
            weaponPrice.FontScale = 0.3f / (720 - shopWindowBorderIndent) * shopHeight;

            weaponInfo.Text = "Weapon Info: " + GunType.GunTypeArray[currentWeaponIndex].GunInfo;
            weaponInfo.Position.MinX = shopOrigin.X + itemSpacing - (itemFrameSize / 2);
            weaponInfo.Position.MinY = shopOrigin.Y + itemFrameYOffset + itemFrameSize + (200 / (720 - shopWindowBorderIndent) * shopHeight);
            weaponInfo.FontScale = 0.3f / (720 - shopWindowBorderIndent) * shopHeight;
        }
    }
}
