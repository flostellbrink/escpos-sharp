namespace EscPosSharp
{
    /// <summary>
    /// Supply ESC/POS text style commands
    /// Note: If your printer isn't compatible with this class, you can try to use PrintModeStyle class
    /// </summary>
    /// <remarks>@seePrintModeStyle</remarks>
    public class Style : EscPosConst
    {
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        public class FontName
        {
            public static FontName Font_A_Default = new(48);
            public static FontName Font_B = new(49);
            public static FontName Font_C = new(50);

            public FontName(int value)
            {
                this.value = value;
            }

            public int value;
        }

        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        public class FontSize
        {
            public static FontSize _1 = new(0);
            public static FontSize _2 = new(1);
            public static FontSize _3 = new(2);
            public static FontSize _4 = new(3);
            public static FontSize _5 = new(4);
            public static FontSize _6 = new(5);
            public static FontSize _7 = new(6);
            public static FontSize _8 = new(7);

            public int value;

            private FontSize(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        public class Underline
        {
            public static Underline None_Default = new(48);
            public static Underline OneDotThick = new(49);
            public static Underline TwoDotThick = new(50);

            public int value;

            private Underline(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        public class ColorMode
        {
            public static ColorMode BlackOnWhite_Default = new(0);
            public static ColorMode WhiteOnBlack = new(1);

            public int value;

            private ColorMode(int value)
            {
                this.value = value;
            }
        }

        protected FontName fontName;

        protected bool bold;

        protected Underline underline;

        protected FontSize fontWidth;

        protected FontSize fontHeight;

        protected Justification justification;

        protected bool defaultLineSpacing;

        protected int lineSpacing;

        protected ColorMode colorMode;

        public Style()
        {
            Reset();
        }

        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        public Style(Style another)
        {
            SetFontName(another.fontName);
            SetBold(another.bold);
            SetFontSize(another.fontWidth, another.fontHeight);
            SetUnderline(another.underline);
            SetJustification(another.justification);
            defaultLineSpacing = another.defaultLineSpacing;
            SetLineSpacing(another.lineSpacing);
            SetColorMode(another.colorMode);
        }

        /// <summary>
        /// Reset values to default.
        /// </summary>
        public void Reset()
        {
            fontName = FontName.Font_A_Default;
            fontWidth = FontSize._1;
            fontHeight = FontSize._1;
            bold = false;
            underline = Underline.None_Default;
            justification = Justification.Left_Default;
            ResetLineSpacing();
            colorMode = ColorMode.BlackOnWhite_Default;
        }

        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public Style SetFontName(FontName fontName)
        {
            this.fontName = fontName;
            return this;
        }

        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        public Style SetBold(bool bold)
        {
            this.bold = bold;
            return this;
        }

        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public Style SetFontSize(FontSize fontWidth, FontSize fontHeight)
        {
            this.fontWidth = fontWidth;
            this.fontHeight = fontHeight;
            return this;
        }

        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public Style SetUnderline(Underline underline)
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
        public Style SetJustification(Justification justification)
        {
            this.justification = justification;
            return this;
        }

        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacing">value used on ESC 3 n</param>
        /// <returns>this object</returns>
        /// <exception cref="ArgumentException">when lineSpacing is not between 0 and 255.</exception>
        /// <remarks>@see#getConfigBytes()</remarks>
        public Style SetLineSpacing(int lineSpacing)
        {
            if (lineSpacing < 0 || lineSpacing > 255)
            {
                throw new ArgumentException("lineSpacing must be between 0 and 255");
            }

            this.defaultLineSpacing = false;
            this.lineSpacing = lineSpacing;
            return this;
        }

        /// <summary>
        /// Reset line spacing to printer default used on ESC 2
        /// </summary>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        public Style ResetLineSpacing()
        {
            this.defaultLineSpacing = true;
            lineSpacing = 0;
            return this;
        }

        /// <summary>
        /// set color mode background / foreground reverse.
        /// </summary>
        /// <param name="colorMode">value used on GS B n</param>
        /// <returns>this object</returns>
        public Style SetColorMode(ColorMode colorMode)
        {
            this.colorMode = colorMode;
            return this;
        }

        /// <summary>
        /// Configure font Style.
        ///
        /// Select character font.
        ///
        /// ASCII ESC M n
        ///
        ///
        /// Turn emphasized(bold) mode on/off.
        ///
        /// ASCII ESC E n
        ///
        ///
        /// set font size.
        ///
        /// ASCII GS ! n
        ///
        ///
        /// select underline mode
        ///
        /// ASCII ESC – n
        ///
        ///
        /// Select justification
        ///
        /// ASCII ESC a n
        ///
        ///
        /// Select default line spacing
        ///
        /// ASCII ESC 2
        ///
        ///
        /// Set line spacing
        ///
        /// ASCII ESC 3 n
        ///
        ///
        /// Turn white/black reverse print mode on/off
        /// ASCII GS B n
        /// </summary>
        /// <returns>ESC/POS commands to configure style</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        public virtual byte[] GetConfigBytes()
        {
            using var bytes = new MemoryStream();

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'M');
            bytes.WriteByte((byte)fontName.value);

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'E');
            var n = bold ? 1 : 0;
            bytes.WriteByte((byte)n);

            //
            n = fontWidth.value << 4 | fontHeight.value;
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'!');
            bytes.WriteByte((byte)n);

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'-');
            bytes.WriteByte((byte)underline.value);

            //
            bytes.WriteByte((byte)ESC);
            bytes.WriteByte((byte)'a');
            bytes.WriteByte((byte)justification.value);

            //
            if (defaultLineSpacing)
            {
                bytes.WriteByte((byte)ESC);
                bytes.WriteByte((byte)'2');
            }
            else
            {
                bytes.WriteByte((byte)ESC);
                bytes.WriteByte((byte)'3');
                bytes.WriteByte((byte)lineSpacing);
            }

            //
            bytes.WriteByte((byte)GS);
            bytes.WriteByte((byte)'B');
            bytes.WriteByte((byte)colorMode.value);

            return bytes.ToArray();
        }
    }
}
