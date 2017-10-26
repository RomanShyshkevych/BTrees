using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTrees.BSTree;

namespace DrawBSTree
{
    public partial class TDraw : UserControl
    {
        public TDraw()
        {
            InitializeComponent();
        }
        public XData data = null;
        public void Draw(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();
            Node root = data.root;

            data.right = pb.Width;
            data.dy = pb.Height / (data.Height() + 1);
            data.xp = pb.Width / 2;

            DrawNode(root, g, data.left, data.right, data.dy, data.level, data.xp, data.yp);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            data.Init(data.arrTree);
            Draw(pictureBox1);
        }

        private void DrawNode(Node p, Graphics g, int left, int right, int dy, int level, int xp, int yp)
        {
            if (p == null)
                return;

            int x = (left + right) / 2;
            int y = ++level * dy;

            g.DrawLine(new Pen(Color.Black), x, y - 10, xp, yp);
            g.DrawEllipse(new Pen(Color.Green), x - 10, y - 10, 20, 20);
            g.DrawString("" + p.val, new Font("Arial", 10), Brushes.Black, x - 7, y - 7);

            DrawNode(p.left, g, left, x, dy, level, x, y + 10);
            DrawNode(p.right, g, x, right, dy, level, x, y + 10);
        }
    }
}
