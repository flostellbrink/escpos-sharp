using System.Text;

namespace EscPosSharp.Barcode
{
    /// <summary>
    /// Supply ESC/POS PDF417 bar-code commands
    /// </summary>
    public class PDF417 : EscPosConst, BarCodeWrapperInterface<PDF417>
    {
        /// <summary>
        /// Values of Error Correction Level.
        /// Used on function 069
        /// </summary>
        /// <remarks>@see#setErrorLevel(PDF417ErrorLevel)</remarks>
        public class PDF417ErrorLevel
        {
            /// <summary>
            /// Error Level Zero.
            /// </summary>
            public static PDF417ErrorLevel _0 = new(48);

            /// <summary>
            /// Error Level One.
            /// </summary>
            public static PDF417ErrorLevel _1_Default = new(49);

            /// <summary>
            /// Error Level Two.
            /// </summary>
            public static PDF417ErrorLevel _2 = new(50);

            /// <summary>
            /// Error Level Tree.
            /// </summary>
            public static PDF417ErrorLevel _3 = new(51);

            /// <summary>
            /// Error Level Four.
            /// </summary>
            public static PDF417ErrorLevel _4 = new(52);

            /// <summary>
            /// Error Level Five.
            /// </summary>
            public static PDF417ErrorLevel _5 = new(53);

            /// <summary>
            /// Error Level Six.
            /// </summary>
            public static PDF417ErrorLevel _6 = new(54);

            /// <summary>
            /// Error Level Seven.
            /// </summary>
            public static PDF417ErrorLevel _7 = new(55);

            /// <summary>
            /// Error Level Eight.
            /// </summary>
            public static PDF417ErrorLevel _8 = new(56);

            public int value;

            private PDF417ErrorLevel(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        public class PDF417Option
        {
            /// <summary>
            /// Standard Default.
            /// </summary>
            public static PDF417Option Standard_Default = new(0);

            /// <summary>
            /// Truncated.
            /// </summary>
            public static PDF417Option Truncated = new(1);

            public int value;

            private PDF417Option(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected Justification justification;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int numberOfColumns;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int numberOfRows;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int width;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected int height;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected PDF417ErrorLevel errorLevel;

        /// <summary>
        /// Values of PDF417 Option.
        /// Used on function 070
        /// </summary>
        /// <remarks>@see#setOption(PDF417Option)</remarks>
        protected PDF417Option option;

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
        /// Set the number of columns in the data region.
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
        /// Set the number of rows.
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
        /// Set the width of the module.
        /// The module height is recommended to be set to 3-5.
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
        /// Set the row height.
        /// The module height is recommended to be set to 3-5.
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
        /// Set the row height.
        /// The module height is recommended to be set to 3-5.
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height.
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
        /// Set the error correction level.
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
        /// Set the row height.
        /// The module height is recommended to be set to 3-5.
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height.
        //     * Used on function 068
        //     * @return this object
        //     * @see #setHeight(int)
        //     * @see #getBytes(java.lang.String)
        //     */
        //    public PDF417 resetHeight(){
        //        this.height = 3;
        //        return this;
        //    }/// <summary>
        /// Select the option.
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
        /// Set the row height.
        /// The module height is recommended to be set to 3-5.
        /// Used on function 068
        /// </summary>
        /// <param name="height">value used on function 068</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when height is not between 2 and 8</exception>
        /// <remarks>@see#getBytes(java.lang.String)</remarks>
        //    /**
        //     * Reset the row height.
        //     * Used on function 068
        //     * @return this object
        //     * @see #setHeight(int)
        //     * @see #getBytes(java.lang.String)
        //     */
        //    public PDF417 resetHeight(){
        //        this.height = 3;
        //        return this;
        //    }/// <summary>
        /// BarCode Assembly into ESC/POS bytes.
        ///
        /// Select justification
        /// ASCII ESC a n
        ///
        /// Function 065: Set the number of columns in the data region
        /// ASCII GS ( k pL pH cn 65 n
        ///
        /// Function 066: Set the number of rows
        /// ASCII GS ( k pL pH cn 66 n
        ///
        /// Function 067: Sets the width of the module for PDF417 to n dots.
        /// ASCII GS ( k pL pH cn 67 n
        ///
        /// Function 068: Sets the row height for PDF417 to [n × (the width of the module)].
        /// ASCII GS ( k pL pH cn 68 n
        ///
        /// Function 069: Sets the error correction level for PDF417.
        /// ASCII GS ( k pL pH cn 69 48 n
        ///
        /// Function 070: Select the options
        /// ASCII GS (k pL pH cn 70 n
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
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)65); // fn
            bytes.WriteByte((byte)numberOfColumns); // m

            // Function 066
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)66); // fn
            bytes.WriteByte((byte)numberOfRows); // m

            // Function 067
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)67); // fn
            bytes.WriteByte((byte)width); // m

            // Function 068
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)68); // fn
            bytes.WriteByte((byte)height); // m

            // Function 069
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)4); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)69); // fn
            bytes.WriteByte((byte)48); // m
            bytes.WriteByte((byte)errorLevel.value); // n

            // Function 070
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)3); // pL size of bytes
            bytes.WriteByte((byte)0); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)70); // fn
            bytes.WriteByte((byte)option.value); // m

            // Function 080
            int numberOfBytes = data.Length + 3;
            int pL = numberOfBytes & 0xFF;
            int pH = (numberOfBytes & 0xFF00) >> 8;
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'(');
            bytes.WriteByte((byte)'k');
            bytes.WriteByte((byte)pL); // pL size of bytes
            bytes.WriteByte((byte)pH); // pH size of bytes
            bytes.WriteByte((byte)48); // cn
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
            bytes.WriteByte((byte)48); // cn
            bytes.WriteByte((byte)81); // fn
            bytes.WriteByte((byte)48); // m

            return bytes.ToArray();
        }
    }
}
