using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hemming
{
    class Program
    {
        static void print(int[,] arr)
        {
            for (int i = 0; i <arr.GetLength(0); i++)
            {
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write(" " + arr[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void print(List<int[]> arr)
        {
            foreach(var v in arr)
            {
                foreach(var n in v)
                {
                    Console.Write(" " + n);
                }
                Console.WriteLine();
            }
        }

        static int[] ConvertToBinaryArray(string message)
        {
            int[] res = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                res[i]=Convert.ToInt32(((Convert.ToString(message[i], 2))));
            }
            return res;
        }

        static int[] GetEncodedCharacter(int ch)
        {
            string str = Convert.ToString(ch);
            int pow_two = 0;
            while (true) //Ищем степень двойки, для колличества контрольных бит.
            {
                if(Math.Pow(2,pow_two)>str.Length+pow_two)
                {
                    pow_two--;
                    break;
                }
                pow_two++;
            }
            int[,] array_control_bits = new int[pow_two + 2,str.Length + pow_two + 1]; //Создание матрицы по которой будет вычисляться код Хэмминга
            for (int i = 0; i <= pow_two; i++) //Заполняем контрольные биты значениями "-1"
            {
                array_control_bits[0, (int)Math.Pow(2, i) - 1] = -1;
            }
            int count = 0;
            for (int i = 0; i < str.Length + pow_two + 1; i++) //Заполняем массив битами сообщения
            {
                if (array_control_bits[0, i] != -1)
                {
                    array_control_bits[0, i] = str[count++].Equals('1')?1:0;
                }
            }
            for (int i = 1; i < pow_two+2; i++) //Заполнение оставшихся строк контрольными битами (только тех бит, которые являются контролируемыми этим контрольным битом)
            {
                for (int j = (int)Math.Pow(2,i-1)-1; j < array_control_bits.GetLength(1); j+=(int)Math.Pow(2,i))
                {
                    for (int h = 0; h < Math.Pow(2,i-1); h++)
                    {
                        if (j + h < array_control_bits.GetLength(1)) array_control_bits[i, j + h] = -1;
                        else break;
                    }
                }
            }
            //print(array_control_bits);
            for (int i = 1; i < pow_two + 2; i++) //В первой строчке массива, содержащей бинарное представление символа и контрольные биты, мы меняем значение контрольных бит на "1" или "0".
            {
                count = 0;
                for (int j = 0; j < array_control_bits.GetLength(1); j++)
                {
                    if ((array_control_bits[i, j] == -1) && (array_control_bits[0, j] == 1)) count++;
                }
                if(count%2==0)
                {
                    array_control_bits[0, (int)Math.Pow(2, i - 1) - 1] = 0;
                }
                else array_control_bits[0, (int)Math.Pow(2, i - 1) - 1] = 1;
            }
            //print(array_control_bits);
            int[] res = new int[array_control_bits.GetLength(1)];
            for (int i = 0; i < array_control_bits.GetLength(1); i++) //Составляем результирующий массив, отбрасывая ненужные строчки матрицы.
            {
                res[i] = array_control_bits[0, i];
            }
            res[5] = 1;
            return res;
        }

        static string GetDecodedCharacter(int[] in_arr)
        {
            int[] arr=new int[in_arr.Length];
            in_arr.CopyTo(arr, 0);
            int pow_two = 0;
            while (true) //Ищем степень двойки, для колличества контрольных бит.
            {
                if (Math.Pow(2, pow_two) > arr.Length)
                {
                    pow_two--;
                    break;
                }
                pow_two++;
            }
            int[,] array_control_bits = new int[pow_two + 2, arr.Length]; //Создание матрицы по которой будет вычисляться код Хэмминга
            for (int i = 0; i <= pow_two; i++) //Заполняем контрольные биты значениями "-1"
            {
                array_control_bits[0, (int)Math.Pow(2, i) - 1] = -1;
                arr[(int)Math.Pow(2, i) - 1] = -1;
            }
            for (int i = 0; i < arr.Length; i++) //Заполняем массив битами сообщения
            {
                if (array_control_bits[0, i] != -1)
                {
                    array_control_bits[0, i] = arr[i];
                }
            }
            for (int i = 1; i < pow_two + 2; i++) //Заполнение оставшихся строк контрольными битами (только тех бит, которые являются контролируемыми этим контрольным битом)
            {
                for (int j = (int)Math.Pow(2, i - 1) - 1; j < array_control_bits.GetLength(1); j += (int)Math.Pow(2, i))
                {
                    for (int h = 0; h < Math.Pow(2, i - 1); h++)
                    {
                        if (j + h < array_control_bits.GetLength(1)) array_control_bits[i, j + h] = -1;
                        else break;
                    }
                }
            }
            int count;
            for (int i = 1; i < pow_two + 2; i++) //В первой строчке массива, содержащей бинарное представление символа и контрольные биты, мы меняем значение контрольных бит на "1" или "0".
            {
                count = 0;
                for (int j = 0; j < array_control_bits.GetLength(1); j++)
                {
                    if ((array_control_bits[i, j] == -1) && (array_control_bits[0, j] == 1)) count++;
                }
                if (count % 2 == 0)
                {
                    array_control_bits[0, (int)Math.Pow(2, i - 1) - 1] = 0;
                }
                else array_control_bits[0, (int)Math.Pow(2, i - 1) - 1] = 1;
            }
            count = -1;
            for (int i = 0; i <= pow_two; i++) //
            {
                if(array_control_bits[0, (int)Math.Pow(2, i) - 1] != in_arr[(int)Math.Pow(2, i) - 1])
                {
                    count+= (int)Math.Pow(2,i);
                }
            }
            if(count>0) array_control_bits[0, count] = array_control_bits[0, count] == 1 ? 0 : 1;
            for (int i = 0; i <= pow_two; i++) //Заполняем контрольные биты значениями "-1"
            {
                array_control_bits[0, (int)Math.Pow(2, i) - 1] = -1;
            }
            string res="";
            for (int i = 0; i < array_control_bits.GetLength(1); i++) //Составляем результирующий массив, отбрасывая ненужные строчки матрицы.
            {
                if(array_control_bits[0, i]!=-1) res += Convert.ToString(array_control_bits[0, i]);
            }
            print(array_control_bits);
            return res;
        }

        static void Main(string[] args)
        {
            string mess = Console.ReadLine();
            List<int[]> code_arr=new List<int[]>();
            foreach (var v in ConvertToBinaryArray(mess))
            {
                code_arr.Add(GetEncodedCharacter(v));
                //GetDecodedCharacter(code_arr[0]);
            }
            Console.ReadLine();
        }
    }
}
