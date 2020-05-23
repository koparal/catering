using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{

    class Catering
    {
        const int BOREK = 0;
        const int CAKE = 1;
        const int DRINK = 2;

        public int edible_borek = 30;
        public int edible_cake = 15;
        public int edible_drink = 30;

        List<int> had_eaten_guests = new List<int>();

        public int borek_loop = 15;
        public int cake_loop = 0;
        public int drink_loop = 15;

        public int[,] trays = new int[3, 3] {
                                        { 5, 5 ,5 },
                                        { 5, 5, 5 },
                                        { 5, 5 ,5 }
                                    };

        public int[,] records= new int[10, 3] {
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


        public void setTrayRecord(int tray_no,int food_no,int amountFood)
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

        public void setGuestRecord(int guest_id, int food_type, int amount)
        {
            // Get Old Value
            int oldVal = Convert.ToInt32(records.GetValue(guest_id, food_type));

            // Sum of new value and old value
            int result = oldVal + amount;

            //Console.WriteLine("Before Guest {0}", records.GetValue(guest_id, food_type));

            // Set value
            records.SetValue(result,guest_id, food_type);

            if(food_type == BOREK)
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
            if (!had_eaten_guests.Contains(guest_id))
            {
                had_eaten_guests.Add(guest_id);
            }

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
                        if(fillTrays(i, j, amount) == false )
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public Boolean checkTrayByIndex(int tray_no, int food_no,int qty)
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if(i == tray_no && j == food_no)
                    {
                        if(trays[i, j] >= qty)
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

        public void checkIndexZeroGuest()
        {
            int rowLength = records.GetLength(0);
            int colLength = records.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {

                    if (records[i, j] == 0)
                    {
                        setGuestRecord(i, j, 1);
                    }

                }
            }
        }

        public Boolean checkGuestByIndex(int guest_id, int food_no, int qty)
        {
            int guestValue = Convert.ToInt32(records.GetValue(guest_id, food_no));
            int totalValue = guestValue + qty;
            //Console.WriteLine("Üyenin limiti {0}", guestValue);
            if (food_no == BOREK && guestValue <= 5 && qty<=5 && totalValue<=5)
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

            if(food_no == DRINK)
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

                    if (selected_food < addVal) {
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
                ConsoleWriteLine("**** Tray [" + display_tray + "] Updated *** ", ConsoleColor.DarkYellow);
                //displayTrays();

            }
            else
            {
                //Console.WriteLine("Tray Not Updated. Because {0} Tray Count",selected_food);
                return false;
            }
            return true;

        }

        public void displayTrays()
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            Console.WriteLine("\nTrays (First Column: Borek, Second: Cake : Third : Drink)\n");
            for (int i = 0; i < rowLength; i++)
            {
                int inc = i + 1;
                Console.Write("Tray " + inc + " -> ");
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", trays[i, j]));
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
            for (int i = 0; i < rowLength; i++)
            {
                int inc = i + 1;
                Console.Write("Guest " + inc + " -> ");
                for (int j = 0; j < colLength; j++)
                {
                    if(j == BOREK) borekTotal += records[i, j];
                    if(j == CAKE) cakeTotal += records[i, j];
                    if(j == DRINK) drinkTotal += records[i, j];

                    Console.Write(string.Format("{0} ", records[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            //Console.WriteLine("Borek : {0} Cake : {1} Drink : {2}",borekTotal,cakeTotal,drinkTotal);
        }

        public int findFoodByName(string name)
        {

            if(name == "borek")
            {
                return 0;
            } 

            else if(name == "cake")
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

        public Boolean minEatingFood(int guest_id, int food_type, int qty)
        {
            List<int> hicAlmayanlarBorek = new List<int>();
            List<int> hicAlmayanlarCake = new List<int>();
            List<int> hicAlmayanlarDrink = new List<int>();


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
                            if (!hicAlmayanlarBorek.Contains(i))
                            {
                                hicAlmayanlarBorek.Add(i);
                            }
                        }


                        if (food_type == CAKE)
                        {
                            if (!hicAlmayanlarCake.Contains(i))
                            {
                                hicAlmayanlarCake.Add(i);
                            }
                        }


                       

                    }
                }
            }

            if (food_type == BOREK)
            {
                int kalacakBorekQty = edible_borek - qty;
                int hicAlmayanlarinSayisiBorek = hicAlmayanlarBorek.Count();

                if (hicAlmayanlarBorek.Contains(guest_id) && hicAlmayanlarinSayisiBorek <= edible_borek)
                {
                    if (hicAlmayanlarinSayisiBorek-1 <= kalacakBorekQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (hicAlmayanlarinSayisiBorek <= edible_borek && hicAlmayanlarinSayisiBorek <= kalacakBorekQty)
                    {
                        return true;
                    }
                }
            }


            if (food_type == CAKE)
            {
                int kalacakCakeQty = edible_cake - qty;

                int hicAlmayanlarinSayisiCake = hicAlmayanlarCake.Count();

                if (hicAlmayanlarCake.Contains(guest_id) && hicAlmayanlarinSayisiCake <= edible_cake)
                {
                    if (hicAlmayanlarinSayisiCake - 1 <= kalacakCakeQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (hicAlmayanlarinSayisiCake <= edible_cake && hicAlmayanlarinSayisiCake <= kalacakCakeQty)
                    {
                        return true;
                    }
                }
            }


            if (food_type == DRINK)
            {
                int kalacakDrinkQty = edible_drink - qty;
                int hicAlmayanlarinSayisiDrink = hicAlmayanlarDrink.Count();
                //Console.WriteLine("Hiç Almayan drink {0} Kalan {1}", hicAlmayanlarinSayisiDrink, edible_drink);

                if (hicAlmayanlarDrink.Contains(guest_id) && hicAlmayanlarinSayisiDrink <= edible_drink)
                {
                    if (hicAlmayanlarinSayisiDrink - 1 <= kalacakDrinkQty)
                    {
                        return true;
                    }
                }
                else
                {
                    if (hicAlmayanlarinSayisiDrink <= edible_drink && hicAlmayanlarinSayisiDrink <= kalacakDrinkQty)
                    {
                        return true;
                    }
                }
            }



            return false;
        }

        public void ConsoleWriteLine(string message, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.WriteLine("[" + DateTime.Now + "] " + message + " ");
            Console.ResetColor();
        }


    }
    class Program
    {

        static void Main(string[] args)
        {
            Catering cat = new Catering();
            int guest_id,tray,food,qty;
            cat.displayTrays();

            while (true)
            {

                if (cat.checkIndexZeroTray())
                {

                Random randomG = new Random();
                int randomGuest = randomG.Next(0, 10);

                //Console.Write("Which Guest(1-10) : ");
                //guest_id = Convert.ToInt32(Console.ReadLine())-1;
                guest_id = randomGuest;

                while (true)
                {
                    cat.checkTraysEmpty();

                    int randomTray = randomG.Next(0, 3);

                    int randomFood = randomG.Next(0,3);

                    int randomQty = randomG.Next(1, 2);

                    //Console.Write("Which Tray (1-3) : ");
                    //tray = Convert.ToInt32(Console.ReadLine())-1;
                    tray = randomTray;

                    //Console.Write("Which Food (borek, cake, drink) : ");
                    //string foodName = Console.ReadLine();
                    //food = cat.findFoodByName(foodName);
                    food = randomFood;

                    //Console.Write("How Many? :  ");
                    //qty = Convert.ToInt32(Console.ReadLine());

                    qty = randomQty;
                    //Console.WriteLine("Guest {0} Tray {1} Food {2} QTY {3}", guest_id, tray, food, qty);

                    if (cat.minEatingFood(guest_id, food, qty))
                    {
                        if (cat.checkGuestByIndex(guest_id, food, qty))
                        {
                            if (cat.checkTrayByIndex(tray, food, qty))
                            {
                                cat.setGuestRecord(guest_id, food, qty);
                                cat.setTrayRecord(tray, food, qty);
                                cat.ConsoleWriteLine("Customer ["+guest_id+"] ate "+qty+" "+cat.findFoodNameById(food) + "", ConsoleColor.DarkGreen);
                                break;
                            }
                            else
                            {
                             //Console.WriteLine("\n Tepsi de alabileceğiniz kadar miktar yok. Tekrar deneyiniz");
                             break;
                            }
                        }
                        else
                        {
                            //Console.WriteLine("\n Bu müşterinin limiti dolmuştur. Tekrar deneyiniz");
                            break;
                        }
                    }
                    else
                    {
                        //Console.WriteLine("\n Min yiyecek limiti dışında kaldınız.");
                        break;
                    }
                }

                }
                else
                {
                    cat.checkIndexZeroGuest();
                    break;
                }
            }

            cat.displayRecords();
            cat.displayTrays();

            cat.ConsoleWriteLine("**** Tüm yiyecek ve içecekler bitmiştir.. *** ", ConsoleColor.DarkGreen);
            Console.ReadKey();
        }
    }
}
