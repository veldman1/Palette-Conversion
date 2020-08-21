using System.Collections.Generic;
using System.Dynamic;

namespace PaletteConversion
{
    public interface IFileFormat
    {
        IList<string> FileExtensions { get; }
    }
}