// <copyright file="UiElement.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI
{
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Sprite;
    using global::Engine.Renderer.Text;
    using global::Engine.Renderer.UI.Graph;
    using global::Engine.Renderer.UI.Primitive;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// UI alignment class.
    /// </summary>
    public enum UiAlignment
    {
        /// <summary>
        /// Float left.
        /// </summary>
        Left,

        /// <summary>
        /// Float right.
        /// </summary>
        Right,
    }

    /// <summary>
    /// UI element class.
    /// </summary>
    public abstract class UiElement : IRenderer
    {
        private UiAlignment alignment;
        private bool fitToViewport;

        /// <summary>
        /// Initializes a new instance of the <see cref="UiElement"/> class.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="font">Font.</param>
        /// <param name="alignment">Alignment.</param>
        /// <param name="fitToViewport">Set if content should fit to viewport.</param>
        protected UiElement(Rectangle bounds, Color4 backgroundColor, Font font, UiAlignment alignment = UiAlignment.Left, bool fitToViewport = false)
        {
            this.alignment = alignment;
            this.Renderers = new List<IRenderer>();
            this.Font = font;
            this.BackgroundColor = backgroundColor;
            this.Bounds = bounds;
            this.Hidden = false;
            this.fitToViewport = fitToViewport;
            if (alignment == UiAlignment.Left)
            {
                this.AbsoluteBounds = new Rectangle(0, 0, 1, 1);
            }
            else
            {
                Vector2i monitor = Engine.GameWindow.Size;
                this.AbsoluteBounds = new Rectangle(monitor.X - this.Bounds.MinX - this.Bounds.SizeX, this.Bounds.MinY, this.Bounds.SizeX, this.Bounds.SizeY);
            }

            this.AddQuad(this.AbsoluteBounds, backgroundColor);
        }

        /// <summary>
        /// Gets the Renderers.
        /// </summary>
        public List<IRenderer> Renderers { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether hidden.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets the absolute bounds.
        /// </summary>
        protected Rectangle AbsoluteBounds { get; }

        /// <summary>
        /// Gets or sets Bounds.
        /// </summary>
        protected Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets Font.
        /// </summary>
        protected Font Font { get; set; }

        /// <summary>
        /// Gets or sets background color.
        /// </summary>
        protected Color4 BackgroundColor { get; set; }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            if (this.Hidden)
            {
                return;
            }

            this.OnRender(args);
            this.Renderers.ForEach(renderer => renderer.Render(args));
        }

        /// <inheritdoc/>
        public virtual void Resize(ResizeEventArgs args)
        {
            if (this.alignment == UiAlignment.Right)
            {
                this.AbsoluteBounds.MinX = args.Width - this.Bounds.MinX - this.Bounds.SizeX;
            }

            if (this.fitToViewport)
            {
                this.AbsoluteBounds.MaxX = args.Width;
                this.AbsoluteBounds.MaxY = args.Height;
            }
        }

        /// <inheritdoc/>
        public abstract void OnRendererCreate();

        /// <summary>
        /// Renders before everything on ui.
        /// </summary>
        /// <param name="args">Frame args.</param>
        public abstract void OnRender(FrameEventArgs args);

        /// <summary>
        /// Adds an renderer to the ui element.
        /// </summary>
        /// <param name="renderer">Renderer element.</param>
        public void AddRenderElement(IRenderer renderer)
        {
            this.Renderers.Add(renderer);
        }

        /// <summary>
        /// Adds an quad to the ui element.
        /// </summary>
        /// <param name="rectangleBounds">Bounds of the quad.</param>
        /// <param name="color">Color of the quad.</param>
        /// <param name="filled">Quad filled.</param>
        /// <returns>The Quad element.</returns>
        public QuadRenderer AddQuad(IRectangle rectangleBounds, Color4 color, bool filled = true)
        {
            QuadRenderer quadRenderer = new QuadRenderer(rectangleBounds, color, filled);
            this.AddRenderElement(quadRenderer);
            return quadRenderer;
        }

        /// <summary>
        /// Adds a graph to the ui element.
        /// </summary>
        /// <param name="title">Title of graph.</param>
        /// <param name="graphBounds">Bounds of graph.</param>
        /// <param name="min">Min.</param>
        /// <param name="max">Max.</param>
        /// <returns>Graph renderer object.</returns>
        public GraphRenderer AddGraph(string title, IRectangle graphBounds, float min = 0f, float max = 10f)
        {
            GraphRenderer graphRenderer = new GraphRenderer(title, this.Font, graphBounds, min, max);
            this.AddRenderElement(graphRenderer);
            return graphRenderer;
        }

        /// <summary>
        /// Add text to ui element.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="color">Color of text.</param>
        /// <param name="position">Position of text.</param>
        /// <param name="textScale">Scale of text.</param>
        /// <returns>Text renderer object.</returns>
        public TextRenderer AddText(string text, Color4 color, IRectangle position, float textScale)
        {
            TextRenderer textRenderer = new TextRenderer(text, this.Font, color, position, textScale);
            this.AddRenderElement(textRenderer);
            return textRenderer;
        }

        /// <summary>
        /// Adds a new sprite to the renderer.
        /// </summary>
        /// <param name="sprite">Sprite to render.</param>
        /// <param name="bounds">Bounds set to.</param>
        /// <returns>The renderer.</returns>
        public SpriteRenderer AddSprite(ISprite sprite, IRectangle bounds)
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(sprite, bounds);
            this.AddRenderElement(spriteRenderer);
            return spriteRenderer;
        }

        /// <inheritdoc/>
        public virtual void OnRendererDelete()
        {
        }
    }
}