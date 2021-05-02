﻿// <copyright file="GraphRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI.Graph
{
    using System;
    using System.Collections.Generic;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Text;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Graph renderer class.
    /// </summary>
    public class GraphRenderer : IRenderer
    {
        private readonly Rectangle bounds;
        private readonly float min;
        private readonly float max;
        private readonly List<GraphDataSet> data;

        private readonly TextRenderer maxText;
        private readonly TextRenderer minText;
        private readonly TextRenderer titleText;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphRenderer"/> class.
        /// </summary>
        /// <param name="title">Graph title.</param>
        /// <param name="font">Font of text.</param>
        /// <param name="bounds">Bounds of the graph.</param>
        /// <param name="min">Min y.</param>
        /// <param name="max">Max y.</param>
        public GraphRenderer(string title, Font font, Rectangle bounds, float min = 0f, float max = 15f)
        {
            this.bounds = bounds;
            this.min = min;
            this.max = max;
            this.data = new List<GraphDataSet>();
            this.maxText = new TextRenderer(string.Empty + this.max, font, Color4.White, new RelativeRectangle(this.bounds, 5, 5, 1, 1), 0.15f);
            this.titleText = new TextRenderer(title, font, Color4.White, new RelativeRectangle(this.bounds, 30, 5, 1, 1), 0.15f);
            this.minText = new TextRenderer(string.Empty + this.min, font, Color4.White, new RelativeRectangle(this.bounds, 5, this.bounds.SizeY - 20, 1, 1), 0.15f);
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            float calcMax = this.max - this.min;
            float yMultiply = this.bounds.SizeY / calcMax;

            // Outer box
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color4(Color4.White);
            GL.LineWidth(2);
            GL.Vertex2(this.bounds.MinX, this.bounds.MinY);
            GL.Vertex2(this.bounds.MinX + this.bounds.SizeX, this.bounds.MinY);
            GL.Vertex2(this.bounds.MinX + this.bounds.SizeX, this.bounds.MinY + this.bounds.SizeY);
            GL.Vertex2(this.bounds.MinX, this.bounds.MinY + this.bounds.SizeY);
            GL.End();

            foreach (GraphDataSet dataSet in this.data)
            {
                GL.Color4(dataSet.Color);
                GL.LineWidth(2);
                GL.Begin(PrimitiveType.LineStrip);

                float xAdvance = (float)this.bounds.SizeX / dataSet.Data.Length;
                float x = xAdvance;
                foreach (float point in dataSet.Data)
                {
                    float y = MathHelper.Clamp(point, this.min, this.max) - this.min;
                    GL.Vertex2(this.bounds.MinX + x, this.bounds.MinY + this.bounds.SizeY - (y * yMultiply));
                    x += xAdvance;
                }

                GL.End();
            }

            this.minText.Render(args);
            this.maxText.Render(args);
            this.titleText.Render(args);
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnCreate()
        {
        }

        /// <summary>
        /// Adds an dataset to the graph.
        /// </summary>
        /// <param name="dataSet">Dataset to add.</param>
        public void AddDataSet(GraphDataSet dataSet)
        {
            this.data.Add(dataSet);
        }
    }
}