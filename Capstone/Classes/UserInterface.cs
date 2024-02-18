using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class UserInterface
    {
        private Store store = new Store();
        public void Run()
        {
            Console.WriteLine("Welcome to the Silva Sisters Candy Co.");

            bool done = false;

            while (!done)
            {
                MainMenu();
                string userResponse = Console.ReadLine();
                switch (userResponse)
                {
                    case "1":
                        PrintInventory(store.inventoryList);
                        break;
                    case "2":
                        SubMenu(store);
                        break;
                    case "3":
                        Console.WriteLine("See you next time!");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Input error. Try again");
                        break;
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please make a selection");
            Console.WriteLine("(1) Show Inventory");
            Console.WriteLine("(2) Make Sale");
            Console.WriteLine("(3) Quit");
        }

        public void PrintInventory(List<Candy> candyList)
        {
            Console.WriteLine("ID".PadRight(12) + "Name".PadRight(25) + "Wrapper?".PadRight(13) + "Qty".PadRight(10) + "Price");

            candyList.Sort((x, y) => x.Id.CompareTo(y.Id));

            foreach (Candy c in candyList)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine();
        }

        public void SubMenu(Store store)
        {
            bool endSale = false;

            while (!endSale)
            {
                Console.WriteLine();
                Console.WriteLine("You are now in the SUBMENU");
                Console.WriteLine("(1) Add Customer Balance");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Purchase");
                Console.WriteLine("Current Customer Balance: " + "$" + store.CustomerBalance);
                string subMenuResponse = Console.ReadLine();

                switch (subMenuResponse)
                {
                    case "1":
                        Console.Write("Please enter the amount to add to the customer balance: ");
                        string addedBalance = Console.ReadLine();
                        string result = store.AddToBalance(addedBalance);
                        if (result != "Success")
                            Console.WriteLine(result);
                        break;
                    case "2":
                        PrintInventory(store.inventoryList);
                        Console.Write("Enter product code: ");
                        string productCode = Console.ReadLine();
                        int itemIndex = store.InputProductCode(productCode);
                        if (itemIndex < 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid selection. Try again");
                        }
                        else
                        {
                            Console.Write("Enter quantity: ");
                            string userQuantity = Console.ReadLine();
                            string validQty = store.ValidateQuantity(userQuantity, itemIndex);
                            if (validQty == "Success")
                            {
                                string validBalance = store.ValidateFunds(userQuantity, itemIndex);
                                if (validBalance != "Success")
                                {
                                    Console.WriteLine(validBalance);
                                }
                            }
                            else
                            {
                                Console.WriteLine(validQty);
                            }
                        }
                        break;
                    case "3":
                        List<int> receipt = GenerateReceipt(store.SendCart());
                        PrintChange(receipt);
                        store.CustomerBalance = 0.00M;
                        store.ClearCart();
                        endSale = true;
                        break;
                    default:
                        Console.WriteLine("Input error. Please try again");
                        Console.WriteLine();
                        break;
                }
            }
        }
        public List<int> GenerateReceipt(List<Candy> customerCart)
        {
            decimal grandTotal = 0.00M;

            Console.WriteLine();
            Console.WriteLine("Qty".PadRight(15) + "Name".PadRight(20) + "Product Type".PadRight(30) +
                "Price".PadRight(15) + "Subtotal");

            foreach (Candy item in customerCart)
            {
                decimal subTotal = item.Quantity * item.Price;
                grandTotal += subTotal;
                Console.WriteLine(item.Quantity.ToString().PadRight(10) + item.Name.PadRight(20) +
                    item.LongProductType.PadRight(35) + item.Price.ToString().PadRight(15) + subTotal);
            }
            Console.WriteLine();
            Console.WriteLine("Grand total: " + grandTotal);
            Console.WriteLine();
            Console.WriteLine("Change due: " + store.CustomerBalance);

            List<int> result = store.CalculateChange(store.CustomerBalance);
            return result;
        }
        private void PrintChange(List<int> listOfChange)
        {
            List<string> namesOfDenominations = new List<string>
            {
                "Twenties",
                "Tens",
                "Fives",
                "Ones",
                "Quarters",
                "Dimes",
                "Nickels"
            };

            for (int i = 0; i < listOfChange.Count; i++)
            {
                if (listOfChange[i] > 0)
                    Console.Write($"({listOfChange[i]}) {namesOfDenominations[i]} ");
            }
            Console.WriteLine();
        }
    }
}
