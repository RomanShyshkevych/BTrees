using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawBSTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            XData data = new XData();
            tDraw1.data = data;
        }
    }
}
