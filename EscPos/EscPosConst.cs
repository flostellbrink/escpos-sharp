/*
 * Use of this source code is governed by the MIT license that can be
 * found in the LICENSE file.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static EscPos.CharacterCodeTable;
using static EscPos.CutMode;
using static EscPos.FontName;
using static EscPos.Justification;
using static EscPos.PinConnector;

namespace EscPos
{
    public interface EscPosConst
    {
        /// <summary>
        /// </summary>
        public readonly int NUL = 0;
        public readonly int LF = 10;
        public readonly int ESC = 27;
        public readonly int GS = 29;

        /// <summary>
        /// Values for print justification.
        /// </summary>
        /// <remarks>@seeStyle#setJustification(Justification)</remarks>
        public enum Justification
        {
            // Left_Default(48)
            Left_Default,

            // Center(49)
            Center,

            // Right(50)
            Right

            // --------------------
            // TODO enum body members
            // public int value;
            // private Justification(int value) {
            //     this.value = value;
            // }
            // --------------------
        }
    }
}
