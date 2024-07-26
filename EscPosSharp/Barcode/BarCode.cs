using System.Text;
using System.Text.RegularExpressions;

namespace EscPosSharp.Barcode
{
    /// <summary>
    /// Supply ESC/POS BarCode commands
    /// </summary>
    public class BarCode : EscPosConst, BarCodeWrapperInterface<BarCode>
    {
        /// <summary>
        /// Provides bar-code system.
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        public class BarCodeSystem
        {
            public static BarCodeSystem UPCA = new(0, "\\d{11,12}$");
            public static BarCodeSystem UPCA_B = new(65, "^\\d{11,12}$");
            public static BarCodeSystem UPCE_A = new(
                1,
                "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"
            );
            public static BarCodeSystem UPCE_B = new(
                66,
                "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"
            );
            public static BarCodeSystem JAN13_A = new(2, "^\\d{12,13}$");
            public static BarCodeSystem JAN13_B = new(67, "^\\d{12,13}$");
            public static BarCodeSystem JAN8_A = new(3, "^\\d{7,8}$");
            public static BarCodeSystem JAN8_B = new(68, "^\\d{7,8}$");
            public static BarCodeSystem CODE39_A = new(
                4,
                "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"
            );
            public static BarCodeSystem CODE39_B = new(
                69,
                "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"
            );
            public static BarCodeSystem ITF_A = new(5, "^([\\d]{2})+$");
            public static BarCodeSystem ITF_B = new(70, "^([\\d]{2})+$");
            public static BarCodeSystem CODABAR_A = new(
                6,
                "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"
            );
            public static BarCodeSystem CODABAR_B = new(
                71,
                "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"
            );
            public static BarCodeSystem CODE93_Default = new(72, "^[\\x00-\\x7F]+$");
            public static BarCodeSystem CODE128 = new(73, "^\\{[A-C][\\x00-\\x7F]+$");

            public int code;
            public string regex;

            private BarCodeSystem(int code, string regex)
            {
                this.code = code;
                this.regex = regex;
            }
        }

        /// <summary>
        /// Provides Bar Code HRI Positions.
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        public class BarCodeHRIPosition
        {
            public static BarCodeHRIPosition NotPrinted_Default = new(48);
            public static BarCodeHRIPosition AboveBarCode = new(49);
            public static BarCodeHRIPosition BelowBarCode = new(50);
            public static BarCodeHRIPosition AboveAndBelowBarCode = new(51);

            public int value;

            private BarCodeHRIPosition(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Provides textHRI font for bar-code.
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        public class BarCodeHRIFont
        {
            public static BarCodeHRIFont Font_A_Default = new(48);
            public static BarCodeHRIFont Font_B = new(49);
            public static BarCodeHRIFont Font_C = new(50);

            public int value;

            private BarCodeHRIFont(int value)
            {
                this.value = value;
            }
        }

        protected BarCodeSystem sytem;
        protected int width;
        protected int height;
        protected BarCodeHRIPosition HRIPosition;
        protected BarCodeHRIFont HRIFont;
        protected Justification justification;

        /// <summary>
        /// Creates object with default values.
        /// </summary>
        public BarCode()
        {
            sytem = BarCodeSystem.CODE93_Default;
            width = 2;
            height = 100;
            HRIPosition = BarCodeHRIPosition.NotPrinted_Default;
            HRIFont = BarCodeHRIFont.Font_A_Default;
            justification = Justification.Left_Default;
        }

        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual BarCode SetSystem(BarCodeSystem barCodeSystem)
        {
            this.sytem = barCodeSystem;
            return this;
        }

        /// <summary>
        /// Set bar-code size.
        /// ASCII GS w n
        /// </summary>
        /// <param name="width">codes for widths of the module,
        /// the width of module depends of printer model.</param>
        /// <param name="height">height of a bar code in dots.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 2 ≤ width ≤ 6, 68 ≤ width ≤ 76</exception>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 1 ≤ height ≤ 255</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual BarCode SetBarCodeSize(int width, int height)
        {
            if ((width < 2 || width > 6) && (width < 68 || width > 76))
            {
                throw new ArgumentException("with must be between 1 and 255");
            }

            if (height < 1 || height > 255)
            {
                throw new ArgumentException("height must be between 1 and 255");
            }

            this.width = width;
            this.height = height;
            return this;
        }

        /// <summary>
        /// Set bar-code HRI Position.
        /// </summary>
        /// <param name="barCodeHRI">position of the text</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual BarCode SetHRIPosition(BarCodeHRIPosition barCodeHRI)
        {
            this.HRIPosition = barCodeHRI;
            return this;
        }

        /// <summary>
        /// Set bar-code HRI Font.
        /// </summary>
        /// <param name="HRIFont">font of the text printed with bar-code.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual BarCode SetHRIFont(BarCodeHRIFont HRIFont)
        {
            this.HRIFont = HRIFont;
            return this;
        }

        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        public virtual BarCode SetJustification(Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// BarCode Assembly into ESC/POS bytes.
        ///
        /// Set bar code height
        /// ASCII GS h n
        ///
        /// Set bar code width
        /// ASCII GS w n
        ///
        /// Select print position of Human Readable Interpretation (HRI) characters
        /// ASCII GS H n
        ///
        /// Select font for HRI characters
        /// ASCII GS f n
        ///
        /// Select justification
        /// ASCII ESC a n
        ///
        /// print BarCode
        /// ASCII GS k m d1 ... dk
        /// </summary>
        /// <param name="data">to be printed in bar-code</param>
        /// <exception cref="IllegalArgumentException">when data do no match with regex.</exception>
        /// <returns>bytes of ESC/POS commands to print the bar-code</returns>
        public virtual byte[] GetBytes(string data)
        {
            using var bytes = new MemoryStream();

            if (!Regex.IsMatch(data, sytem.regex))
            {
                throw new ArgumentException(
                    String.Format("data must match with \"%s\"", sytem.regex)
                );
            }

            //
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'h');
            bytes.WriteByte((byte)height);

            //
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'w');
            bytes.WriteByte((byte)width);

            //
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'H');
            bytes.WriteByte((byte)HRIPosition.value);

            //
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'f');
            bytes.WriteByte((byte)HRIFont.value);

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'a');
            bytes.WriteByte((byte)justification.value);

            ////
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)sytem.code);

            var dataBytes = Encoding.ASCII.GetBytes(data);
            if (sytem.code <= 6)
            {
                bytes.Write(dataBytes, 0, dataBytes.Length);
                bytes.WriteByte((byte)NUL);
            }
            else
            {
                bytes.WriteByte((byte)dataBytes.Length);
                bytes.Write(dataBytes, 0, dataBytes.Length);
            }

            return bytes.ToArray();
        }
    }
}
