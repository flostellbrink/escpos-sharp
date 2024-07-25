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

namespace Com.Github.Anastaciocintra.Escpos.Image
{
    /// <summary>
    /// Supply ESC/POS Graphics print Image commands.<p>
    /// using <code>GS(L</code>
    /// </summary>
    public class GraphicsImageWrapper : EscPosConst, ImageWrapperInterface
    {
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        public enum GraphicsImageBxBy
        {
            // Normal_Default(1, 1)
            Normal_Default,
            // DoubleWidth(2, 1)
            DoubleWidth,
            // DoubleHeight(1, 2)
            DoubleHeight,
            // Quadruple(2, 2)
            Quadruple 

            // --------------------
            // TODO enum body members
            // public int bx;
            // public int by;
            // private GraphicsImageBxBy(int bx, int by) {
            //     this.bx = bx;
            //     this.by = by;
            // }
            // --------------------
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        protected Justification justification;
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        protected GraphicsImageBxBy graphicsImageBxBy;
        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        public GraphicsImageWrapper()
        {
            justification = EscPosConst.Justification.Left_Default;
            graphicsImageBxBy = GraphicsImageBxBy.Normal_Default;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        public virtual GraphicsImageWrapper SetJustification(EscPosConst.Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set values of Bx and By referring to the image size. <p>
        /// </summary>
        /// <param name="graphicsImageBxBy">values used on function 112</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        public virtual GraphicsImageWrapper SetGraphicsImageBxBy(GraphicsImageBxBy graphicsImageBxBy)
        {
            this.graphicsImageBxBy = graphicsImageBxBy;
            return this;
        }

        /// <summary>
        /// Values for Raster Bit Image mode.
        /// </summary>
        /// <remarks>@see#setGraphicsImageBxBy(GraphicsImageBxBy)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set values of Bx and By referring to the image size. <p>
        /// </summary>
        /// <param name="graphicsImageBxBy">values used on function 112</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(EscPosImage)</remarks>
        /// <summary>
        /// Bit Image commands Assembly into ESC/POS bytes. <p>
        /// 
        /// Select justification <p>
        /// ASCII ESC a n <p>
        /// 
        /// function 112 Store the graphics data in the print buffer  <p>
        /// GS(L pL pH m fn a bx by c xL xH yL yH d1...dk  <p>
        /// 
        /// function 050 Prints the buffered graphics data <p>
        /// GS ( L pL pH m fn  <p>
        /// </summary>
        /// <param name="image">to be printed</param>
        /// <returns>bytes of ESC/POS</returns>
        /// <remarks>
        /// @seeEscPosImage#getRasterBytes()
        /// @seeEscPosImage#getRasterSizeInBytes()
        /// </remarks>
        public virtual byte[] GetBytes(EscPosImage image)
        {
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();

            //
            bytes.Write(ESC);
            bytes.Write('a');
            bytes.Write(justification.value);

            //
            int paramSize = image.GetRasterSizeInBytes() + 10;
            int pL = paramSize & 0xFF;
            int pH = (paramSize & 0xFF00) >> 8;
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('L');
            bytes.Write(pL); // pl
            bytes.Write(pH); // ph
            bytes.Write(48); // m
            bytes.Write(112); //fn
            bytes.Write(48); // a
            bytes.Write(graphicsImageBxBy.bx); // bx
            bytes.Write(graphicsImageBxBy.by); // by
            bytes.Write(49); // c

            //  bits in horizontal direction for the bit image
            int horizontalBits = image.GetWidthOfImageInBits();
            int xL = horizontalBits & 0xFF;
            int xH = (horizontalBits & 0xFF00) >> 8;

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

            // write bytes
            byte[] rasterBytes = image.GetRasterBytes().ToByteArray();
            bytes.Write(rasterBytes, 0, rasterBytes.length);

            // function 050
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('L');
            bytes.Write(2); // pl
            bytes.Write(0); // ph
            bytes.Write(48); //m
            bytes.Write(50); //fn

            //
            return bytes.ToByteArray();
        }
    }
}