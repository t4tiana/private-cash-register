using Capstone.Classes;
using System;

namespace Capstone
{
    class Program
    {
        /* This cash register application is designed for use by the sales representatives
         * of a fictional candy company. 
         * It keeps track of inventory, sales, money received, and money returned*/
        static void Main(string[] args)
        {
            UserInterface consoleInterface = new UserInterface();
            consoleInterface.Run();
        }
    }
}
