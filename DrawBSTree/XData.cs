using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTrees;

namespace DrawBSTree
{
    public class XData: BSTree
    {
        public int[] arrTree = new int[] { 3, 7, 9, 4, 1, 12, 2, 5};

        public int left = 0;
        public int right = 0;
        public int dy = 0;
        public int level = 0;
        public int xp = 0; // x-потомок
        public int yp = 0; // y-потомок
    }
}
