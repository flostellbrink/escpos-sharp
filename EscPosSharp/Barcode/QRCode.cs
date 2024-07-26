using System.Text;

namespace EscPosSharp.Barcode
{
    /// <summary>
    /// Supply ESC/POS QRCode bar-code commands
    /// </summary>
    public class QRCode : EscPosConst, BarCodeWrapperInterface<QRCode>
    {
        /// <summary>
        /// Values for QRCode model.
        /// </summary>
        /// <remarks>@see#setModel(QRModel)</remarks>
        public class QRModel
        {
            public static QRModel _1_Default = new(48);
            public static QRModel _2 = new(49);

            public int value;

            private QRModel(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        public class QRErrorCorrectionLevel
        {
            /// <summary>
            /// 7%
            /// </summary>
            public static QRErrorCorrectionLevel QR_ECLEVEL_L = new(48);

            /// <summary>
            /// 15%
            /// </summary>
            public static QRErrorCorrectionLevel QR_ECLEVEL_M_Default = new(
                49
            );

            /// <summary>
            /// 25%
            /// </summary>
            public static QRErrorCorrectionLevel QR_ECLEVEL_Q = new(50);

            /// <summary>
            /// 30%
            /// </summary>
            public static QRErrorCorrectionLevel QR_ECLEVEL_H = new(51);

            public int value;

            private QRErrorCorrectionLevel(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected Justification justification;

        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected QRModel model;

        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected int size;

        /// <summary>
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%
        protected QRErrorCorrectionLevel errorCorrectionLevel;

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
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%/// <summary>
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
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%/// <summary>
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
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%/// <summary>
        /// Select the error correction level.
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
        /// Values for QR Error Correction Level.
        /// </summary>
        // 7%
        //15%
        //25%
        //30%/// <summary>
        /// QRCode Assembly into ESC/POS bytes.
        ///
        /// Select justification
        /// ASCII ESC a n
        ///
        /// Function 065: Selects the model for QR Code.
        /// ASCII GS ( k pL pH cn 65 n1 n2
        ///
        /// Function 067: Sets the size of the module for QR Code in dots.
        /// ASCII GS ( k pL pH cn 67 n
        ///
        /// Function 069: Selects the error correction level for QR Code.
        /// ASCII GS ( k pL pH cn 69 n
        ///
        /// Function 080: Store the data in the symbol storage area
        /// ASCII GS ( k pL pH cn 80 m d1...dk
        ///
        /// Function 081: Print the symbol data in the symbol storage area
        /// ASCII GS ( k pL pH cn 81 m
        /// </summary>
        /// <param name="data">to be printed in barcode</param>
        /// <returns>bytes of ESC/POS commands to print the barcode</returns>
        public virtual byte[] GetBytes(string data)
        {
            using var bytes = new MemoryStream();

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'a');
            bytes.WriteByte((byte)justification.value);

            // Function 065
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)4); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)49); // cn
            bytes.WriteByte((byte)65); // fn
            bytes.WriteByte((byte)model.value); // n1
            bytes.WriteByte((byte)0); // n2

            // Function 067
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)49); // cn
            bytes.WriteByte((byte)67); // fn
            bytes.WriteByte((byte)size); // n

            // Function 069
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)49); // cn
            bytes.WriteByte((byte)69); // fn
            bytes.WriteByte((byte)errorCorrectionLevel.value); // n

            // Function 080
            var numberOfBytes = data.Length + 3;
            var pL = numberOfBytes & 0xFF;
            var pH = (numberOfBytes & 0xFF00) >> 8;
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)pL); // pL size of bytes
            bytes.WriteByte((byte)pH); // pH size of bytes
            bytes.WriteByte((byte)49); // cn
            bytes.WriteByte((byte)80); // fn
            bytes.WriteByte((byte)48); // m
            var dataBytes = Encoding.ASCII.GetBytes(data);
            bytes.Write(dataBytes, 0, dataBytes.Length);

            // Function 081
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)49); // cn
            bytes.WriteByte((byte)81); // fn
            bytes.WriteByte((byte)48); // m

            return bytes.ToArray();
        }
    }
}
