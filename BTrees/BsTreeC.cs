using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTrees
{
    public class BsTreeC : ITree
    {
        class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        Node root = null;

        public void Add(int val)
        {
            if (root == null)
            {
                root = new Node(val);
                return;
            }

            Node cur = root;
            while (true)
            {
                if (val < cur.val)
                {
                    if (cur.left == null)
                    {
                        cur.left = new Node(val);
                        return;
                    }
                    cur = cur.left;
                }
                else
                {
                    if (cur.right == null)
                    {
                        cur.right = new Node(val);
                        return;
                    }
                    cur = cur.right;
                }
            }
        }

        public void Clear()
        {
            root = null;
        }

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

        public bool Equal(ITree tree)
        {
            bool eq = true;

            Node cur = root;
            Node otherCur = (tree as BsTreeC).root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null || otherCur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                    stack.Push(otherCur);
                    otherCur = otherCur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        otherCur = stack.Pop();
                        cur = stack.Pop();
                        if (cur.val != otherCur.val)
                            return false;
                        cur = cur.right;
                        otherCur = otherCur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
            return eq;
        }

        public int Height()
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

                    if (node.left != null)
                        queue.Enqueue(node.left);

                    if (node.right != null)
                        queue.Enqueue(node.right);

                    nodeCount--;
                }
            }
        }

        public void Init(int[] ini)
        {
            if (ini == null)
                ini = new int[0];

            foreach (int i in ini)
            {
                Add(i);
            }
        }

        public int Leaves()
        {
            int leaves = 0;

            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        if (cur.left == null && cur.right == null)
                            leaves++;
                        cur = cur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
            return leaves;
        }

        public int Nodes()
        {
            int nodes = 0;

            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        if (cur.left != null || cur.right != null)
                            nodes++;
                        cur = cur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }

            return nodes;
        }

        public void Reverse()
        {
            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        Node temp = cur.left;
                        cur.left = cur.right;
                        cur.right = temp;
                        cur = cur.left;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
        }

        public int Size()
        {
            int size = 0;

            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        size++;
                        cur = cur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
            return size;
        }

        public int[] ToArray()
        {
            int[] ret = new int[Size()];
            int i = 0;

            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        ret[i++] = cur.val;
                        cur = cur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }

            return ret;
        }

        public int Width()
        {
            if (root == null)
                return 0;

            int maxwidth = 0;

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int count = q.Count;
                maxwidth = Math.Max(maxwidth, count);
                while (count-- > 0)
                {
                    Node temp = q.Dequeue();

                    if (temp.left != null)
                        q.Enqueue(temp.left);

                    if (temp.right != null)
                        q.Enqueue(temp.right);
                }
            }
            return maxwidth;
        }

        public override string ToString()
        {
            string ret = "";

            Node cur = root;
            Stack<Node> stack = new Stack<Node>();
            bool done = false;

            while (!done)
            {
                if (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        cur = stack.Pop();
                        ret += cur.val + ", ";
                        cur = cur.right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
            return ret.TrimEnd(',', ' ');
        }
    }
}
