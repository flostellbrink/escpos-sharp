/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
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