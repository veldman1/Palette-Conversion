using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteConversion;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PaletteConversionTests
{
    [TestClass]
    public class TestFormatReading
    {
        private List<Color> _expectedColors;

        [TestInitialize]
        public void TestInitialize()
        {
            _expectedColors = new List<Color> {
                Color.FromArgb(255, 82, 18, 150),
                Color.FromArgb(255, 138, 31, 172),
                Color.FromArgb(255, 212, 134, 74),
                Color.FromArgb(255, 235, 219, 94),
            };
        }

        [TestMethod]
        public void TestPlainTxt()
        {
            var plainTextConversion = new TxtConversion();
            var loadedPalette = plainTextConversion.ReadPaletteFromPath("./TestRead/test-plain.txt");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("test-plain", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestPaintNetTxt()
        {
            var plainTextConversion = new TxtConversion();
            var loadedPalette = plainTextConversion.ReadPaletteFromPath("./TestRead/test-paintnet.txt");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("TestPalette", loadedPalette.Title);
            Assert.AreEqual("My Description", loadedPalette.Description);
        }

        [TestMethod]
        public void TestCss()
        {
            var cssExpectedColors = new List<Color>(_expectedColors);
            cssExpectedColors.Add(Color.FromArgb(255, 255, 255));
            cssExpectedColors.Add(Color.FromArgb(255, 0, 0));
            cssExpectedColors.Add(Color.FromArgb(255, 0, 255));
            cssExpectedColors.Add(Color.FromArgb(0, 0, 255));

            var cssConversion = new CssConversion();
            var loadedPalette = cssConversion.ReadPaletteFromPath("./TestRead/test.css");

            Assert.IsTrue(Enumerable.SequenceEqual(cssExpectedColors, loadedPalette.Colors));
            Assert.AreEqual("test", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestHex()
        {
            var hexConversion = new HexConversion();
            var loadedPalette = hexConversion.ReadPaletteFromPath("./TestRead/test.hex");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("test", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestPal()
        {
            var palConversion = new PalConversion();
            var loadedPalette = palConversion.ReadPaletteFromPath("./TestRead/test.pal");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("test", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestGpl()
        {
            var gplConversion = new GplConversion();
            var loadedPalette = gplConversion.ReadPaletteFromPath("./TestRead/test.gpl");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("TestPalette", loadedPalette.Title);
            Assert.AreEqual("My Description", loadedPalette.Description);
        }
    }
}
