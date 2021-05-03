// <copyright file="DebugRenderer.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer
{
    using System;
    using System.Diagnostics;
    using global::Engine.Component;
    using global::Engine.GameObject;
    using global::Engine.Renderer.Text;
    using global::Engine.Renderer.UI;
    using global::Engine.Renderer.UI.Graph;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;

    /// <summary>
    /// Debug renderer class.
    /// </summary>
    public class DebugRenderer : UiElement
    {
        private readonly TextRenderer engineInfoText;
        private readonly TextRenderer frameInfoText;
        private readonly TextRenderer playerInfoText;

        private readonly GraphRenderer frameGraphRenderer;
        private readonly GraphRenderer movementGraphRenderer;

        private readonly GraphDataSet tickDataSet;
        private readonly GraphDataSet velXDataSet;
        private readonly GraphDataSet velYDataSet;

        private readonly Physics physics;
        private double deltaTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugRenderer"/> class.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="font">Font.</param>
        /// <param name="gameObject">Gameobject with physics component.</param>
        /// <param name="alignment">Alignment.</param>
        public DebugRenderer(Rectangle bounds, Color4 backgroundColor, Font font, GameObject gameObject, UiAlignment alignment = UiAlignment.Left)
            : base(bounds, backgroundColor, font, alignment)
        {
            this.engineInfoText = this.AddText(string.Empty, Color4.White, new RelativeRectangle(this.AbsoluteBounds, 5, 10, 1, 1), 0.2f);
            this.frameInfoText = this.AddText(string.Empty, Color4.White, new RelativeRectangle(this.AbsoluteBounds, 5, 25, 1, 1), 0.2f);
            this.playerInfoText = this.AddText(string.Empty, Color4.White, new RelativeRectangle(this.AbsoluteBounds, 5, 40, 1, 1), 0.2f);
            this.physics = gameObject.GetComponent<Physics>();

            this.tickDataSet = new GraphDataSet(100, Color4.Yellow);
            this.velXDataSet = new GraphDataSet(100, Color4.Coral);
            this.velYDataSet = new GraphDataSet(100, Color4.Lime);

            this.frameGraphRenderer = this.AddGraph("Render time (Yellow)", new RelativeRectangle(this.AbsoluteBounds, 5, 70, 290, 120), 0, 30);
            this.movementGraphRenderer = this.AddGraph("Player velocity (X=Red, Y=Green)", new RelativeRectangle(this.AbsoluteBounds, 5, 200, 290, 120), -15, 15);

            this.frameGraphRenderer.AddDataSet(this.tickDataSet);
            this.movementGraphRenderer.AddDataSet(this.velXDataSet);
            this.movementGraphRenderer.AddDataSet(this.velYDataSet);
        }

        /// <inheritdoc/>
        public override void OnCreate()
        {
        }

        /// <inheritdoc/>
        public override void OnRender(FrameEventArgs args)
        {
            Engine engine = Engine.Instance();
            this.engineInfoText.Text = $"GO: {engine.GameObjects.Count} - COL: {engine.Colliders.Count}";
            this.frameInfoText.Text = $"mem: {GC.GetTotalMemory(false) / 1000 / 1000,-3}MB - time: {MathHelper.Round(args.Time * 1000, 2)}ms";
            this.playerInfoText.Text = $"velX: {MathHelper.Round(this.physics.GetVelocity().X, 2),-6} velY: {MathHelper.Round(this.physics.GetVelocity().Y, 2),-6}";

            this.deltaTime += args.Time;
            if (this.deltaTime >= 0.05)
            {
                this.deltaTime = 0;
                this.tickDataSet.AddData((float)args.Time * 1000);
                this.velXDataSet.AddData(this.physics.GetVelocity().X);
                this.velYDataSet.AddData(this.physics.GetVelocity().Y);
            }
        }
    }
}