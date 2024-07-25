/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
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
    /// Implements bitonal using one threshold value.
    /// </summary>
    public class BitonalThreshold : Bitonal
    {
        protected readonly int threshold;
        /// <summary>
        /// construct BitonalThreshold
        /// </summary>
        /// <param name="threshold">unique threshold value with range 0 to 255.</param>
        public BitonalThreshold(int threshold)
        {
            if (threshold < 0 || threshold > 255)
            {
                throw new ArgumentException("threshold range must be between 0 and 255");
            }

            this.threshold = threshold;
        }

        /// <summary>
        /// construct BitonalThreshold
        /// </summary>
        /// <param name="threshold">unique threshold value with range 0 to 255.</param>
        /// <summary>
        /// construct BitonalThreshold with default value.
        /// </summary>
        public BitonalThreshold()
        {
            this.threshold = 127;
        }

        /// <summary>
        /// construct BitonalThreshold
        /// </summary>
        /// <param name="threshold">unique threshold value with range 0 to 255.</param>
        /// <summary>
        /// construct BitonalThreshold with default value.
        /// </summary>
        /// <summary>
        /// translate RGBA colors to 0 or 1 (print or not). <p>
        /// the return is based on threshold value.
        /// </summary>
        /// <param name="alpha">range from 0 to 255</param>
        /// <param name="red">range from 0 to 255</param>
        /// <param name="green">range from 0 to 255</param>
        /// <param name="blue">range from 0 to 255</param>
        /// <param name="x">the X coordinate of the image</param>
        /// <param name="y">the Y coordinate of the image</param>
        /// <returns> 0 or 1</returns>
        /// <remarks>@seeBitonal#zeroOrOne(int, int, int, int, int, int)</remarks>
        public override int ZeroOrOne(int alpha, int red, int green, int blue, int x, int y)
        {
            int luminance = 0xFF;
            if (alpha > 127)
            {
                luminance = (red + green + blue) / 3;
            }

            return (luminance < threshold) ? 1 : 0;
        }
    }
}