using System;
using System.Threading;
using System.Text;

namespace TS
{
  
    class Program
    {
        static Mutex mtx = new Mutex(); //Използваме Mutex за синхронизация на данните между нишките
        static Thread thMain = new Thread(new ThreadStart(Read)); //Главна нишка
        static Thread thCaused = new Thread(new ThreadStart(Print)); //Породена нишка
        static string input = ""; //Входни данни
        static ManualResetEvent reset = new ManualResetEvent(true); //Възобновяване на работата на нишките

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Въведете низ:");
            input = Console.ReadLine();

            thMain.Start();
            thCaused.Start();

            thMain.Join();
            thCaused.Join();
        }
        static void Read() //Метод за четене на низа
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                mtx.WaitOne();
                input = userInput;

                if (input.Equals("end"))
                {
                    reset.Reset();
                }
                else
                {
                    reset.Set();
                }

                mtx.ReleaseMutex(); 
            }
        }
        static void Print() //Метод за отпечатване на низа
        {
            while (true)
            {
                reset.WaitOne();
                Thread.Sleep(2000);

                if (input.Equals("end"))
                {
                    reset.Reset();
                }
                else
                {
                    reset.Set();
                    mtx.WaitOne();
                    Console.WriteLine(input);
                    mtx.ReleaseMutex();
                }
            }
        }
    }
}
