using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHistory
{
    class TheHistoryList : TheHistory
    {
        // This implementation should use a string List so don't change that!
        private List<string> WordsList = new List<string>();

        public override void Add(string text)
        {
            List<string> textToAdd = text.Split(' ').ToList();
            WordsList.AddRange(textToAdd);
        }

        public override void Clear()
        {
            WordsList.Clear();
        }

        public override void RemoveWord(string wordToBeRemoved)
        {
//            for (int i = 0; i < WordsList.Count; i++)
//            {
//                if (WordsList[i] == wordToBeRemoved)
//                {
//                    WordsList.Remove(WordsList[i]);
//                }
//            }
            WordsList.RemoveAll((s => s.Equals(wordToBeRemoved)));
        }

        protected override void ReplaceMoreWords(string[] fromWords, string[] toWords)
        {
            int counter;
            for (int k = 0; k < WordsList.Count; k++)
            {
                bool isMatch = true;
                counter = k;
                for (int i = 0; i < fromWords.Length; i++)
                {
                    if (counter >= WordsList.Count)
                    {
                        isMatch = false;
                        break;
                    }
                    else if (WordsList[counter] == fromWords[i])
                    {
                        counter++;
                    }
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    int deleteCounter = k;
                    int inputCounter = k;
                    int lastKnown = 0;

                        for (int j=0; j < fromWords.Length; j++)
                        {
                            WordsList.RemoveAt(deleteCounter);
                        }

                        for (int z = 0; z < toWords.Length; z++)
                        {
                            if (inputCounter > WordsList.Count)
                            {
                                WordsList.Add(toWords[z]);
                            }
                            else
                            {
                                WordsList.Insert(inputCounter, toWords[z]);
                                lastKnown = inputCounter;
                            }
                            
                            inputCounter++;
                            
                        }
                        k = k + toWords.Length-1;
                }
                
            }
            
        }

        protected override void ReplaceOneWord(string from, string to)
        {
            for (int i = 0; i < WordsList.Count; i++)
            {
                if (WordsList[i] == from)
                {
                    WordsList[i] = to;
                }
            }
        }

        public override int Size()
        {
            return WordsList.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in WordsList)
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
