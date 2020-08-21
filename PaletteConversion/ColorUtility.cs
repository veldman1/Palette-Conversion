using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace PaletteConversion
{
    class ColorUtility
    {
        public static Color ParseHexColor(string hex)
        {
            // Try to parse the line as a color
            if (hex.Length == 8)
            {
                byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));

                return Color.FromArgb(a, r, g, b);
            }
            else if (hex.Length == 6)
            {
                byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));

                return Color.FromArgb(255, r, g, b);
            } else if (hex.Length == 3)
            {
                int r = int.Parse(hex.Substring(0, 1) + hex.Substring(0, 1), NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(1, 1) + hex.Substring(1, 1), NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(2, 1) + hex.Substring(2, 1), NumberStyles.HexNumber);

                return Color.FromArgb(255, r, g, b);
            }
            return Color.Black;
        }

        public static string ColorToHexString(Color color)
        {
            byte[] bytes = { color.R, color.G, color.B };
            return BitConverter.ToString(bytes).ToLower().Replace("-", string.Empty);
        }
    }
}
