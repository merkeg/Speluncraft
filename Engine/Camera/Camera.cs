namespace Engine.Camera
{
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using System;

    public class Camera
    {
        public Matrix4 CameraMatrix => cameraMatrix;

        public Matrix4 InvViewportMatrix { get; private set; }

        public void Draw()
        {
            GL.LoadMatrix(ref cameraMatrix);
            GL.Scale(new Vector3(1, -1, 1));
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height); // tell OpenGL to use the whole window for drawing

            _windowAspectRatio = height / (float)width;

            var viewport = Transformation2d.Combine(Transformation2d.Translate(Vector2.One), Transformation2d.Scale(width / 2f, height / 2f));
            InvViewportMatrix = viewport.Inverted();
            UpdateMatrix();
        }

        public Vector2 Center
        {
            get => _center;
            set
            {
                _center = value;
                UpdateMatrix();
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                UpdateMatrix();
            }
        }

        public float Scale
        {
            get => _scale;
            set
            {
                _scale = MathF.Max(0.001f, value); // avoid division by 0 and negative
                UpdateMatrix();
            }
        }

        private Matrix4 cameraMatrix = Matrix4.Identity;
        private float _scale = 1f;
        private float _windowAspectRatio = 1f;

        private Vector2 _center;
        private float _rotation;

        private void UpdateMatrix()
        {
            var translate = Transformation2d.Translate(-Center);
            var rotate = Transformation2d.Rotation(MathHelper.DegreesToRadians(Rotation));
            var scale = Transformation2d.Scale(1f / Scale);
            var aspect = Transformation2d.Scale(_windowAspectRatio, 1f);
            cameraMatrix = Transformation2d.Combine(translate, rotate, scale, aspect);

        }
    }
}
