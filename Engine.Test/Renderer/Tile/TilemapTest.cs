using Engine.Renderer.Tile;
using Engine.Renderer.Tile.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Engine.Test.Renderer.Tile
{
    [TestClass]
    public class TilemapTest
    {
        [TestMethod]
        public void TestTilemapParsing()
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //using Stream tilesheet = assembly.GetManifestResourceStream("Game.Test.Resources.tilesheet.png");
            //using Stream tilemapStream = assembly.GetManifestResourceStream("Game.Test.Resources.all.json");

            //Tileset tileset = new Tileset(tilesheet, 16);
            //TilemapModel model = TilemapParser.ParseTilemap(tilemapStream);
            //Tilemap tilemap = new Tilemap(tileset, model);

            //Assert.IsTrue(tileset.AmountTileWidth == 48, "Tileset was read wrong, Tile amount in width should be 48, not " + tileset.AmountTileWidth);
            //Assert.IsTrue(tileset.AmountTileHeight == 22, "Tileset was read wrong, Tile amount in height should be 22, not " + tileset.AmountTileHeight);
        }
    }
}
