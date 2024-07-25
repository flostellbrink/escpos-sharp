using Java.Awt.Image;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static EscPos.Image.CharacterCodeTable;
using static EscPos.Image.CutMode;
using static EscPos.Image.PinConnector;
using static EscPos.Image.Justification;
using static EscPos.Image.FontName;
using static EscPos.Image.FontSize;
using static EscPos.Image.Underline;
using static EscPos.Image.ColorMode;
using static EscPos.Image.BarCodeSystem;
using static EscPos.Image.BarCodeHRIPosition;
using static EscPos.Image.BarCodeHRIFont;
using static EscPos.Image.PDF417ErrorLevel;
using static EscPos.Image.PDF417Option;
using static EscPos.Image.QRModel;
using static EscPos.Image.QRErrorCorrectionLevel;
using static EscPos.Image.BitImageMode;

namespace EscPos.Image
{
    /// <summary>
    /// implements CoffeeImage using Java BufferedImage
    /// </summary>
    /// <remarks>
    /// @seeCoffeeImage
    /// @seeBufferedImage
    /// </remarks>
    public class CoffeeImageImpl : CoffeeImage
    {
        protected BufferedImage image;
        public CoffeeImageImpl(BufferedImage image)
        {
            this.image = image;
        }

        public virtual int GetWidth()
        {
            return image.GetWidth();
        }

        public virtual int GetHeight()
        {
            return image.GetHeight();
        }

        public virtual CoffeeImage GetSubimage(int x, int y, int w, int h)
        {
            return new CoffeeImageImpl(image.GetSubimage(x, y, w, h));
        }

        public virtual int GetRGB(int x, int y)
        {
            return image.GetRGB(x, y);
        }
    }
}