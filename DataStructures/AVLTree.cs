using System;
using System.Collections.Generic;
using MunicipalService_P3.Models;   // ✅ Adjusted to your project namespace

namespace MunicipalService_P3.DataStructures
{
    public class AvlNode
    {
        public int Key { get; set; }
        public ServiceRequest Value { get; set; }
        public AvlNode Left { get; set; }
        public AvlNode Right { get; set; }
        public int Height { get; set; } = 1;

        public AvlNode(int key, ServiceRequest val)
        {
            Key = key;
            Value = val;
        }
    }

    public class AvlTree
    {
        public AvlNode Root { get; private set; }

        private int Height(AvlNode n) => n?.Height ?? 0;
        private int Balance(AvlNode n) => n == null ? 0 : Height(n.Left) - Height(n.Right);

        private AvlNode RightRotate(AvlNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));

            return x;
        }

        private AvlNode LeftRotate(AvlNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));

            return y;
        }

        private AvlNode InsertInternal(AvlNode node, int key, ServiceRequest val)
        {
            if (node == null) return new AvlNode(key, val);

            if (key < node.Key)
                node.Left = InsertInternal(node.Left, key, val);
            else if (key > node.Key)
                node.Right = InsertInternal(node.Right, key, val);
            else
            {
                // Update existing node
                node.Value = val;
                return node;
            }

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = Balance(node);

            // Balance cases
            if (balance > 1 && key < node.Left.Key) return RightRotate(node);
            if (balance < -1 && key > node.Right.Key) return LeftRotate(node);
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public void Insert(int key, ServiceRequest val) => Root = InsertInternal(Root, key, val);

        public ServiceRequest Search(int key)
        {
            var cur = Root;
            while (cur != null)
            {
                if (key == cur.Key) return cur.Value;
                cur = key < cur.Key ? cur.Left : cur.Right;
            }
            return null;
        }

        public IEnumerable<ServiceRequest> InOrder()
        {
            var list = new List<ServiceRequest>();
            InOrderInternal(Root, list);
            return list;
        }

        private void InOrderInternal(AvlNode node, List<ServiceRequest> outList)
        {
            if (node == null) return;
            InOrderInternal(node.Left, outList);
            outList.Add(node.Value);
            InOrderInternal(node.Right, outList);
        }

        // For debugging: pretty-print the tree
        public string PrettyPrint() => Pretty(Root, "", true);

        private string Pretty(AvlNode node, string indent, bool last)
        {
            if (node == null) return "";
            var s = indent;
            if (!string.IsNullOrEmpty(indent)) s += last ? "└─ " : "├─ ";
            s += $"{node.Key} (Priority:{node.Value.Priority}, Status:{node.Value.Status})\n";
            indent += last ? "   " : "│  ";
            s += Pretty(node.Left, indent, false);
            s += Pretty(node.Right, indent, true);
            return s;
        }
    }
}