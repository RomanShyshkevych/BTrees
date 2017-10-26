using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrees
{
    public class BsTreeV : ITree
    {
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        public Node root = null;

        private void Visit(Node node, IVisitor v)
        {
            if (node == null)
                return;
            Visit(node.left, v);
            v.Action(node);
            Visit(node.right, v);
        }
        private interface IVisitor
        {
            void Action(Node node);
        }

        public void Init(int[] ini)
        {
            if (ini == null)
                return;

            Clear();
            for (int i = 0; i < ini.Length; i++)
            {
                Add(ini[i]);
            }
        }

        public void Clear()
        {
            root = null;
        }
        // visitor +
        #region Size
        private class SizeVisitor : IVisitor
        {
            public int size = 0;
            public void Action(Node node)
            {
                size++;
            }
        }
        public int Size()
        {
            SizeVisitor v = new SizeVisitor();
            Visit(root, v);
            return v.size;
        }
        #endregion 
        // visitor +
        #region ToString
        private class StringVisitor : IVisitor
        {
            public string str = "";
            public void Action(Node node)
            {
                str += node.val + ", ";
            }
        }
        public override String ToString()
        {
            StringVisitor v = new StringVisitor();
            Visit(root, v);
            return v.str.TrimEnd(new char[] { ',', ' ' });
        }

        #endregion //
        // visitor +
        #region ToArray
        private class ArrayVisitor : IVisitor
        {
            public int[] arr = null;
            int i = 0;
            public ArrayVisitor(int size)
            {
                arr = new int[size];
            }
            public void Action(Node node)
            {
                arr[i++] = node.val;
            }
        }
        public int[] ToArray()
        {
            ArrayVisitor v = new ArrayVisitor(Size());
            Visit(root, v);
            return v.arr;
        }
        #endregion
        // visitor +
        #region Leaves
        private class LeavesVisitor : IVisitor
        {
            public int leaves = 0;
            public void Action(Node node)
            {
                if (node.left == null && node.right == null)
                    leaves++;
            }
        }
        public int Leaves()
        {
            LeavesVisitor v = new LeavesVisitor();
            Visit(root, v);
            return v.leaves;
        }
        #endregion
        // visitor +
        #region Nodes
        private class NodesVisitor : IVisitor
        {
            public int nodes = 0;
            public void Action(Node node)
            {
                if (node.left != null || node.right != null)
                    nodes++;
            }
        }
        public int Nodes()
        {
            NodesVisitor v = new NodesVisitor();
            Visit(root, v);
            return v.nodes;
        }
        #endregion
        // visitor +
        #region Width

        public int Width()
        {
            if (root == null)
                return 0;
            WidthVisitor v = new WidthVisitor(Height(), root);
            Visit(root, v);
            return v.ret.Max();
        }
        private class WidthVisitor : IVisitor
        {
            public int[] ret;
            private Node root;
            public WidthVisitor(int Height, Node root)
            {
                ret = new int[Height];
                this.root = root;
            }
            public void Action(Node node)
            {
                int currentDepth = HeightV(root, node);
                ++ret[currentDepth - 1];
            }
        }
        public static int HeightV(Node root, Node dest)
        {
            if (root == null)
                return 0;

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(root);
            int height = 0;

            while (true)
            {
                int nodeCount = queue.Count;

                if (nodeCount == 0)
                    return height;

                height++;

                while (nodeCount > 0)
                {
                    Node node = queue.Dequeue();
                    if (node == dest)
                        return height;

                    if (node.left != null)
                        queue.Enqueue(node.left);

                    if (node.right != null)
                        queue.Enqueue(node.right);

                    nodeCount--;
                }
            }
        }
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }
        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;
        }
        #endregion

        #region Add
        public void Add(int val)
        {
            if (root == null)
                root = new Node(val);
            else
                AddNode(root, val);
        }
        private void AddNode(Node node, int val)
        {
            if (val < node.val)
            {
                if (node.left == null)
                    node.left = new Node(val);
                else
                    AddNode(node.left, val);
            }
            else if (val > node.val)
            {
                if (node.right == null)
                    node.right = new Node(val);
                else
                    AddNode(node.right, val);
            }
        }
        #endregion

        #region Del
        public void Del(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

            DeleteNode(root, val);
        }
        private Node FindNode(Node node, int val)
        {
            if (node == null || val == node.val)
                return node;
            if (val < node.val)
                return FindNode(node.left, val);
            else
                return FindNode(node.right, val);
        }
        private Node DeleteNode(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
            {
                node.left = DeleteNode(node.left, val);
            }
            else if (val > node.val)
            {
                node.right = DeleteNode(node.right, val);
            }
            else if (node.left != null && node.right != null)
            {
                node.val = Min(node.right).val;
                node.right = DeleteNode(node.right, node.val);
            }
            else
            {
                if (node.left != null)
                    node = node.left;
                else
                    node = node.right;
            }
            return node;
        }
        private Node Min(Node node)
        {
            if (node.left == null)
                return node;

            return Min(node.left);
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Node node)
        {
            if (node == null)
                return;

            SwapSides(node.left);
            Node temp = node.right;
            node.right = node.left;
            node.left = temp;
            SwapSides(node.left);
        }
        #endregion

        #region Equal
        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as BsTreeV).root);
        }

        private bool CompareNodes(Node curTree, Node tree)
        {
            if (curTree == null && tree == null)
                return true;
            if (curTree == null || tree == null)
                return false;

            bool equal = false;
            equal = CompareNodes(curTree.left, tree.left);
            equal = equal & (curTree.val == tree.val);
            equal = CompareNodes(curTree.right, tree.right);
            return equal;
        }
        #endregion
    }

}
