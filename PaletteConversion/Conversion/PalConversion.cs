using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PaletteConversion
{
    public class PalConversion : IPaletteFormatReader, IPaletteFormatWriter
    {
        private IList<string> _fileExtensions;

        public PalConversion()
        {
            _fileExtensions = new List<string>
            {
                ".pal"
            };
        }

        public IList<string> FileExtensions { get
            {
                return _fileExtensions;
            }
        }

        public Palette ReadPaletteFromPath(string path)
        {
            var colors = new List<Color>();
            var title = Path.GetFileNameWithoutExtension(path);
            var description = string.Empty;

            Regex _regexHex = new Regex(@"^\s*([0-9]+)\s+([0-9]+)\s+([0-9]+)\s*$", RegexOptions.Multiline);

            var content = File.ReadAllText(path);

            var rgbMatches = _regexHex.Matches(content);
            foreach (Match item in rgbMatches)
            {
                int r = int.Parse(item.Groups[1].Value);
                int g = int.Parse(item.Groups[2].Value);
                int b = int.Parse(item.Groups[3].Value);

                colors.Add(Color.FromArgb(r, g, b));
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
            builder.AppendLine("JASC-PAL");
            builder.AppendLine("0100");
            builder.AppendLine(palette.Colors.Count.ToString());
            foreach (var color in palette.Colors)
            {
                builder.AppendLine(color.R + " " + color.G + " " + color.B);
            }
            return builder.ToString();
        }
    }
}
