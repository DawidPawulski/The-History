using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHistory
{
    class TheHistoryArray : TheHistory
    {
        // This implementation should use a string array so don't change that!
        private string[] WordsArray = new string[0];

        public override void Add(string text)
        {
            string[] words = text.Split(' ');
            int sourceArrayCount = WordsArray.Length;
            int parArrayLength = words.Length;
            int totalLength = sourceArrayCount + parArrayLength;
            string [] destinationArray = new string[totalLength];
            Array.Copy(WordsArray, destinationArray, sourceArrayCount);
            Array.Copy(words, 0, destinationArray, sourceArrayCount, parArrayLength);
            WordsArray = destinationArray;
        }

        public override void Clear()
        {
            // THIS IS NOT WORKING! IT'S ONLY FOR MY INFO
            //int arrayLength = WordsArray.Length;
            //Array.Clear(WordsArray, 0, arrayLength);
            // THIS IS WORKING!
            WordsArray = new string[0];
        }

        public override void RemoveWord(string wordToBeRemoved)
        {
            for (int i = 0; i < WordsArray.Length; i++)
            {
                if (WordsArray[i] == wordToBeRemoved)
                {
                    WordsArray[i] = null;
                }
            }
            WordsArray = WordsArray.Where(c => c != null).ToArray();
        }

        protected override void ReplaceMoreWords(string[] fromWords, string[] toWords)
        {
            string fromWordsString = string.Join(" ", fromWords);
            string toWordsString = string.Join(" ", toWords);
            string wordsArray = string.Join(" ", WordsArray);
            wordsArray = wordsArray.Replace(fromWordsString, toWordsString);
            Clear();
            Add(wordsArray);
        }

        protected override void ReplaceOneWord(string from, string to)
        {
            //string[] tempArray = WordsArray.Select(x => x.Replace(from, to)).ToArray();
            //WordsArray =  WordsArray.Select(s => s!= from ? s : to).ToArray();
            //WordsArray = tempArray;
            string wordsArray = string.Join(" ", WordsArray);
            wordsArray = wordsArray.Replace(from, to);
            Clear();
            Add(wordsArray);
        }

        public override int Size()
        {
            int arrayLength = WordsArray.Length;
            return arrayLength;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in WordsArray)
            {
                sb.Append(word).Append(" ");
            }

            if (sb.Length > 0)
            {
                // remove last space character
                sb.Length--;
            }
            return sb.ToString();
        }
    }
}
