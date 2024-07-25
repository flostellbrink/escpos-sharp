using Java.Awt.Image;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Com.Github.Anastaciocintra.Escpos.Image.CharacterCodeTable;
using static Com.Github.Anastaciocintra.Escpos.Image.CutMode;
using static Com.Github.Anastaciocintra.Escpos.Image.PinConnector;
using static Com.Github.Anastaciocintra.Escpos.Image.Justification;
using static Com.Github.Anastaciocintra.Escpos.Image.FontName;
using static Com.Github.Anastaciocintra.Escpos.Image.FontSize;
using static Com.Github.Anastaciocintra.Escpos.Image.Underline;
using static Com.Github.Anastaciocintra.Escpos.Image.ColorMode;
using static Com.Github.Anastaciocintra.Escpos.Image.BarCodeSystem;
using static Com.Github.Anastaciocintra.Escpos.Image.BarCodeHRIPosition;
using static Com.Github.Anastaciocintra.Escpos.Image.BarCodeHRIFont;
using static Com.Github.Anastaciocintra.Escpos.Image.PDF417ErrorLevel;
using static Com.Github.Anastaciocintra.Escpos.Image.PDF417Option;
using static Com.Github.Anastaciocintra.Escpos.Image.QRModel;
using static Com.Github.Anastaciocintra.Escpos.Image.QRErrorCorrectionLevel;
using static Com.Github.Anastaciocintra.Escpos.Image.BitImageMode;

namespace Com.Github.Anastaciocintra.Escpos.Image
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