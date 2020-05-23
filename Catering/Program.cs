using System;
using System.Threading;

namespace Catering
{
    class Program
    {
        static void Main(string[] args)
        {
            CateringClass cat = new CateringClass();
            int customer_id,tray,food,qty, process_type;

            Console.Write("Which Type \n 1- Auto Random Data : \n 2- Manual Data Entry : \n");
            process_type = Convert.ToInt32(Console.ReadLine());

            cat.displayTrays();

            while (true)
            {
                Thread.Sleep(20);

                if (cat.checkIndexZeroTray())
                {

                Random randomNumber = new Random();

                if (process_type == 1)
                {
                    int randomCustomerID = randomNumber.Next(0, 10);
                    customer_id = randomCustomerID;
                }
                else
                {
                    Console.Write("Which Guest(1-10) : ");
                    customer_id = Convert.ToInt32(Console.ReadLine())-1;
                }



                while (true)
                {
                    cat.checkTraysEmpty();

                    if(process_type == 1)
                    {
                        int randomTray = randomNumber.Next(0, 3);
                        int randomFood = randomNumber.Next(0, 3);
                        int randomQty = randomNumber.Next(1, 2);
                        tray = randomTray;
                        food = randomFood;
                        qty = randomQty;
                    }
                    else
                    {
                        Console.Write("Which Tray (1-3) : ");
                        tray = Convert.ToInt32(Console.ReadLine())-1;

                        Console.Write("Which Food (borek, cake, drink) : ");
                        string foodName = Console.ReadLine();
                        food = cat.findFoodByName(foodName);

                        Console.Write("How Many? :  ");
                        qty = Convert.ToInt32(Console.ReadLine());
                    }

                    if (cat.minEatingFood(customer_id, food, qty))
                    {
                        if (cat.checkCustomerByIndex(customer_id, food, qty))
                        {
                            if (cat.checkTrayByIndex(tray, food, qty))
                            {
                                cat.setCustomerRecord(customer_id, food, qty);
                                cat.setTrayRecord(tray, food, qty);
                                cat.ConsoleWriteLine("Customer ["+customer_id+"] ate "+qty+" "+cat.findFoodNameById(food) + "", ConsoleColor.DarkGreen);
                                break;
                            }
                            else
                            {
                                cat.ConsoleWriteLine("Tray [" + tray + "] is not enough to buy a tray. Try again", ConsoleColor.DarkYellow);
                                break;
                            }
                        }
                        else
                        {
                            cat.ConsoleWriteLine("Customer [" + customer_id + "] has limit is reached. Try again", ConsoleColor.DarkRed);
                            break;
                        }
                    }
                    else
                    {
                        cat.ConsoleWriteLine("Customer ["+customer_id+"] has exceeded the min food limit.", ConsoleColor.DarkRed);
                        break;
                    }
                }

                }
                else
                {
                    cat.checkIndexZeroCustomer();
                    break;
                }
            }

            cat.displayRecords();
            cat.displayTrays();

            cat.ConsoleWriteLine("\n\n **** All food and drinks are finished. *** ", ConsoleColor.DarkGreen);
            Console.ReadKey();
        }
    }
}
