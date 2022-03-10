using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillCamp2022
{
    public class RegMark
    {
        private static string _markLetters = "ABEKMHOPCTYX";
        private static List<int> _listRegions = new List<int> { 1, 4, 02, 102, 3, 5, 6, 7, 8, 9, 10, 11, 111, 82, 12, 13, 113, 14, 15, 16, 116, 17, 18, 19, 95, 21, 121, 22, 75, 80, 41, 23, 93, 123, 24, 124, 84, 88, 59, 81, 159, 25, 125, 26, 126, 27, 28, 29, 30, 31, 32, 33, 34, 134, 35, 36, 136, 37, 38, 138, 85, 39, 91, 40, 42, 142, 43, 44, 45, 46, 47, 48, 49, 50, 90, 150, 190, 750, 51, 52, 152, 53, 54, 154, 55, 56, 57, 58, 60, 61, 161, 62, 63, 163, 64, 164, 65, 66, 96, 196, 67, 68, 69, 70, 71, 72, 73, 173, 74, 174, 76, 77, 97, 99, 177, 197, 199, 777, 78, 98, 178, 92, 79, 83, 86, 186, 87, 89, 94 };


        /// <summary>
        /// Метод проверки гос.номера автомобиля
        /// </summary>
        /// <param name="mark">Регистрационный номер автомобиля</param>
        /// <returns>Результат проверки</returns>
        public static bool CheckMark(string mark)
        {
            try
            {
                if (CheckNumber(mark) && CheckSeries(mark) && CheckRegion(mark))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool CheckNumber(string mark)
        {
            string numberString = mark.Substring(1, 3);
            if (int.TryParse(numberString, out int numberInt))
            {
                if (numberInt > 0)
                    return true;
            }

            return false;
        }

        private static bool CheckSeries(string mark)
        {
            string series = mark.Remove(1, 3).Remove(3);
            if (series.All(x => _markLetters.Contains(x)))
                return true;
            else
                return false;
        }

        private static bool CheckRegion(string mark)
        {
            return int.TryParse(mark.Substring(6), out int result) && _listRegions.Contains(result);
        }

        /// <summary>
        /// Метод для получения следующего гос.номера автомобиля
        /// </summary>
        /// <param name="mark">Регистрационный номер автомобиля</param>
        /// <returns>Следующий гос.номер</returns>
        public static string GetNextMarkAfter(string mark)
        {
            if (!CheckMark(mark))
                return null;

            // Увеличение номерной части
            string numberString = mark.Substring(1, 3);
            int.TryParse(numberString, out int numberInt);
            string newNumber = numberInt == 999 ? "001" : (++numberInt).ToString("D3");

            mark = mark.Remove(1, 3);
            mark = mark.Insert(1, newNumber);


            // Увеличение серийной части
            if (newNumber == "001")
            {
                int seriesRank1 = _markLetters.IndexOf(mark[5]);
                int seriesRank2 = _markLetters.IndexOf(mark[4]);
                int seriesRank3 = _markLetters.IndexOf(mark[0]);

                int rankBorder = _markLetters.Length;

                seriesRank1++;
                if (seriesRank1 == rankBorder)
                {
                    seriesRank2++;
                    seriesRank1 = 0;
                }
                if (seriesRank2 == rankBorder)
                {
                    seriesRank3++;
                    seriesRank2 = 0;
                }
                if (seriesRank3 == rankBorder)
                {
                    return null;
                }

                mark = mark.Remove(0, 1);
                mark = mark.Insert(0, _markLetters[seriesRank3].ToString());
                mark = mark.Remove(4, 1);
                mark = mark.Insert(4, _markLetters[seriesRank2].ToString());
                mark = mark.Remove(5, 1);
                mark = mark.Insert(5, _markLetters[seriesRank1].ToString());
            }

            return mark;
        }

        /// <summary>
        /// Метод для подсчета количества комбинаций в диапазоне
        /// </summary>
        /// <param name="mark1">Регистрационный номер автомобиля 1</param>
        /// <param name="mark2">Регистрационный номер автомобиля 2</param>
        /// <returns>Количество комбинаций</returns>
        public static int GetCombinationsCountInRange(string mark1, string mark2)
        {

            if (CheckMark(mark1) && CheckMark(mark2))
            {
                string minMark = MinMark(mark1, mark2);
                string maxMark = mark1 == minMark ? mark2 : mark1;
                int count = 1;
                while (minMark != maxMark)
                {
                    minMark = GetNextMarkAfter(minMark);
                    count++;
                }

                return count;
            }
            return -1;
        }

        private static string MinMark(string mark1, string mark2)
        {
            if (CheckMark(mark1) && CheckMark(mark2))
            {
                int mark1SeriesRank1 = _markLetters.IndexOf(mark1[5]);
                int mark1SeriesRank2 = _markLetters.IndexOf(mark1[4]);
                int mark1SeriesRank3 = _markLetters.IndexOf(mark1[0]);

                int mark2SeriesRank1 = _markLetters.IndexOf(mark2[5]);
                int mark2SeriesRank2 = _markLetters.IndexOf(mark2[4]);
                int mark2SeriesRank3 = _markLetters.IndexOf(mark2[0]);

                if (mark1SeriesRank3 == mark2SeriesRank3)
                {
                    if (mark1SeriesRank2 == mark2SeriesRank2)
                    {
                        if (mark1SeriesRank1 == mark2SeriesRank1)
                        {
                            int mark1DigitValue = int.Parse(mark1.Substring(1, 3));
                            int mark2DigitValue = int.Parse(mark2.Substring(1, 3));
                            if (mark1DigitValue < mark2DigitValue)
                                return mark1;
                        }
                        else
                        {
                            if (mark1SeriesRank1 < mark2SeriesRank1)
                                return mark1;
                        }
                    }
                    else
                    {
                        if (mark1SeriesRank2 < mark2SeriesRank2)
                            return mark1;
                    }
                }
                else
                {
                    if (mark1SeriesRank3 < mark2SeriesRank3)
                        return mark1;
                }
                return mark2;
            }
            return null;
        }
    }
}
