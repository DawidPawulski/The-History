using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// READ THIS AFTER TheHistory.cs!
///
///
/// DO NOT CHANGE THE TESTS (please)!
/// You may put extra logs to be able to differentiate better which test case fails and/or why.
/// but make sure you restore everything and run all tests in their 
/// original form when you are finished.
///
///
/// Unit Testing in general:
/// You'll learn about unit testing later but some small info:
/// - you can't test every possibility so test some good and bad cases
/// But how to decide what to test?
/// - always test with good values but 1-2 test usually are enough
/// - always test the _corner cases_
/// What are the corner cases?
/// Those are the special cases:
/// - The first and last good input (for ex. if you handle a certain set of numbers as input)
/// - The 'one before the first good' and 'one after the last good' cases
///
/// Of course that's not all but a glimpse from the world of testing. Now check out the tests
/// and the notes!
/// 
/// NOTE: this class doesn't use any official Unit Test framework but made by hand.
/// Later when the Unit testing gets introduced we'll use more sofisticated testing.
/// Challenge: if you find any functionality what these tests are not covering, tell us!
/// </summary>
namespace TheHistory
{
    class TestTheHistory
    {
        /// <summary>
        /// Helper method for file reading
        /// </summary>
        /// <param name="filename">name of the with path</param>
        /// <returns>the file's content in one big string</returns>
        private string ReadFromFile(string filename)
        {
            string result;
            using (StreamReader reader = File.OpenText(filename))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Helper method for running a functionality test on TheHistory. 
        /// Note we are using TheHistory abstract class so we can
        /// use this method with _any_ TheHistory implementation!
        /// </summary>
        /// <param name="theHistory">the abstract class we are using to run the test</param>
        /// <param name="sourceText">the text what TheHistory will use to run the test on</param>
        /// <param name="fromWords">these are the words we are looking for in the "sourceText"</param>
        /// <param name="toWords">we would like to change the text found in the "fromWords" to this text</param>
        /// <returns>returns the modified text</returns>
        private string RunFunctionalityTest(TheHistory theHistory, string sourceText,
                                            string fromWords, string toWords)
        {
            theHistory.Add(sourceText);
            theHistory.Replace(fromWords, toWords);
            string result = theHistory.ToString();
            theHistory.Clear();
            return result;
        }

        /// <summary>
        /// All the functionality-related tests are here. These are not checking performance
        /// but if your implementation works correctly. Notice we are using TheHistory 
        /// abstract class here also to be able to use all the implementation instead of writing
        /// the same tests for all the different implementations(like for List, LinkedList etc..).
        /// NOTE: later when you are doing Unit testing don't make long methods like this but separate
        /// every test into a different method!
        /// </summary>
        /// <param name="theHistory">the abstract class we are using for the testing.</param>
        public void RunAllFunctionalityTests(TheHistory theHistory)
        {
            string sourceText = "replace replace me replace me me me replace me me";
            string result;

            // All the tests using the same pattern: there is a source text, some text we 
            // want to change and some other text we would like to change to.
            // And compare the result, to the expected output.

            // just change words
            result = RunFunctionalityTest(theHistory, sourceText, "replace me", "HAPPY FUN");
            if (!"replace HAPPY FUN HAPPY FUN me me HAPPY FUN me".Equals(result))
            {
                Console.WriteLine("replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, "replace", "REPLACE");
            if (!"REPLACE REPLACE me REPLACE me me me REPLACE me me".Equals(result))
            {
                Console.WriteLine("Test 1: replace() IS NOT WORKING AS EXPECTED!");
            }

            // replace the whole text
            result = RunFunctionalityTest(theHistory, sourceText, sourceText, sourceText);
            if (!sourceText.Equals(result))
            {
                Console.WriteLine("Test 2: replace() IS NOT WORKING AS EXPECTED!");
            }

            // insert new words into the text
            result = RunFunctionalityTest(theHistory, sourceText, "me", "HAPPY FUN");
            if (!"replace replace HAPPY FUN replace HAPPY FUN HAPPY FUN HAPPY FUN replace HAPPY FUN HAPPY FUN".Equals(result))
            {
                Console.WriteLine("Test 3: replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, "me me", "SUPER HAPPY FUN");
            if (!"replace replace me replace SUPER HAPPY FUN me replace SUPER HAPPY FUN".Equals(result))
            {
                Console.WriteLine("Test 4: replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, "me", "SUPER me FUN");
            if (!"replace replace SUPER me FUN replace SUPER me FUN SUPER me FUN SUPER me FUN replace SUPER me FUN SUPER me FUN"
                    .Equals(result))
            {
                Console.WriteLine("Test 5: replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, "me replace me", "AWE SUPER HAPPY FUN");
            if (!"replace replace AWE SUPER HAPPY FUN me AWE SUPER HAPPY FUN me".Equals(result))
            {
                Console.WriteLine("Test 6: replace() IS NOT WORKING AS EXPECTED!");
            }

            // remove words from the text
            result = RunFunctionalityTest(theHistory, sourceText, "me me me", "REPLACE");
            if (!"replace replace me replace REPLACE replace me me".Equals(result))
            {
                Console.WriteLine("Test 7: replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, "replace replace", "REPLACE");
            if (!"REPLACE me replace me me me replace me me".Equals(result))
            {
                Console.WriteLine("Test 8: replace() IS NOT WORKING AS EXPECTED!");
            }

            result = RunFunctionalityTest(theHistory, sourceText, sourceText, "REPLACE");
            if (!"REPLACE".Equals(result))
            {
                Console.WriteLine("Test 9: replace() IS NOT WORKING AS EXPECTED!");
            }

            // no match -> nothing changed
            result = RunFunctionalityTest(theHistory, sourceText, "cant find", "cant change");
            if (!sourceText.Equals(result))
            {
                Console.WriteLine("Test 10: replace() IS NOT WORKING AS EXPECTED!");
            }
        }

        /// <summary>
        /// Performance testing. After all the functionality test passes, the next thing to 
        /// take care of is the performance. Can you make your code faster? 
        /// And no, you shouldn't change the tests.
        /// </summary>
        /// <param name="theHistory">theHistory abstract class to TheHistory implementation</param>
        public void RunAllPerformanceTests(TheHistory theHistory)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Restart();
            theHistory.Add(ReadFromFile("Resources/Iliad.txt"));
            stopWatch.Stop();
            Console.WriteLine("Reading words from file took " + stopWatch.ElapsedMilliseconds + " ms (the size is now " + theHistory.Size() + ")");

            stopWatch.Restart();
            theHistory.RemoveWord("king");
            theHistory.RemoveWord("Zeus");
            theHistory.RemoveWord("Apollo");
            theHistory.RemoveWord("it");
            stopWatch.Stop();
            Console.WriteLine("Removing words took " + stopWatch.ElapsedMilliseconds + " ms");

            stopWatch.Restart();
            theHistory.Replace("Achilles", "Il");
            theHistory.Replace("Agamemnon", "Il");
            theHistory.Replace("Priam", "Trumm");
            theHistory.Replace("chariot", "tank");
            theHistory.Replace("bow", "missile");
            theHistory.Replace("arrow", "nuke");
            theHistory.Replace("the", "the");
            stopWatch.Stop();
            Console.WriteLine("Replacing words took " + stopWatch.ElapsedMilliseconds + " ms");

            stopWatch.Restart();
            theHistory.Replace("Il", "Pet Il");
            theHistory.Replace("Pet Il", "Pet Il (blessed be his name)");
            theHistory.Replace("Trumm", "coward and insane Trumm");
            theHistory.Replace("the", "the big");
            theHistory.Replace("the big", "the very big");
            theHistory.Replace("a", "a big");
            theHistory.Replace("a big", "a very big");
            stopWatch.Stop();
            Console.WriteLine("Replacing multiple words (with insertion) took " + stopWatch.ElapsedMilliseconds + " ms");

            stopWatch.Restart();
            theHistory.Replace("Pet Il (blessed be his name)", "Pet Il (blessed be the name)");
            theHistory.Replace("coward and insane Trumm", "coward and liar Trumm");
            theHistory.Replace("the very big", "the super big");
            theHistory.Replace("the super big", "the really big");
            theHistory.Replace("a very big", "a super big");
            theHistory.Replace("a super big", "a really big");
            stopWatch.Stop();
            Console.WriteLine("Replacing multiple words (equal number) took " + stopWatch.ElapsedMilliseconds + " ms");

            stopWatch.Restart();
            theHistory.Replace("Pet Un (blessed be the name)", "Pet Un The Wise");
            theHistory.Replace("coward and liar Trumm", "President Trumm");
            theHistory.Replace("the really big", "the big");
            theHistory.Replace("the big", "the");
            theHistory.Replace("a really big", "a big");
            theHistory.Replace("a big", "a");
            stopWatch.Stop();
            Console.WriteLine("Replacing multiple words (with removal) took " + stopWatch.ElapsedMilliseconds + " ms");
        }
    }
}
