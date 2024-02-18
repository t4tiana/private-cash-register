using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileAccess
    {
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
        //write to log
    }
}
