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
    /// Specify the interface to work with image
    /// </summary>
    /// <remarks>
    /// @seeCoffeeImageImpl
    /// @see<a href="https://github.com/anastaciocintra/AdroidEscposCoffee">EscPosCoffee image on Android</a>
    /// </remarks>
    public interface CoffeeImage
    {
        /// <summary>
        /// Returns the width of the image
        /// </summary>
        /// <remarks>@seeBufferedImage#getWidth()</remarks>
        int GetWidth();
        /// <summary>
        /// Returns the height of the image
        /// </summary>
        /// <remarks>@seeBufferedImage#getHeight()</remarks>
        int GetHeight();
        /// <summary>
        /// Returns a subimage defined by a specified rectangular region.
        /// The returned CoffeeImage shares the same data array as the original image.
        /// </summary>
        /// <param name="x">- the X coordinate of the upper-left corner of the specified rectangular region</param>
        /// <param name="y">- the Y coordinate of the upper-left corner of the specified rectangular region</param>
        /// <param name="w">- the width of the specified rectangular region</param>
        /// <param name="h">- the height of the specified rectangular region</param>
        /// <returns>a CoffeeImage that is the subimage of this CoffeeImage.</returns>
        /// <remarks>@seeBufferedImage#getSubimage(int, int, int, int)</remarks>
        CoffeeImage GetSubimage(int x, int y, int w, int h);
        /// <summary>
        /// Returns an integer pixel in the default RGB color model (TYPE_INT_ARGB) and default sRGB colorspace.
        /// Color conversion takes place if this default model does not match the image ColorModel.
        /// There are only 8-bits of precision for each color component in the returned data when using this method.
        /// </summary>
        /// <param name="x">- the X coordinate of the pixel from which to get the pixel in the default RGB color model and sRGB color space</param>
        /// <param name="y">- the Y coordinate of the pixel from which to get the pixel in the default RGB color model and sRGB color space</param>
        /// <returns>an integer pixel in the default RGB color model and default sRGB colorspace.</returns>
        /// <remarks>@seeBufferedImage#getRGB(int, int)</remarks>
        int GetRGB(int x, int y);
    }
}