using Engine.Renderer.Tile;

namespace EngineTest.Renderer
{
    public class TilesheetStud : ITilesheet
    {
        public int Handle => 0;
        public int AmountTileHeight => 10;
        public int AmountTileWidth => 10;
        public int TileSizeX => 16;
        public int TileSizeY => 16;
    }
}