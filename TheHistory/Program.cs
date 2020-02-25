using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHistory
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTheHistory test = new TestTheHistory();
            // Functionality tests            
            Console.WriteLine("****** Functionality Tests - Array *******");
            test.RunAllFunctionalityTests(new TheHistoryArray());
            Console.WriteLine("****** Functionality Tests - List *******");
            test.RunAllFunctionalityTests(new TheHistoryList());
            Console.WriteLine("****** Functionality Tests - LinkedList *******");
            test.RunAllFunctionalityTests(new TheHistoryLinkedList());

            // Performance tests
            Console.WriteLine("****** Array Tests *******");
            test.RunAllPerformanceTests(new TheHistoryArray());
            Console.WriteLine("****** List Tests *******");
            test.RunAllPerformanceTests(new TheHistoryList());
            Console.WriteLine("****** LinkedList Tests *******");
            test.RunAllPerformanceTests(new TheHistoryLinkedList());

            Console.WriteLine("\n\nAll test Finished. Press a key to exit.");
            Console.ReadKey();
        }
    }
}
