using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimuProteus
{
    class UcPanel : ScrollableControl
    {
        public UcPanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        }

        public string TT { get; set; }

        public bool FinishD { get; set; }

        protected override CreateParams CreateParams//v1.10 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }
    }
}
