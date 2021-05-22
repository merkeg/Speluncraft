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
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Class for Item Shop ingame.
    /// </summary>
    public class ItemShop : IRenderer
    {
        private static Player.Player player;

        private static Vector3 shopSpriteAspect = new Vector3(1280, 720, 720f / 1280f); // width, height and original aspect-ratio (I hate scaling :()
        private static Vector2 screenCenter;
        private static Vector2 screenSize;
        private static float shopWidth;
        private static float shopWindowBorderIndent = 300;
        private static Vector2 shopOrigin;

        private static float itemSpacing;
        private static float itemFrameSize = 128;
        private static Vector4[] itemHitboxList;

        private static Vector2 windowMousePosition; // Button1 = LeftClick, Button2 = RightClick, Middle = MiddleClick.

        private static Assembly assembly;
        private static Sprite shopBackground;
        private static Sprite shopItemFrame;
        private static Sprite shopStartButton;
        private static Font font;

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

            // check if mouse is clicked inside of itemFrame
            int index = 0; // poormans index enumerable too dumb and lazy to do it right at the moment.
            foreach (Vector4 hitbox in itemHitboxList)
            {
                // Debug.WriteLine(hitbox.X + " | " + hitbox.Y + " | " + hitbox.Z + " | " + hitbox.W);
                if (windowMousePosition.X > hitbox.X && windowMousePosition.X < hitbox.Z && windowMousePosition.Y > hitbox.Y && windowMousePosition.Y < hitbox.W)
                {
                    Debug.WriteLine("You Chose: " + GunType.GunTypeArray[index].GunName);
                    player.ChangeGun(GunType.GunTypeArray[index].Gun);

                    // change this
                    this.ShopActive = false;
                    return;
                }

                index++;
            }
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

            using Stream shopStartButtonSpriteStream = assembly.GetManifestResourceStream("Game.Resources.Sprite.UI.ItemShop.button.png");
            shopStartButton = new Sprite(shopStartButtonSpriteStream, true);

            return;
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
                GL.Vertex2(screenCenter.X - (shopWidth / 2f), screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(1, 1);
                GL.Vertex2(screenCenter.X + (shopWidth / 2f), screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(1, 0);
                GL.Vertex2(screenCenter.X + (shopWidth / 2f), screenCenter.Y + (shopSpriteAspect.Z * shopWidth / 2));

                GL.TexCoord2(0, 0);
                GL.Vertex2(screenCenter.X - (shopWidth / 2f), screenCenter.Y + (shopSpriteAspect.Z * shopWidth / 2));

                GL.End();

                this.RenderItems(args);

                // render start button TODO still ugly
                GL.BindTexture(TextureTarget.Texture2D, shopStartButton.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2(shopOrigin.X + 50, shopOrigin.Y + (shopSpriteAspect.Z * shopWidth) - 50 - 70);

                GL.TexCoord2(1, 1);
                GL.Vertex2(shopOrigin.X + shopWidth - 50, shopOrigin.Y + (shopSpriteAspect.Z * shopWidth) - 50 - 70);

                GL.TexCoord2(1, 0);
                GL.Vertex2(shopOrigin.X + shopWidth - 50, shopOrigin.Y + (shopSpriteAspect.Z * shopWidth) - 50);

                GL.TexCoord2(0, 0);
                GL.Vertex2(shopOrigin.X + 50, shopOrigin.Y + (shopSpriteAspect.Z * shopWidth) - 50);

                GL.End();

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
            shopWidth = screenSize.X - shopWindowBorderIndent;

            shopOrigin.X = screenCenter.X - (shopWidth / 2f);
            shopOrigin.Y = screenCenter.Y - (shopSpriteAspect.Z * shopWidth / 2);

            itemSpacing = shopWidth / (this.ItemCount + 1);
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
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + 70);

                // top left corner
                itemHitboxList[i - 1].X = (itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2);
                itemHitboxList[i - 1].Y = shopOrigin.Y + 70;

                GL.TexCoord2(1, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + 70);

                GL.TexCoord2(1, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + 70 + itemFrameSize);

                // bottom right corner
                itemHitboxList[i - 1].Z = (itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2);
                itemHitboxList[i - 1].W = shopOrigin.Y + 70 + itemFrameSize;

                GL.TexCoord2(0, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + 70 + itemFrameSize);

                GL.End();

                // render actual item inside itemframe
                GL.BindTexture(TextureTarget.Texture2D, GunType.GunTypeArray[i - 1].GunSprite.Handle);
                GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 1.0f));
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + 70);

                GL.TexCoord2(1, 1);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + 70);

                GL.TexCoord2(1, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X + (itemFrameSize / 2), shopOrigin.Y + 70 + itemFrameSize);

                GL.TexCoord2(0, 0);
                GL.Vertex2((itemSpacing * i) + shopOrigin.X - (itemFrameSize / 2), shopOrigin.Y + 70 + itemFrameSize);

                GL.End();
            }
        }
    }
}
