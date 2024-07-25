/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using EscPos;
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

namespace EscPos.Barcode
{
    public interface BarCodeWrapperInterface<T>
    {
        byte[] GetBytes(string data);
        T SetJustification(EscPosConst.Justification justification);
    }
}