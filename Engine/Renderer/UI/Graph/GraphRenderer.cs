// <copyright file="GraphRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI.Graph
{
    using System;
    using System.Collections.Generic;
    using global::Engine.Renderer.Text;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Graph renderer class.
    /// </summary>
    public class GraphRenderer : IRenderer
    {
        private readonly Vector2 position;
        private readonly int width;
        private readonly int height;
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
        /// <param name="position">Position of graph (Top left).</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="min">Min y.</param>
        /// <param name="max">Max y.</param>
        public GraphRenderer(string title, Font font, Vector2 position, int width, int height, float min = 0f, float max = 15f)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.min = min;
            this.max = max;
            this.data = new List<GraphDataSet>();
            this.maxText = new TextRenderer(string.Empty + this.max, font, Color4.White, new Vector2d(position.X + 5, position.Y + 5), 0.15f);
            this.titleText = new TextRenderer(title, font, Color4.White, new Vector2d(position.X + 30, position.Y + 5), 0.15f);
            this.minText = new TextRenderer(string.Empty + this.min, font, Color4.White, new Vector2d(position.X + 5, position.Y + this.height - 20), 0.15f);
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            float calcMax = this.max - this.min;
            float yMultiply = this.height / calcMax;

            // Outer box
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color4(Color4.White);
            GL.LineWidth(2);
            GL.Vertex2(this.position.X, this.position.Y);
            GL.Vertex2(this.position.X + this.width, this.position.Y);
            GL.Vertex2(this.position.X + this.width, this.position.Y + this.height);
            GL.Vertex2(this.position.X, this.position.Y + this.height);
            GL.End();

            foreach (GraphDataSet dataSet in this.data)
            {
                GL.Color4(dataSet.Color);
                GL.LineWidth(2);
                GL.Begin(PrimitiveType.LineStrip);

                float xAdvance = (float)this.width / dataSet.Data.Length;
                float x = xAdvance;
                foreach (float point in dataSet.Data)
                {
                    float y = MathHelper.Clamp(point, this.min, this.max) - this.min;
                    GL.Vertex2(this.position.X + x, this.position.Y + this.height - (y * yMultiply));
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