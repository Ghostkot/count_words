using LemmaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace count_words
{
    static class StringHelper
    {

        static public List<Word> EqualWords(string[] str)
        {
            ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.Russian);
            var words = (from s in str
                         where s.Length > 2
                         group s by lmtz.Lemmatize(s.ToLower()) into d
                         let count = d.Count()
                         select new
                         {
                             num = count,
                             word = d.GroupBy(p => p.ToLower()).Where(p => count > 1),
                             num1 = d.GroupBy(p => p.ToLower()).Where(p => count > 1).Count()
                         }).OrderByDescending(p => p.num);
            List<Word> wordsList = new List<Word>();
            foreach (var y in words)
            {
                foreach (var s in y.word) { wordsList.Add(new Word(s.Key, y.num, y.num1)); }
            }
            return wordsList;


        }

        static public List<int> GetIndexForKeyWord(string content, List<string> key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            List<int> indexForKeyWord = new List<int>();


            var results = key.Select(x =>
                 new
                 {
                     word = x,
                     indexes = Regex.Matches(content, @"\b" + x + @"\b", RegexOptions.IgnoreCase)
                       .Cast<Match>().Select(y => y.Index)
                       .ToList()
                 });
            foreach (var match in results)
            {
                foreach (int index in match.indexes)
                {
                    indexForKeyWord.Add(index);
                }
            }
            indexForKeyWord.Sort();
            for (var i = indexForKeyWord.Count - 1; i > 0; i--)
            {
                if (indexForKeyWord[i] - indexForKeyWord[i - 1] > 2000)
                {
                    if (i != 1) indexForKeyWord.Remove(indexForKeyWord[i]);
                    else
                    {
                        indexForKeyWord.Remove(indexForKeyWord[1]);
                        indexForKeyWord.Remove(indexForKeyWord[0]);
                    }
                }
            }
            return indexForKeyWord;
        }
        static public int UniqWords(string[] str)
        {
            var words = (from s in str
                         group s by s.ToLower() into d
                         select d);
            return words.Count();
        }


    }

}
