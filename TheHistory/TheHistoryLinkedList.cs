using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl.Runtime;

namespace TheHistory
{
    class TheHistoryLinkedList : TheHistory
    {
        // This implementation should use a string LinkedList so don't change that!
        private LinkedList<string> WordsLinkedList = new LinkedList<string>();

        public override void Add(string text)
        {
            //var splitText = new LinkedList<string>(text.Split(','));
            string[] words = text.Split(' ');
            foreach (string word in words)
            {
                WordsLinkedList.AddLast(word);
            }
        }

        public override void Clear()
        {
            WordsLinkedList.Clear();
        }

        public override void RemoveWord(string wordToBeRemoved)
        {
            var currentNode = WordsLinkedList.First;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                if (currentNode.Value == wordToBeRemoved)
                {
                    WordsLinkedList.Remove(currentNode);
                }
                currentNode = nextNode;
            }
        }

        protected override void ReplaceMoreWords(string[] fromWords, string[] toWords)
        {
            var currentNode = WordsLinkedList.First;
            int fromWordLength = fromWords.Length;
            int toWordLength = toWords.Length;
            var lastElement = currentNode.Next;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                
                if (currentNode.Value == fromWords[0])
                {
                    var insideLoopNode = currentNode;
                    var insideLoopNextNode = currentNode.Next;
                    for (int i=0; i < fromWordLength; i++)
                    {
                        if (insideLoopNode != null && insideLoopNode.Value == fromWords[i])
                        {
                            insideLoopNextNode = insideLoopNode.Next;
                            insideLoopNode = insideLoopNextNode;
                            
                        }
                        else
                        {
                            goto NoMatch;
                        }
                        
                        lastElement = insideLoopNextNode;
                    }
                    
                    var before = currentNode;
                    insideLoopNode = currentNode;
                    if(fromWordLength <= toWordLength)
                    {
                        for (int i = 0; i < toWords.Length; i++)
                        {
                            if (i >= fromWordLength)
                            {
                                before = WordsLinkedList.AddAfter(before, toWords[i]);
                                
                            }
                            else if (insideLoopNode != null && insideLoopNode != lastElement && insideLoopNode.Next != null)
                            {
                                insideLoopNode.Value = toWords[i];
                                before = insideLoopNode;
                            }
                            else if (insideLoopNode == lastElement)
                            {
                                //before = WordsLinkedList.AddAfter(before, toWords[i]);
                                insideLoopNode.Value = toWords[i];
                                before = insideLoopNode;
                            }
                            else if (insideLoopNode.Next == null)
                            {
                                insideLoopNode.Value = toWords[i];
                                for (int j = i+1; j < toWords.Length; j++)
                                {
                                    WordsLinkedList.AddLast(toWords[j]);
                                }

                                i = toWords.Length - 1;

                            }

                            if(insideLoopNode.Next != null){insideLoopNode = insideLoopNode.Next;}
                            
                        }
                    }
                    
                    
                    
                    else if (toWordLength < fromWordLength)
                    {
                        var changeLoop = currentNode;
                        var changeLoopNext = changeLoop.Next;
                        int toWordCounter = 0;
                        for (int i=0; i < fromWordLength; i++)
                        {
                            
                            if (changeLoop != null && toWordCounter < toWordLength)
                            {
                                changeLoopNext = changeLoop.Next;
                                changeLoop.Value = toWords[i];
                                
                            }
                            else
                            {
                                changeLoopNext = changeLoop.Next;
                                WordsLinkedList.Remove(changeLoop);
                            }
                            changeLoop = changeLoopNext;
                            toWordCounter++;
                            lastElement = changeLoopNext;
                        }
                    }
                    
                    
                    
                }
                NoMatch:
                currentNode = nextNode;
            }
        }

        protected override void ReplaceOneWord(string from, string to)
        {
            var currentNode = WordsLinkedList.First;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                if (currentNode.Value == from)
                {
                    currentNode.Value = to;
                }

                currentNode = nextNode;
            }
        }

        public override int Size()
        {
            return WordsLinkedList.Count();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in WordsLinkedList)
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
