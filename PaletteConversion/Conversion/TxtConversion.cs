using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PaletteConversion
{
    public class TxtConversion : IPaletteFormatReader, IPaletteFormatWriter
    {
        private IList<string> _fileExtensions;

        public TxtConversion()
        {
            _fileExtensions = new List<string>
            {
                ".txt"
            };

            WritePaintNetMetadata = false;
        }

        public IList<string> FileExtensions { get
            {
                return _fileExtensions;
            }
        }

        public bool WritePaintNetMetadata { get; set; }

        public Palette FromContents(string path, string title = "palette")
        {
            var colors = new List<Color>();
            var description = string.Empty;

            Regex hexRegex = new Regex("[a-fA-F0-9]+", RegexOptions.Compiled);
            Regex commentsRegex = new Regex(";[^\n]+", RegexOptions.Compiled);
            Regex paintNetTitleRegex = new Regex(@";Palette Name: ([^\n]+)", RegexOptions.Compiled);
            Regex paintNetDescriptionRegex = new Regex(@";Description: ([^\n]+)", RegexOptions.Compiled);

            var lines = File.ReadAllLines(path);
            var txtContent = File.ReadAllText(path);

            var paintNetMetaData = false;
            if (lines[0].Trim().StartsWith(";paint.net Palette File"))
            {
                paintNetMetaData = true;
            }

            // Parse metadata
            if (paintNetMetaData)
            {
                txtContent = commentsRegex.Replace(txtContent, "");
                // For every line in the file
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];

                    // If there is metadata, try to parse this line for title or description
                    if (line.StartsWith(";"))
                    {
                        var paintNetTitleMatch = paintNetTitleRegex.Match(line);
                        if (paintNetTitleMatch.Success)
                        {
                            title = paintNetTitleMatch.Groups[1].Value;
                        }

                        var paintNetDescriptionMatch = paintNetDescriptionRegex.Match(line);
                        if (paintNetDescriptionMatch.Success)
                        {
                            description = paintNetDescriptionMatch.Groups[1].Value;
                        }
                    }
                }
            }

            // Parse hexes
            var hexMatches = hexRegex.Matches(txtContent);
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

            if (WritePaintNetMetadata)
            {
                builder.AppendLine(";paint.net Palette File");
                builder.AppendLine(";Palette Name: " + palette.Title);
                builder.AppendLine(";Description: " + palette.Description);
                builder.AppendLine(";Colors: " + palette.Colors.Count);
            }

            foreach (var item in palette.Colors)
            {
                var colorString = ColorUtility.ColorToHexString(item);
                builder.AppendLine("#" + colorString);
            }
            return builder.ToString();
        }
    }
}
