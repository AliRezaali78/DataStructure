using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Data_Structure_2
{
    public class Trie
    {
        public TrieNode Root { get; private set; } = new TrieNode();
        
        public void Insert(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return;

            var current = Root;
            for (var i = 0; i < word.ToUpper().Length; i++)
            {
                var character = word.ToUpper()[i];
                if (!current.Exists(character))
                    current.CreateChild(word[i]);

                current = current.GetChild(character);
            }

            current.IsEndOfTheWord = true;
        }

        public bool Contains(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            word = word.ToUpper();

            var current = Root;
            foreach (var character in word)
            {
                if (!current.Exists(character))
                    return false;

                current = current.GetChild(character);
            }

            return current.IsEndOfTheWord;
        }
        public bool ContainsRecursive(string word)
        {
            word = word.ToUpper();
            return !string.IsNullOrWhiteSpace(word) && ContainsRecursive(Root, word);
        }
        private bool ContainsRecursive(TrieNode current, string word, int index = 0)
        {
            if (word.Length == index)
                return current.IsEndOfTheWord;

            if (current == null || !current.Exists(word[index]))
                return false;

            return ContainsRecursive(current.GetChild(word[index]),word, index + 1);
        }

        public void Remove(string word)
        {
            word = word.ToUpper();
            if (!Contains(word)) 
                return;

            Remove(Root, word);
        }
        private void Remove(TrieNode root, string word, int index = 0)
        {
            if (index == word.Length)
            {
                root.IsEndOfTheWord = false;
                return;
            }

            var child = root.GetChild(word[index]);
            if (child == null) return;

            Remove(child, word, index + 1);

            if (!child.HasChildren() && !child.IsEndOfTheWord)
                root.RemoveChild(word[index]);
        }

        public string[] LookUp(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return new string[0];

            var current = FindNodeForPrefix(word);
            if (current == null) return new string[0];

            var list = new List<string>();

            LookUp(current, word, list);

            return list.ToArray();
        }
        public void LookUp(TrieNode current, string word, List<string> wordList)
        {
            if (current == null) return;

            if(current.IsEndOfTheWord)
                wordList.Add(word);

            foreach (var child in current.Children)
                LookUp(child, word + child.Value, wordList);
        }
        private TrieNode FindNodeForPrefix(string word)
        {
            var current = Root;
            foreach (var character in word.ToUpper())
            {
                current = current.GetChild(character);
                if (current == null)
                    return null;
            }
            return current;
        }

        public int CountWords()
        {
            return CountWords(Root);
        }
        private int CountWords(TrieNode current)
        {
            if (current == null || !current.HasChildren())
                return 0;

            var count = 0;
            foreach (var child in current.Children)
                 count += CountWords(child) + 1;

            return count;
        }

        public class TrieNode
        {
            public char Value { get; set; }
            private readonly Dictionary<char, TrieNode> _children; 
            public TrieNode[] Children => _children.Values.ToArray();

            public bool IsEndOfTheWord { get; set; }

            public TrieNode(char value=default)
            {
                Value = value;
                _children = new Dictionary<char, TrieNode>();
            }

            public bool Exists(in char character)
            {
                return _children.ContainsKey(character);
            }

            public TrieNode CreateChild(in char character)
            {
                var node = new TrieNode(character);
                _children.Add(character.ToString().ToUpper()[0], node);
                return node;
            }

            public TrieNode GetChild(in char character)
            {
                return _children.GetValueOrDefault(character);
            }

            public bool HasChildren()
            {
                return _children.Count != 0;
            }

            public void RemoveChild(char character)
            {;
                _children.Remove(character);
            }
        }
    }
}
