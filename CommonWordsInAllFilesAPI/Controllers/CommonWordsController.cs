using CommonWordsInAllFilesAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace CommonWordsInAllFilesAPI.Controllers
{
    public class CommonWordsController : ApiController
    {
        [Route("GetCommonWords")]
        [HttpPost]
        public string GetCommonWords([FromBody]string[] filePaths)
        {
            return ExtractCommonWords(filePaths);
        }

        private string ExtractCommonWords(string[] filePaths)
        {
            StringBuilder sb = new StringBuilder();
            Trie root = new Trie();
            WordHelper w = new WordHelper();
            Regex reg = new Regex("^[a-zA-Z0-9]+$");
            for(int i=0;i<filePaths.Length;i++)
            {
                string path = filePaths[i];
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var text = line.Split(' ');
                        for(int j = 0; j < text.Length; j++)
                        {
                            if (reg.IsMatch(text[i])) {
                                if (i == filePaths.Length - 1)
                                {
                                    string common = w.InsertWord(root, text[j], i);
                                    if (!string.IsNullOrEmpty(common))
                                    {
                                        sb.Append(common + " ");
                                    }
                                }
                                else
                                {
                                    w.InsertWord(root, text[j], i);
                                }
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}