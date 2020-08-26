using System;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_2
{

    public class AVLTree<T> 
    {
        public class AvlNode
        {
            public AvlNode(int key, T value)
            {
                Value = value;
                Key = key;
            }

            public int Key { get; internal set; }
            public T Value { get; internal set; }
            public AvlNode LeftChild { get; internal set; }
            public AvlNode RightChild { get; internal set; }
            public int Height { get; set; }
        }
        public AvlNode Root { get; private set; }
      
        public void Insert(int key,T value = default)
        {
            Root = Insert(Root,key, value);
        }
        private AvlNode Insert(AvlNode root,int key , T value)
        {
            if (root == null)
                return new AvlNode(key, value);

            if (root.Key == key)
            {
                root.Value = value;
                return root;
            }

            if (key < root.Key)
                root.LeftChild = Insert(root.LeftChild, key, value);
            else
                root.RightChild = Insert(root.RightChild, key, value);

            root.Height = SetHeight(root);

            return Balance(root);
        }
      
        private AvlNode Balance(AvlNode root)
        {
            if (IsLeftHeavy(root))
            {
                if (GetBalanceFactor(root.LeftChild) < 0)
                    root.LeftChild = LeftRotate(root.LeftChild);

                return RightRotate(root);
            }
            else if (IsRightHeavy(root))
            {
                if (GetBalanceFactor(root.RightChild) > 0)
                 root.RightChild = RightRotate(root.RightChild);

                return LeftRotate(root);
            }

            return root;
        }
      
        private AvlNode RightRotate(AvlNode root)
        {
            var newRoot = root.LeftChild;
            root.LeftChild = newRoot.RightChild;
            newRoot.RightChild = root;

            root.Height = SetHeight(root);
            newRoot.Height = SetHeight(newRoot);

            return newRoot;
        }
        private AvlNode LeftRotate(AvlNode root)
        {
            var newRoot = root.RightChild;
            root.RightChild = newRoot.LeftChild;
            newRoot.LeftChild = root;

            root.Height = SetHeight(root);
            newRoot.Height = SetHeight(newRoot);

            return newRoot;
        }

        private bool IsLeftHeavy(AvlNode node)
        {
            return GetBalanceFactor(node) > 1;
        }
        private bool IsRightHeavy(AvlNode node)
        {
            return GetBalanceFactor(node) < -1;
        }
        private int GetBalanceFactor(AvlNode node)
        {
            return node==null ? 0 : Height(node.LeftChild) - Height(node.RightChild);
        }
      
        public int Height()
        {
            return Height(Root);
        }
        private int Height(AvlNode root)
        {
            return root?.Height ?? -1;
        }
        private int SetHeight(AvlNode root)
        {
            return Math.Max(Height(root.LeftChild), Height(root.RightChild)) + 1;
        }
      
        public List<int> GetList()
        {
            var list = new List<int>();
            GetList(Root, list);
            return list;
        }
        private void GetList(AvlNode root, List<int> list)
        {
            if (root == null) return;

            GetList(root.LeftChild, list);
            list.Add(root.Key);
            GetList(root.RightChild, list);
        }

        public bool TreeIsBalanced()
        {
            return IsBalanced(Root);
        }
        private bool IsBalanced(AvlNode root)
        {
            if(root == null) return true;

            if (!(Math.Max(Height(root.LeftChild), Height(root.RightChild)) <= 1))
                return false;

            return IsBalanced(root.LeftChild) & IsBalanced(root.RightChild);
        }

        public bool IsPerfectTree()
        {
            return IsPerfectTree(Root);
        }
        private bool IsPerfectTree(AvlNode root)
        {
            if(root == null)
                return true;
            return
                Height(root.LeftChild) == Height(root.RightChild) &&
                IsPerfectTree(root.LeftChild) &&
                IsPerfectTree(root.RightChild);
        }
    }
}
