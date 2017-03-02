﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimuProteus
{
    public class Draw
    {
        static Color colorFoot = Color.Black, colorLine = Color.Green;
        static float widthFoot = 5f, widthLine = 2f;

        /// <summary>
        /// 绘制元器件的管脚
        /// </summary>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void LineFoot(Control board, int x, int y, int ox, int oy)
        {
            Graphics g = board.CreateGraphics();
            Pen pen = new Pen(colorFoot, widthFoot);
            g.DrawLine(pen, new Point(x, y), new Point(ox, oy));
        }

        /// <summary>
        /// 连接两个元器件
        /// </summary>
        /// <param name="board"></param>
        /// <param name="isXFirst"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ox"></param>
        /// <param name="oy"></param>
        public static void LineBetweenElement(Control board, bool isXFirst, int x, int y, int ox, int oy)
        {
            Graphics g = board.CreateGraphics();
            Pen pen = new Pen(colorLine, widthLine);
            if (isXFirst)
            {
                g.DrawLine(pen, new Point(x, y), new Point(ox, y));
                g.DrawLine(pen, new Point(ox, y), new Point(ox, oy));
            }
            else
            {
                g.DrawLine(pen, new Point(x, y), new Point(x, oy));
                g.DrawLine(pen, new Point(x, oy), new Point(ox, oy));
            }
        }

        /// <summary>
        /// 画实心圆
        /// </summary>
        /// <param name="board"></param>
        /// <param name="color"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public static void DrawSolidCircle(Control board, int color, Point center, int radius)
        {
            Color colorItem = Color.FromArgb(color);
            Rectangle rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2 , radius * 2);
            
            Graphics g = board.CreateGraphics();
            Pen pen = new Pen(colorItem);
            g.DrawEllipse(pen, rect);
            Brush brush = new SolidBrush(colorItem);
            g.FillEllipse(brush, rect);
        }

        /// <summary>
        /// 写字
        /// </summary>
        /// <param name="board"></param>
        /// <param name="color"></param>
        /// <param name="location"></param>
        /// <param name="words"></param>
        public static void DrawWords(Control board, Color color,PointF location, string words)
        {
            Graphics g = board.CreateGraphics();
            Brush brush = new SolidBrush(color);
            Font font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);

            g.DrawString(words, font, brush, location);
        }
    }
}