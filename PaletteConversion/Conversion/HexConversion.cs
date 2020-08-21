using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PaletteConversion
{
    public class HexConversion : IPaletteFormatReader, IPaletteFormatWriter
    {
        private IList<string> _fileExtensions;

        public HexConversion()
        {
            _fileExtensions = new List<string>
            {
                ".hex"
            };
        }

        public IList<string> FileExtensions { get
            {
                return _fileExtensions;
            }
        }

        public Palette ReadPaletteFromPath(string path)
        {
            Regex hexRegex = new Regex("[a-fA-F0-9]+", RegexOptions.Compiled);

            var colors = new List<Color>();
            var title = Path.GetFileNameWithoutExtension(path);
            var description = string.Empty;

            Regex _regexHex = new Regex("[^a-fA-F0-9]", RegexOptions.Compiled);

            var content = File.ReadAllText(path);

            var hexMatches = hexRegex.Matches(content);
            foreach (Match item in hexMatches)
            {
                colors.Add(ColorUtility.ParseHexColor(item.Value));
            }

            return new Palette
            {
                Colors = colors,
                Description = description,
                Title = title,
            };
        }

        public string PaletteToFormat(Palette palette)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var color in palette.Colors)
            {
                builder.AppendLine(ColorUtility.ColorToHexString(color));
            }

            return builder.ToString();

        }
    }
}
