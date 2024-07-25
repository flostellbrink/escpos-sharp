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
using static EscPos.Barcode.BarCodeHRIFont;
using static EscPos.Barcode.BarCodeHRIPosition;
using static EscPos.Barcode.BarCodeSystem;
using static EscPos.Barcode.CharacterCodeTable;
using static EscPos.Barcode.ColorMode;
using static EscPos.Barcode.CutMode;
using static EscPos.Barcode.FontName;
using static EscPos.Barcode.FontSize;
using static EscPos.Barcode.Justification;
using static EscPos.Barcode.PinConnector;
using static EscPos.Barcode.Underline;

namespace EscPos.Barcode
{
    /// <summary>
    /// Supply ESC/POS BarCode commands
    /// </summary>
    public class BarCode : EscPosConst, BarCodeWrapperInterface
    {
        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        public enum BarCodeSystem
        {
            /// <summary>
            /// <code>regex: "\\d{11,12}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "\\d{11,12}$"</code>
            //  */
            // UPCA(0, "\\d{11,12}$")
            UPCA,

            /// <summary>
            /// <code>regex: "^\\d{11,12}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{11,12}$"</code>
            //  */
            // UPCA_B(65, "^\\d{11,12}$")
            UPCA_B,

            /// <summary>
            /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
            //  */
            // UPCE_A(1, "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$")
            UPCE_A,

            /// <summary>
            /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
            //  */
            // UPCE_B(66, "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$")
            UPCE_B,

            /// <summary>
            /// <code>regex: "^\\d{12,13}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{12,13}$"</code>
            //  */
            // JAN13_A(2, "^\\d{12,13}$")
            JAN13_A,

            /// <summary>
            /// <code>regex: "^\\d{12,13}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{12,13}$"</code>
            //  */
            // JAN13_B(67, "^\\d{12,13}$")
            JAN13_B,

            /// <summary>
            /// <code>regex: "^\\d{7,8}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{7,8}$"</code>
            //  */
            // JAN8_A(3, "^\\d{7,8}$")
            JAN8_A,

            /// <summary>
            /// <code>regex: "^\\d{7,8}$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^\\d{7,8}$"</code>
            //  */
            // JAN8_B(68, "^\\d{7,8}$")
            JAN8_B,

            /// <summary>
            /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
            //  */
            // CODE39_A(4, "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$")
            CODE39_A,

            /// <summary>
            /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
            //  */
            // CODE39_B(69, "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$")
            CODE39_B,

            /// <summary>
            /// <code>regex: "^([\\d]{2})+$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^([\\d]{2})+$"</code>
            //  */
            // ITF_A(5, "^([\\d]{2})+$")
            ITF_A,

            /// <summary>
            /// <code>regex: "^([\\d]{2})+$"</code>
            /// </summary>
            // /**
            //  * <code>regex: "^([\\d]{2})+$"</code>
            //  */
            // ITF_B(70, "^([\\d]{2})+$")
            ITF_B,

            /// <summary>
            /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
            /// </summary>
            // /**
            //  * <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
            //  */
            // CODABAR_A(6, "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$")
            CODABAR_A,

            /// <summary>
            /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
            /// </summary>
            // /**
            //  * <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
            //  */
            // CODABAR_B(71, "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$")
            CODABAR_B,

            /// <summary>
            /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
            /// </summary>
            // /**
            //  * <code>regex:  "^[\\x00-\\x7F]+$"</code>
            //  */
            // CODE93_Default(72, "^[\\x00-\\x7F]+$")
            CODE93_Default,

            /// <summary>
            /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
            /// </summary>
            // /**
            //  * <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
            //  */
            // CODE128(73, "^\\{[A-C][\\x00-\\x7F]+$")
            CODE128

            // --------------------
            // TODO enum body members
            // public int code;
            // public String regex;
            // private BarCodeSystem(int code, String regex) {
            //     this.code = code;
            //     this.regex = regex;
            // }
            // --------------------
        }

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        public enum BarCodeHRIPosition
        {
            /// <summary>
            /// Do not Print the text
            /// </summary>
            // /**
            //  * Do not Print the text
            //  */
            // NotPrinted_Default(48)
            NotPrinted_Default,

            /// <summary>
            /// Print the text above the bar-code
            /// </summary>
            // /**
            //  * Print the text above the bar-code
            //  */
            // AboveBarCode(49)
            AboveBarCode,

            /// <summary>
            /// Print the text below the bar-code
            /// </summary>
            // /**
            //  * Print the text below the bar-code
            //  */
            // BelowBarCode(50)
            BelowBarCode,

            /// <summary>
            /// Print the text above and below the bar-code
            /// </summary>
            // /**
            //  * Print the text above and below the bar-code
            //  */
            // AboveAndBelowBarCode(51)
            AboveAndBelowBarCode

            // --------------------
            // TODO enum body members
            // public int value;
            // private BarCodeHRIPosition(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        public enum BarCodeHRIFont
        {
            // Font_A_Default(48)
            Font_A_Default,

            // Font_B(49)
            Font_B,

            // Font_C(50)
            Font_C

            // --------------------
            // TODO enum body members
            // public int value;
            // private BarCodeHRIFont(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected BarCodeSystem sytem;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected int width;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected int height;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected BarCodeHRIPosition HRIPosition;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected BarCodeHRIFont HRIFont;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        protected Justification justification;

        /// <summary>
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code size.<p>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code size.<p>
        /// ASCII GS w n
        /// </summary>
        /// <param name="width">codes for widths of the module,
        /// the width of module depends of printer model.</param>
        /// <param name="height">height of a bar code in dots.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 2 ≤ width ≤ 6, 68 ≤ width ≤ 76</exception>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 1 ≤ height ≤ 255</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Position.<p>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code size.<p>
        /// ASCII GS w n
        /// </summary>
        /// <param name="width">codes for widths of the module,
        /// the width of module depends of printer model.</param>
        /// <param name="height">height of a bar code in dots.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 2 ≤ width ≤ 6, 68 ≤ width ≤ 76</exception>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 1 ≤ height ≤ 255</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Position.<p>
        /// </summary>
        /// <param name="barCodeHRI">position of the text</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Font.<p>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code size.<p>
        /// ASCII GS w n
        /// </summary>
        /// <param name="width">codes for widths of the module,
        /// the width of module depends of printer model.</param>
        /// <param name="height">height of a bar code in dots.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 2 ≤ width ≤ 6, 68 ≤ width ≤ 76</exception>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 1 ≤ height ≤ 255</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Position.<p>
        /// </summary>
        /// <param name="barCodeHRI">position of the text</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Font.<p>
        /// </summary>
        /// <param name="HRIFont">font of the text printed with bar-code.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
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
        /// Provides bar-code system. <p>
        /// Each system have one <code>regex</code> to help on validate data.
        /// </summary>
        /// <remarks>
        /// @see#setSystem(BarCodeSystem)
        /// @seejava.util.regex.Pattern
        /// </remarks>
        /// <summary>
        /// <code>regex: "\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{11,12}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{6}$|^0{1}\\d{6,7}$|^0{1}\\d{10,11}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{12,13}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^\\d{7,8}$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^[\\d\\p{Upper}\\ \\$\\%\\*\\+\\-\\.\\/]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex: "^([\\d]{2})+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[A-Da-d][\\d\\$\\+\\-\\.\\/\\:]*[A-Da-d]$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^[\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// <code>regex:  "^\\{[A-C][\\x00-\\x7F]+$"</code>
        /// </summary>
        /// <summary>
        /// Provides Bar Code HRI Positions.<p>
        /// Human Readable Interpretation (HRI) position is the position of the text relative
        /// to the position of the bar-code.
        /// </summary>
        /// <remarks>@see#setHRIPosition(BarCodeHRIPosition)</remarks>
        /// <summary>
        /// Do not Print the text
        /// </summary>
        /// <summary>
        /// Print the text above the bar-code
        /// </summary>
        /// <summary>
        /// Print the text below the bar-code
        /// </summary>
        /// <summary>
        /// Print the text above and below the bar-code
        /// </summary>
        /// <summary>
        /// Provides textHRI font for bar-code.<p>
        /// Human Readable Interpretation (HRI) font is the font of the text
        /// printed with bar-code.
        /// </summary>
        /// <remarks>@see#setHRIFont(BarCodeHRIFont)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set bar-code system.
        /// </summary>
        /// <param name="barCodeSystem">type of bar-code system.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code size.<p>
        /// ASCII GS w n
        /// </summary>
        /// <param name="width">codes for widths of the module,
        /// the width of module depends of printer model.</param>
        /// <param name="height">height of a bar code in dots.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 2 ≤ width ≤ 6, 68 ≤ width ≤ 76</exception>
        /// <exception cref="IllegalArgumentException">when this condition is not true: 1 ≤ height ≤ 255</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Position.<p>
        /// </summary>
        /// <param name="barCodeHRI">position of the text</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set bar-code HRI Font.<p>
        /// </summary>
        /// <param name="HRIFont">font of the text printed with bar-code.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set horizontal justification of bar-code
        /// </summary>
        /// <param name="justification">left, center or right</param>
        /// <returns>this object</returns>
        /// <summary>
        /// BarCode Assembly into ESC/POS bytes. <p>
        ///
        /// Set bar code height <p>
        /// ASCII GS h n <p>
        ///
        /// Set bar code width <p>
        /// ASCII GS w n <p>
        ///
        /// Select print position of Human Readable Interpretation (HRI) characters <p>
        /// ASCII GS H n
        ///
        /// Select font for HRI characters <p>
        /// ASCII GS f n
        ///
        /// Select justification <p>
        /// ASCII ESC a n <p>
        ///
        /// print BarCode <p>
        /// ASCII GS k m d1 ... dk <p>
        /// </summary>
        /// <param name="data">to be printed in bar-code</param>
        /// <exception cref="IllegalArgumentException">when data do no match with regex.</exception>
        /// <returns>bytes of ESC/POS commands to print the bar-code</returns>
        public virtual byte[] GetBytes(string data)
        {
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();
            if (!data.Matches(sytem.regex))
            {
                throw new ArgumentException(
                    String.Format("data must match with \"%s\"", sytem.regex)
                );
            }

            //
            bytes.Write(GS);
            bytes.Write('h');
            bytes.Write(height);

            //
            bytes.Write(GS);
            bytes.Write('w');
            bytes.Write(width);

            //
            bytes.Write(GS);
            bytes.Write('H');
            bytes.Write(HRIPosition.value);

            //
            bytes.Write(GS);
            bytes.Write('f');
            bytes.Write(HRIFont.value);

            //
            bytes.Write(ESC);
            bytes.Write('a');
            bytes.Write(justification.value);

            ////
            bytes.Write(GS);
            bytes.Write('k');
            bytes.Write(sytem.code);
            if (sytem.code <= 6)
            {
                bytes.Write(data.GetBytes(), 0, data.Length());
                bytes.Write(NUL);
            }
            else
            {
                bytes.Write(data.Length());
                bytes.Write(data.GetBytes(), 0, data.Length());
            }

            return bytes.ToByteArray();
        }
    }
}
