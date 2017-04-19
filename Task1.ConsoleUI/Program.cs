using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Task1.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Queue test");
            Queue<int> queue = new Queue<int>(new List<int> {
                0, 1, 2, 4, 5, -5, -8, 95
            });

            Console.WriteLine("Test foreach:");
            foreach (var i in queue)
            {
                Console.Write($"{i} .. ");
            }

            queue.Enqueue(57);
            queue.Enqueue(-57);
            queue.Enqueue(0);

            Console.WriteLine();
            Console.WriteLine("Test Enqueue 57, -57, 0");
            foreach (var i in queue)
            {
                Console.Write($"{i} .. ");
            }

            Console.WriteLine();
            Console.WriteLine("Test Error when enumerate and modify");
            try
            {
                foreach (var i in queue)
                {
                    Console.Write($"{i} .. ");
                    Console.WriteLine($"Try to dequeue: {queue.Dequeue()}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Test Dequeue 6 elements");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine($"Dequeue: {queue.Dequeue()}");
            Console.WriteLine("Write after dequeue");
            foreach (var i in queue)
            {
                Console.Write($"{i} .. ");
            }

            Console.WriteLine();
            Console.WriteLine("Check Peek");
            Console.WriteLine($"Peek: {queue.Peek()}");

            Console.WriteLine("Test Clear");
            queue.Clear();
            Console.WriteLine($"Count: {queue.Count}");

            Console.ReadKey();
        }
    }
}
