// <copyright file="StresstestComponent.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game
{
    using Engine.Component;
    using Engine.GameObject;
    using Engine.Renderer.Sprite;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// Stresstest component class.
    /// </summary>
    public class StresstestComponent : Component
    {
        private readonly Sprite sprite;
        private float deltaTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="StresstestComponent"/> class.
        /// </summary>
        /// <param name="sprite">Sprite to render.</param>
        public StresstestComponent(Sprite sprite)
        {
            this.sprite = sprite;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.deltaTime += frameTime;

            if (this.deltaTime >= 0.2f)
            {
                this.deltaTime = 0;
                KeyboardState keyboardState = Engine.Engine.GameWindow.KeyboardState;

                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    int toSpawn = 1;
                    toSpawn *= keyboardState.IsKeyDown(Keys.LeftShift) ? 10 : 1;
                    toSpawn *= keyboardState.IsKeyDown(Keys.LeftControl) ? 100 : 1;

                    for (int i = 0; i < toSpawn; i++)
                    {
                        GameObject gameObject = new GameObject(-2, 0, 1, 1, this.sprite);
                        gameObject.AddComponent(new Physics());
                        gameObject.AddComponent(new Collider());
                        Engine.Engine.GameObjectsToAdd.Add(gameObject);
                    }
                }
            }
        }
    }
}