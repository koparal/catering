using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{

    class Catering
    {

        public int guests = 2;
        public int borek = 30;
        public int cake = 15;
        public int drink = 30;

        const int BOREK = 0;
        const int CAKE = 1;
        const int DRINK = 2;

        int BOREK_LOOP = 2;
        int CAKE_LOOP = 1;
        int DRINK_LOOP = 2;

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

            Console.WriteLine("Tray Before {0}", trays.GetValue(tray_no, food_no));

            // Set value
            trays.SetValue(resultTray, tray_no, food_no);

            Console.WriteLine("Tray After {0}", trays.GetValue(tray_no, food_no));

        }

        public void setGuestRecord(int guest_id, int food_type, int amount)
        {
            // Get Old Value
            int oldVal = Convert.ToInt32(records.GetValue(guest_id, food_type));

            // Sum new value and old value
            int result = oldVal + amount;

            Console.WriteLine("Before Guest {0}", records.GetValue(guest_id, food_type));

            // Set value
            records.SetValue(result,guest_id, food_type);

            Console.WriteLine("After Guest {0}", records.GetValue(guest_id, food_type));

        }

        public void displayRecords()
        {
            int rowLength = records.GetLength(0);
            int colLength = records.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", records[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public void checkTraysEmpty()
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
                        Console.WriteLine("Check i {0} j : {1} amount : {2}", i, j, amount);
                        fillTrays(i, j, amount);
                    }
                }
            }
        }

        public void fillTrays(int tray_no, int food_no, int amount)
        {
            Console.WriteLine("Fill Trays Tray : {0} Food : {1}, Amount : {2}", tray_no, food_no, amount);

            int selected_food = 0;

            if(food_no == BOREK)
            {
                selected_food = BOREK;
            }else if(food_no == CAKE)
            {
                selected_food = CAKE;
            }
            else
            {
                selected_food = DRINK;
            }
            if(selected_food != 0)
            {
                Console.WriteLine("Tray Update Start");
                displayTrays();
                int result = 0;
                int oldVal = Convert.ToInt32(trays.GetValue(tray_no, food_no));

                if(amount == 1)
                {
                    result = oldVal + 4;
                        
                }
                else if(amount == 0)
                {
                    result = oldVal + 5;
                }
                else
                {
                    result = 5;
                }

                if (food_no == BOREK)
                {
                    BOREK_LOOP--;
                }
                else if (food_no == CAKE)
                {
                     CAKE_LOOP--;
                }
                else
                {
                    DRINK_LOOP--;
                }

                
                trays.SetValue(result, tray_no, food_no);
                Console.WriteLine("Tray Updated");
                displayTrays();
            }
        }

        public void displayTrays()
        {
            int rowLength = trays.GetLength(0);
            int colLength = trays.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", trays[i, j]));
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

    }
    class Program
    {

        static void Main(string[] args)
        {

            /*
            $arr = [
                ["5","5","5"],
                ["5","5","5"],
                ["5","5","5"],
            ];
            */


            Catering cat = new Catering();

            
            while (true)
            {
                Console.Write("Which Guest(1-10) : ");
                int guest_id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Which Tray (0-2) : ");
                int tray = Convert.ToInt32(Console.ReadLine());

                Console.Write("Which Food (borek, cake, drink) : ");
                string foodName = Console.ReadLine();
                int food = cat.findFoodByName(foodName);

                Console.Write("How Many? :  ");
                int qty = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Guest : {0}, Food : {1},Tray : {2}  Qty : {3}", guest_id,food,tray, qty);

                cat.setGuestRecord(guest_id,food, qty);
                cat.setTrayRecord(tray,food, qty);

                Console.WriteLine("--------------");
            }
            
            Console.ReadKey();
        }
    }
}
