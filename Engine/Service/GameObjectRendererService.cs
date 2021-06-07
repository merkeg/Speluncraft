// <copyright file="GameObjectRendererService.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Engine.GameObject;
    using global::Engine.Renderer;
    using global::Engine.Renderer.Sprite;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Sprite service class.
    /// </summary>
    [ServiceInfo("gameobject", RenderLayer.GAME)]
    public class GameObjectRendererService : IService
    {
        /// <summary>
        /// Gets the Renders.
        /// </summary>
        public List<GameObject> Objects { get; } = new List<GameObject>();

        /// <summary>
        /// Gets the Sprites.
        /// </summary>
        public List<ISprite> Sprites { get; } = new List<ISprite>();

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            float timeElapsed = (float)args.Time;

            foreach (GameObject gameObject in Engine.GameObjects.ToList())
            {
                ISprite sprite = gameObject.Sprite;

                if (sprite == null)
                {
                    continue;
                }

                if (!this.Sprites.Contains(sprite))
                {
                    this.Sprites.Add(sprite);
                }

                GL.BindTexture(TextureTarget.Texture2D, sprite.Handle);
                GL.Color4(sprite.Color ?? Color4.White);
                GL.Begin(PrimitiveType.Quads);

                if (gameObject.Mirrored)
                {
                    GL.TexCoord2(sprite.TexX1, sprite.TexY0);
                    GL.Vertex2(gameObject.MinX, gameObject.MinY);

                    GL.TexCoord2(sprite.TexX0, sprite.TexY0);
                    GL.Vertex2(gameObject.MaxX, gameObject.MinY);

                    GL.TexCoord2(sprite.TexX0, sprite.TexY1);
                    GL.Vertex2(gameObject.MaxX, gameObject.MaxY);

                    GL.TexCoord2(sprite.TexX1, sprite.TexY1);
                    GL.Vertex2(gameObject.MinX, gameObject.MaxY);
                }
                else
                {
                    GL.TexCoord2(sprite.TexX0, sprite.TexY0);
                    GL.Vertex2(gameObject.MinX, gameObject.MinY);

                    GL.TexCoord2(sprite.TexX1, sprite.TexY0);
                    GL.Vertex2(gameObject.MaxX, gameObject.MinY);

                    GL.TexCoord2(sprite.TexX1, sprite.TexY1);
                    GL.Vertex2(gameObject.MaxX, gameObject.MaxY);

                    GL.TexCoord2(sprite.TexX0, sprite.TexY1);
                    GL.Vertex2(gameObject.MinX, gameObject.MaxY);
                }

                GL.End();
            }

            this.Sprites.ToList().ForEach(el => el.TimeElapsed(timeElapsed));
        }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void OnRendererCreate()
        {
        }

        /// <inheritdoc/>
        public void OnRendererDelete()
        {
        }

        /// <inheritdoc/>
        public void OnUpdate(float frameTime)
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableDestroy()
        {
        }

        /// <inheritdoc/>
        public void OnUpdatableCreate()
        {
        }

        /// <inheritdoc/>
        public void SceneChangeCleanup()
        {
            this.Objects.Clear();
            this.Sprites.Clear();
        }
    }
}