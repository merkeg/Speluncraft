// <copyright file="Bundle.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Scene
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bundle class for scene change intents.
    /// </summary>
    public class Bundle : Dictionary<string, object>
    {
        /// <summary>
        /// Gets the default bundle.
        /// </summary>
        public static Bundle Default { get; } = new Bundle();

        /// <summary>
        /// Adds to the bundle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">Type.</typeparam>
        public void Add<T>(string key, T value)
        {
            base.Add(key, value);
        }

        /// <summary>
        /// Get typed item from bundle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="def">Default value.</param>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>The value.</returns>
        public T Get<T>(string key, T def)
        {
            if (this.ContainsKey(key))
            {
                return (T)this[key];
            }

            return def;
        }
    }
}