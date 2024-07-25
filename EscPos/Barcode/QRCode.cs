/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using EscPos;
using Java.Io;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static EscPos.Barcode.CharacterCodeTable;
using static EscPos.Barcode.CutMode;
using static EscPos.Barcode.PinConnector;
using static EscPos.Barcode.Justification;
using static EscPos.Barcode.FontName;
using static EscPos.Barcode.FontSize;
using static EscPos.Barcode.Underline;
using static EscPos.Barcode.ColorMode;
using static EscPos.Barcode.BarCodeSystem;
using static EscPos.Barcode.BarCodeHRIPosition;
using static EscPos.Barcode.BarCodeHRIFont;
using static EscPos.Barcode.PDF417ErrorLevel;
using static EscPos.Barcode.PDF417Option;
using static EscPos.Barcode.QRModel;
using static EscPos.Barcode.QRErrorCorrectionLevel;

namespace EscPos.Barcode
{
    /// <summary>
    /// Supply ESC/POS QRCode bar-code commands
    /// </summary>
    public class QRCode : EscPosConst, BarCodeWrapperInterface
    {
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        public enum QRModel
        {
            // _1_Default(48)
            _1_Default,
            // _2(49)
            _2 

            // --------------------
            // TODO enum body members
            // public int value;
            // private QRModel(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        public enum QRErrorCorrectionLevel
        {
            // // 7%
            // QR_ECLEVEL_L(48)
            QR_ECLEVEL_L // 7%
        ,
            // //15%
            // QR_ECLEVEL_M_Default(49)
            QR_ECLEVEL_M_Default //15%
        ,
            // //25%
            // QR_ECLEVEL_Q(50)
            QR_ECLEVEL_Q //25%
        ,
            // //30%
            // QR_ECLEVEL_H(51)
            QR_ECLEVEL_H //30%
            

            // --------------------
            // TODO enum body members
            // public int value;
            // private QRErrorCorrectionLevel(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected Justification justification;
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected QRModel model;
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected int size;
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected QRErrorCorrectionLevel errorCorrectionLevel;
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        public QRCode()
        {
            justification = Justification.Left_Default;
            model = QRModel._1_Default;
            size = 3;
            errorCorrectionLevel = QRErrorCorrectionLevel.QR_ECLEVEL_M_Default;
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        public virtual QRCode SetJustification(Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// Set model of bar-code.
        /// </summary>
        /// <param name="model">value used on function 65</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual QRCode SetModel(QRModel model)
        {
            this.model = model;
            return this;
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// Set model of bar-code.
        /// </summary>
        /// <param name="model">value used on function 65</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the size of module in dots.
        /// </summary>
        /// <param name="size">value used on function 67</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when size is not between 1 and 16.</exception>
        public virtual QRCode SetSize(int size)
        {
            if (size < 1 || size > 16)
            {
                throw new ArgumentException("size must be between 1 and 16");
            }

            this.size = size;
            return this;
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// Set model of bar-code.
        /// </summary>
        /// <param name="model">value used on function 65</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the size of module in dots.
        /// </summary>
        /// <param name="size">value used on function 67</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when size is not between 1 and 16.</exception>
        /// <summary>
        /// Select the error correction level.<p>
        /// </summary>
        /// <param name="errorCorrectionLevel">value to be used on function 69</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual QRCode SetErrorCorrectionLevel(QRErrorCorrectionLevel errorCorrectionLevel)
        {
            this.errorCorrectionLevel = errorCorrectionLevel;
            return this;
        }

        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// Set model of bar-code.
        /// </summary>
        /// <param name="model">value used on function 65</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the size of module in dots.
        /// </summary>
        /// <param name="size">value used on function 67</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when size is not between 1 and 16.</exception>
        /// <summary>
        /// Select the error correction level.<p>
        /// </summary>
        /// <param name="errorCorrectionLevel">value to be used on function 69</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// QRCode Assembly into ESC/POS bytes. <p>
        /// 
        /// Select justification <p>
        /// ASCII ESC a n <p>
        /// 
        /// Function 065: Selects the model for QR Code. <p>
        /// ASCII GS ( k pL pH cn 65 n1 n2 <p>
        /// 
        /// Function 067: Sets the size of the module for QR Code in dots. <p>
        /// ASCII GS ( k pL pH cn 67 n <p>
        /// 
        /// Function 069: Selects the error correction level for QR Code. <p>
        /// ASCII GS ( k pL pH cn 69 n <p>
        /// 
        /// Function 080: Store the data in the symbol storage area <p>
        /// ASCII GS ( k pL pH cn 80 m d1...dk <p>
        /// 
        /// Function 081: Print the symbol data in the symbol storage area <p>
        /// ASCII GS ( k pL pH cn 81 m <p>
        /// </summary>
        /// <param name="data">to be printed in barcode</param>
        /// <returns>bytes of ESC/POS commands to print the barcode</returns>
        public virtual byte[] GetBytes(string data)
        {
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();

            //
            bytes.Write(ESC);
            bytes.Write('a');
            bytes.Write(justification.value);

            // Function 065
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(4); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(49); // cn
            bytes.Write(65); // fn
            bytes.Write(model.value); // n1
            bytes.Write(0); // n2

            // Function 067
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(49); // cn
            bytes.Write(67); // fn
            bytes.Write(size); // n

            // Function 069
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(49); // cn
            bytes.Write(69); // fn
            bytes.Write(errorCorrectionLevel.value); // n

            // Function 080
            int numberOfBytes = data.Length() + 3;
            int pL = numberOfBytes & 0xFF;
            int pH = (numberOfBytes & 0xFF00) >> 8;
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(pL); // pL size of bytes
            bytes.Write(pH); // pH size of bytes
            bytes.Write(49); // cn
            bytes.Write(80); // fn
            bytes.Write(48); // m
            bytes.Write(data.GetBytes(), 0, data.Length());

            // Function 081
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(49); // cn
            bytes.Write(81); // fn
            bytes.Write(48); // m
            return bytes.ToByteArray();
        }
    }
}