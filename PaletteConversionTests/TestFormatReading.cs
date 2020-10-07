using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteConversion;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            var loadedPalette = plainTextConversion.FromContents("./TestRead/test-plain.txt", "test-plain");

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("test-plain", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestPaintNetTxt()
        {
            var plainTextConversion = new TxtConversion();
            var loadedPalette = plainTextConversion.FromContents("./TestRead/test-paintnet.txt");

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

            var path = "./TestRead/test.css";
            var contents = File.ReadAllText(path);
            var loadedPalette = cssConversion.FromContents(contents);

            Assert.IsTrue(Enumerable.SequenceEqual(cssExpectedColors, loadedPalette.Colors));
            Assert.AreEqual("palette", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestHex()
        {
            var hexConversion = new HexConversion();

            var path = "./TestRead/test.hex";
            var contents = File.ReadAllText(path);
            var loadedPalette = hexConversion.FromContents(contents);

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("palette", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestPal()
        {
            var palConversion = new PalConversion();

            var path = "./TestRead/test.pal";
            var contents = File.ReadAllText(path);
            var loadedPalette = palConversion.FromContents(contents);

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("palette", loadedPalette.Title);
            Assert.AreEqual(string.Empty, loadedPalette.Description);
        }

        [TestMethod]
        public void TestGpl()
        {
            var gplConversion = new GplConversion();

            var path = "./TestRead/test.gpl";
            var contents = File.ReadAllText(path);
            var loadedPalette = gplConversion.FromContents(contents);

            Assert.IsTrue(Enumerable.SequenceEqual(_expectedColors, loadedPalette.Colors));
            Assert.AreEqual("TestPalette", loadedPalette.Title);
            Assert.AreEqual("My Description", loadedPalette.Description);
        }
    }
}
