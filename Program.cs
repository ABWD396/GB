using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int arrayLength = 10;
            int[] array = SetArray(arrayLength);

            WriteArray(array);

            for (int i = 0; i < arrayLength; i++)
            {
                int minValue = array[i];
                int minValuePos = i;
                for (int j = i+1; j < arrayLength; j++)
                {
                    if(array[j] < minValue){
                        minValue = array[j];
                        minValuePos = j;
                    }
                }
                if(minValuePos != i) {
                    array[minValuePos] = array[i];
                    array[i] = minValue;
                }
            }

            Console.WriteLine("Sorted");

            WriteArray(array);
        }

        static int[] SetArray(int length)
        {
            int[] array = new int[length];
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(-100, 101);
            }

            return array;
        }

        static void WriteArray(int[] array)
        {
            foreach (int i in array)
            {
                Console.WriteLine(i);
            }
        }
    }
}
