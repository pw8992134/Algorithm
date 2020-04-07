using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Trie
{
    /// <summary>
    /// 字典树(前缀树)
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// 字典树(前缀树)节点
        /// </summary>
        private class Node
        {
            /// <summary>
            /// 字符的下个字符集合
            /// </summary>
            public readonly Dictionary<char, Node> Nexts;

            /// <summary>
            /// 当前字符的下个字符集合大小
            /// </summary>
            public int Size => Nexts.Count;

            /// <summary>
            /// 当前节点是否为一个单词
            /// </summary>
            public bool IsWord { get; set; }

            /// <summary>
            /// 构造函数
            /// </summary>
            public Node()
            {
                Nexts=new Dictionary<char, Node>();
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        private readonly Node _root;

        /// <summary>
        /// 保存的单词数目
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Trie()
        {
            _root=new Node();
            Size = 0;
        }

        /// <summary>
        /// 向Trie树中添加一个单词
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Node cur = _root;
            foreach (char c in word)
            {
                if(!cur.Nexts.ContainsKey(c))
                    cur.Nexts.Add(c,new Node());
                cur = cur.Nexts[c];
            }

            if (!cur.IsWord)
            {
                cur.IsWord = true;
                Size++;
            }
        }

        /// <summary>
        /// 是否包含某个单词
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Contains(string word)
        {
            Node cur = _root;
            foreach (var c in word)
            {
                if (!cur.Nexts.ContainsKey(c)) return false;
                cur = cur.Nexts[c];
            }
            return cur.IsWord;
        }

        /// <summary>
        /// 前缀查询(确认是否包含某个前缀的单词)
        /// </summary>
        /// <returns></returns>
        public bool StartsWith(string prefix)
        {
            Node cur = _root;
            foreach (var c in prefix)
            {
                if (!cur.Nexts.ContainsKey(c)) return false;
                cur = cur.Nexts[c];
            }
            return true;
        }

        /// <summary>
        /// 模式匹配(是否存在符合模式的单词,.代表任意字符)
        /// </summary>
        /// <returns></returns>
        public bool PatternMatch(string word)
        {
            return Match(_root,word,0);
        }

        /// <summary>
        /// 模式匹配(是否存在符合模式的单词,.代表任意字符)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool Match(Node node,string word,int index)
        {
            if (index == word.Length) return node.IsWord;

            char c = word.ElementAt(index);
            if (c.Equals('.'))
            {
                foreach (var kv in node.Nexts)
                {
                    if (Match(kv.Value, word, index+1)) return true;
                }

                return false;
            }
            else
            {
                if (!node.Nexts.ContainsKey(c)) return false;
                return Match(node.Nexts[c], word, index + 1);
            }
        }
    }
}