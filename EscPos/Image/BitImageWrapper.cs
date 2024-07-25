/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EscPos;
using Java.Io;
using Java.Util;
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
    /// Supply ESC/POS Bit Image commands.<p>
    /// using <code>ESC '*'</code>
    /// </summary>
    public class BitImageWrapper : ImageWrapperInterface, EscPosConst
    {
        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        public enum BitImageMode
        {
            // _8DotSingleDensity(0, 8)
            _8DotSingleDensity,

            // _8DotDoubleDensity(1, 8)
            _8DotDoubleDensity,

            // _24DotSingleDensity(32, 24)
            _24DotSingleDensity,

            // _24DotDoubleDensity_Default(33, 24)
            _24DotDoubleDensity_Default

            // --------------------
            // TODO enum body members
            // public int value;
            // public int bitsForVerticalData;
            // private BitImageMode(int value, int bitsPerSlice) {
            //     this.value = value;
            //     this.bitsForVerticalData = bitsPerSlice;
            // }
            // --------------------
        }

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        protected Justification justification;

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        protected BitImageMode mode;

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        public BitImageWrapper()
        {
            justification = EscPosConst.Justification.Left_Default;
            mode = BitImageMode._24DotDoubleDensity_Default;
        }

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        public virtual BitImageWrapper SetJustification(EscPosConst.Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// Select bit-image mode. <p>
        /// </summary>
        /// <param name="mode">mode to be used on command ESC *</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        public virtual BitImageWrapper SetMode(BitImageMode mode)
        {
            this.mode = mode;
            return this;
        }

        /// <summary>
        /// Values for Bit Image Mode.
        /// </summary>
        /// <remarks>@see#setMode(BitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// Select bit-image mode. <p>
        /// </summary>
        /// <param name="mode">mode to be used on command ESC *</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        /// <summary>
        /// Bit Image commands Assembly into ESC/POS bytes. <p>
        ///
        /// Select justification <p>
        /// ASCII ESC a n <p>
        ///
        /// Set lineSpace in bytes <p>
        /// ASCII ESC '3' n <p>
        ///
        /// write all rows of the raster image <p>
        /// ASCII ESC ✻ m nL nH d1 ... dk <p>
        /// </summary>
        /// <param name="image">to be printed</param>
        /// <returns>bytes of ESC/POS</returns>
        /// <remarks>@seeEscPosImage</remarks>
        public virtual byte[] GetBytes(EscPosImage image)
        {
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();

            //
            bytes.Write(ESC);
            bytes.Write('a');
            bytes.Write(justification.value);

            //
            bytes.Write(ESC);
            bytes.Write('3');
            bytes.Write(16);

            // getting first and second bytes separatted
            int nL = image.GetWidthOfImageInBits() & 0xFF;
            int nH = (image.GetWidthOfImageInBits() & 0xFF00) >> 8;
            IList<ByteArrayOutputStream> RasterColumns = image.GetRasterRows(
                mode.bitsForVerticalData
            );
            foreach (ByteArrayOutputStream rol in RasterColumns)
            {
                //write one rol to print
                bytes.Write(ESC);
                bytes.Write('*');
                bytes.Write(mode.value);
                bytes.Write(nL);
                bytes.Write(nH);
                bytes.Write(rol.ToByteArray(), 0, rol.Count);
                bytes.Write(LF);
            }

            //
            return bytes.ToByteArray();
        }
    }
}
