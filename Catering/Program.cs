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
                                        { 0, 0 ,0 },
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
            }else if(food_type == CAKE)
            {
                edible_cake -= amount;
            }
            else if (food_type == DRINK)
            {
                edible_drink -= amount;
            }

            // Yemek yiyen kullanıcıyı listeye ekle
            if (!had_eaten_guests.Contains(guest_id))
            {
                had_eaten_guests.Add(guest_id);
            }

            /*
            for (int i = 0; i < had_eaten_guests.Count(); i++)
            {
                Console.WriteLine("List in {0}", had_eaten_guests[i]);
            }
            */

            //Console.WriteLine("After Guest {0}", records.GetValue(guest_id, food_type));

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

        public Boolean checkGuestByIndex(int guest_id, int food_no, int qty)
        {
            int guestValue = Convert.ToInt32(records.GetValue(guest_id, food_no));
            int totalValue = guestValue + qty;
            //Console.WriteLine("Üyenin limiti {0}", guestValue);
            if (food_no == BOREK && guestValue <= 5 && qty<=5 && totalValue<=5)
            {
                return true;
            }

            if (food_no == DRINK && guestValue <= 5 && qty <=5 && totalValue <= 5)
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
            //Console.WriteLine("Fill Trays Tray : {0} Food : {1}, Amount : {2}", tray_no, food_no, amount);

            int selected_food = 0;

            if(food_no == BOREK)
            {
                selected_food = borek_loop;
            }
            else if(food_no == CAKE)
            {
                selected_food = cake_loop;
            }
            else if(food_no == DRINK)
            {
                selected_food = drink_loop;
            }

            

            if(selected_food != 0)
            {
                //Console.WriteLine("Tray Update Start");
                //displayTrays();
                int result = 0;
                int oldVal = Convert.ToInt32(trays.GetValue(tray_no, food_no));
                int addVal = 0;

                if(amount == 1)
                {
                    addVal = 4;
                    result = oldVal + 4;
                        
                }
                else if(amount == 0)
                {
                    addVal = 5;
                    result = oldVal + 5;
                }
                else
                {
                    result = 5;
                }

                if (food_no == BOREK)
                {
                    borek_loop-=result;
                }
                else if (food_no == CAKE)
                {
                     cake_loop-=result;
                }
                else if(food_no == DRINK)
                {
                    drink_loop-=result;
                }

                
                trays.SetValue(result, tray_no, food_no);
                Console.WriteLine("***** Tray Updated *****");
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

            Console.WriteLine("\nGuest Eaten Foods (First Column: Borek, Second: Cake : Third : Drink)\n");
            for (int i = 0; i < rowLength; i++)
            {
                int inc = i + 1;
                Console.Write("Guest " + inc + " -> ");
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", records[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
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

        public Boolean MinEatingFood(int guest_id, int food_type, int qty)
        {
            int yemegen_sayisi = 10 - had_eaten_guests.Count();
            int kalacak_yiyecek = edible_borek - qty;
            /*
            if (edible_borek >= yemegen_sayisi  && kalacak_yiyecek <= yemegen_sayisi)
            {
                return true;
            }
            */

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


                        if (food_type == DRINK)
                        {
                            if (!hicAlmayanlarDrink.Contains(i))
                            {
                                hicAlmayanlarDrink.Add(i);
                            }
                        }

                    }
                }
            }

            if (food_type == BOREK)
            {
                int hicAlmayanlarinSayisiBorek = hicAlmayanlarBorek.Count();
                Console.WriteLine("Borek almayan {0} kalan börek {1}", hicAlmayanlarinSayisiBorek, edible_borek);
                if (hicAlmayanlarinSayisiBorek <= edible_borek)
                {
                    return true;
                }
            }


            if (food_type == CAKE)
            {
                int hicAlmayanlarinSayisiCake = hicAlmayanlarCake.Count();
                Console.WriteLine("Cake almayan {0} kalan cake {1}", hicAlmayanlarinSayisiCake, edible_cake);
                if (hicAlmayanlarinSayisiCake <= edible_cake)
                {
                    return true;
                }
            }


            if (food_type == DRINK)
            {
                int hicAlmayanlarinSayisiDrink = hicAlmayanlarDrink.Count();

                Console.WriteLine("Drink almayan {0} kalan drink {1}", hicAlmayanlarDrink, edible_drink);
                if (hicAlmayanlarinSayisiDrink <= edible_drink )
                {
                    return true;
                }
            }


            return false;
        }

    }
    class Program
    {

        static void Main(string[] args)
        {
            Catering cat = new Catering();
            int guest_id,tray,food,qty;
            cat.displayTrays();
            //cat.displayRecords();
            int loopCounter = 0;
            while (true)
            {

                if (cat.checkIndexZeroTray())
                {

               // Console.WriteLine("Loop {0}", loopCounter);
                Random randomG = new Random();
                int randomGuest = randomG.Next(0, 10);
                //Console.WriteLine("Guest {0}", randomGuest);

                //Console.WriteLine("Yemek yiyen sayısı {0}", cat.checkMinEatingFood(1));
                cat.checkTraysEmpty();

                Console.Write("Which Guest(1-10) : ");
                guest_id = Convert.ToInt32(Console.ReadLine())-1;
                //guest_id = randomGuest;

                int fail_counter = 0;
                while (true)
                {
                    cat.checkTraysEmpty();

                    Random randomT = new Random();
                    int randomTray = randomT.Next(0, 3);

                    Random randomF = new Random();
                    int randomFood = randomF.Next(0, 3);

                    Random randomQ = new Random();
                    int randomQty = randomQ.Next(1, 1);

                    Console.Write("Which Tray (1-3) : ");
                    tray = Convert.ToInt32(Console.ReadLine())-1;
                    //tray = randomTray;

                    Console.Write("Which Food (borek, cake, drink) : ");
                    string foodName = Console.ReadLine();
                    food = cat.findFoodByName(foodName);
                    //food = randomFood;

                    Console.Write("How Many? :  ");
                    qty = Convert.ToInt32(Console.ReadLine());

                    //qty = randomQty;

                    
                    if (cat.checkGuestByIndex(guest_id, food, qty))
                    {
                        if (cat.checkTrayByIndex(tray, food, qty))
                        {
                            if (cat.MinEatingFood(guest_id, food, qty))
                            {
                            cat.setGuestRecord(guest_id, food, qty);
                            cat.setTrayRecord(tray, food, qty);
                            Console.WriteLine("Kalan Borek {0}, Cake {1}, Drink {2}", cat.edible_borek, cat.edible_cake, cat.edible_drink);
                            break;
                            }
                            else
                            {
                                //fail_counter++;
                                //if (fail_counter > 5) break;
                                Console.WriteLine("Tepsi de alabileceğiniz kadar miktar yok. Tekrar deneyiniz");
                                continue;
                            }
                        }
                        else
                        {
                            //fail_counter++;
                            //if (fail_counter > 5) break;
                            Console.WriteLine("Tepsi de alabileceğiniz kadar miktar yok. Tekrar deneyiniz");
                            continue;
                        }
                    }
                    else
                    {
                        //fail_counter++;
                        //if (fail_counter > 5) break;
                        Console.WriteLine("Bu müşterinin limiti dolmuştur. Tekrar deneyiniz");
                        continue;
                    }
                   
                  
           
                }

                cat.checkTraysEmpty();

                //Console.WriteLine("Guest : {0}, Food : {1},Tray : {2}  Qty : {3}", guest_id,food,tray, qty);

                //cat.setGuestRecord(guest_id,food, qty);
                loopCounter++;

                //cat.displayRecords();
                cat.displayTrays();
                Console.WriteLine("--------------");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Tüm yiyecek ve içecekler bitmiştir..");
            Console.ReadKey();
        }
    }
}
