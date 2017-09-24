using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zopa.LoanCalculator.Core;
using Moq;

namespace Zopa.LoanCalculator.Test
{
    [TestClass]
    public class LendersManagerTests
    {
        private Mock<IFileReader> fileReaderMock;
                
        [TestInitialize()]
        public void Initialize()
        {
            fileReaderMock = new Mock<IFileReader>();            
        }

        [TestMethod]
        public void Load_InputFileSuccessfully_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Lender,Rate,Available
Bob,0.075,6400
Jane,0.069,480
Fred,0.070,5200
Mary,0.104,1700
John,0.081,3200
Dave,0.074,1400
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual(1000, result.LoanAmount);
            Assert.AreEqual(36, result.LoanTerm);
            Assert.AreEqual(30.78, Math.Round(result.MonthlyRepayment, 2));
            Assert.AreEqual(0.07, result.Rate);

        }

        [TestMethod]
        public void Load_InputFileNotSuccessfullyLoaded_MissingData_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Lender,Rate,Available
Bob,0.075,6400
Jane,0.069,480
Fred,0.070,5200
Mary,0.104,1700
John,0.081,3200
Dave,0.074,
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual("Error in the lenders file or the requested loan amount: \n\r Input string was not in a correct format.", result.ErrorMessage);
            Assert.AreEqual(true, result.HasError);
            
        }

        [TestMethod]
        public void Load_InputFileHasNoHeader_ThereforeSkippingBestOffer_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Bob,0.070,6400
Jane,0.069,480
Fred,0.078,5200
Mary,0.104,1700
John,0.081,3200
Dave,0.074,444
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual(1000, result.LoanAmount);
            Assert.AreEqual(36, result.LoanTerm);
            Assert.AreEqual(31.12, Math.Round(result.MonthlyRepayment, 2));
            Assert.AreEqual(0.078, result.Rate);
        }

        [TestMethod]
        public void Load_InputFileNotSuccessfullyLoaded_AmountNotDouble_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Lender,Rate,Available
Bob,0.075,6400
Jane,0.069,480
Fred,0.070,5200
Mary,0.104,1700
John,0.081,3200
Dave,0.074,abc
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual("Error in the lenders file or the requested loan amount: \n\r Input string was not in a correct format.", result.ErrorMessage);
            Assert.AreEqual(true, result.HasError);

        }

        [TestMethod]
        public void GetBestQuote_ErrorDuringLoading_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Lender,Rate,Available
Bob,0.075,6400
Jane,0.069,480
Fred,0.070,5200
Mary,0.104,1700
John,0.081,3200
Dave,0.074
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual("Error in the lenders file or the requested loan amount: \n\r Index was outside the bounds of the array.", result.ErrorMessage);
            Assert.AreEqual(true, result.HasError);

        }

        [TestMethod]
        public void GetBestQuote_NoLenderWasFound_Test()
        {
            //Arrange
            var loanAmount = 1000;
            var loanTerm = 36;
            var numberOfPaymentsPerYear = 12;
            string fileContent =
@"Lender,Rate,Available
Bob,0.075,640
Jane,0.069,480
Fred,0.070,520
Mary,0.104,170
John,0.081,320
Dave,0.074,344
Angela,0.069,600";

            fileReaderMock.Setup(foo => foo.ReadLines("test.csv")).Returns(fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            var lendersMgr = new LendersManager(fileReaderMock.Object, new Calculator(), "test.csv");

            //Act
            var result = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            //Assert
            Assert.AreEqual("Error in the lenders file or the requested loan amount: \n\r It is not possible to provide a quote at this time!", result.ErrorMessage);
            Assert.AreEqual(true, result.HasError);

        }
    }
}
