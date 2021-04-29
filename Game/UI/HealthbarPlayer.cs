// <copyright file="HealthbarPlayer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.UI
{
    using System;
    using System.Collections.Generic;
    using Engine;
    using Engine.Component;
    using Engine.GameObject;
    using Engine.Renderer;
    using Game.Player;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// The HealthbarPlayer class renders the healthbar UI element.
    /// </summary>
    public class HealthbarPlayer : IRenderer
    {
        private static HealthbarPlayer instance;
        private static Player player;
        private static Engine engine;

        private static int currentHP = 0;

        private static Vector2d origin;
        private static Vector2d currentPos;
        private static float xMinOffset = -6.0f;
        private static float yMinOffset = 0.0f;
        private static float width = 3.5f;
        private static float height = 0.3f;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthbarPlayer"/> class.
        /// </summary>
        public HealthbarPlayer()
        {
            engine = Engine.Instance();
            instance = this;

            foreach (IRectangle r in engine.Colliders)
            {
                if (r is Player)
                {
                    GameObject g = (GameObject)r;
                    player = (Player)g;
                }
            }

            origin = new Vector2d(player.MinX, player.MinY);
            currentPos = new Vector2d(player.MinX, player.MinY);
        }

        /// <summary>
        /// Function so player can retrieve always the same healthbar instance (Singleton).
        /// </summary>
        /// <returns>HealtbarPlayer instance</returns>
        public static HealthbarPlayer Instance()
        {
            if (instance == null)
            {
                instance = new HealthbarPlayer();
            }

            return instance;
        }

        /// <summary>
        /// Render Healthbar.
        /// </summary>
        /// <param name="args">.</param>
        public void Render(FrameEventArgs args)
        {

            GL.LineWidth(3);
            GL.Color3(System.Drawing.Color.White);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(3.0, -8.0);
            GL.Vertex2(3.0, -5.0);
            GL.Vertex2(5.0, -5.0);
            GL.Vertex2(5.0, -8.0);
            GL.End();

            /*
            currentHP = player.GetComponent<HealthPoints>().GetCurrHP();

            // remove only for debugging
            Console.WriteLine("Current HP player: " + currentHP + "\nPosition: " + player.MinX + " | " + player.MinY);
            currentPos.X = player.MinX;
            currentPos.Y = player.MinY;

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(System.Drawing.Color.Cyan);
            GL.Vertex2(currentPos.X - origin.X + xMinOffset, currentPos.Y - origin.Y + yMinOffset);
            GL.Vertex2(currentPos.X - origin.X + xMinOffset + width, currentPos.Y - origin.Y + yMinOffset);
            GL.Vertex2(currentPos.X - origin.X + xMinOffset + width, currentPos.Y - origin.Y + yMinOffset - height);
            GL.Vertex2(currentPos.X - origin.X + xMinOffset, currentPos.Y - origin.Y + yMinOffset - height);
            GL.End();
            */
        }

        public void Resize(ResizeEventArgs args)
        {
            return;
        }

        public void OnCreate()
        {
            return;
        }
    }
}
