﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace w26e72_VersionControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = ResourceVC.FirstName;
            label2.Text = ResourceVC.LastName;
            button1.Text = ResourceVC.Add;
        }
    }
}
