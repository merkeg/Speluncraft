// <copyright file="GraphDataSet.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.UI.Graph
{
    using System;
    using OpenTK.Mathematics;

    /// <summary>
    /// Graph data set class.
    /// </summary>
    public class GraphDataSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphDataSet"/> class.
        /// </summary>
        /// <param name="dataPoints">Data points.</param>
        /// <param name="color">Color to draw with.</param>
        public GraphDataSet(int dataPoints, Color4 color)
        {
            this.Data = new float[dataPoints];
            this.Color = color;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public float[] Data { get; private set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color4 Color { get; set; }

        /// <summary>
        /// Function to add a data point to the DataSet.
        /// </summary>
        /// <param name="data">Data point.</param>
        public void AddData(float data)
        {
            float[] newData = new float[this.Data.Length];
            Array.Copy(this.Data, 1, newData, 0, this.Data.Length - 1);
            newData[^1] = data;
            this.Data = newData;
        }
    }
}