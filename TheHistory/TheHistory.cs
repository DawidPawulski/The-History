using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// START THE ASSIGNMENT BY READING/UNDERSTANDING THIS FILE!
///
/// If we want to implement functionalities which share common features 
/// with different implementations we can use an abstract base class.
/// If a concrete (not abstract) class inherits from an abstract class it 
/// should implement all it's methods.
///
/// Why is it good? Because we can use polymorphism to access the 
/// class' instance through any of the classes in the inheritance hierarchy. 
/// That's what we are using in the TestTheHistory.cs to avoid duplicate 
/// test case implementations.
/// </summary>

namespace TheHistory
{
    abstract class TheHistory
    {
        /// <summary>
        /// Splits the incoming text to words and adds the words to the container of the
        /// implementing class
        /// </summary>
        /// <param name="text">a string containing words separated with spaces</param>
        public abstract void Add(string text);

        /// <summary>
        /// Removes all occurrences of a word from the stored data
        /// </summary>
        /// <param name="wordToBeRemoved">only one word. No spaces just the word 
        /// otherwise it won't remove anything</param>
        public abstract void RemoveWord(string wordToBeRemoved);

        /// <summary>
        /// Returns the number of words in the stored text
        /// </summary>
        /// <returns>the number of words the stored text contains</returns>
        public abstract int Size();

        /// <summary>
        /// Empties the stored text
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Replaces all occurrences of a word with another word.
        /// NOTE: Replace() method uses this method!
        /// </summary>
        /// <param name="from">all occurrences of this word will be replaced</param>
        /// <param name="to">all occurrences of 'from' will be replaced with this word</param>
        protected abstract void ReplaceOneWord(string from, string to);

        /// <summary>
        /// Replaces all occurrences of a sentence or part of a sentence with
        /// another(part of a) sentence.
        /// The order of words are important.Also the 'fromWords' and 'toWords' arrays
        /// are not necessarily same sized.
        /// NOTE: Replace() method uses this method!
        /// </summary>
        /// <param name="fromWords">array of words what should be replaced</param>
        /// <param name="toWords">array of words which should replace the words of "fromWords"</param>
        protected abstract void ReplaceMoreWords(string[] fromWords, string[] toWords);

        /// <summary>
        /// Replaces all occurrences of sentences or words with sentences or words.
        /// The tests are using this method instead of ReplaceOneWord() or ReplaceMoreWords().
        /// </summary>
        /// <param name="from">the sentence or word what needs to be replaced</param>
        /// <param name="to">the sentence or word which replaces the sentence found in "from"</param>
        public void Replace(string from, string to)
        {
            string[] fromWords = Regex.Split(from, "\\s+");
            string[] toWords = Regex.Split(to, "\\s+");
            if (fromWords.Length == 1 && toWords.Length == 1)
            {
                ReplaceOneWord(from, to);
            }
            else
            {
                ReplaceMoreWords(fromWords, toWords);
            }
        }
    }
}
