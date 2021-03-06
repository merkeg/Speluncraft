// <copyright file="TilemapModel.cs" company="RWUwU">
// Copyright (c) RWUwU. All rights reserved.
// </copyright>

namespace Engine.Renderer.Tile.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    public class TilemapModel
    {
        public int tileheight { get; set; }

        public int tilewidth { get; set; }

        public int height { get; set; }

        public int width { get; set; }

        public string backgroundcolor { get; set; }

        public IList<TilemapLayerModel> layers { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Model class")]
    public class TilemapLayerModel
    {
        public uint[] data { get; set; }

        public TilemapLayerObjectModel[] objects { get; set; }

        public int height { get; set; }

        public int width { get; set; }

        public int id { get; set; }

        public float opacity { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public CustomPropertyModel[] properties { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Model class")]
    public class CustomPropertyModel
    {
        public string name { get; set; }

        public string type { get; set; }

        public object value { get; set; }

        public T ValueAsType<T>()
        {
            return (T)Convert.ChangeType(this.value, typeof(T), CultureInfo.InvariantCulture);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Model class")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Model class")]
    public class TilemapLayerObjectModel
    {
        public float x { get; set; }

        public float y { get; set; }

        public float width { get; set; }

        public float height { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public bool point { get; set; }

        public string type { get; set; }

        public CustomPropertyModel[] properties { get; set; }
    }
}
