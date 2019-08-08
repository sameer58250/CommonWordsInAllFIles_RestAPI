using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonWordsInAllFilesAPI.Services
{
    public class WordHelper
    {
        public string ExtractCommonWords(string[] filePaths)
        {
            StringBuilder sb = new StringBuilder();
            Trie root = new Trie();
            Regex reg = new Regex("^[a-zA-Z0-9]+$");
            for (int i = 0; i < filePaths.Length; i++)
            {
                string path = filePaths[i];
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var text = line.Split(' ');
                        for (int j = 0; j < text.Length; j++)
                        {
                            if (reg.IsMatch(text[i]))
                            {
                                if (i == filePaths.Length - 1)
                                {
                                    string common = InsertWord(root, text[j], i);
                                    if (!string.IsNullOrEmpty(common))
                                    {
                                        sb.Append(common + " ");
                                    }
                                }
                                else
                                {
                                    InsertWord(root, text[j], i);
                                }
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
        private string InsertWord(Trie root, string word,int fileIndex)
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