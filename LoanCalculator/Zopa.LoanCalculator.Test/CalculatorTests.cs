using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zopa.LoanCalculator.Core;

namespace Zopa.LoanCalculator.Test
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void GetMonthlyPayment_Test()
        {
            //Arrange
            var calc = new Calculator();

            //Act
            var monthlyPayment = calc.GetMonthlyPayment(1000, 0.07, 36, 12);

            Assert.AreEqual(30.78, Math.Round(monthlyPayment,2));
        }
    }
}
