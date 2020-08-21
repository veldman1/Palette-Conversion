using System;

namespace PaletteConversion
{
    public interface IPaletteFormatReader : IFileFormat

    {
        Palette ReadPaletteFromPath(string path);
    }
}
