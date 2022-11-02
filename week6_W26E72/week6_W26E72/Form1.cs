using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week6_W26E72
{
    public partial class Form1 : Form
    {
        private List<Entities.Ball> _balls = new List<Entities.Ball>();
        private Entities.BallFactory _factory;
        public Entities.BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        public Form1()
        {
            InitializeComponent();
            createTimer.Tick += CreateTimer_Tick;
            conveyorTimer.Tick += ConveyorTimer_Tick;
        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _balls[0];
                panel1.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            panel1.Controls.Add(ball);
        }
    }
}
