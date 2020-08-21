namespace PaletteConversion
{
    public interface IPaletteFormatWriter : IFileFormat
    {
        string PaletteToFormat(Palette palette);
    }
}