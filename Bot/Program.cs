using System;

namespace Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(Telegram.GetInstance().GetUpdates());                
            }
        }        
    }
}
