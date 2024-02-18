using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CapstoneTests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void StoreTestObjectCreation()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)

            //Assert
            Assert.IsNotNull(testObject);
        }

        [TestMethod]
        public void StockStartsAt100()
        {
            //Arrange
            Store testObject = new Store();

            //Act
            int result = testObject.inventoryList[2].Quantity;

            //Assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void PurchaseReducesQuantity()
        {
            //Arrange
            Store testObject = new Store();
            testObject.CustomerBalance = 100;
            string quantity = "20";
            int index = 0;


            //Act
            string input = testObject.ValidateFunds(quantity, index);
            int result = testObject.inventoryList[index].Quantity;

            //Assert
            Assert.AreEqual(80, result);
        }

        [TestMethod]
        public void PurchaseReducesBalance()
        {
            //Arrange
            Store testObject = new Store();
            testObject.CustomerBalance = 100.00M;
            string quantity = "20";
            int index = 0;

            //Act
            testObject.ValidateFunds(quantity, index);
            
            //Assert
            Assert.AreEqual(73.00M, testObject.CustomerBalance);
        }

        [TestMethod]
        public void CalculateCorrectChange()
        {
            //Arrange
            Store testObject = new Store();
            decimal changeDue = 22.30M;
            List<int> changeList = new List<int>();

            //Act
            changeList = testObject.CalculateChange(changeDue);

            //Assert
            CollectionAssert.AreEqual(new List<int> { 1,0,0,2,1,0,1 }, changeList);
        }
    }
}
