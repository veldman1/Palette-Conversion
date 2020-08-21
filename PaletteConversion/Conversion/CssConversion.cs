using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PaletteConversion
{
    public class CssConversion : IPaletteFormatReader
    {
        private IList<string> _fileExtensionsList;

        private Regex _regexRgb;
        private Regex _regexHex;

        public CssConversion()
        {
            _fileExtensionsList = new List<string>
            {
                ".css"
            };

            _regexRgb = new Regex(@"rgb\s*\(\s*([0-9]+)[\s,]*([0-9]+)[\s,]*([0-9]+)[\s,]*\)", RegexOptions.Compiled);
            _regexHex = new Regex(@"color\s*:\s*#?([a-fA-F0-9]+)", RegexOptions.Compiled);
        }

        public IList<string> FileExtensions
        {
            get
            {
                return _fileExtensionsList;
            }
        }

        public Palette ReadPaletteFromPath(string path)
        {
            var cssContent = File.ReadAllText(path);

            var colors = new List<Color>();

            var rgbMatches = _regexRgb.Matches(cssContent);
            foreach (Match match in rgbMatches)
            {
                var r = int.Parse(match.Groups[1].Value);
                var g = int.Parse(match.Groups[2].Value);
                var b = int.Parse(match.Groups[3].Value);
                colors.Add(Color.FromArgb(r, g, b));
            }

            var hexMatches = _regexHex.Matches(cssContent);
            foreach (Match item in hexMatches)
            {
                colors.Add(ColorUtility.ParseHexColor(item.Groups[1].Value));
            }

            return new Palette
            {
                Colors = colors,
                Description = string.Empty,
                Title = Path.GetFileNameWithoutExtension(path),
            };
        }
    }
}
