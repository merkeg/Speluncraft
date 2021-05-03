using System.IO;
using System.Reflection;
using Engine.Renderer.Text.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineTest.Renderer
{
    [TestClass]
    public class FontTest
    {

        [TestMethod]
        public void TestFontDataParsing()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream fontStream = Util.GetEmbeddedResourceStream(assembly, "Resources.font.fnt");
            FontModel model = FontModel.Parse(fontStream);
            
            Assert.IsTrue(model.Characters.Count == 98, "Character count does not match given parameters");
        }

        [TestMethod]
        public void TestFontCharacter()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream fontStream = Util.GetEmbeddedResourceStream(assembly, "Resources.font.fnt");
            FontModel model = FontModel.Parse(fontStream);
            
            FontModel.FontModelCharacter character = model.Characters['a'];
            
            // Test if character has the right parameters from fontmap
            Assert.IsTrue(character.X == 101);
            Assert.IsTrue(character.Y == 360);
            Assert.IsTrue(character.Width == 40);
            Assert.IsTrue(character.Height == 52);
            Assert.IsTrue(character.XOffset == 0);
            Assert.IsTrue(character.YOffset == 39);
            Assert.IsTrue(character.XAdvance == 45);
        }
    }
}