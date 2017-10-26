using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrees
{
    public interface IDelete : ITree
    {
        void DelRight(int val);
        void DelLeft(int val);
        void DelRightRotation(int val);
        void DelLeftRotation(int val);

    }
}
