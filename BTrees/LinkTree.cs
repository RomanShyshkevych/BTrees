using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrees
{
    public class LinkTree : ITree, IDelete
    {
        public class Node
        {
            public int val;
            public Link left = new Link(null);
            public Link right = new Link(null);
            public Node(int val)
            {
                this.val = val;
            }
        }
        public class Link
        {
            public Node node;
            public Link(Node node)
            {
                this.node = node;
            }
        }

        public Link root = new Link(null);

        public void Init(int[] ini)
        {
            Clear();
            if (ini == null)
                return;

            for (int i = 0; i < ini.Length; i++)
            {
                Add(ini[i]);
            }
        }

        #region Add
        public void Add(int val)
        {
            AddNode(root, val);
        }
        private void AddNode(Link l, int val)
        {
            if (l.node == null)
            {
                l.node = new Node(val);
                return;
            }
           
           if (val < l.node.val)
            {
                AddNode(l.node.left, val);
            }
            else
            {
                AddNode(l.node.right, val);
            }
        }
        #endregion

        #region Del
        public void Del(int val)
        {
            DelRight(val);
        }
        private Link FindNode(Link l, int val)
        {
            if(l.node == null || val == l.node.val)
                return l;
            Link res = new Link(null);
            if (val < l.node.val)
                res = FindNode(l.node.left, val);
            else
                res = FindNode(l.node.right, val);
            return res;
        }
       
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }
        private int GetHeight(Link l)
        {
            if (l.node == null)
                return 0;

            return Math.Max(GetHeight(l.node.left), GetHeight(l.node.right)) + 1;
        }
        #endregion

        #region Width
        public int Width()
        {
            if (root.node == null)
                return 0;

            int[] ret = new int[Height()];
            GetWidth(root, ret, 0);
            return ret.Max();
        }
        private void GetWidth(Link l, int[] levels, int level)
        {
            if (l.node == null)
                return;

            GetWidth(l.node.left, levels, level + 1);
            levels[level]++;
            GetWidth(l.node.right, levels, level + 1);
        }
        #endregion

        #region Leaves
        public int Leaves()
        {
            return GetLeaves(root);
        }
        private int GetLeaves(Link l)
        {
            if (l.node == null)
                return 0;

            int leaves = 0;
            leaves += GetLeaves(l.node.left);
            if (l.node.left.node == null && l.node.right.node == null)
                leaves++;
            leaves += GetLeaves(l.node.right);
            return leaves;
        }
        #endregion

        #region Nodes
        public int Nodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(Link l)
        {
            if (l.node == null)
                return 0;
            int nodes = 0;
            nodes += GetNodes(l.node.left);
            if (l.node.left.node != null || l.node.right.node != null)
                nodes++;
            nodes += GetNodes(l.node.right);
            return nodes;
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Link l)
        {
            if (l.node == null)
                return;

            SwapSides(l.node.left);
            Link temp = l.node.right;
            l.node.right = l.node.left;
            l.node.left = temp;
            SwapSides(l.node.left);
        }
        #endregion

        #region Size
        public int Size()
        {
            return GetSize(root);
        }
        private int GetSize(Link l)
        {
            if (l.node == null)
                return 0;

            int count = 0;
            count += GetSize(l.node.left);
            count++;
            count += GetSize(l.node.right);
            return count;
        }
        #endregion

        #region ToArray
        public int[] ToArray()
        {
            if (root.node == null)
                return new int[] { };

            int[] ret = new int[Size()];
            int i = 0;
            NodeToArray(root, ret, ref i);
            return ret;


        }
        private void NodeToArray(Link l, int[] ini, ref int n)
        {
            if (l.node == null)
                return;

            NodeToArray(l.node.left, ini, ref n);
            ini[n++] = l.node.val;
            NodeToArray(l.node.right, ini, ref n);

        }
        #endregion

        #region ToString
        public override String ToString()
        {
            return NodeToString(root).TrimEnd(new char[] { ',', ' ' });
        }

        private String NodeToString(Link l)
        {
            if (l.node == null)
                return "";

            String str = "";
            str += NodeToString(l.node.left);
            str += l.node.val + ", ";
            str += NodeToString(l.node.right);
            return str;
        }

        public void Clear()
        {
            root = new Link(null);
        }
        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as LinkTree).root);
        }

        private bool CompareNodes(Link curTree, Link tree)
        {
            if (curTree.node == null && tree.node == null)
                return true;
            if (curTree.node == null || tree.node == null)
                return false;

            bool equal = false;
            equal = CompareNodes(curTree.node.left, tree.node.left);
            equal = equal & (curTree.node.val == tree.node.val);
            equal = CompareNodes(curTree.node.right, tree.node.right);
            return equal;
        }
        #endregion
        public void DelLeft(int val)
        {
            Link lnk = FindNode(root, val);
            if (lnk.node == null)
                return; 
            DeleteNodeLeft(lnk);
        }

        private void DeleteNodeLeft(Link l)
        {
            Link rMax = MaxR(l.node.left);
            if (rMax.node == null)
            {
                l.node = l.node.right.node;
                return;
            }
            Node p = rMax.node;
            rMax.node = rMax.node.left.node;
            p.right.node = l.node.right.node;
            p.left.node = l.node.left.node;
            l.node = p;

        }
        private Link MaxR(Link l)
        {
            if (l.node == null || l.node.right.node == null)
                return l;

            return MaxR(l.node.right);
        }
        public void DelRight(int val)
        {
            Link lnk = FindNode(root, val);
            if (lnk.node == null)
                return;
            DeleteNodeRight(lnk);
        }
        private void DeleteNodeRight(Link l)
        {
            Link lMin = MinL(l.node.right);
            if (lMin.node == null)
            {
                l.node = l.node.left.node;
                return;
            }
            Node p = lMin.node;
            lMin.node = lMin.node.right.node;
            p.right.node = l.node.right.node;
            p.left.node = l.node.left.node;
            l.node = p;

        }
        private Link MinL(Link l)
        {
            if (l.node == null || l.node.left.node == null)
                return l;

            return MinL(l.node.left);
        }

        public void DelLeftRotation(int val)
        {
            Link lnk = FindNode(root, val);
            if (lnk.node == null)
                return;
            DeleteNodeLeftRotation(lnk);
        }
        private void DeleteNodeLeftRotation(Link l)
        {
            if(l.node.left.node == null)
            {
                l.node = l.node.right.node;
                return;
            }
            Node p = l.node.right.node;
            l.node = l.node.left.node;
            Link rMax = MaxR(l);
            rMax.node.right.node = p;

        }
        public void DelRightRotation(int val)
        {
            Link lnk = FindNode(root, val);
            if (lnk.node == null)
                return;
            DeleteNodeRightRotation(lnk);
        }
        private void DeleteNodeRightRotation(Link l)
        {
            if (l.node.right.node == null)
            {
                l.node = l.node.left.node;
                return;
            }
            Node p = l.node.left.node;
            l.node = l.node.right.node;
            Link rMax = MinL(l);
            rMax.node.left.node = p;

        }

    }
}
