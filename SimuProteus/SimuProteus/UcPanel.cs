using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimuProteus
{
    class UcPanel: Control
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Console.WriteLine("eeefffff" + TT);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_ERASEBKGND = 0x14;
            if (m.Msg != WM_ERASEBKGND)
            {
                 base.WndProc(ref m);
                if (m.Msg != 132 && m.Msg != 32 && m.Msg != 512 && m.Msg != 673 && m.Msg != 675)
                    Console.WriteLine(m.Msg + "   " + TT);
            }
        }
    }
}
