using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWordsInAllFilesAPI.Services
{
    public class WordHelper
    {
        public string InsertWord(Trie root, string word,int fileIndex)
        {
            if (string.IsNullOrEmpty(word))
            {
                return null;
            }
            Trie temp = root;
            word = word.ToLower();
            for(int i = 0; i < word.Length; i++)
            {
                int idx = CalcIndex(word[i]);
                if (temp.Children[idx] != null)
                {
                    temp = temp.Children[idx];
                }
                else
                {
                    temp.Children[idx] = new Trie();
                    temp = temp.Children[idx];
                }
            }
            if (temp.fileIdx == fileIndex - 1)
            {
                temp.fileIdx = fileIndex;
                return word;
            }
            return null;
        }
        private int CalcIndex(char c)
        {
            if(c>='a' && c <= 'z')
            {
                return c - 'a'+10;
            }
            else
            {
                return c - '0';
            }
        }
    }

    public class Trie
    {
        public Trie[] Children;
        public int fileIdx;
        public Trie()
        {
            fileIdx = -1;
            Children = new Trie[36];
        }
    }
}