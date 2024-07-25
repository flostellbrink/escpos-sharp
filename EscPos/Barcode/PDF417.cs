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

namespace EscPos.Barcode
{
    /// <summary>
    /// Supply ESC/POS PDF417 bar-code commands
    /// </summary>
    public class PDF417 : EscPosConst, BarCodeWrapperInterface
    {
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        public enum PDF417ErrorLevel
        {
            /// <summary>
            /// Error Level Zero.
            /// </summary>
            // /**
            //  * Error Level Zero.
            //  */
            // _0(48)
            _0,
            /// <summary>
            /// Error Level One.
            /// </summary>
            // /**
            //  * Error Level One.
            //  */
            // _1_Default(49)
            _1_Default,
            /// <summary>
            /// Error Level Two.
            /// </summary>
            // /**
            //  * Error Level Two.
            //  */
            // _2(50)
            _2,
            /// <summary>
            /// Error Level Tree.
            /// </summary>
            // /**
            //  * Error Level Tree.
            //  */
            // _3(51)
            _3,
            /// <summary>
            /// Error Level Four.
            /// </summary>
            // /**
            //  * Error Level Four.
            //  */
            // _4(52)
            _4,
            /// <summary>
            /// Error Level Five.
            /// </summary>
            // /**
            //  * Error Level Five.
            //  */
            // _5(53)
            _5,
            /// <summary>
            /// Error Level Six.
            /// </summary>
            // /**
            //  * Error Level Six.
            //  */
            // _6(54)
            _6,
            /// <summary>
            /// Error Level Seven.
            /// </summary>
            // /**
            //  * Error Level Seven.
            //  */
            // _7(55)
            _7,
            /// <summary>
            /// Error Level Eight.
            /// </summary>
            // /**
            //  * Error Level Eight.
            //  */
            // _8(56)
            _8 

            // --------------------
            // TODO enum body members
            // public int value;
            // private PDF417ErrorLevel(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        public enum PDF417Option
        {
            // Standard_Default(0)
            Standard_Default,
            // Truncated(1)
            Truncated 

            // --------------------
            // TODO enum body members
            // public int value;
            // private PDF417Option(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected Justification justification;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int numberOfColumns;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int numberOfRows;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int width;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int height;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected PDF417ErrorLevel errorLevel;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected PDF417Option option;
        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        public PDF417()
        {
            justification = Justification.Left_Default;
            numberOfColumns = 0;
            numberOfRows = 0;
            width = 3;
            height = 3;
            errorLevel = PDF417ErrorLevel._1_Default;
            option = PDF417Option.Standard_Default;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetJustification(Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetNumberOfColumns(int numberOfColumns)
        {
            if (numberOfColumns < 0 || numberOfColumns > 30)
            {
                throw new ArgumentException("numberOfColumns must be between 0 and 30");
            }

            this.numberOfColumns = numberOfColumns;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetNumberOfRows(int numberOfRows)
        {
            if (numberOfRows != 0 && (numberOfColumns < 3 || numberOfColumns > 90))
            {
                throw new ArgumentException("numberOfRows must be 0 or between 3 and 90");
            }

            this.numberOfRows = numberOfRows;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the width of the module. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 067
        /// </summary>
        /// <param name="width">value used on function 067</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when width is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetWidth(int width)
        {
            if ((width < 2 || width > 8))
            {
                throw new ArgumentException("width must be between 2 and 8");
            }

            this.width = width;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the width of the module. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 067
        /// </summary>
        /// <param name="width">value used on function 067</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when width is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the row height. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetHeight(int height)
        {
            if ((height < 2 || height > 8))
            {
                throw new ArgumentException("height must be between 2 and 8");
            }

            this.height = height;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the width of the module. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 067
        /// </summary>
        /// <param name="width">value used on function 067</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when width is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the row height. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height. <p>
        //     * Used on function 068
        //     * @return this object
        //     * @see #setHeight(int)  
        //     * @see #getBytes(java.lang.String) 
        //     */
        //    public PDF417 resetHeight(){
        //        this.height = 3;
        //        return this;
        //    }
        /// <summary>
        /// Set the error correction level.<p>
        /// Used on function 069
        /// </summary>
        /// <param name="errorLevel">error level of function 069</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetErrorLevel(PDF417ErrorLevel errorLevel)
        {
            this.errorLevel = errorLevel;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the width of the module. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 067
        /// </summary>
        /// <param name="width">value used on function 067</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when width is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the row height. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height. <p>
        //     * Used on function 068
        //     * @return this object
        //     * @see #setHeight(int)  
        //     * @see #getBytes(java.lang.String) 
        //     */
        //    public PDF417 resetHeight(){
        //        this.height = 3;
        //        return this;
        //    }
        /// <summary>
        /// Set the error correction level.<p>
        /// Used on function 069
        /// </summary>
        /// <param name="errorLevel">error level of function 069</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Select the option. <p>
        /// Used on function 070
        /// </summary>
        /// <param name="option">options of function 070</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        public virtual PDF417 SetOption(PDF417Option option)
        {
            this.option = option;
            return this;
        }

        /// <summary>
        /// Values of Error Correction Level.<p>
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        /// <summary>
        /// Error Level Zero.
        /// </summary>
        /// <summary>
        /// Error Level One.
        /// </summary>
        /// <summary>
        /// Error Level Two.
        /// </summary>
        /// <summary>
        /// Error Level Tree.
        /// </summary>
        /// <summary>
        /// Error Level Four.
        /// </summary>
        /// <summary>
        /// Error Level Five.
        /// </summary>
        /// <summary>
        /// Error Level Six.
        /// </summary>
        /// <summary>
        /// Error Level Seven.
        /// </summary>
        /// <summary>
        /// Error Level Eight.
        /// </summary>
        /// <summary>
        /// Values of PDF417 Option.<p>
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        /// <summary>
        /// Creates object with default values.
        /// </summary>
        /// <summary>
        /// Set horizontal justification.
        /// </summary>
        /// <param name="justification">left, center or right.</param>
        /// <returns>this object.</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of columns in the data region. <p>
        /// Used on function 065
        /// </summary>
        /// <param name="numberOfColumns">value used on function 065</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfColumns is not between 0 and 30</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the number of rows. <p>
        /// Used on function 066
        /// </summary>
        /// <param name="numberOfRows">value used on function 066</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when numberOfRows is not between 3 and 90 and not equal 0</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the width of the module. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 067
        /// </summary>
        /// <param name="width">value used on function 067</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when width is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Set the row height. <p>
        /// The module height is recommended to be set to 3-5.<p>
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height. <p>
        //     * Used on function 068
        //     * @return this object
        //     * @see #setHeight(int)  
        //     * @see #getBytes(java.lang.String) 
        //     */
        //    public PDF417 resetHeight(){
        //        this.height = 3;
        //        return this;
        //    }
        /// <summary>
        /// Set the error correction level.<p>
        /// Used on function 069
        /// </summary>
        /// <param name="errorLevel">error level of function 069</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// Select the option. <p>
        /// Used on function 070
        /// </summary>
        /// <param name="option">options of function 070</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        /// <summary>
        /// BarCode Assembly into ESC/POS bytes. <p>
        /// 
        /// Select justification <p>
        /// ASCII ESC a n <p>
        /// 
        /// Function 065: Set the number of columns in the data region <p>
        /// ASCII GS ( k pL pH cn 65 n <p>
        /// 
        /// Function 066: Set the number of rows <p>
        /// ASCII GS ( k pL pH cn 66 n <p>
        /// 
        /// Function 067: Sets the width of the module for PDF417 to n dots. <p>
        /// ASCII GS ( k pL pH cn 67 n <p>
        /// 
        /// Function 068: Sets the row height for PDF417 to [n × (the width of the module)]. <p>
        /// ASCII GS ( k pL pH cn 68 n <p>
        /// 
        /// Function 069: Sets the error correction level for PDF417. <p>
        /// ASCII GS ( k pL pH cn 69 48 n <p>
        /// 
        /// Function 070: Select the options <p>
        /// ASCII GS (k pL pH cn 70 n <p>
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
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(65); // fn
            bytes.Write(numberOfColumns); // m

            // Function 066
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(66); // fn
            bytes.Write(numberOfRows); // m

            // Function 067
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(67); // fn
            bytes.Write(width); // m

            // Function 068
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(68); // fn
            bytes.Write(height); // m

            // Function 069
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(4); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(69); // fn
            bytes.Write(48); // m
            bytes.Write(errorLevel.value); // n

            // Function 070
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(70); // fn
            bytes.Write(option.value); // m

            // Function 080
            int numberOfBytes = data.Length() + 3;
            int pL = numberOfBytes & 0xFF;
            int pH = (numberOfBytes & 0xFF00) >> 8;
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(pL); // pL size of bytes
            bytes.Write(pH); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(80); // fn
            bytes.Write(48); // m
            bytes.Write(data.GetBytes(), 0, data.Length());

            // Function 081
            bytes.Write(GS);
            bytes.Write('(');
            bytes.Write('k');
            bytes.Write(3); // pL size of bytes
            bytes.Write(0); // pH size of bytes
            bytes.Write(48); // cn
            bytes.Write(81); // fn
            bytes.Write(48); // m
            return bytes.ToByteArray();
        }
    }
}