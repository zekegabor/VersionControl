using System;
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
        BindingList<Entities.User> users = new BindingList<Entities.User>();
        public Form1()
        {
            InitializeComponent();
            label2.Text = ResourceVC.LastName;
            button1.Text = ResourceVC.Add;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

            button1.MouseDown += Button1_MouseDown;
        }

        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            var u = new Entities.User()
            {
                FullName = textBox1.Text,
                //FirstName = textBox2.Text
            };
            users.Add(u);
        }
    }
}
