// <copyright file="AnimationScheduler.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Game.GameComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Engine.Renderer.Sprite;

    /// <summary>
    /// Used for Schedueling Animations.
    /// </summary>
    public class AnimationScheduler : Engine.Component.Component
    {
        private List<AnimationWithTimeAndPrio> animationQueue = new List<AnimationWithTimeAndPrio>();

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            this.animationQueue.Sort();
            this.GameObject.Sprite = this.animationQueue[0].Animation;

            List<AnimationWithTimeAndPrio> toBeRemoved = new List<AnimationWithTimeAndPrio>();

            foreach (AnimationWithTimeAndPrio a in this.animationQueue)
            {
                a.TimeLeft -= frameTime;
                if (a.TimeLeft <= 0)
                {
                    toBeRemoved.Add(a);
                }
            }

            foreach (AnimationWithTimeAndPrio a in toBeRemoved)
            {
                this.animationQueue.Remove(a);
            }
        }

        /// <summary>
        /// Add a new Animation.
        /// </summary>
        /// <param name="prio">The Prio this animation has. ( Lower means better).</param>
        /// <param name="time">How long to play this animation.</param>
        /// <param name="animation">The Animation to be played.</param>
        public void AddAnimation(int prio, float time, ISprite animation)
        {
            this.animationQueue.Add(new AnimationWithTimeAndPrio(prio, time, animation));
        }
    }
}
