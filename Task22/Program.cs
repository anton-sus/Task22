﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Сформировать массив случайных целых чисел (размер  задается пользователем). Вычислить сумму чисел
//массива и максимальное число в массиве.  Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.



namespace Task22
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(SumArray);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(MaxArray);
            Task task3 = task1.ContinueWith(action2);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
        }

        static void SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int Sum = 0;
            for (int i = 0; i < array.Count(); i++)
                Sum += array[i];

            Console.WriteLine($"сумма:{Sum}");
        }

        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine($"макс.элемент:{array.Max()} ");
        }

    }
}