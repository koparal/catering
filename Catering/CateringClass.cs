
/*
    Author sabankoparal@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{
    class CateringClass
    {
        const int BOREK = 0;
        const int CAKE = 1;
        const int DRINK = 2;

        public int edible_borek = 30;
        public int edible_cake = 15;
        public int edible_drink = 30;

        public int borek_loop = 15;
        public int cake_loop = 0;
        public int drink_loop = 15;

        List<int> had_eaten_customers = new List<int>();

        public int[,] trays = new int[3, 3] {
                                        { 5, 5 ,5 },
                                        { 5, 5, 5 },
                                        { 5, 5 ,5 }
                                    };

        public int[,] records = new int[10, 3] {
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 },
                                        { 0, 0 ,0 }
                                    };


        public void setTrayRecord(int tray_no, int food_no, int amountFood)
        {
            checkTraysEmpty();
            // Get Old Value
            int oldValTray = Convert.ToInt32(trays.GetValue(tray_no, food_no));

            // Sum new value and old value
            int resultTray = oldValTray - amountFood;

            //Console.WriteLine("Tray Before {0}", trays.GetValue(tray_no, food_no));

            // Set value
            trays.SetValue(resultTray, tray_no, food_no);

            //Console.WriteLine("Tray After {0}", trays.GetValue(tray_no, food_no));

        }

        public void setCustomerRecord(int customer_id, int food_type, int amount)
        {
            // Get Old Value
            int oldVal = Convert.ToInt32(records.GetValue(customer_id, food_type));

            // Sum of new value and old value
            int result = oldVal + amount;

            //Console.WriteLine("Before Guest {0}", records.GetValue(customer_id, food_type));

            // Set value
            records.SetValue(result, customer_id, food_type);

            if (food_type == BOREK)
            {
                edible_borek -= amount;
            }

            if (food_type == CAKE)
            {
                edible_cake -= amount;
            }

            if (food_type == DRINK)
            {
                edible_drink -= amount;
            }

            // Yemek yiyen kullanıcıyı listeye ekle
            if (!had_eaten_customers.Contains(customer_id))
            {
                had_eaten_customers.Add(customer_id);
            }

        }

        public Boolean fillTrays(int tray_no, int food_no, int amount)
        {
            int selected_food = 0;

            if (food_no == BOREK)
            {
                selected_food = borek_loop;
            }

            if (food_no == CAKE)
            {
                selected_food = cake_loop;
            }

            if (food_no == DRINK)
            {
                selected_food = drink_loop;
            }

            if (selected_food != 0)
            {
                //Console.WriteLine("Tray Update Start");
                //displayTrays();
                int result = 0;
                int oldVal = Convert.ToInt32(trays.GetValue(tray_no, food_no));
                int addVal = 0;

                if (amount == 1)
                {
                    addVal = 4;

                    if (selected_food < addVal)
                    {
                        addVal = selected_food;
                    }

                    result = oldVal + addVal;

                }
                else if (amount == 0)
                {
                    addVal = 5;

                    if (selected_food < addVal)
                    {
                        addVal = selected_food;
                    }

                    result = oldVal + addVal;
                }
                else
                {
                    addVal = 0;
                }

                if (food_no == BOREK)
                {
                    borek_loop -= addVal;
                }
                if (food_no == CAKE)
                {
                    cake_loop -= addVal;
                }
                if (food_no == DRINK)
                {
                    drink_loop -= addVal;
                }

                trays.SetValue(result, tray_no, food_no);

                int display_tray = tray_no + 1;
                ConsoleWriteLine("**** Tray [" + display_tray + "] " + findFoodNameById(food_no) + " Filled **** ", ConsoleColor.DarkYellow);
                //displayTrays();

            }
            else
            {
                //Console.WriteLine("Tray Not Updated. Because {0} Tray Count",selected_food);
                return false;
            }
            return true;

        }

        public Boolean minEatingFood(int customer_id, int food_type, int qty)
        {
            List<int> neverTakeBorek = new List<int>();
            List<int> neverTakeCake = new List<int>();
            List<int> neverTakeDrink = new List<int>();


            int rowLength = records.GetLength(0);
            int colLength = records.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (j == food_type && records[i, j] == 0)
                    {

                        if (food_type == BOREK)
                        {
                            if (!neverTakeBorek.Contains(i))
                            {
                                neverTakeBorek.Add(i);
                            }
                        }


                        if (food_type == CAKE)
                        {
                            if (!neverTakeCake.Contains(i))
                            {
                                neverTakeCake.Add(i);
                            }
                        }

                        if (food_type == DRINK)
                        {
                            if (!neverTakeDrink.Contains(i))
                            {
                                neverTakeDrink.Add(i);
                            }
                        }
                    }
                }
            }

            if (food_type == BOREK)
            {
                int lastBorekQty = edible_borek - qty;
                int neverTakeNumberBorek = neverTakeBorek.Count();

                if (neverTakeBorek.Contains(customer_id) && neverTakeNumberBorek <= edible_borek)
                {
                    if (neverTakeNumberBorek - 1 <= lastBorekQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (neverTakeNumberBorek <= edible_borek && neverTakeNumberBorek <= lastBorekQty)
                    {
                        return true;
                    }
                }
            }


            if (food_type == CAKE)
            {
                int lastCakeQty = edible_cake - qty;

                int neverTakeNumberCake = neverTakeCake.Count();

                if (neverTakeCake.Contains(customer_id) && neverTakeNumberCake <= edible_cake)
                {
                    if (neverTakeNumberCake - 1 <= lastCakeQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (neverTakeNumberCake <= edible_cake && neverTakeNumberCake <= lastCakeQty)
                    {
                        return true;
                    }
                }
            }


            if (food_type == DRINK)
            {
                int lastDrinkQty = edible_drink - qty;
                int neverTakeNumberDrink = neverTakeDrink.Count();
                //Console.WriteLine("Hiç Almayan drink {0} Kalan {1}", neverTakeNumberDrink, edible_drink);

                if (neverTakeDrink.Contains(customer_id) && neverTakeNumberDrink <= edible_drink)
                {
                    if (neverTakeNumberDrink - 1 <= lastDrinkQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (neverTakeNumberDrink <= edible_drink && neverTakeNumberDrink <= lastDrinkQty)
                    {
                        return true;
                    }
                }
            }



            return false;
        }

        public Boolean checkTraysEmpty()
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    // Empty check
                    if (trays[i, j] == 0 || trays[i, j] == 1)
                    {
                        int amount = trays[i, j];
                        //Console.WriteLine("Check i {0} j : {1} amount : {2}", i, j, amount);
                        if (fillTrays(i, j, amount) == false)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public Boolean checkTrayByIndex(int tray_no, int food_no, int qty)
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (i == tray_no && j == food_no)
                    {
                        if (trays[i, j] >= qty)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Boolean checkIndexZeroTray()
        {
            int counter = 0;
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {

                    if (trays[i, j] == 0)
                    {
                        counter++;
                    }

                }
            }

            if (counter < 9) return true;

            return false;
        }

        public void checkIndexZeroCustomer()
        {
            int rowLength = records.GetLength(0);
            int colLength = records.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {

                    if (records[i, j] == 0)
                    {
                        setCustomerRecord(i, j, 1);
                    }

                }
            }
        }

        public Boolean checkCustomerByIndex(int customer_id, int food_no, int qty)
        {
            int guestValue = Convert.ToInt32(records.GetValue(customer_id, food_no));
            int totalValue = guestValue + qty;
            //Console.WriteLine("Üyenin limiti {0}", guestValue);
            if (food_no == BOREK && guestValue <= 5 && qty <= 5 && totalValue <= 5)
            {
                return true;
            }

            if (food_no == DRINK && guestValue <= 5 && qty <= 5 && totalValue <= 5)
            {
                return true;
            }

            if (food_no == CAKE && guestValue <= 2 && qty <= 2 && totalValue <= 2)
            {
                return true;
            }

            return false;
        }

        public void displayTrays()
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            Console.WriteLine();
            ConsoleWriteLine("\n\n --- Trays ---", ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.WriteLine("         [Borek] [Cake] [Drink]");
            for (int i = 0; i < rowLength; i++)
            {
                int inc = i + 1;
                Console.Write("Tray " + inc + " -> ");
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format(" [{0}]    ", trays[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public void displayRecords()
        {
            int rowLength = records.GetLength(0);
            int colLength = records.GetLength(1);

            int borekTotal = 0;
            int cakeTotal = 0;
            int drinkTotal = 0;

            Console.WriteLine();
            ConsoleWriteLine("\n\n --- The amount of food consumed by customers ---", ConsoleColor.DarkCyan);
            Console.WriteLine();

            Console.WriteLine("             [Borek] [Cake] [Drink]");
            for (int i = 0; i < rowLength; i++)
            {
                int inc = i + 1;
                Console.Write("Customer " + inc + " -> ");
                for (int j = 0; j < colLength; j++)
                {
                    if (j == BOREK) borekTotal += records[i, j];
                    if (j == CAKE) cakeTotal += records[i, j];
                    if (j == DRINK) drinkTotal += records[i, j];

                    Console.Write(string.Format(" [{0}]    ", records[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            //Console.WriteLine("Borek : {0} Cake : {1} Drink : {2}",borekTotal,cakeTotal,drinkTotal);
        }

        public void ConsoleWriteLine(string message, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.WriteLine("[" + DateTime.Now + "] " + message + " ");
            Console.ResetColor();
        }

        public int findFoodByName(string name)
        {

            if (name == "borek")
            {
                return 0;
            }

            else if (name == "cake")
            {
                return 1;
            }
            else
            {
                return 2;
            }

        }

        public string findFoodNameById(int food)
        {

            if (food == 0)
            {
                return "Borek";
            }

            else if (food == 1)
            {
                return "Cake";
            }
            else
            {
                return "Drink";
            }

        }

    }
}
