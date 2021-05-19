using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Game.UI
{
    using System;
    using Engine.GameObject;
    using Engine.Renderer;
    using Engine.Renderer.Particle;
    using Engine.Renderer.Sprite;
    using Engine.Renderer.Text;
    using Engine.Renderer.Tile;
    using Engine.Renderer.UI;
    using Engine.Service;
    using OpenTK.Mathematics;

    public class InteractableElement: GameObject
    {
        private static Tilesheet tilesheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractableElement"/> class.
        /// </summary>
        /// <param name="minX">mX.</param>
        /// <param name="minY">mY.</param>
        /// <param name="sizeX">X.</param>
        /// <param name="sizeY">Y.</param>
        /// <param name="font">Font.</param>
        /// <param name="text">Info text.</param>
        /// <param name="colorText">Color of text.</param>
        /// <param name="range">The view range.</param>
        /// <param name="colorSprite">Color of sprite.</param>
        /// <param name="closeOnInteract">Destroy Element on interact.</param>
        public InteractableElement(float minX, float minY, float sizeX, float sizeY, Font font, string text, Color4 colorSprite, Color4 colorText, float range = 4, bool closeOnInteract = true)
            : base(minX, minY, sizeX, sizeY, null)
        {
            if (InteractableElement.tilesheet == null)
            {
                InteractableElement.tilesheet = new Tilesheet("Game.Resources.UI.interaction_dot.png", 16, 16);
            }

            this.Range = range;
            this.Sprite = new AnimatedSprite(InteractableElement.tilesheet, Keyframe.RangeX(0, 1, 0, 1));
            ((AnimatedSprite)this.Sprite).Paused = true;
            this.Sprite.Color = colorSprite;

            this.Text = new TextRenderer(text, font, colorText, new Rectangle(minX + 0.4f, minY + sizeY, 1, 1), 0.25f, true);
            this.Text.Hidden = true;

            this.Particle = new ParticleEmitter
            {
                VelocityMin = new Vector2(-.5f, -.5f),
                VelocityMax = new Vector2(.5f, .5f),
                ParticleSizeMin = .1f,
                ParticleSizeMax = .1f,
                SpawnCooldown = .3f,
                ParticleLifetimeMin = .3f,
                ParticleLifetimeMax = 1f,
                Gravity = .5f,
                SpawnAmountMin = 2,
                SpawnAmountMax = 5,
            };
            this.Particle.Colours.Add(new Color4(colorSprite.R, colorSprite.G, colorSprite.B, 0.3f));

            this.InRange = false;

            Engine.Engine.GetService<InputService>().Subscribe(Keys.W, this.OnPress);
            if (closeOnInteract)
            {
                this.Interact += this.Destroy;
            }
        }

        /// <summary>
        /// Event handler if interacted with.
        /// </summary>
        public event Action Interact;

        /// <summary>
        /// Gets the text.
        /// </summary>
        public TextRenderer Text { get; private set; }

        /// <summary>
        /// Gets the Particle.
        /// </summary>
        public ParticleEmitter Particle { get; }

        /// <summary>
        /// Gets or sets the view range.
        /// </summary>
        public float Range { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether in range.
        /// </summary>
        public bool InRange { get; set; }

        /// <inheritdoc/>
        public override void OnUpdatableCreate()
        {
            base.OnUpdatableCreate();
            Engine.Engine.AddRenderer(this.Text, RenderLayer.UI);
            Engine.Engine.GetService<ParticleService>().Emit(this.Particle, new RelativeRectangle(this, .1f, .1f, 1, 1), 0);
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            base.OnUpdate(frameTime);
            float distance = this.Distance(this, Program.Player);
            if (distance > this.Range && this.InRange)
            {
                this.InRange = false;
                ((AnimatedSprite)this.Sprite).SetState(0);
                this.Text.Hidden = true;

                this.Particle.SpawnAmountMin = 2;
                this.Particle.SpawnAmountMax = 5;
            }

            if (distance <= this.Range && !this.InRange)
            {
                this.InRange = true;
                ((AnimatedSprite)this.Sprite).SetState(1);
                this.Text.Hidden = false;

                this.Particle.SpawnAmountMin = 0;
                this.Particle.SpawnAmountMax = 0;
            }
        }

        /// <summary>
        /// Destroy the element.
        /// </summary>
        public void Destroy()
        {
            Engine.Engine.RemoveGameObject(this);
            Engine.Engine.RemoveRenderer(this.Text, RenderLayer.UI);
            Engine.Engine.GetService<ParticleService>().Remove(this.Particle);
            Engine.Engine.GetService<InputService>().Unsubscribe(Keys.W, this.OnPress);
        }

        private void OnPress()
        {
            if (!this.InRange)
            {
                return;
            }

            this.Interact?.Invoke();
        }

        private float Distance(IRectangle rectangle1, IRectangle rectangle2)
        {
            return (float)Math.Sqrt(Math.Pow((double)(rectangle1.MinX - rectangle2.MinX), 2) +
                                     Math.Pow((double)(rectangle1.MinY - rectangle2.MinY), 2));
        }
    }
}