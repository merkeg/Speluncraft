// <copyright file="TilemapParser.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using global::Engine.GameObject;
    using Newtonsoft.Json;
    using OpenTK.Mathematics;

    /// <summary>
    /// Helper class to parse Tilemap json files into models.
    /// </summary>
    public class TilemapParser
    {
        /// <summary>
        /// Parses Tilemap json files into a Tilemap model.
        /// </summary>
        /// <param name="resource">The Tilemap json stream.</param>
        /// <returns>The Tilemap model.</returns>
        public static TilemapModel ParseTilemap(string resource)
        {
            Assembly asm = Assembly.GetEntryAssembly();
            return ParseTilemap(asm.GetManifestResourceStream(resource));
        }

        /// <summary>
        /// Parses Tilemap json files into a Tilemap model.
        /// </summary>
        /// <param name="resource">The Tilemap json stream.</param>
        /// <returns>The Tilemap model.</returns>
        public static TilemapModel ParseTilemap(Stream resource)
        {
            if (resource == null)
            {
                throw new ArgumentException("resource cannot be null.");
            }

            StreamReader reader = new StreamReader(resource);
            string json = reader.ReadToEnd();
            TilemapModel model = JsonConvert.DeserializeObject<TilemapModel>(json);
            return model;
        }

        /// <summary>
        /// Generates an list of collisions from the tile layer.
        /// </summary>
        /// <param name="layer">The layer the list will be chosen from.</param>
        /// <param name="originX">Origin X.</param>
        /// <param name="originY">Origin Y.</param>
        /// <returns>a list of collisions.</returns>
        public static List<Rectangle> GenerateCollisionMap(TilemapLayerModel layer, int originX, int originY)
        {
            List<Rectangle> collisions = new List<Rectangle>();

            for (int i = 0; i < layer.data.Length; i++)
            {
                if (layer.data[i] == 0)
                {
                    continue;
                }

                int[] pos = { i - layer.width, i + layer.width, i - 1, i + 1 };

                foreach (int position in pos)
                {
                    if (position >= 0 && position < layer.data.Length)
                    {
                        if (layer.data[position] == 0)
                        {
                            collisions.Add(new Rectangle(originX + (i % layer.width), originY - (i / layer.width), 1, 1));
                            break;
                        }
                    }
                }
            }

            return collisions;
        }
    }
}
