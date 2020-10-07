using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PaletteConversion
{
    public class GplConversion : IPaletteFormatReader, IPaletteFormatWriter
    {
        private IList<string> _fileExtensions;

        public GplConversion()
        {
            _fileExtensions = new List<string>
            {
                ".gpl"
            };
        }

        public IList<string> FileExtensions { get
            {
                return _fileExtensions;
            }
        }

        public Palette FromContents(string gplContent, string title = "palette")
        {
            var colors = new List<Color>();
            var description = string.Empty;

            Regex gplColorRegex = new Regex(@"([0-9]+)\s([0-9]+)\s([0-9]+)\s+[0-9a-zA-Z]+", RegexOptions.Compiled);
            Regex commentsRegex = new Regex(";[^\n]+", RegexOptions.Compiled);
            Regex gplTitleRegex = new Regex(@"#Palette Name: ([^\n]+)", RegexOptions.Compiled);
            Regex gplDescriptionRegex = new Regex(@"#Description: ([^\n]+)", RegexOptions.Compiled);

            var lines = gplContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            gplContent = commentsRegex.Replace(gplContent, "");

            // For every line in the file except first line
            // Parse Metadata
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                // If there is metadata, try to parse this line for title or description
                if (line.StartsWith("#"))
                {
                    var titleMatch = gplTitleRegex.Match(line);
                    if (titleMatch.Success)
                    {
                        title = titleMatch.Groups[1].Value;
                    }

                    var descriptionMatch = gplDescriptionRegex.Match(line);
                    if (descriptionMatch.Success)
                    {
                        description = descriptionMatch.Groups[1].Value;
                    }
                }
            }

            var colorMatches = gplColorRegex.Matches(gplContent);

            foreach (Match item in colorMatches)
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
            var builder = new StringBuilder();
            builder.AppendLine("GIMP Palette");
            builder.AppendLine("#Palette Name: " + palette.Title);
            builder.AppendLine("#Description: " + palette.Description);
            builder.AppendLine("#Colors: " + palette.Colors.Count);

            foreach (var color in palette.Colors)
            {
                builder.AppendLine(color.R + "\t" + color.G + "\t" + color.B + "\t" +
                    ColorUtility.ColorToHexString(color));
            }
            return builder.ToString();
        }
    }
}
