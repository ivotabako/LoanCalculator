using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zopa.LoanCalculator.Core;
using Moq;

namespace Zopa.LoanCalculator.Test
{
    [TestClass]
    public class InputValidatorTests
    {
        private Mock<IFileReader> fileReaderMock;

        [TestInitialize()]
        public void Initialize()
        {
            fileReaderMock = new Mock<IFileReader>();
        }

        [TestMethod]
        public void IsInputValid_ArgumentsAreNotTwo_Test()
        {
            //Arrange
            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv"});

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsInputValid_FileDoesNotExist_Test()
        {
            //Arrange
            
            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(false);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv" ,"1000"});

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsInputValid_FileExists_Test()
        {
            //Arrange

            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(true);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv", "1000" });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsInputValid_LoanAmount_NotInteger_Test()
        {
            //Arrange

            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(true);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv", "abc" });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsInputValid_LoanAmount_OutOfRange_Test()
        {
            //Arrange

            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(true);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv", "900" });
            Assert.AreEqual(false, result);

            //Act
            result = inputValidator.IsInputValid(new[] { "test.csv", "16000" });
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsInputValid_LoanAmount_InvalidAmount_Test()
        {
            //Arrange

            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(true);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv", "9050" });
            Assert.AreEqual(false, result);
            
        }

        [TestMethod]
        public void IsInputValid_LoanAmount_ValidAmount_Test()
        {
            //Arrange

            fileReaderMock.Setup(foo => foo.Exists("test.csv")).Returns(true);

            var inputValidator = new InputValidator(fileReaderMock.Object);

            //Act
            var result = inputValidator.IsInputValid(new[] { "test.csv", "5000" });
            Assert.AreEqual(true, result);

        }
    }
}
