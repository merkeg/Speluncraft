// <copyright file="UiElement.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI
{
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Text;
    using global::Engine.Renderer.UI.Graph;
    using global::Engine.Renderer.UI.Primitive;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// UI element class.
    /// </summary>
    public abstract class UiElement : IRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UiElement"/> class.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="font">Font.</param>
        protected UiElement(Rectangle bounds, Color4 backgroundColor, Font font)
        {
            this.Renderers = new List<IRenderer>();
            this.Font = font;
            this.BackgroundColor = backgroundColor;
            this.Bounds = bounds;
            this.AddQuad(new Rectangle(0, 0, this.Bounds.SizeX, this.Bounds.SizeY), backgroundColor);
        }

        /// <summary>
        /// Gets the Renderers.
        /// </summary>
        public List<IRenderer> Renderers { get; private set; }

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
            this.OnRender(args);
            this.Renderers.ForEach(renderer => renderer.Render(args));
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public abstract void OnCreate();

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
        public QuadRenderer AddQuad(Rectangle rectangleBounds, Color4 color, bool filled = true)
        {
            rectangleBounds = this.Combine(this.Bounds, rectangleBounds);
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
        public GraphRenderer AddGraph(string title, Rectangle graphBounds, float min = 0f, float max = 10f)
        {
            graphBounds = this.Combine(this.Bounds, graphBounds);
            GraphRenderer graphRenderer = new GraphRenderer(title, this.Font, new Vector2(graphBounds.MinX, graphBounds.MinY), (int)graphBounds.SizeX, (int)graphBounds.SizeY, min, max);
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
        public TextRenderer AddText(string text, Color4 color, Vector2 position, float textScale)
        {
            TextRenderer textRenderer = new TextRenderer(text, this.Font, color, new Vector2(this.Bounds.MinX + position.X, this.Bounds.MinY + position.Y), textScale);
            this.AddRenderElement(textRenderer);
            return textRenderer;
        }

        private Rectangle Combine(Rectangle a, Rectangle b)
        {
            return new Rectangle(a.MinX + b.MinX, a.MinY + b.MinY, b.SizeX, b.SizeY);
        }
    }
}