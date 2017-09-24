using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zopa.LoanCalculator.Core;
using Moq;

namespace Zopa.LoanCalculator.Test
{
    [TestClass]
    public class LoanOfferTests
    {
        [TestMethod]
        public void Print_PrintError_Test()
        {
            //Arrange
            var loanOffer = new LoanOffer("Some Error");
           
            int counter = 0;

            var mockDelegate = new Mock<Action<string, object>>();
            mockDelegate.Setup(x => x("Error in the lenders file or the requested loan amount: \n\r Some Error", null)).Callback<string, object>((s, obj) => { counter++; });            

            //Act
            loanOffer.Print(mockDelegate.Object);

            mockDelegate.Verify(foo => foo("Error in the lenders file or the requested loan amount: \n\r Some Error", null), Times.Once);
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void Print_Successful_Results_Test()
        {
            //Arrange
            var loanOffer = new LoanOffer(1500, 0.06, 30.78, 36);

            int counter = 0;

            var mockDelegate = new Mock<Action<string, object>>();
            mockDelegate.Setup(x => x("Requested amount: £1500\r\nRate: 6.0%\r\nMonthly repayment: £30.78\r\nTotal repayment: £1,108.08", null)).Callback<string, object>((s, obj) => { counter++; });

            //Act
            loanOffer.Print(mockDelegate.Object);

            mockDelegate.Verify(foo => foo("Requested amount: £1500\r\nRate: 6.0%\r\nMonthly repayment: £30.78\r\nTotal repayment: £1,108.08", null), Times.Once);
            Assert.AreEqual(1, counter);
        }
    }
}
