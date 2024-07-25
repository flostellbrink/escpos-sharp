/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using Com.Github.Anastaciocintra.Escpos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Com.Github.Anastaciocintra.Escpos.Barcode.CharacterCodeTable;
using static Com.Github.Anastaciocintra.Escpos.Barcode.CutMode;
using static Com.Github.Anastaciocintra.Escpos.Barcode.PinConnector;
using static Com.Github.Anastaciocintra.Escpos.Barcode.Justification;
using static Com.Github.Anastaciocintra.Escpos.Barcode.FontName;
using static Com.Github.Anastaciocintra.Escpos.Barcode.FontSize;
using static Com.Github.Anastaciocintra.Escpos.Barcode.Underline;
using static Com.Github.Anastaciocintra.Escpos.Barcode.ColorMode;
using static Com.Github.Anastaciocintra.Escpos.Barcode.BarCodeSystem;
using static Com.Github.Anastaciocintra.Escpos.Barcode.BarCodeHRIPosition;
using static Com.Github.Anastaciocintra.Escpos.Barcode.BarCodeHRIFont;

namespace Com.Github.Anastaciocintra.Escpos.Barcode
{
    public interface BarCodeWrapperInterface<T>
    {
        byte[] GetBytes(string data);
        T SetJustification(EscPosConst.Justification justification);
    }
}