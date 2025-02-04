using System.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;

// Перше завдання

//namespace HW21_01
//{

//     class Program
//     {
//         static async Task Main()
//         {
//             Console.Write("Нижня границя або Ентер для 2: ");
//             string? lowerInput = Console.ReadLine();
//             int lowerBound = string.IsNullOrWhiteSpace(lowerInput) ? 2 : int.Parse(lowerInput);

//             Console.Write("Верхня границя або Ентер для безкінечності: ");
//             string? upperInput = Console.ReadLine();
//             int? upperBound = string.IsNullOrWhiteSpace(upperInput) ? null : int.Parse(upperInput);

//             using (CancellationTokenSource cts = new CancellationTokenSource())
//             {
//                 Task primeTask = Task.Run(() => GeneratePrimes(lowerBound, upperBound, cts.Token));

//                 Console.WriteLine("Натисніть клавішу для завершення...");
//                 Console.ReadKey();
//                 cts.Cancel();

//                 await primeTask;  
//             }
//         }

//         static void GeneratePrimes(int start, int? end, CancellationToken token)
//         {
//             for (int num = start; end == null || num <= end; num++)
//             {
//                 if (token.IsCancellationRequested)
//                 {
//                     Console.WriteLine("Зупинка...");
//                     return;
//                 }

//                 if (IsPrime(num))
//                     Console.WriteLine(num);

//                 Thread.Sleep(100); 
//             }
//         }

//         static bool IsPrime(int number)
//         {
//             if (number < 2) return false;
//             for (int i = 2; i * i <= number; i++)
//             {
//                 if (number % i == 0)
//                     return false;
//             }
//             return true;
//         }
//     }
// }


// Друге завдання

namespace HW21_01
{

    internal class Program
    {

        
        private static bool running = true;

        static void Main()
        {
            int lowerBound = 2;
            int? upperBound = null;

            Console.Write("Нижня границя або Ентер для 2: ");
            if (int.TryParse(Console.ReadLine(), out int lowerInput))
            {
                Validation(lowerInput);
                lowerBound = lowerInput;
            }

            Console.Write("Верхня границя або Ентер для безкінечності: ");
            if (int.TryParse(Console.ReadLine(), out int upperInput))
            {
                Validation(upperInput);
                upperBound = upperInput;
            }

            Console.WriteLine("Натисніть клавішу для завершення...");

            Thread primeThread = new Thread(() => GeneratePrimes(lowerBound, upperBound));
            Thread fibonacciThread = new Thread(GenerateFibonacci);

            primeThread.Start();
            fibonacciThread.Start();

            Console.ReadKey();
            running = false;
            primeThread.Join();
            fibonacciThread.Join();
        }

       
        private static void GenerateFibonacci()
        {
            int a = 0, b = 1;

            while (running)
            {
                Console.WriteLine($"Число Фібоначчі: {a}");
                int next = a + b;
                a = b;
                b = next;
                Thread.Sleep(200);
            }

            Console.WriteLine("Генерацію чисел Фібоначчі завершено");
        }

        private static void GeneratePrimes(int lowerBound, int? upperBound)
        {
            Random random = new Random();

            try
            {
                while (running)
                {
                    int candidate = random.Next(lowerBound, upperBound ?? int.MaxValue);

                    if (IsPrime(candidate))
                    {
                        Console.WriteLine(candidate);
                        Thread.Sleep(100);
                    }
                }

                Console.WriteLine("Генерацію завершено");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
                if (number % i == 0) return false;

            return true;
        }


        private static void Validation(int number)
        {
            if (number < 0)
                throw new ArgumentException("Число не може бути від'ємним");
        }
    }
}



