//Title of this code
//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Rextester
{
    public class Program
    {
        //Поменять местами в двумерном массиве противоположные элементы(например 5.7 и -5.7), 
        //с учетов перестановки элемента только один раз
        public static void Main(string[] args)
        {
            //Зададим количество строк и столбцов в массиве
            const int rows = 4;
            const int lines = 4;

            //Будем помещать уже обработанные числа
            HashSet<float> processedElements = new HashSet<float>();

            //Исходный двумерный массив
            float[,] initialArray = new float[lines, rows] { { -3, 3, 2, 1 }, { 3, 7, -7, -1 }, { 4, -3, 8, 9 }, { 1, -8, 0, 6 } };

            //Словарь для подсчета количества одинаковых чисел в массиве
            Dictionary<float, int> dictValuesAmount = new Dictionary<float, int>();
            
            //Выведем массив для просмотра
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(initialArray[i, j] + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            //Находим сколько раз каждое число содержится в массиве и формируем словарь
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (!dictValuesAmount.ContainsKey(initialArray[i, j]))
                    {
                        dictValuesAmount.Add(initialArray[i, j], 1);
                    }
                    else
                    {
                        dictValuesAmount[initialArray[i, j]] += 1;
                    }
                }
            }

            //Выведем словарь для просмотра
            foreach (KeyValuePair<float, int> kvp in dictValuesAmount)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            //Теперь берем проделываем поиск противоположных по знаку ключей, выявляем наименьшее значение для них (выявляем сколько раз мы должны осуществить замену чисел в массиве),
            //и проделываем замену
            for (int i = 0; i < (dictValuesAmount.Count - 1); i++)
            {
                for (int j = i+1; j < dictValuesAmount.Count; j++)
                {
                    //Проверяем есть ли вообще в словаре разные по знаку ключи и содержатся ли они в хеш сете
                    if ((dictValuesAmount.ElementAt(i).Key == -dictValuesAmount.ElementAt(j).Key) & !processedElements.Contains(dictValuesAmount.ElementAt(i).Key) & !processedElements.Contains(dictValuesAmount.ElementAt(j).Key))
                    {
                        Console.WriteLine(dictValuesAmount.ContainsKey(-dictValuesAmount.ElementAt(i).Key) + " " + -dictValuesAmount.ElementAt(i).Key);
                        if (dictValuesAmount.ElementAt(i).Value <= dictValuesAmount.ElementAt(j).Value)
                        {
                            //Выясняем сколько раз необходимо сделать замену (не забываем, что 1 замена = 2 операции - заменить положительное число на отрицательное,
                            // и отрицательное на положительное)
                            int numberOfChanges = dictValuesAmount.ElementAt(i).Value*2;
                            //Добавляем обработанные элементы в хеш сеты, дабы больше не обрабатывать те же элементы
                            processedElements.Add(dictValuesAmount.ElementAt(j).Key);

                            int k = 0;
                            for (int l = 0; l < lines; l++)
                            {
                                for (int m = 0; m < rows; m++)
                                {
                                    if (initialArray[l, m] == dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = -dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                        if (k > (numberOfChanges - 1))
                                        {
                                            break;
                                        }
                                    }
                                    else if (initialArray[l, m] == -dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                        if (k > (numberOfChanges - 1))
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int numberOfChanges = dictValuesAmount.ElementAt(j).Value;
                            processedElements.Add(dictValuesAmount.ElementAt(j).Key);
                            int k = 0;
                            for (int l = 0; l < lines; l++)
                            {
                                for (int m = 0; m < rows; m++)
                                {
                                    if (initialArray[l, m] == dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = -dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                        if (k > (numberOfChanges - 1))
                                        {
                                            break;
                                        }
                                    }
                                    else if (initialArray[l, m] == -dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                        if (k > (numberOfChanges - 1))
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            //Выведем результат
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(initialArray[i, j] + " ");
                }
            }
            Console.ReadKey();
        }
    }
}

