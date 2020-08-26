using System.Collections.Generic;

namespace DataStructure.Data_Structure_2
{
    public interface ITreeSearchEngine<T>
    {
        Dictionary<int,T> Search(TreeNode<T> root);
    }

    class PreOrderSearch<T> : ITreeSearchEngine<T>
    {
        public Dictionary<int,T> Search(TreeNode<T> root)
        {
            if (root == null) return new Dictionary<int, T>();

            var list = new Dictionary<int, T> {{root.Key, root.Value}};

            foreach (var item in Search(root.LeftChild))
                list.Add(item.Key, item.Value);

            foreach (var item in Search(root.RightChild))
                list.Add(item.Key, item.Value);

            return list;
        }
    }

    class InOrderSearch<T> : ITreeSearchEngine<T>
    {
        public Dictionary<int,T> Search(TreeNode<T> root)
        {
            if (root == null) return new Dictionary<int, T>();

            var list = new Dictionary<int, T>();

            foreach (var item in Search(root.LeftChild))
                list.Add(item.Key, item.Value);
            
            list.Add(root.Key, root.Value);

            foreach (var item in Search(root.RightChild))
                list.Add(item.Key, item.Value);

            return list;
        }
    }
  
    class PostOrderSearch<T> : ITreeSearchEngine<T>
    {
        public Dictionary<int,T> Search(TreeNode<T> root)
        {
            if (root == null) return new Dictionary<int, T>();

            var list = new Dictionary<int, T>();

            foreach (var item in Search(root.LeftChild))
                list.Add(item.Key, item.Value);

            foreach (var item in Search(root.RightChild))
                list.Add(item.Key, item.Value);

            list.Add(root.Key, root.Value);

            return list;
        }
    }

}
