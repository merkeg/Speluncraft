﻿using Engine.Renderer.Tile;
using Engine.Renderer.Tile.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Engine.Test.Renderer
{
    [TestClass]
    public class TilemapTest
    {
        [TestMethod]
        public void TestTilemapModel()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream tilemapStream = GetEmbeddedResourceStream(assembly, "Resources.all.json");

            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            
            Assert.IsTrue(model.width == 48, "Model was read wrong, Tile amount in width should be 48, not " + model.width);
            Assert.IsTrue(model.height == 22, "Model was read wrong, Tile amount in height should be 22, not " + model.height);
            Assert.IsTrue(model.layers.Count == 1, "Model was read wrong, Layer amount should be 1.");
            Assert.IsTrue(model.tilewidth == 16, "Tile width should be 16");
            Assert.IsTrue(model.tileheight == 16, "Tile height should be 16");

            TilemapLayerModel layerModel = model.layers[0];
            Assert.IsNotNull(layerModel, "Layer should not be null");

            Assert.IsTrue(layerModel.id == 1, "Layer id should be 1");
            Assert.IsTrue(layerModel.opacity == 1, "Layer opacity should be 1");
            Assert.IsTrue(layerModel.width == 48, "Layer width must be 48");
            Assert.IsTrue(layerModel.height == 22, "Layer height must be 22");

            Assert.IsTrue(layerModel.properties == null, "There should be no properties");
        }

        [TestMethod]
        public void TestTilemap()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream tilemapStream = GetEmbeddedResourceStream(assembly, "Resources.all.json");

            TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            Tilemap tilemap = new Tilemap(null, model);

            Assert.IsTrue(tilemap.Layers.Length == 1, "Tilemap layer count should be 1");
            TilemapLayer layer = tilemap[0];
            Assert.IsTrue(layer.Width == 48, "Layer width must be 48");
            Assert.IsTrue(layer.Height == 22, "Layer height must be 22");

            Assert.IsTrue(layer[0, 0] == 1, "Wrong tile, " + layer[0, 0]);
        }

        // https://www.codeproject.com/Tips/5256504/Using-Embedded-Resources-in-Unit-Tests-with-NET
        public static Stream GetEmbeddedResourceStream(Assembly assembly, string relativeResourcePath)
        {
            if (string.IsNullOrEmpty(relativeResourcePath))
                throw new ArgumentNullException("relativeResourcePath");

            var resourcePath = String.Format("{0}.{1}",
                Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$", string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);

            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException(String.Format("The specified embedded resource \"{0}\" is not found.", relativeResourcePath));
            return stream;
        
        }
    }
}
