/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static EscPos.Image.BarCodeHRIFont;
using static EscPos.Image.BarCodeHRIPosition;
using static EscPos.Image.BarCodeSystem;
using static EscPos.Image.BitImageMode;
using static EscPos.Image.CharacterCodeTable;
using static EscPos.Image.ColorMode;
using static EscPos.Image.CutMode;
using static EscPos.Image.FontName;
using static EscPos.Image.FontSize;
using static EscPos.Image.Justification;
using static EscPos.Image.PDF417ErrorLevel;
using static EscPos.Image.PDF417Option;
using static EscPos.Image.PinConnector;
using static EscPos.Image.QRErrorCorrectionLevel;
using static EscPos.Image.QRModel;
using static EscPos.Image.Underline;

namespace EscPos.Image
{
    /// <summary>
    /// Abstract base for  algorithms that transform RGB to bitonal.<p>
    /// Used on {@link EscPosImage#EscPosImage EscPosImage constructor}.
    /// Any dither algorithm can be implemented, like ordered grid or noise dither.
    /// Generally, you need to call image.getRGB(x, y) and decide if return zero or one.
    /// you need to Override only the {@link #zeroOrOne(int, int, int, int, int, int) zeroOrOne} method
    /// but if you want, you can Override {@link  #getBitonalVal(CoffeeImage, int, int)}  getBitonalVal} too.
    /// </summary>
    public abstract class Bitonal
    {
        /// <summary>
        /// Pre-work each coordinate (x, y) of image by discover RGBA values.
        /// get the 8-bits values separated by alpha, red, blue and green and return by
        /// calling {@link #zeroOrOne(int, int, int, int, int, int) } to make print or not decision.
        /// </summary>
        /// <param name="image">RGB image.</param>
        /// <param name="x">the X coordinate of the pixel from which to get
        ///          the pixel</param>
        /// <param name="y">the Y coordinate of the pixel from which to get
        ///          the pixel</param>
        /// <returns> call zeroOrOne to make decision (0 or 1)</returns>
        /// <remarks>@see#zeroOrOne(int, int, int, int, int, int)</remarks>
        public virtual int GetBitonalVal(CoffeeImage image, int x, int y)
        {
            int RGBA = image.GetRGB(x, y);
            int alpha = (RGBA >> 24) & 0xFF;
            int red = (RGBA >> 16) & 0xFF;
            int green = (RGBA >> 8) & 0xFF;
            int blue = RGBA & 0xFF;
            return ZeroOrOne(alpha, red, green, blue, x, y);
        }

        /// <summary>
        /// Subclasses need to translate the 8-bits RGBA colors to 0 or 1 (print or not) <p>
        /// for any coordinate x, y of the BufferedImage.
        /// </summary>
        /// <param name="alpha">range from 0 to 255</param>
        /// <param name="red">range from 0 to 255</param>
        /// <param name="green">range from 0 to 255</param>
        /// <param name="blue">range from 0 to 255</param>
        /// <param name="x">the X coordinate of the image</param>
        /// <param name="y">the Y coordinate of the image</param>
        /// <returns> 0 or 1</returns>
        public abstract int ZeroOrOne(int alpha, int red, int green, int blue, int x, int y);
    }
}