using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hemming
{
    class Program
    {
        static int[] ConvertToBinaryArray(string message)
        {
            int[] res = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                res[i]=Convert.ToInt32(((Convert.ToString(message[i], 2))));
            }
            return res;
        }

        static int GetEncodedCharacter(int ch)
        {

        }

        static void Main(string[] args)
        {
            string mess = Console.ReadLine();
            foreach (var v in ConvertToBinaryArray(mess))
            {
                Console.WriteLine(v);
            }
            Console.ReadLine();
        }
    }
}
