using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Windows.Office2007
{
    public class OfficeStatusStrip : StatusStrip
    {
        public OfficeStatusStrip()
        {
            this.Paint += new PaintEventHandler(Dibujar);
        }

        private void Dibujar(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            grfx.DrawLine(new Pen(Color.FromArgb(86, 125, 176)), 0, 0, this.Width - 1, 0);
            grfx.DrawLine(Pens.White, 0, 1, this.Width - 1, 1);

            int height = Convert.ToInt32((this.Height - 1) / 3);
            Rectangle lrect = new Rectangle(0, 2, this.Width, height);
            Brush br = new LinearGradientBrush(lrect, Color.FromArgb(215, 230, 249), Color.FromArgb(199, 220, 248), LinearGradientMode.Vertical);

            grfx.FillRectangle(br, lrect);

            lrect.Y = lrect.Y + height;
            lrect.Height = this.Height - lrect.Y;

            br = new LinearGradientBrush(lrect, Color.FromArgb(179, 208, 245), Color.FromArgb(205, 224, 247), LinearGradientMode.Vertical);

            grfx.FillRectangle(br, lrect);

            if (this.SizingGrip == true)
            {
                Rectangle sgb = this.SizeGripBounds;
                Brush fbr = new SolidBrush(Color.FromArgb(69, 93, 128));
                Brush bbr = new SolidBrush(Color.FromArgb(177, 201, 232));

                //right-top
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 3, sgb.Height + sgb.Y - 13, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 4, sgb.Y + sgb.Height - 14, 2, 2));

                //right-middle
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 3, sgb.Height + sgb.Y - 9, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 4, sgb.Y + sgb.Height - 10, 2, 2));

                //right-bottom
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 3, sgb.Height + sgb.Y - 5, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 4, sgb.Y + sgb.Height - 6, 2, 2));

                //middle-center
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 7, sgb.Height + sgb.Y - 9, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 8, sgb.Y + sgb.Height - 10, 2, 2));

                //middle-bottom
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 7, sgb.Height + sgb.Y - 5, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 8, sgb.Y + sgb.Height - 6, 2, 2));

                //bottom left
                grfx.FillRectangle(bbr, new Rectangle(sgb.X + sgb.Width - 11, sgb.Height + sgb.Y - 5, 2, 2));
                grfx.FillRectangle(fbr, new Rectangle(sgb.X + sgb.Width - 12, sgb.Y + sgb.Height - 6, 2, 2));
            }
        }
    }
}