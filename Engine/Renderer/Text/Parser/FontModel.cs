namespace Engine.Renderer.Text.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Font model class.
    /// </summary>
    public class FontModel
    {

        /// <summary>
        /// Character model.
        /// </summary>
        public struct FontModelCharacter
        {
            /// <summary>
            /// Character id.
            /// </summary>
            public int ID;

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
