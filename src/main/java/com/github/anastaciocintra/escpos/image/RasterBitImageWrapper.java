/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using Com.Github.Anastaciocintra.Escpos;
using Com.Github.Anastaciocintra.Escpos.EscPosConst;
using Java.Io;
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
using static Com.Github.Anastaciocintra.Escpos.Image.GraphicsImageBxBy;
using static Com.Github.Anastaciocintra.Escpos.Image.RasterBitImageMode;

namespace Com.Github.Anastaciocintra.Escpos.Image
{
    /// <summary>
    /// Supply ESC/POS Raster bit Image commands.<p>
    /// using <code>GS 'v' '0'</code>
    /// </summary>
    public class RasterBitImageWrapper : EscPosConst, ImageWrapperInterface
    {
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        public enum RasterBitImageMode
        {
            // Normal_Default(0)
            Normal_Default,
            // DoubleWidth(1)
            DoubleWidth,
            // DoubleHeight(2)
            DoubleHeight,
            // Quadruple(3)
            Quadruple 

            // --------------------
            // TODO enum body members
            // public int value;
            // private RasterBitImageMode(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        protected Justification justification;
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        protected RasterBitImageMode rasterBitImageMode;
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        public RasterBitImageWrapper()
        {
            justification = EscPosConst.Justification.Left_Default;
            rasterBitImageMode = RasterBitImageMode.Normal_Default;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        public virtual RasterBitImageWrapper SetJustification(EscPosConst.Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// Set the mode of Raster Bit Image.<p>
        /// </summary>
        /// <param name="rasterBitImageMode">mode to be used with GS v 0</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        public virtual RasterBitImageWrapper SetRasterBitImageMode(RasterBitImageMode rasterBitImageMode)
        {
            this.rasterBitImageMode = rasterBitImageMode;
            return this;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setRasterBitImageMode(RasterBitImageMode)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// Set the mode of Raster Bit Image.<p>
        /// </summary>
        /// <param name="rasterBitImageMode">mode to be used with GS v 0</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        /// <summary>
        /// Bit Image commands Assembly into ESC/POS bytes. <p>
        /// 
        /// Select justification <p>
        /// ASCII ESC a n <p>
        /// 
        /// Print raster bit image <p>
        /// ASCII GS v 0 m xL xH yL yH d1...dk <p>
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
            bytes.Write(GS);
            bytes.Write('v');
            bytes.Write('0');
            bytes.Write(rasterBitImageMode.value);

            //
            //  bytes in horizontal direction for the bit image
            int horizontalBytes = image.GetHorizontalBytesOfRaster();
            int xL = horizontalBytes & 0xFF;
            int xH = (horizontalBytes & 0xFF00) >> 8;

            // 
            //  bits in vertical direction for the bit image
            int verticalBits = image.GetHeightOfImageInBits();

            // getting first and second bytes separatted
            int yL = verticalBits & 0xFF;
            int yH = (verticalBits & 0xFF00) >> 8;
            bytes.Write(xL);
            bytes.Write(xH);
            bytes.Write(yL);
            bytes.Write(yH);

            // write raster bytes
            byte[] rasterBytes = image.GetRasterBytes().ToByteArray();
            bytes.Write(rasterBytes, 0, rasterBytes.length);

            //
            return bytes.ToByteArray();
        }
    }
}