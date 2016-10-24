using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication35
{
    class Program
    {
        public static void datechecker(int offset)
        {
            switch (offset % 7)
            {
                case 0:
                    Console.Write("MONDAY");
                    break;
                case 1:
                    Console.Write("TUESDAY");
                    break;
                case 2:
                    Console.Write("WEDNESDAY");
                    break;
                case 3:
                    Console.Write("THURSDAY");
                    break;
                case 4:
                    Console.Write("FRIDAY");
                    break;
                case 5:
                    Console.Write("SATURDAY");
                    break;
                case 6:
                    Console.Write("SUNDAY");
                    break;
            }
            Console.WriteLine();
        }

        public static void swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        static void Main(string[] args)
        {
            int noofdays;
            int startdate = 1, startmonth = 1, startyear = 1;
        x:
            Console.Write("\n\n\tENTER THE DATE (DD MM YYYY) : ");
            string fulldate = Console.ReadLine();
            if (fulldate.LongCount<char>() != 10)
            {
                Console.Clear();
                Console.WriteLine("\n\n\tINVALID ENTRY . . . e.g., : 05 01 1996");
                goto x;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i != 2 && i != 5)
                    {
                        if (!(fulldate[i] >= 48 && fulldate[i] <= 57))
                        {
                            Console.Clear();
                            Console.WriteLine("INVALID ENTRY . . . e.g., : 05 01 1996");
                            goto x;
                        }
                    }
                    else
                    {
                        if (!(fulldate[i] != 2))
                        {
                            Console.Clear();
                            Console.WriteLine("INVALID ENTRY . . . e.g., : 05 01 1996");
                            goto x;
                        }
                    }
                }
            }
            int date = (fulldate[0] - 48) * 10 + (fulldate[1] - 48);
            int month = (fulldate[3] - 48) * 10 + (fulldate[4] - 48);
            int year = (fulldate[6] - 48) * 1000 + (fulldate[7] - 48) * 100 + (fulldate[8] - 48) * 10 + (fulldate[9] - 48);

            //check and increment loop
            for (noofdays = 0; !(startdate == date && startmonth == month && startyear == year); noofdays++)
            {

                //Console.Write("{0,2}, {1,2}, {2,4}, {3,3}, ", startdate, startmonth, startyear, noofdays);
                //datechecker(noofdays);
                //Console.ReadKey();

                startdate++;

                //updating month with days
                if (startmonth == 2)
                {
                    if (startyear % 4 == 0 && startyear % 100 != 0 || startyear % 400 == 0)
                    {
                        if (startdate == 30)
                        {
                            startmonth += 1;
                            startdate -= 29;
                        }
                    }
                    else
                    {
                        if (startdate == 29)
                        {
                            startmonth += 1;
                            startdate -= 28;
                        }
                    }
                }
                else
                {
                    if (startmonth == 4 || startmonth == 6 || startmonth == 9 || startmonth == 11)
                    {
                        if (startdate == 31)
                        {
                            startmonth += 1;
                            startdate -= 30;
                        }
                    }
                    else
                    {
                        if (startdate == 32)
                        {
                            startmonth += 1;
                            startdate -= 31;
                        }
                    }
                }

                //updating year with month
                if (startmonth == 13)
                {
                    startyear += 1;
                    startmonth -= 12;
                }

                /*if (startdate == date && startmonth == month)
                {
                    Console.Write("\n\n\tThe Day of {0} {1} {2} is ", date, month, startyear);
                    datechecker(noofdays);
                    Console.ReadKey();
                }*/
            }
            Console.WriteLine("\n\n\tTHE OFFSET IS : {0}", noofdays);
            Console.Write("\n\n\tThe Day of {0} {1} {2} is ", date, month, year);
            datechecker(noofdays);
        }
    }
}