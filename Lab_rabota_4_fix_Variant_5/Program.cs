﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Lab_rabota_fix_Variant_5
{
    public class Program
    {
        //Поменять местами в двумерном массиве противоположные элементы(например 5.7 и -5.7), 
        //с учетов перестановки элемента только один раз
        public static void Main(string[] args)
        {
            //Зададим количество строк и столбцов в массиве
            const int rows = 5;
            const int lines = 4;

            //Будем помещать уже обработанные числа
            HashSet<float> processedElements = new HashSet<float>();

            //Исходный двумерный массив
            float[,] initialArray = new float[lines, rows] { { -3, 3, 2, 1, 1 }, { 3, -7, 7, -1, 1 }, { 4, -3, 7, 9, 8 }, { 1, -8, 0, 6, 1 } };

            //Словарь для подсчета количества одинаковых чисел в массиве
            Dictionary<float, int> dictValuesAmount = new Dictionary<float, int>();

            //Выведем массив для просмотра
            Console.WriteLine("Исходный массив:");
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

            //Теперь проделываем поиск противоположных по знаку ключей, выявляем наименьшее значение для них (выявляем сколько раз мы должны осуществить замену чисел в массиве),
            //и проделываем замену
            for (int i = 0; i < (dictValuesAmount.Count - 1); i++)
            {
                for (int j = i+1; j < dictValuesAmount.Count; j++)
                {
                    //Проверяем есть ли вообще в словаре разные по знаку ключи и содержатся ли они в хеш сете
                    if ((dictValuesAmount.ElementAt(i).Key == -dictValuesAmount.ElementAt(j).Key) & !processedElements.Contains(dictValuesAmount.ElementAt(i).Key) & !processedElements.Contains(dictValuesAmount.ElementAt(j).Key))
                    {
                        //Console.WriteLine(dictValuesAmount.ContainsKey(-dictValuesAmount.ElementAt(i).Key) + " " + -dictValuesAmount.ElementAt(i).Key);
                        if (dictValuesAmount.ElementAt(i).Value <= dictValuesAmount.ElementAt(j).Value)
                        {
                            //Выясняем сколько раз необходимо сделать замену (не забываем, что 1 замена = 2 операции - заменить положительное число на отрицательное,
                            // и отрицательное на положительное)
                            int numberOfChanges = dictValuesAmount.ElementAt(i).Value*2;
                            //Добавляем обработанные элементы в хеш сеты, дабы больше не обрабатывать те же элементы
                            processedElements.Add(dictValuesAmount.ElementAt(i).Key);
                            processedElements.Add(dictValuesAmount.ElementAt(j).Key);

                            int k = 0;
                            for (int l = 0; l < lines; l++)
                            {
                                for (int m = 0; m < rows; m++)
                                {
                                    if (k > (numberOfChanges - 1))
                                    {
                                        break;
                                    }
                                    if (initialArray[l, m] == dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = -dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                    }
                                    else if (initialArray[l, m] == -dictValuesAmount.ElementAt(i).Key)
                                    {
                                        initialArray[l, m] = dictValuesAmount.ElementAt(i).Key;
                                        k++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int numberOfChanges = dictValuesAmount.ElementAt(j).Value*2;
                            processedElements.Add(dictValuesAmount.ElementAt(i).Key);
                            processedElements.Add(dictValuesAmount.ElementAt(j).Key);
                            int p = 0; //для чисел одного знака
                            int q = 0; //для чисел противоположного знака
                            for (int l = 0; l < lines; l++)
                            {
                                for (int m = 0; m < rows; m++)
                                {
                                    if (initialArray[l, m] == dictValuesAmount.ElementAt(i).Key)
                                    {
                                        if (p == (numberOfChanges / 2 ))
                                        {
                                            continue;
                                        }
                                        initialArray[l, m] = -dictValuesAmount.ElementAt(i).Key;
                                        p++;
                                    }
                                    else if (initialArray[l, m] == -dictValuesAmount.ElementAt(i).Key)
                                    {
                                        if (q == (numberOfChanges / 2))
                                        {
                                            continue;
                                        }
                                        initialArray[l, m] = dictValuesAmount.ElementAt(i).Key;
                                        q++;
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
            Console.WriteLine("Результирующий массив:");
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

