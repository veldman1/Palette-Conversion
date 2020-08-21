using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteConversion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PaletteConversionTests
{
    [TestClass]
    public class TestFormatWriting
    {
        public static Palette GetSamplePalette()
        {
            return new Palette
            {
                Colors = new List<Color> {
                    Color.FromArgb(255, 82, 18, 150),
                    Color.FromArgb(255, 138, 31, 172),
                    Color.FromArgb(255, 212, 134, 74),
                    Color.FromArgb(255, 235, 219, 94),
                },
                Description = string.Empty,
                Title = "test",
            };
        }

        public static Palette GetSamplePaletteWithMetadata()
        {
            return new Palette
            {
                Colors = new List<Color> {
                    Color.FromArgb(255, 82, 18, 150),
                    Color.FromArgb(255, 138, 31, 172),
                    Color.FromArgb(255, 212, 134, 74),
                    Color.FromArgb(255, 235, 219, 94),
                },
                Description = "My Description",
                Title = "TestPalette",
            };
        }

        [TestMethod]
        public void TestPlainTxt()
        {
            TestWriteSimple(typeof(TxtConversion), "expected-plain.txt");
        }

        [TestMethod]
        public void TestHex()
        {
            TestWriteSimple(typeof(HexConversion), "expected.hex");
        }

        [TestMethod]
        public void TestPal()
        {
            TestWriteSimple(typeof(PalConversion), "expected.pal");
        }

        public void TestWriteSimple(Type conversionType, string filename)
        {
            var conversion = (IPaletteFormatWriter)Activator.CreateInstance(conversionType);
            var palette = GetSamplePalette();
            var actual = conversion.PaletteToFormat(palette);

            var expected = File.ReadAllText("./ExpectedWrite/" + filename);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGpl()
        {
            var gplConversion = new GplConversion();
            var palette = GetSamplePaletteWithMetadata();
            var actual = gplConversion.PaletteToFormat(palette);

            var expected = File.ReadAllText("./ExpectedWrite/expected.gpl");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPaintNetTxt()
        {
            var txtConversion = new TxtConversion { WritePaintNetMetadata = true };
            var palette = GetSamplePaletteWithMetadata();
            var actual = txtConversion.PaletteToFormat(palette);

            var expected = File.ReadAllText("./ExpectedWrite/expected-paintnet.txt");

            Assert.AreEqual(expected, actual);
        }
    }
}
