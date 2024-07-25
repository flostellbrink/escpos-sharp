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
using static EscPos.Image.CharacterCodeTable;
using static EscPos.Image.CutMode;
using static EscPos.Image.PinConnector;
using static EscPos.Image.Justification;
using static EscPos.Image.FontName;
using static EscPos.Image.FontSize;
using static EscPos.Image.Underline;
using static EscPos.Image.ColorMode;
using static EscPos.Image.BarCodeSystem;
using static EscPos.Image.BarCodeHRIPosition;
using static EscPos.Image.BarCodeHRIFont;
using static EscPos.Image.PDF417ErrorLevel;
using static EscPos.Image.PDF417Option;
using static EscPos.Image.QRModel;
using static EscPos.Image.QRErrorCorrectionLevel;
using static EscPos.Image.BitImageMode;
using static EscPos.Image.GraphicsImageBxBy;

namespace EscPos.Image
{
    public interface ImageWrapperInterface<T>
    {
        byte[] GetBytes(EscPosImage image);
        T SetJustification(EscPosConst.Justification justification);
    }
}