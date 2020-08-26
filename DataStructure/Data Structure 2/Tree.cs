using System;
using System.Collections.Generic;

namespace DataStructure.Data_Structure_2
{

    public class TreeNode<T> 
    {
        public TreeNode<T> LeftChild { get; set; }
        public TreeNode<T> RightChild { get; set; }
        public T Value { get; set; }
        public int Key { get; set; }
        public TreeNode(int key, T value)
        {
            Key = key;
            Value = value;
        }
    }

    public class Tree<T> 
    {
        public TreeNode<T> Root { get;private set; }
        public int Size { get; private set; }

        public void Insert(int key, T value)
        {
            var node = new TreeNode<T>(key, value);
            if (Root == null)
            {
                Root = node;
                Size++;

                return;
            }

            var result = SearchByKey(key);
            if (result.Current == null)
            {
                if (key < result.Root.Key)
                    result.Root.LeftChild = node;
                else
                    result.Root.RightChild = node;

                Size++;
                return;
            }

            result.Current.Value = value;
        }

        public void Remove(int key)
        {
            if(IsEmpty()) throw new TreeIsEmptyException();

            var result = SearchByKey(key);
            if(result.Current == null) throw new NullReferenceException("Element does not exist in tree");
            Size--;
            if (result.Current == Root)
            {
                RemoveRoot();
                return;
            }

            var direction = DetermineTheDirection(result);

            if (result.Current.LeftChild != null)
            {
                if (direction == NodeDirection.Right)
                     result.Root.RightChild = result.Current.LeftChild;
                else result.Root.LeftChild = result.Current.LeftChild;

                if (result.Current.RightChild == null) return;
                var deepRightNode = NavigateToDeepRight(result.Current.LeftChild);
                deepRightNode.RightChild = result.Current.RightChild;
                return;
            }
            
            if (direction == NodeDirection.Right)
                result.Root.RightChild = result.Current.RightChild;
            else result.Root.LeftChild = result.Current.RightChild;

        }

        public T Find(int key)
        {
            return SearchByKey(key).Current.Value;
        }

        public Dictionary<int, T> Traverse(ITreeSearchEngine<T> searchEngine)
        {
            return searchEngine.Search(Root);
        }

        public SearchNodeResult<T> SearchByKey(int key)
        {
            if(IsEmpty()) throw new TreeIsEmptyException();

            var current = Root;
            var rootOfCurrent = Root;
            while (current != null)
            {
                if (current.Key == key)
                    return new SearchNodeResult<T>(){Current = current, Root = rootOfCurrent };

                if (key < current.Key)
                {
                    rootOfCurrent = current;
                    current = current.LeftChild;
                    continue;
                }

                rootOfCurrent = current;
                current = current.RightChild;
            }

            return new SearchNodeResult<T>(){Current = null, Root = rootOfCurrent};
        }

        public int Min(TreeNode<T> root)
        {
            if (root.LeftChild == null && root.RightChild == null) return root.Key;

            if (root.LeftChild == null)
                return root.RightChild.Key;
            
            if(root.RightChild == null)
                return root.LeftChild.Key;

            var left = Min(root.LeftChild);
            var right = Min(root.RightChild);
            return Math.Min(Math.Min(left, right), root.Key);
        }

        public bool IsBinarySearchTree()
        {
            return IsBinarySearchTree(Root, int.MinValue, int.MaxValue);
        }

        private bool IsBinarySearchTree(TreeNode<T> root, int min, int max)
        {
            if (root == null)
                return true;

            if (!(min < root.Key || root.Key < max))
                return false;

            return IsBinarySearchTree(root.LeftChild, min, root.Key )
                   && IsBinarySearchTree(root.RightChild, root.Key, max);
        }

        public void FindNodesAtKDistance(int kth)
        {
            FindNodesAtKDistance(Root, kth);
        }
        private void FindNodesAtKDistance(TreeNode<T> root , int kth)
        {
            if (root == null) return;


            if (kth == 0)
            {
                Console.WriteLine(root.Key);
                return;
            }

            FindNodesAtKDistance(root.LeftChild, kth - 1);
            FindNodesAtKDistance(root.RightChild, kth - 1);
        }

        public int Height()
        {
            return Height(Root);
        }

        private int Height(TreeNode<T> node)
        {
            if (node == null) return -1;

            return 1 + Math.Max(
                Height(node.LeftChild),
                Height(node.RightChild));
        }

        public bool Equals(Tree<T> tree)
        {
            return NodeEqual(Root, tree.Root);
        }

        private bool NodeEqual(TreeNode<T> first, TreeNode<T> second)
        {
            if (first == null && second == null) return true;

            if(first != null && second != null) 
                return first.Key == second.Key &&
                       NodeEqual(first.LeftChild, second.LeftChild) 
                       && NodeEqual(first.RightChild, second.RightChild);
            return false;

        }

        private static TreeNode<T> NavigateToDeepRight(TreeNode<T> node)
        {
            var current = node;
            while (current.RightChild != null)
            {
                current = current.RightChild;
            }

            return current;
        }

        private static int DetermineTheDirection(SearchNodeResult<T> result)
        {
            return result.Root.RightChild == result.Current ? NodeDirection.Right : NodeDirection.Left;
        }

        private void RemoveRoot()
        {
            if (Root.LeftChild != null)
            {
                var deepRightNode = NavigateToDeepRight(Root.LeftChild);
                deepRightNode.RightChild = Root.RightChild;
                Root = Root.LeftChild;
                return;
            }

            Root = Root.RightChild;
        }

        private bool IsEmpty()
        {
            return Size==0;
        }

        public int CalculateSize()
        {
            return CalculateSize(Root);
        }
        private int CalculateSize(TreeNode<T> root)
        {
            if (root == null) return 0;
            return CalculateSize(root.LeftChild) + CalculateSize(root.RightChild) + 1;

        }

        public int CountLeaves()
        {
            return CountLeaves(Root);
        }
        private int CountLeaves(TreeNode<T> root)
        {
            if (root == null) return 0;
            if (root.LeftChild == null && root.RightChild == null) return 1;

            return CountLeaves(root.LeftChild) + CountLeaves(root.RightChild);
        }

        public int MaxValue()
        {
            return MaxValue(Root);
        }
        private int MaxValue(TreeNode<T> root)
        {
            return root == null ? 0 : Math.Max(Math.Max(MaxValue(root.LeftChild), MaxValue(root.RightChild)), root.Key);
        }
     
        public int MaxValueBinary()
        {
            return MaxValueBinary(Root);
        }
        private int MaxValueBinary(TreeNode<T> root)
        {
            if (root.RightChild == null) return root.Key;

            return MaxValueBinary(root.RightChild);
        }

        public T FindRecursion(int key)
        {
            return FindRecursion(Root, key);
        }
        private T FindRecursion(TreeNode<T> root, int key)
        {
            if (root.Key == key) return root.Value;
            if (key < root.Key) return FindRecursion(root.LeftChild, key);
            if (root.Key < key) return FindRecursion(root.RightChild, key);

            return default;
        }

        public void PrintAncestors(int key)
        {
            var list = new List<T>();
            GetAncestors(Root, key, list);
            foreach (var t in list)
            {
                Console.WriteLine(t);
            }
        }
        private bool GetAncestors(TreeNode<T> root, int key, List<T> list)
        {
            if (root == null) return false;
            if (root.Key == key) return true;

            if (!GetAncestors(root.Key < key ? root.RightChild : root.LeftChild, key, list)) return false;

            list.Add(root.Value);
            return true;

        }

        private class TreeIsEmptyException : Exception
        {
            public TreeIsEmptyException(string exception ="Tree is empty")
            : base(exception)
            {}
        }

        private static class NodeDirection
        {
            public const int Left = 1;
            public const int Right = 2;
        }
    }

    public class SearchNodeResult<T>
    {
        public TreeNode<T> Root { get; set; }
        public TreeNode<T> Current { get; set; }
    }
}
