using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_hw
{
    class Program
    {
        static long n = Convert.ToInt64(Math.Pow(10, 9));
        static long[] portionResults;
        static long portionSize;
        static void Main(string[] args)
        {
            Console.WriteLine("Gauss Metodu Sonucu={0}", gauss(n));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 1; i < 17; i++)
            {
                int thread_num = i;
                portionResults = new long[thread_num];
                if (n % thread_num == 0)
                {
                    portionSize = n / thread_num;
                    additionWithThreading(thread_num);
                }
                else
                {
                    long mode = n % thread_num;
                    portionSize = n / thread_num;
                    additionWithThreadingNotDivided(thread_num, mode);
                }
            }
            watch.Stop();
            Console.WriteLine("Toplam sure=" + watch.Elapsed);




            Console.ReadKey();
        }
        static void sumAPortion(object portionNumber)
        {
            long sum = 0;
            long portionNumber_long = Convert.ToInt64(portionNumber);
            long index = portionNumber_long * portionSize;
            for (long i = index; i < index + portionSize; i++)
            {
                sum += i;
            }
            portionResults[portionNumber_long] = sum;
        }

        static void additionWithThreading(int thread_num)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Thread[] threads = new Thread[thread_num];
            for (int i = 0; i < thread_num; i++)
            {
                threads[i] = new Thread(sumAPortion);
                threads[i].Start(i);
            }

            for (int i = 0; i < thread_num; i++)
            {
                threads[i].Join();
            }

            long result = 0;
            for (int i = 0; i < thread_num; i++)
            {
                result += portionResults[i];
            }
            result += n;
            watch.Stop();
            Console.WriteLine("{0} iplik icin SONUC={1} SURE={2}", thread_num, result, watch.Elapsed);

        }
        static void additionWithThreadingNotDivided(int thread_num, long mode)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Thread[] threads = new Thread[thread_num];
            for (int i = 0; i < thread_num; i++)
            {
                threads[i] = new Thread(sumAPortion);
                threads[i].Start(i);
            }

            for (int i = 0; i < thread_num; i++)
            {
                threads[i].Join();
            }

            long result = 0;
            for (int i = 0; i < thread_num; i++)
            {
                result += portionResults[i];
            }
            for (long i = n - 1; i >= n - mode; i--)
            {
                result += i;
            }
            result += n;
            watch.Stop();
            Console.WriteLine("{0} iplik icin SONUC={1} SURE={2}", thread_num, result, watch.Elapsed);

        }
        static long gauss(long n)
        {
            return (n * (n + 1) / 2);
        }
    }
}
