using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree l = new RBTree();
            l.Init(new int[] { 3, 7, 1, 0, 9, 2, 8 });
            l.Del(1);
            l.DisplayTree();
            Console.ReadKey();
        }
    }
}
