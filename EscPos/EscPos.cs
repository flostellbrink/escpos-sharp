using System.Text;
using EscPosSharp.Barcode;
using EscPosSharp.Image;

namespace EscPosSharp
{
    /// <summary>
    /// Write some usual ESC/POS commands to the OutPutStream.
    /// ESC/POS was developed by <i>Seiko Epson Corporation</i>
    /// Most receipt printers on the market recognize these commands.
    /// and can be used to print texts, barcodes or images
    /// </summary>
    public class EscPos : EscPosConst, IDisposable
    {
        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        public class CharacterCodeTable
        {
            public static CharacterCodeTable CP437_USA_Standard_Europe = new CharacterCodeTable(
                0,
                "cp437"
            );
            public static CharacterCodeTable Katakana = new CharacterCodeTable(1);
            public static CharacterCodeTable CP850_Multilingual = new CharacterCodeTable(
                2,
                "cp850"
            );
            public static CharacterCodeTable CP860_Portuguese = new CharacterCodeTable(3, "cp860");
            public static CharacterCodeTable CP863_Canadian_French = new CharacterCodeTable(
                4,
                "cp863"
            );
            public static CharacterCodeTable CP865_Nordic = new CharacterCodeTable(5, "cp865");
            public static CharacterCodeTable CP851_Greek = new CharacterCodeTable(11);
            public static CharacterCodeTable CP853_Turkish = new CharacterCodeTable(12);
            public static CharacterCodeTable CP857_Turkish = new CharacterCodeTable(13, "cp857");
            public static CharacterCodeTable CP737_Greek = new CharacterCodeTable(14, "cp737");
            public static CharacterCodeTable ISO8859_7_Greek = new CharacterCodeTable(
                15,
                "iso8859_7"
            );
            public static CharacterCodeTable WPC1252 = new CharacterCodeTable(16, "cp1252");
            public static CharacterCodeTable CP866_Cyrillic_2 = new CharacterCodeTable(17, "cp866");
            public static CharacterCodeTable CP852_Latin2 = new CharacterCodeTable(18, "cp852");
            public static CharacterCodeTable CP858_Euro = new CharacterCodeTable(19, "cp858");
            public static CharacterCodeTable KU42_Thai = new CharacterCodeTable(20);
            public static CharacterCodeTable TIS11_Thai = new CharacterCodeTable(21);
            public static CharacterCodeTable TIS18_Thai = new CharacterCodeTable(26);
            public static CharacterCodeTable TCVN_3_1_Vietnamese = new CharacterCodeTable(30);
            public static CharacterCodeTable TCVN_3_2_Vietnamese = new CharacterCodeTable(31);
            public static CharacterCodeTable PC720_Arabic = new CharacterCodeTable(32);
            public static CharacterCodeTable WPC775_BalticRim = new CharacterCodeTable(33);
            public static CharacterCodeTable CP855_Cyrillic = new CharacterCodeTable(34, "cp855");
            public static CharacterCodeTable CP861_Icelandic = new CharacterCodeTable(35, "cp861");
            public static CharacterCodeTable CP862_Hebrew = new CharacterCodeTable(36, "cp862");
            public static CharacterCodeTable CP864_Arabic = new CharacterCodeTable(37, "cp864");
            public static CharacterCodeTable CP869_Greek = new CharacterCodeTable(38, "cp869");
            public static CharacterCodeTable ISO8859_2_Latin2 = new CharacterCodeTable(
                39,
                "iso8859_2"
            );
            public static CharacterCodeTable ISO8859_15_Latin9 = new CharacterCodeTable(
                40,
                "iso8859_15"
            );
            public static CharacterCodeTable CP1098_Farsi = new CharacterCodeTable(41, "cp1098");
            public static CharacterCodeTable CP1118_Lithuanian = new CharacterCodeTable(42);
            public static CharacterCodeTable CP1119_Lithuanian = new CharacterCodeTable(43);
            public static CharacterCodeTable CP1125_Ukrainian = new CharacterCodeTable(44);
            public static CharacterCodeTable WCP1250_Latin2 = new CharacterCodeTable(45, "cp1250");
            public static CharacterCodeTable WCP1251_Cyrillic = new CharacterCodeTable(
                46,
                "cp1251"
            );
            public static CharacterCodeTable WCP1253_Greek = new CharacterCodeTable(47, "cp1253");
            public static CharacterCodeTable WCP1254_Turkish = new CharacterCodeTable(48, "cp1254");
            public static CharacterCodeTable WCP1255_Hebrew = new CharacterCodeTable(49, "cp1255");
            public static CharacterCodeTable WCP1256_Arabic = new CharacterCodeTable(50, "cp1256");
            public static CharacterCodeTable WCP1257_BalticRim = new CharacterCodeTable(
                51,
                "cp1257"
            );
            public static CharacterCodeTable WCP1258_Vietnamese = new CharacterCodeTable(
                52,
                "cp1258"
            );
            public static CharacterCodeTable KZ_1048_Kazakhstan = new CharacterCodeTable(53);
            public static CharacterCodeTable User_defined_page = new CharacterCodeTable(255);

            public int value;
            public string charsetName;

            private CharacterCodeTable(int value)
            {
                this.value = value;
                this.charsetName = "cp437";
            }

            private CharacterCodeTable(int value, string charsetName)
            {
                this.value = value;
                this.charsetName = charsetName;
            }
        }

        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        public class CutMode
        {
            public static CutMode FULL = new CutMode(48);
            public static CutMode PART = new CutMode(49);

            public int value;

            private CutMode(int value)
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        public class PinConnector
        {
            public static PinConnector Pin_2 = new PinConnector(48);
            public static PinConnector Pin_5 = new PinConnector(49);

            public int value;

            private PinConnector(int value)
            {
                this.value = value;
            }
        }

        protected Stream outputStream;
        protected string charsetName;
        protected Style style;

        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
        public EscPos(Stream outputStream)
        {
            this.outputStream = outputStream;
            this.charsetName = CharacterCodeTable.CP437_USA_Standard_Europe.charsetName;
            style = new Style();
        }

        /// <summary>
        /// Writes one byte directly to outputStream. Can be used to send customized
        /// commands to printer.
        /// </summary>
        /// <param name="b">the <code>byte</code>.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> may be thrown if the output stream has been
        /// closed.</exception>
        /// <remarks>@seejava.io.OutputStream#write(int)</remarks>
        public virtual EscPos Write(byte b)
        {
            this.outputStream.WriteByte(b);
            return this;
        }

        public virtual EscPos Write(int b)
        {
            return Write((byte)b);
        }

        public virtual EscPos Write(char b)
        {
            return Write((byte)b);
        }

        /// <summary>
        /// Writes bytes directly to outputStream. Can be used to send customizes
        /// commands to printer.
        /// </summary>
        /// <param name="b">the data.</param>
        /// <param name="off">the start offset in the data.</param>
        /// <param name="len">the number of bytes to write.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>@seejava.io.OutputStream#write(byte[], int, int)</remarks>
        public virtual EscPos Write(byte[] b, int off, int len)
        {
            this.outputStream.Write(b, off, len);
            return this;
        }

        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        public virtual void Flush()
        {
            outputStream.Flush();
        }

        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        public virtual void Close()
        {
            this.outputStream.Close();
        }

        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        public virtual EscPos SetOutputStream(Stream outputStream)
        {
            this.outputStream = outputStream;
            return this;
        }

        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        public virtual Stream GetOutputStream()
        {
            return outputStream;
        }

        /// <summary>
        /// Set style of actual instance.
        ///
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        public virtual EscPos SetStyle(Style style)
        {
            this.style = style;
            return this;
        }

        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        public virtual Style GetStyle()
        {
            return style;
        }

        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        public EscPos SetCharsetName(string charsetName)
        {
            this.charsetName = charsetName;
            return this;
        }

        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        public virtual string GetDefaultCharsetName()
        {
            return charsetName;
        }

        /// <summary>
        /// Select character code table.
        /// Each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0 to 255</exception>
        public virtual EscPos SetCharacterCodeTable(CharacterCodeTable table)
        {
            SetCharsetName(table.charsetName);
            SetPrinterCharacterTable(table.value);
            return this;
        }

        /// <summary>
        /// Select character code table on the printer.
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        public virtual EscPos SetPrinterCharacterTable(int characterCodeTable)
        {
            if (characterCodeTable < 0 || characterCodeTable > 255)
            {
                throw new ArgumentException("characterCodeTable must be between 0 and 255");
            }

            Write(ESC);
            Write('t');
            Write(characterCodeTable);
            return this;
        }

        /// <summary>
        /// Write String to outputStream.
        ///
        /// Configure Style by with ESC/POS commands, encode String by charsetName
        /// and write to outputStream.
        /// </summary>
        /// <param name="style">text style to be used.</param>
        /// <param name="text">content to be encoded and write to outputStream.</param>
        /// <returns>this object.</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        public virtual EscPos Write(Style style, string text)
        {
            byte[] configBytes = style.GetConfigBytes();
            Write(configBytes, 0, configBytes.Length);
            var encoding = Encoding.GetEncoding(charsetName);
            var bytes = encoding.GetBytes(text);
            this.outputStream.Write(bytes, 0, bytes.Length);
            return this;
        }

        /// <summary>
        /// Write String to outputStream.
        ///
        /// Configure printModeStyle by with ESC/POS commands, encode String by charsetName
        /// and write to outputStream.
        /// </summary>
        /// <param name="printModeStyle">- text style to be used.</param>
        /// <param name="text">- content to be encoded and write to outputStream.</param>
        /// <returns>this object.</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>
        /// @see#setCharsetName(java.lang.String)
        /// @seePrintModeStyle
        /// </remarks>
        public virtual EscPos Write(PrintModeStyle printModeStyle, string text)
        {
            byte[] configBytes = printModeStyle.GetConfigBytes();
            Write(configBytes, 0, configBytes.Length);
            var encoding = Encoding.GetEncoding(charsetName);
            var bytes = encoding.GetBytes(text);
            this.outputStream.Write(bytes, 0, bytes.Length);
            return this;
        }

        /// <summary>
        /// Calls write with default style.
        /// </summary>
        /// <param name="text">content to be send.</param>
        /// <returns>this object.</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>@see#write(Style, String)</remarks>
        public virtual EscPos Write(string text)
        {
            return Write(style, text);
        }

        /// <summary>
        /// Calls write and feed on end.
        /// </summary>
        /// <param name="style">value to be send.</param>
        /// <param name="text">content to be send.</param>
        /// <returns>this object</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>
        /// @see#write(Style, String)
        /// @see#feed(int)
        /// </remarks>
        public virtual EscPos WriteLF(Style style, string text)
        {
            Write(style, text);
            Write(LF);
            return this;
        }

        /// <summary>
        /// Calls write and feed on end.
        /// </summary>
        /// <param name="printModeStyle">value to be send.</param>
        /// <param name="text">content to be send.</param>
        /// <returns>this object</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>@see#write(PrintModeStyle, String)</remarks>
        public virtual EscPos WriteLF(PrintModeStyle printModeStyle, string text)
        {
            Write(printModeStyle, text);
            Write(LF);
            return this;
        }

        /// <summary>
        /// Calls write and feed on end. The style to be used is de default. You can
        /// configure this default style using {@link #getStyle() getStyle} and/or
        /// {@link #setStyle(Style)}   setStyle}
        /// </summary>
        /// <param name="text">content to be send.</param>
        /// <returns>this object.</returns>
        /// <exception cref="UnsupportedEncodingException">If the named charset is not
        /// supported</exception>
        /// <exception cref="IOException">if an I/O error occurs. In particular, an
        /// <code>IOException</code> is thrown if the output stream is closed.</exception>
        /// <remarks>
        /// @see#writeLF(Style, String)
        /// @see#setStyle(Style)
        /// @see#getStyle()
        /// </remarks>
        public virtual EscPos WriteLF(string text)
        {
            return WriteLF(style, text);
        }

        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        public virtual EscPos Write<T>(BarCodeWrapperInterface<T> barcode, string data)
        {
            byte[] bytes = barcode.GetBytes(data);
            Write(bytes, 0, bytes.Length);
            return this;
        }

        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        public virtual EscPos Write<T>(ImageWrapperInterface<T> wrapper, EscPosImage image)
        {
            byte[] bytes = wrapper.GetBytes(image);
            Write(bytes, 0, bytes.Length);
            return this;
        }

        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        public virtual EscPos Cut(CutMode mode)
        {
            Write(GS);
            Write('V');
            Write(mode.value);
            return this;
        }

        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        ///
        /// ASCII ESC d
        /// </summary>
        /// <param name="style">to be used on line spacing</param>
        /// <param name="nLines">number of lines</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        public virtual EscPos Feed(Style style, int nLines)
        {
            if (nLines < 1 || nLines > 255)
            {
                throw new ArgumentException("nLines must be between 1 and 255");
            }

            byte[] configBytes = style.GetConfigBytes();
            Write(configBytes, 0, configBytes.Length);
            for (int n = 0; n < nLines; n++)
            {
                Write(LF);
            }

            return this;
        }

        /// <summary>
        /// Call feed with default style.
        /// </summary>
        /// <param name="nLines">number of lines with the line spacing of
        /// <code>defaultStyle</code></param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <remarks>@see#feed(Style, int)</remarks>
        public virtual EscPos Feed(int nLines)
        {
            return Feed(style, nLines);
        }

        /// <summary>
        /// Initialize printer. Clears the data in the print buffer and resets the
        /// printer.
        /// reset style of this object. ASCII ESC @
        ///
        /// </summary>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@see#setStyle(Style)</remarks>
        public virtual EscPos InitializePrinter()
        {
            Write(ESC);
            Write('@');
            style.Reset();
            return this;
        }

        /// <summary>
        /// Generate pulse.
        ///
        /// ASCII ESC p m t1 t2
        /// The pulse for ON time is(t1× 2 msec) and for OFF time is(t2× 2 msec)
        /// .
        /// </summary>
        /// <param name="pinConnector">specifies pin 2 or pin 5</param>
        /// <param name="t1">time one</param>
        /// <param name="t2">time two</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if t1 or t2 out of range 0 to 255</exception>
        public virtual EscPos PulsePin(PinConnector pinConnector, int t1, int t2)
        {
            if (t1 < 0 || t1 > 255)
            {
                throw new ArgumentException("t1 must be between 1 and 255");
            }

            if (t2 < 0 || t2 > 255)
            {
                throw new ArgumentException("t2 must be between 1 and 255");
            }

            Write(ESC);
            Write('p');
            Write(pinConnector.value);
            Write(t1);
            Write(t2);
            return this;
        }

        public void Dispose()
        {
            outputStream.Dispose();
        }
    }
}
