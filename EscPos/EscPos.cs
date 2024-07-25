/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EscPos.Barcode;
using EscPos.Image;
using Java.Io;
using Java.Util;
using static EscPos.CharacterCodeTable;
using static EscPos.CutMode;
using static EscPos.FontName;
using static EscPos.Justification;
using static EscPos.PinConnector;

namespace EscPos
{
    /// <summary>
    /// Write some usual ESC/POS commands to the OutPutStream.<p>
    /// ESC/POS was developed by <i>Seiko Epson Corporation</i><p>
    /// Most receipt printers on the market recognize these commands.<p>
    /// and can be used to print texts, barcodes or images
    /// </summary>
    public class EscPos : Closeable, Flushable, EscPosConst
    {
        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        public enum CharacterCodeTable
        {
            // CP437_USA_Standard_Europe(0, "cp437")
            CP437_USA_Standard_Europe,

            // Katakana(1)
            Katakana,

            // CP850_Multilingual(2, "cp850")
            CP850_Multilingual,

            // CP860_Portuguese(3, "cp860")
            CP860_Portuguese,

            // CP863_Canadian_French(4, "cp863")
            CP863_Canadian_French,

            // CP865_Nordic(5, "cp865")
            CP865_Nordic,

            // CP851_Greek(11)
            CP851_Greek,

            // CP853_Turkish(12)
            CP853_Turkish,

            // CP857_Turkish(13, "cp857")
            CP857_Turkish,

            // CP737_Greek(14, "cp737")
            CP737_Greek,

            // ISO8859_7_Greek(15, "iso8859_7")
            ISO8859_7_Greek,

            // WPC1252(16, "cp1252")
            WPC1252,

            // CP866_Cyrillic_2(17, "cp866")
            CP866_Cyrillic_2,

            // CP852_Latin2(18, "cp852")
            CP852_Latin2,

            // CP858_Euro(19, "cp858")
            CP858_Euro,

            // KU42_Thai(20)
            KU42_Thai,

            // TIS11_Thai(21)
            TIS11_Thai,

            // TIS18_Thai(26)
            TIS18_Thai,

            // TCVN_3_1_Vietnamese(30)
            TCVN_3_1_Vietnamese,

            // TCVN_3_2_Vietnamese(31)
            TCVN_3_2_Vietnamese,

            // PC720_Arabic(32)
            PC720_Arabic,

            // WPC775_BalticRim(33)
            WPC775_BalticRim,

            // CP855_Cyrillic(34, "cp855")
            CP855_Cyrillic,

            // CP861_Icelandic(35, "cp861")
            CP861_Icelandic,

            // CP862_Hebrew(36, "cp862")
            CP862_Hebrew,

            // CP864_Arabic(37, "cp864")
            CP864_Arabic,

            // CP869_Greek(38, "cp869")
            CP869_Greek,

            // ISO8859_2_Latin2(39, "iso8859_2")
            ISO8859_2_Latin2,

            // ISO8859_15_Latin9(40, "iso8859_15")
            ISO8859_15_Latin9,

            // CP1098_Farsi(41, "cp1098")
            CP1098_Farsi,

            // CP1118_Lithuanian(42)
            CP1118_Lithuanian,

            // CP1119_Lithuanian(43)
            CP1119_Lithuanian,

            // CP1125_Ukrainian(44)
            CP1125_Ukrainian,

            // WCP1250_Latin2(45, "cp1250")
            WCP1250_Latin2,

            // WCP1251_Cyrillic(46, "cp1251")
            WCP1251_Cyrillic,

            // WCP1253_Greek(47, "cp1253")
            WCP1253_Greek,

            // WCP1254_Turkish(48, "cp1254")
            WCP1254_Turkish,

            // WCP1255_Hebrew(49, "cp1255")
            WCP1255_Hebrew,

            // WCP1256_Arabic(50, "cp1256")
            WCP1256_Arabic,

            // WCP1257_BalticRim(51, "cp1257")
            WCP1257_BalticRim,

            // WCP1258_Vietnamese(52, "cp1258")
            WCP1258_Vietnamese,

            // KZ_1048_Kazakhstan(53)
            KZ_1048_Kazakhstan,

            // User_defined_page(255)
            User_defined_page

            // --------------------
            // TODO enum body members
            // public int value;
            // public String charsetName;
            // private CharacterCodeTable(int value) {
            //     this.value = value;
            //     this.charsetName = "cp437";
            // }
            // private CharacterCodeTable(int value, String charsetName) {
            //     this.value = value;
            //     this.charsetName = charsetName;
            // }
            // --------------------
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        public enum CutMode
        {
            // FULL(48)
            FULL,

            // PART(49)
            PART

            // --------------------
            // TODO enum body members
            // public int value;
            // private CutMode(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        public enum PinConnector
        {
            // Pin_2(48)
            Pin_2,

            // Pin_5(49)
            Pin_5

            // --------------------
            // TODO enum body members
            // public int value;
            // private PinConnector(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        protected OutputStream outputStream;

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        protected string charsetName;

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        protected Style style;

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
        public EscPos(OutputStream outputStream)
        {
            this.outputStream = outputStream;
            this.SetCharsetName(CharacterCodeTable.CP437_USA_Standard_Europe.charsetName);
            style = new Style();
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        public virtual EscPos Write(int b)
        {
            this.outputStream.Write(b);
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        public virtual void Dispose()
        {
            this.outputStream.Dispose();
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        public virtual EscPos SetOutputStream(OutputStream outputStream)
        {
            this.outputStream = outputStream;
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        public virtual OutputStream GetOutputStream()
        {
            return outputStream;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        public virtual Style GetStyle()
        {
            return style;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        public virtual EscPos SetCharacterCodeTable(CharacterCodeTable table)
        {
            SetCharsetName(table.charsetName);
            SetPrinterCharacterTable(table.value);
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
            Write(configBytes, 0, configBytes.length);
            this.outputStream.Write(text.GetBytes(charsetName));
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
            Write(configBytes, 0, configBytes.length);
            this.outputStream.Write(text.GetBytes(charsetName));
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        public virtual EscPos Write(BarCodeWrapperInterface barcode, string data)
        {
            byte[] bytes = barcode.GetBytes(data);
            Write(bytes, 0, bytes.length);
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        public virtual EscPos Write(ImageWrapperInterface wrapper, EscPosImage image)
        {
            byte[] bytes = wrapper.GetBytes(image);
            Write(bytes, 0, bytes.length);
            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        /// <p>
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
            Write(configBytes, 0, configBytes.length);
            for (int n = 0; n < nLines; n++)
            {
                Write(LF);
            }

            return this;
        }

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        /// <p>
        /// ASCII ESC d
        /// </summary>
        /// <param name="style">to be used on line spacing</param>
        /// <param name="nLines">number of lines</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        /// <p>
        /// ASCII ESC d
        /// </summary>
        /// <param name="style">to be used on line spacing</param>
        /// <param name="nLines">number of lines</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <summary>
        /// Call feed with default style.
        /// </summary>
        /// <param name="nLines">number of lines with the line spacing of
        /// <code>defaultStyle</code></param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <remarks>@see#feed(Style, int)</remarks>
        /// <summary>
        /// Initialize printer. Clears the data in the print buffer and resets the
        /// printer.<p>
        /// reset style of this object. ASCII ESC @
        /// <p>
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
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        /// <p>
        /// ASCII ESC d
        /// </summary>
        /// <param name="style">to be used on line spacing</param>
        /// <param name="nLines">number of lines</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <summary>
        /// Call feed with default style.
        /// </summary>
        /// <param name="nLines">number of lines with the line spacing of
        /// <code>defaultStyle</code></param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <remarks>@see#feed(Style, int)</remarks>
        /// <summary>
        /// Initialize printer. Clears the data in the print buffer and resets the
        /// printer.<p>
        /// reset style of this object. ASCII ESC @
        /// <p>
        /// </summary>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Generate pulse.
        /// <p>
        /// ASCII ESC p m t1 t2<p>
        /// The pulse for ON time is(t1× 2 msec) and for OFF time is(t2× 2 msec)
        /// .<p>
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

        /// <summary>
        /// values of character code table.
        /// </summary>
        /// <remarks>@see#setCharacterCodeTable(CharacterCodeTable)</remarks>
        /// <summary>
        /// Values for CutMode.
        /// </summary>
        /// <remarks>@see#cut(CutMode)</remarks>
        /// <summary>
        /// Values for pin connector
        /// </summary>
        /// <remarks>@see#pulsePin(PinConnector, int, int)</remarks>
        /// <summary>
        /// creates an instance based on outputStream.
        /// </summary>
        /// <param name="outputStream">can be one file, System.out or printer...</param>
        /// <remarks>@seejava.io.OutputStream</remarks>
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
        /// <summary>
        /// call outputStrem.flush().
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#flush()</remarks>
        /// <summary>
        /// call close of outputStream.
        /// </summary>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seejava.io.OutputStream#close()</remarks>
        /// <summary>
        /// Each write will be send to output Stream.
        /// </summary>
        /// <param name="outputStream">value to be used on writes</param>
        /// <returns>this object.</returns>
        /// <summary>
        /// get output stream of this object.
        /// </summary>
        /// <returns>actual value of output stream.</returns>
        /// <summary>
        /// Set style of actual instance.
        /// <p>
        /// </summary>
        /// <param name="style">to be used on writes.</param>
        /// <returns>this object</returns>
        /// <remarks>@see#write(Style, String)</remarks>
        /// <summary>
        /// Get actual style of this object.
        /// </summary>
        /// <returns>actual value.</returns>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Set charsetName used on encodes of Strings.
        /// </summary>
        /// <param name="charsetName">value used on String.getBytes</param>
        /// <returns>this object.</returns>
        /// <remarks>
        /// @seejava.lang.String#getBytes(java.lang.String)
        /// @see#write(java.lang.String)
        /// @see#writeLF(java.lang.String)
        /// </remarks>
        /// <summary>
        /// Get charsetName used by this object.
        /// </summary>
        /// <returns>actual charsetName.</returns>
        /// <remarks>@see#setCharsetName(java.lang.String)</remarks>
        /// <summary>
        /// Select character code table.
        /// <p>
        /// each table represent specifics codes for special characters, example when
        /// we need to print accents characters. This function combine setCharsetName
        /// to be used on  <code>String.getBytes</code> and setPrinterCharacterTable
        /// to be used on printer. Then next time that you call <code>write</code>
        /// with String parameter, then you can send special characters.
        /// </summary>
        /// <param name="table">character table possibilities</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Select character code table on the printer.<p>
        /// Is recommended that you use <code>setCharacterCodeTable</code>, but if
        /// you need to use one specific code that is not included in
        /// <code>CharacterCodeTable</code> or if the <code>CharacterCodeTable</code>
        /// codes diverges of your printer documentation, then you should use this
        /// function.<p>
        /// ASCII ESC t n
        /// </summary>
        /// <param name="characterCodeTable">code of table on printer to be selected.</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if characterCodeTable out of range 0
        /// to 255</exception>
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Write String to outputStream.
        /// <p>
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
        /// <summary>
        /// Send bar-code content to the printer.
        /// </summary>
        /// <param name="barcode">objects that implements BarCodeWrapperInterface.</param>
        /// <param name="data">content of bar-code.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeBarCodeWrapperInterface</remarks>
        /// <summary>
        /// Send image to the printer.
        /// </summary>
        /// <param name="wrapper">objects that implements ImageWrapperInterface.</param>
        /// <param name="image">content to be print.</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeImageWrapperInterface</remarks>
        /// <summary>
        /// Executes paper cutting. GS V
        /// </summary>
        /// <param name="mode">FULL cut or PARTIAL cut</param>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@seeCutMode</remarks>
        /// <summary>
        /// Prints the data in the print buffer and feeds n lines.
        /// <p>
        /// ASCII ESC d
        /// </summary>
        /// <param name="style">to be used on line spacing</param>
        /// <param name="nLines">number of lines</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <summary>
        /// Call feed with default style.
        /// </summary>
        /// <param name="nLines">number of lines with the line spacing of
        /// <code>defaultStyle</code></param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if nLines out of range 0 to 255</exception>
        /// <remarks>@see#feed(Style, int)</remarks>
        /// <summary>
        /// Initialize printer. Clears the data in the print buffer and resets the
        /// printer.<p>
        /// reset style of this object. ASCII ESC @
        /// <p>
        /// </summary>
        /// <returns>this object</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <remarks>@see#setStyle(Style)</remarks>
        /// <summary>
        /// Generate pulse.
        /// <p>
        /// ASCII ESC p m t1 t2<p>
        /// The pulse for ON time is(t1× 2 msec) and for OFF time is(t2× 2 msec)
        /// .<p>
        /// </summary>
        /// <param name="pinConnector">specifies pin 2 or pin 5</param>
        /// <param name="t1">time one</param>
        /// <param name="t2">time two</param>
        /// <returns>this object.</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        /// <exception cref="IllegalArgumentException">if t1 or t2 out of range 0 to 255</exception>
        public virtual EscPos Info()
        {
            Properties properties = new Properties();
            properties.Load(
                Objects.RequireNonNull(
                    GetType().GetClassLoader().GetResourceAsStream("projectinfo.properties")
                )
            );
            string Version = properties.GetProperty("version");
            Style title = new Style()
                .SetFontSize(Style.FontSize._3, Style.FontSize._3)
                .SetColorMode(Style.ColorMode.WhiteOnBlack)
                .SetJustification(Justification.Center);
            Write(title, "EscPos Coffee");
            Feed(5);
            WriteLF("java driver for ESC/POS commands.");
            WriteLF("Version: " + Version);
            Feed(3);
            GetStyle().SetJustification(Justification.Right);
            WriteLF("github.com");
            WriteLF("anastaciocintra/escpos-coffee");
            Feed(5);
            Cut(CutMode.FULL);
            return this;
        }
    }
}
