using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LemmaSharp;

namespace count_words
{
    class Word : ICloneable
    {
        string words;
        int countNotLem;
        int count;
        List<int> wordPos;
        public Word(string word, List<int> wordPos, int count, int countNotLem)
        {
            this.Words = word;
            this.WordPos = wordPos;
            this.Count = count;
            this.CountNotLem = countNotLem;
        }

        public Word(string word, List<int> wordPos, int count)
        {
            this.Words = word;
            this.WordPos = wordPos;
            this.Count = count;
        }

        public Word(string word, int count, int countNotLem)
        {
            this.Words = word;
            this.Count = count;
            this.CountNotLem = countNotLem;
        }
        public Word(string word)
        {
            this.Words = word;
        }

        public string Words { get => words; set => words = value; }
        public List<int> WordPos { get => wordPos; set => wordPos = value; }
        public int Count { get => count; set => count = value; }
        public int CountNotLem { get => countNotLem; set => countNotLem = value; }

        public bool Equals(Word word)
        {
            bool f = false;
            if (word.Words.ToLower() == this.Words.ToLower())
            {
                f = true;
            }
            return f;
        }

        public string GetLemWord()
        {
            ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.Russian);
            return lmtz.Lemmatize(this.Words.ToLower());
        }

        public object Clone()
        {
            var words = this.Words;
            var wordPos = this.WordPos;
            var count = this.Count;
            var countNotLem = this.CountNotLem;
            Word word = new Word(words, wordPos, count, countNotLem);
            return word;
        }
    }
}
