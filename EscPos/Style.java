/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using Java.Io;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Com.Github.Anastaciocintra.Escpos.CharacterCodeTable;
using static Com.Github.Anastaciocintra.Escpos.CutMode;
using static Com.Github.Anastaciocintra.Escpos.PinConnector;
using static Com.Github.Anastaciocintra.Escpos.Justification;
using static Com.Github.Anastaciocintra.Escpos.FontName;
using static Com.Github.Anastaciocintra.Escpos.FontSize;
using static Com.Github.Anastaciocintra.Escpos.Underline;
using static Com.Github.Anastaciocintra.Escpos.ColorMode;

namespace Com.Github.Anastaciocintra.Escpos
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
        public enum FontName
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
            // private FontName(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        public enum FontSize
        {
            // _1(0)
            _1,
            // _2(1)
            _2,
            // _3(2)
            _3,
            // _4(3)
            _4,
            // _5(4)
            _5,
            // _6(5)
            _6,
            // _7(6)
            _7,
            // _8(7)
            _8 

            // --------------------
            // TODO enum body members
            // public int value;
            // private FontSize(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        public enum Underline
        {
            // None_Default(48)
            None_Default,
            // OneDotThick(49)
            OneDotThick,
            // TwoDotThick(50)
            TwoDotThick 

            // --------------------
            // TODO enum body members
            // public int value;
            // private Underline(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        public enum ColorMode
        {
            // BlackOnWhite_Default(0)
            BlackOnWhite_Default,
            // WhiteOnBlack(1)
            WhiteOnBlack 

            // --------------------
            // TODO enum body members
            // public int value;
            // private ColorMode(int value) {
            //     this.value = value;
            // }
            // --------------------
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected FontName fontName;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected bool bold;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected Underline underline;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected FontSize fontWidth;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected FontSize fontHeight;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected Justification justification;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected bool defaultLineSpacing;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected int lineSpacing;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        protected ColorMode colorMode;
        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        public Style()
        {
            Reset();
        }

        /// <summary>
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set Justification for text.
        /// </summary>
        /// <param name="justification">value used on ESC a n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacing">value used on ESC 3 n</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when lineSpacing is not between 0 and 255.</exception>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set Justification for text.
        /// </summary>
        /// <param name="justification">value used on ESC a n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacing">value used on ESC 3 n</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when lineSpacing is not between 0 and 255.</exception>
        /// <remarks>@see#getConfigBytes()</remarks>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set Justification for text.
        /// </summary>
        /// <param name="justification">value used on ESC a n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacing">value used on ESC 3 n</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when lineSpacing is not between 0 and 255.</exception>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Reset line spacing to printer default used on ESC 2
        /// </summary>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
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
        /// Values of font name.
        /// </summary>
        /// <remarks>@see#setFontName(FontName)</remarks>
        /// <summary>
        /// Values of font size.
        /// </summary>
        /// <remarks>@see#setFontSize(FontSize, FontSize)</remarks>
        /// <summary>
        /// values of underline style.
        /// </summary>
        /// <remarks>@see#setUnderline(Underline)</remarks>
        /// <summary>
        /// values of color mode background / foreground reverse.
        /// </summary>
        /// <remarks>@see#setColorMode(ColorMode)</remarks>
        /// <summary>
        /// creates Style object with default values.
        /// </summary>
        /// <summary>
        /// creates Style object with another Style instance values.
        /// </summary>
        /// <param name="another">value to be copied.</param>
        /// <summary>
        /// Reset values to default.
        /// </summary>
        /// <summary>
        /// Set character font name.
        /// </summary>
        /// <param name="fontName">used on ESC M n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set emphasized mode on/off
        /// </summary>
        /// <param name="bold">used on ESC E n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// set font size
        /// </summary>
        /// <param name="fontWidth">value used on GS ! n</param>
        /// <param name="fontHeight">value used on GS ! n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set underline mode.
        /// </summary>
        /// <param name="underline">value used on ESC – n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set Justification for text.
        /// </summary>
        /// <param name="justification">value used on ESC a n</param>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacing">value used on ESC 3 n</param>
        /// <returns>this object</returns>
        /// <exception cref="IllegalArgumentException">when lineSpacing is not between 0 and 255.</exception>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// Reset line spacing to printer default used on ESC 2
        /// </summary>
        /// <returns>this object</returns>
        /// <remarks>@see#getConfigBytes()</remarks>
        /// <summary>
        /// set color mode background / foreground reverse.
        /// </summary>
        /// <param name="colorMode">value used on GS B n</param>
        /// <returns>this object</returns>
        /// <summary>
        /// Configure font Style.
        /// <p>
        /// Select character font.
        /// <p>
        /// ASCII ESC M n
        /// <p>
        /// 
        /// Turn emphasized(bold) mode on/off.
        /// <p>
        /// ASCII ESC E n
        /// <p>
        /// 
        /// set font size.
        /// <p>
        /// ASCII GS ! n
        /// <p>
        /// 
        /// select underline mode
        /// <p>
        /// ASCII ESC – n
        /// <p>
        /// 
        /// Select justification
        /// <p>
        /// ASCII ESC a n
        /// <p>
        /// 
        /// Select default line spacing
        /// <p>
        /// ASCII ESC 2
        /// <p>
        /// 
        /// Set line spacing
        /// <p>
        /// ASCII ESC 3 n
        /// <p>
        /// 
        /// Turn white/black reverse print mode on/off<p>
        /// ASCII GS B n
        /// </summary>
        /// <returns>ESC/POS commands to configure style</returns>
        /// <exception cref="IOException">if an I/O error occurs.</exception>
        public virtual byte[] GetConfigBytes()
        {
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();

            //
            bytes.Write(ESC);
            bytes.Write('M');
            bytes.Write(fontName.value);

            //
            bytes.Write(ESC);
            bytes.Write('E');
            int n = bold ? 1 : 0;
            bytes.Write(n);

            //
            n = fontWidth.value << 4 | fontHeight.value;
            bytes.Write(GS);
            bytes.Write('!');
            bytes.Write(n);

            //
            bytes.Write(ESC);
            bytes.Write('-');
            bytes.Write(underline.value);

            //
            bytes.Write(ESC);
            bytes.Write('a');
            bytes.Write(justification.value);

            //
            if (defaultLineSpacing)
            {
                bytes.Write(ESC);
                bytes.Write('2');
            }
            else
            {
                bytes.Write(ESC);
                bytes.Write('3');
                bytes.Write(lineSpacing);
            }


            //
            bytes.Write(GS);
            bytes.Write('B');
            bytes.Write(colorMode.value);
            return bytes.ToByteArray();
        }
    }
}