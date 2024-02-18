using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileAccess
    {
        public DateTime DateAndTime { get; } = DateTime.Now;

        string fileName = @"C:\Users\tiana\Software\inventory.csv";
        public List<Candy> ReadInventory()
        {
            List<Candy> result  = new List<Candy>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] items = line.Split("|");

                    Candy candy = new Candy();
                    candy.ShortProductType = items[0];
                    candy.Id = items[1];
                    candy.Name = items[2];
                    candy.Price = decimal.Parse(items[3]);
                    candy.Wrapped = items[4];
                    candy.Quantity = 100;

                    result.Add(candy);
                }
            }
            return result;
        }
        public void WritePurchaseToLog(Candy candy, int quantity, decimal balance)
        {
            try
            {
                //has append set to true so that transactions are not overwritten
                using (StreamWriter sw = new StreamWriter(@"C:\Users\tiana\Software\cash-register-c-sharp\audit.txt", true ))
                {
                    string message = $"{quantity} {candy.Name} {candy.Id}";
                    decimal subtotal = candy.Price * quantity;
                    string line = $"{DateAndTime} {message} ${subtotal} ${balance}";
                    sw.WriteLine(line);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred" + ex.Message);
            }
        }

        public void WriteDepositToLog(decimal amount, decimal balance)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\tiana\Software\cash-register-c-sharp\audit.txt", true))
                {
                    string message = "MONEY RECEIVED: ";
                    string line = $"{DateAndTime} {message} ${amount} ${balance}";
                    sw.WriteLine(line);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred" + ex.Message);
            }
        }
        public void WriteChangeGivenToLog(decimal change, decimal balance)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\tiana\Software\cash-register-c-sharp\audit.txt", true))
                {
                    string message = "CHANGE GIVEN: ";
                    string line = $"{DateAndTime} {message} ${change} ${balance}";
                    sw.WriteLine(line);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred" + ex.Message);
            }
        }
    }
}
