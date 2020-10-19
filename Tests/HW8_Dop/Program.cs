using System;

namespace HW8_Dop
{
    class Program
    {
        //4.Каков результат вывода следующего кода?
        private enum SomeEnum
        {
            First = 4,
            Second, // 5, так как при перечислении создается счетчик с 0, а если задается значение, то следующее будет +1
            Third = 7
        }

        //3. Есть таблица Users. Столбцы в ней — Id, Name. Написать SQL-запрос, который выведет имена пользователей,
        //начинающиеся на 'A' и встречающиеся в таблице более одного раза, и их количество.
        //
        //SELECT [Name],
        //COUNT(*) as [count]
        //FROM[TestDB].[dbo].[Users]
        //WHERE[Name] like 'А%'
        //GROUP BY[Name]
        //HAVING COUNT(*) > 1

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Console.WriteLine(Factorial(0));
            //Task2();

            //4.Каков результат вывода следующего кода?
            Console.WriteLine((int)SomeEnum.Second);
            Console.ReadLine();
        }
        /// <summary>
        /// 1. Выполнить без использования среды разработки, используя только ручку и лист бумаги,
        /// задачу на написание консольного приложения нахождения факториала числа N.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Factorial(short n)
        {
            var res = (long)n;
            if (n == 0 || n == 1)
            {
                return 1;
            }

            while (n > 1)
            {
                res *= (n - 1);
                n--;
            }

            return res;
        }

        /// <summary>
        /// 2. Есть ли проблемы в следующем коде?
        /// </summary>
        public static void Task2()
        {
            int i = 1;
            object obj = i;
            ++i;
            Console.WriteLine(i);
            Console.WriteLine(obj);
            Console.WriteLine((short)obj); //Невозможно выполнить преобразование типов вниз(int в short), получим исключение
        }

        //5. *Существует таблица (см. методичку к уроку, раздел ДЗ).
        //Необходимо сформировать SQL-запрос, выбирающий данные (см. методичку к уроку, раздел ДЗ).
        // with rec as
        //(
        //SELECT[group_id], STUFF((SELECT DISTINCT CHAR(10) + CAST([descr]  + ',' as varchar) FROM(SELECT DISTINCT [descr]
        //FROM [TestDB].[dbo].[Users] as us where u.group_id = us.group_id) ADD_NAME
        //    ORDER BY CHAR(10) + CAST([descr]  + ',' as varchar)

        //FOR XML PATH('')
        //    ),1,1,'') as st from[TestDB].[dbo].[Users] as u
        //)

        //select[group_id],replace(left(st, len(st)-1),CHAR(10),'') as descr from rec group by group_id, st

    }
}
