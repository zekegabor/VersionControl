using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace week6_W26E72.Entities
{
    public class Ball : Label
    {
        public Ball()
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Paint += Ball_Paint;
        }

        public void MoveBall()
        {
            Left += 1;
        }

        protected void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }
    }
}
