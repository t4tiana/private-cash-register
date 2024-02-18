using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Capstone.Classes
{
    public class Store
    {
        public List<Candy> inventoryList { get; set; } = new List<Candy>();
        public decimal CustomerBalance { get; set; } = 0.00M;

        ShoppingCart cart = new ShoppingCart();

        private FileAccess fileAccess;

        public Store()
        {
            fileAccess = new FileAccess();
            inventoryList = fileAccess.ReadInventory();
        }
        public string AddToBalance(string money)
        {
            string message = ValidateBalanceInput(money);
            if (message == "Balance added successfully")
            {
                CustomerBalance += int.Parse(money);
                decimal deposit = decimal.Parse(money);
                fileAccess.WriteDepositToLog(deposit, CustomerBalance);
                return "Success";
            }
            else
            {
                return message;
            }
        }

        private string ValidateBalanceInput(string money)
        {
            try
            {
                int intMoney = int.Parse(money);
                if (intMoney < 0)
                {
                    return "Please enter a valid amount";
                }
                else if (intMoney > 100)
                {
                    return "The limit is $100 per deposit. Please try again";
                }
                else if (intMoney + CustomerBalance > 1000)
                {
                    return "The maximum balance is $1000. Please try again";
                }
                else
                {
                    return "Balance added successfully";
                }
            }
            catch (FormatException)
            {
                return "Please enter a valid amount";

            }
            catch (ArgumentNullException)
            {
                return "Please enter a valid amount";

            }
        }

        public int InputProductCode(string productCode)
        {
            int result = ValidateProductCode(productCode);
            if (result < 0)
            {
                return -1;
            }
            else
            {
                return result;
            }

        }

        private int ValidateProductCode(string code)
        {
            int indexOfCandy = -1;
            try
            {
                for (int i = 0; i < inventoryList.Count; i += 1)
                {
                    if (code.ToUpper() == inventoryList[i].Id)
                    {
                        indexOfCandy = i;
                        if (inventoryList[indexOfCandy].Quantity == 0)
                        {
                            return -1;
                        }
                    }
                }
            }
            catch (FormatException)
            {
                return -1;
            }
            catch (ArgumentNullException)
            {
                return -1;
            }
            return indexOfCandy;
        }

        public string ValidateQuantity(string quantity, int i)
        {
            try
            {
                int intQuantity = int.Parse(quantity);

                if (intQuantity < 0)
                    return "Please enter a valid quantity";
                else if (intQuantity > inventoryList[i].Quantity)
                {
                    return "Not enough stock. Try a different quantity";
                }
                else
                {
                    return "Success";
                }
            }
            catch (FormatException)
            {
                return "Invalid input. Try again.";
            }
        }

        public string ValidateFunds(string quantity, int i)
        {
            int intQuantity = int.Parse(quantity);
            decimal subTotal = intQuantity * inventoryList[i].Price;

            if (CustomerBalance >= subTotal)
            {
                CustomerBalance -= subTotal;
                inventoryList[i].Quantity -= intQuantity;
                AddToCart(inventoryList[i], int.Parse(quantity));
                fileAccess.WritePurchaseToLog(inventoryList[i], int.Parse(quantity), CustomerBalance);
                return "Success";
            }
            else
            {
                return "ERROR: Insufficient balance.";
            }
        }

        public void AddToCart(Candy candy, int quantity)
        {
            Candy candyInCart = new Candy();
            candyInCart.ShortProductType = candy.ShortProductType;
            candyInCart.Id = candy.Id;
            candyInCart.Name = candy.Name;
            candyInCart.Price = candy.Price;
            candyInCart.Wrapped = candy.Wrapped;
            candyInCart.Quantity = quantity;

            cart.shoppingCart.Add(candyInCart);
        }

        public List<Candy> SendCart()
        {
            return cart.shoppingCart;
        }

        public List<int> CalculateChange(decimal change)
        {
            fileAccess.WriteChangeGivenToLog(change, 0.00M);

            int intChange = (int)(change * 100);

            List<int> denominations = new List<int> { 2000, 1000, 500, 100, 25, 10, 5 };

            List<int> count = new List<int> { };

            while (intChange > 0)
            {
                for (int i = 0; i < denominations.Count; i++)
                {
                    int remainder = intChange % denominations[i];
                    int billCount = intChange / denominations[i];
                    count.Add(billCount);
                    intChange = remainder;
                }
            }
            return count;
        }

        public void ClearCart()
        {
            cart.shoppingCart.Clear();
        }
    }
}