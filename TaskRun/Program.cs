using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);
            // these are blocking operations; wait on all tasks

            Parallel.For(1, 11, x =>
            {
                Console.Write($"{x * x}\t");
            });
            Console.WriteLine();

            // has a step strictly equal to 1
            // if you want something else...
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);

            string[] letters = { "oh", "what", "a", "night" };
            var po = new ParallelOptions();
            po.MaxDegreeOfParallelism = 2;
            Parallel.ForEach(letters, po, letter =>
            {
                Console.WriteLine($"{letter} has length {letter.Length} (task {Task.CurrentId})");
            });
            
            Execute();
            Console.WriteLine("Hello World");

            Method1();
            Method2();
            Console.ReadLine();
            // CallMe();
        }

        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }
        
        public static async Task Method1()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Method 1 : " + DateTime.Now.Millisecond);
                }
            });
        }

        public static async Task Method2()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Method 2 : " + DateTime.Now.Millisecond);
                }
            });
        }

        static async void Execute()
        {
        // running this method asynchronously.
            int t = await Task.Run(() => Calculate());
            Console.WriteLine("Result: " + t);
        }

        static int Calculate()
        {
        // calculate total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 100000; i++)
                {
                    string value = i.ToString();
                    size += value.Length;
                }
            }

            return size;
        }

        public static void CallMe()
        {
            CallMe();
        }
    }
}
