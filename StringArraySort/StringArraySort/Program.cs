using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringArraySort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Объявляем массив
            string[] myArr = new string[7];
            //Инициализируем каждый элемент массива вручную
            myArr[0] = "Sunday";
            myArr[1] = "Monday";
            myArr[2] = "Tuesday";
            myArr[3] = "Wednesday";
            myArr[4] = "Thursday";
            myArr[5] = "Friday";
            myArr[6] = "Saturday";

            var x = from a in myArr
                    orderby a.Length ascending
                    select a;
            foreach (string i in x)
                
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();

        }
    }
}
