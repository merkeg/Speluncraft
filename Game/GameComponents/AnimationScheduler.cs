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
        private bool mirrored;

        /// <summary>
        /// Get if the Frame must be mirroed or not.
        /// </summary>
        /// <returns>if the Frame must be mirroed or not.</returns>
        public bool GetIfMustBeMirrored()
        {
            return this.mirrored;
        }

        /// <inheritdoc/>
        public override void OnUpdate(float frameTime)
        {
            try
            {
                this.animationQueue.Sort();
                this.GameObject.Sprite = this.animationQueue[0].Animation;
                this.mirrored = this.animationQueue[0].Mirrored;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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
        /// <param name="mirrored">If the Sprite must be mirrored.</param>
        public void AddAnimation(int prio, float time, ISprite animation, bool mirrored)
        {
            this.animationQueue.Add(new AnimationWithTimeAndPrio(prio, time, animation, mirrored));
        }
    }
}
