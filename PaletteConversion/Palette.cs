using System.Collections.Generic;
using System.Drawing;

namespace PaletteConversion
{
    public class Palette
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Color> Colors { get; set; }
    }
}