// <copyright file="FontModel.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Text.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using OpenTK.Graphics.ES11;

    /// <summary>
    /// Font model class.
    /// </summary>
    public class FontModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontModel"/> class.
        /// Font model initializer.
        /// </summary>
        /// <param name="characters">List of characters.</param>
        public FontModel(Dictionary<int, FontModelCharacter> characters)
        {
            this.Characters = characters;
        }

        /// <summary>
        /// Gets the List of characters.
        /// </summary>
        public Dictionary<int, FontModelCharacter> Characters { get; private set; }

        /// <summary>
        /// Parse a file to an font model.
        /// </summary>
        /// <param name="stream">Stream to use.</param>
        /// <returns>Font model.</returns>
        public static FontModel Parse(Stream stream)
        {
            Dictionary<int, FontModelCharacter> charmap = new Dictionary<int, FontModelCharacter>();
            StreamReader reader = new StreamReader(stream);
            string line;

            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = Regex.Split(line, @"\s+");
                List<int> meta = new List<int>();
                foreach (string t in data)
                {
                    int j;

                    if (int.TryParse(Regex.Replace(t, @"\w+=", string.Empty), out j))
                    {
                        meta.Add(j);
                    }
                }

                charmap.Add(meta[0], new FontModelCharacter()
                {
                    Id = meta[0],
                    X = meta[1],
                    Y = meta[2],
                    Width = meta[3],
                    Height = meta[4],
                    XOffset = meta[5],
                    YOffset = meta[6],
                    XAdvance = meta[7],
                });
            }

            return new FontModel(charmap);
        }

        /// <summary>
        /// Character model.
        /// </summary>
        public struct FontModelCharacter
        {
            /// <summary>
            /// Character id.
            /// </summary>
            public int Id;

            /// <summary>
            /// x position of char.
            /// </summary>
            public int X;

            /// <summary>
            /// y position of char.
            /// </summary>
            public int Y;

            /// <summary>
            /// width of char.
            /// </summary>
            public int Width;

            /// <summary>
            /// height of char.
            /// </summary>
            public int Height;

            /// <summary>
            /// XOffset of char.
            /// </summary>
            public int XOffset;

            /// <summary>
            /// YOffset of char.
            /// </summary>
            public int YOffset;

            /// <summary>
            /// XAdvance of char.
            /// </summary>
            public int XAdvance;
        }
    }
}
