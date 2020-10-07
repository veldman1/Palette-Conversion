using System;

namespace PaletteConversion
{
    public interface IPaletteFormatReader : IFileFormat

    {
        Palette FromContents(string path, string title);
    }
}
