// <copyright file="TilemapParser.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile.Parser
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

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
    }
}
