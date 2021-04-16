// <copyright file="Camera.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Camera
{
    using System;
    using global::Engine.Renderer;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Camera class.
    /// </summary>
    public class Camera : IRenderer
    {
        /// <summary>
        /// Gets the camera matrix.
        /// </summary>
        public Matrix4 CameraMatrix => this.cameraMatrix;

        /// <summary>
        /// Gets the viewport matrix.
        /// </summary>
        public Matrix4 InvViewportMatrix { get; private set; }

        /// <inheritdoc/>
        public void Resize(ResizeEventArgs args)
        {
            GL.Viewport(0, 0, args.Width, args.Height);

            this.windowAspectRatio = args.Height / (float)args.Width;

            var viewport = Transformation2d.Combine(Transformation2d.Translate(Vector2.One), Transformation2d.Scale(args.Width / 2f, args.Height / 2f));
            this.InvViewportMatrix = viewport.Inverted();
            this.UpdateMatrix();
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs args)
        {
            GL.LoadMatrix(ref this.cameraMatrix);
        }

        /// <inheritdoc/>
        public void OnCreate()
        {
            return;
        }

        /// <summary>
        /// Gets or sets the camera center.
        /// </summary>
#pragma warning disable SA1201 // Elements should appear in the correct order
        public Vector2 Center
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            get => this.center;
            set
            {
                this.center = value;
                this.UpdateMatrix();
            }
        }

        /// <summary>
        /// Gets or sets the camera rotation.
        /// </summary>
        public float Rotation
        {
            get => this.rotation;
            set
            {
                this.rotation = value;
                this.UpdateMatrix();
            }
        }

        /// <summary>
        /// Gets or sets the camera scale.
        /// </summary>
        public float Scale
        {
            get => this.scale;
            set
            {
                this.scale = MathF.Max(0.001f, value); // avoid division by 0 and negative
                this.UpdateMatrix();
            }
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private Matrix4 cameraMatrix = Matrix4.Identity;
#pragma warning restore SA1201 // Elements should appear in the correct order
        private float scale = 1f;
        private float windowAspectRatio = 1f;

        private Vector2 center;
        private float rotation;

        private void UpdateMatrix()
        {
            var translate = Transformation2d.Translate(-this.Center);
            var rotate = Transformation2d.Rotation(MathHelper.DegreesToRadians(this.Rotation));
            var scale = Transformation2d.Scale(1f / this.Scale);
            var aspect = Transformation2d.Scale(this.windowAspectRatio, 1f);
            this.cameraMatrix = Transformation2d.Combine(translate, rotate, scale, aspect);
        }
    }
}
