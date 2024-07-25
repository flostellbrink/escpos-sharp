namespace EscPosSharp
{
    /// <summary>
    /// Supply ESC/POS text style with the set of Print Mode commands
    /// NOTE: You can use this class when your printer isn't compatible with Style class, otherwise, always
    /// prefer to use Style class, this is because PrintModeStyle have less features than Style class.
    /// Go to samples/textprintmodestyle to see how to use it.
    /// </summary>
    /// <remarks>@seeStyle</remarks>
    public class PrintModeStyle : EscPosConst
    {
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        public class FontName
        {
            public static FontName Font_A_Default = new FontName(0);
            public static FontName Font_B = new FontName(1);

            public int value;

            private FontName(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected FontName fontName;

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected bool bold;

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected bool underline;

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected bool doubleWidth;

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected bool doubleHeight;

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        protected Justification justification;

        /// <summary>
        /// creates PrintModeStyle object with default values.
        /// </summary>
        public PrintModeStyle()
        {
            Reset();
        }

        /// <summary>
        /// creates PrintModeStyle object with another PrintModeStyle instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        public PrintModeStyle(PrintModeStyle another)
        {
            SetFontName(another.fontName);
            SetBold(another.bold);
            SetFontSize(another.doubleWidth, another.doubleHeight);
            SetUnderline(another.underline);
            SetJustification(another.justification);
        }

        /// <summary>
        /// Reset values to default.
        /// </summary>
        public void Reset()
        {
            fontName = FontName.Font_A_Default;
            SetBold(false);
            SetFontSize(false, false);
            justification = Justification.Left_Default;
        }

        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public PrintModeStyle SetFontName(FontName fontName)
        {
            this.fontName = fontName;
            return this;
        }

        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC ! n</param>
        /// <returns>this object</returns>
        public PrintModeStyle SetBold(bool bold)
        {
            this.bold = bold;
            return this;
        }

        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="doubleWidth">value used on ESC ! n</param>
        /// <param name="doubleHeight">value used on ESC ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public PrintModeStyle SetFontSize(bool doubleWidth, bool doubleHeight)
        {
            this.doubleWidth = doubleWidth;
            this.doubleHeight = doubleHeight;
            return this;
        }

        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public PrintModeStyle SetUnderline(bool underline)
        {
            this.underline = underline;
            return this;
        }

        /// <summary>
        /// Set Justification for text.
        /// </summary>
        /// <param name="justification">value used on ESC a n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public PrintModeStyle SetJustification(Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Configure font Style.
        ///
        /// Selects the character font and styles (emphasize, double-height, double-width, and underline) together..
        ///
        /// ASCII ESC ! n
        ///
        ///
        ///
        /// Select justification
        ///
        /// </summary>
        /// <returns>ESC/POS commands to configure style</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        public virtual byte[] GetConfigBytes()
        {
            using var bytes = new MemoryStream();

            // bit combination ...
            int nVal = fontName.value;

            //
            //
            if (bold)
                nVal = nVal | 0x8;

            //
            //
            if (doubleHeight)
                nVal = nVal | 0x10;
            if (doubleWidth)
                nVal = nVal | 0x20;

            //
            //
            if (underline)
                nVal = nVal | 0x80;

            //
            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'!');
            bytes.WriteByte((byte)nVal);

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'a');
            bytes.WriteByte((byte)justification.value);

            return bytes.ToArray();
        }
    }
}
