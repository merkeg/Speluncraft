// <copyright file="GameManager.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game
{
    using Engine.Scene;
    using Game.Scenes;

    /// <summary>
    /// Gameclass.
    /// </summary>
    public class GameManager
    {
        private static bool updatesPaused;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            SceneStart = new StartScene();
            SceneDeath = new DeathScene();
            Engine.Engine.OnSceneChange += OnSceneChange;
            Engine.Engine.ChangeScene(SceneStart);
        }

        /// <summary>
        /// The handler.
        /// </summary>
        /// <param name="stateChangeTo">The state it changed to.</param>
        public delegate void PauseHandler(bool stateChangeTo);

        /// <summary>
        /// Gets or sets a value indicating whether a update is called or not.
        /// </summary>
        public static bool UpdatesPaused
        {
            get => GameManager.updatesPaused;
            set
            {
                if (GameManager.updatesPaused != value)
                {
                    GameManager.updatesPaused = value;
                    if (OnPauseStateChange != null)
                    {
                        OnPauseStateChange(value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the start scene.
        /// </summary>
        public static Scene SceneStart { get; private set; }

        /// <summary>
        /// Gets the Death scene.
        /// </summary>
        public static Scene SceneDeath { get; private set; }

        /// <summary>
        /// Gets or sets Handlers on pause.
        /// </summary>
        public static PauseHandler OnPauseStateChange { get; set; }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public static void Start()
        {
            TextureLoader.LoadTextures();
            new GameManager();
        }

        private static void OnSceneChange(Scene from, Scene to)
        {
            OnPauseStateChange = null;
        }
    }
}
