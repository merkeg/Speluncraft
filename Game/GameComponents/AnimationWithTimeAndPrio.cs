// <copyright file="AnimationWithTimeAndPrio.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.GameComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Wraper class, that says how long which animation should be played.
    /// </summary>
    public class AnimationWithTimeAndPrio : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationWithTimeAndPrio"/> class.
        /// </summary>
        /// <param name="prio">Prio of the animation. (Lower means better)</param>
        /// <param name="time">How long to play the animation.</param>
        /// <param name="animation">The animation.</param>
        public AnimationWithTimeAndPrio(int prio, float time, ISprite animation)
        {
            this.Prio = prio;
            this.TimeLeft = time;
            this.Animation = animation;
        }

        /// <summary>
        /// Gets or sets the Animation.
        /// </summary>
        public ISprite Animation { get; set; }

        /// <summary>
        /// Gets or sets the time Left on this Animation.
        /// </summary>
        public float TimeLeft { get; set; }

        /// <summary>
        /// Gets or sets the Prio of this Animation.
        /// </summary>
        public int Prio { get; set; }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            if (obj is AnimationWithTimeAndPrio)
            {
                return this.Prio - ((AnimationWithTimeAndPrio)obj).Prio;
            }

            return 0;
        }
    }
}
