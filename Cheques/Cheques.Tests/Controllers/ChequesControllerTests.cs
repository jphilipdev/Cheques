using Cheques.Controllers;
using Cheques.Models;
using Cheques.Models.Exceptions;
using Cheques.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cheques.Tests.Controllers
{
    [TestFixture]
    public class ChequesControllerTests
    {
        private Mock<IChequeGenerator> _chequeGenerator;
        private ChequesController _subject;

        [SetUp]
        public void Setup()
        {
            _chequeGenerator = new Mock<IChequeGenerator>();
            _subject = new ChequesController(_chequeGenerator.Object);
        }

        [Test]
        public void GivenValidRequest_WhenChequeGenerationRequested_ThenChequeIsReturned()
        {
            // ARRANGE
            var name = "name";
            var amount = 1m;
            var date = new DateTime(2018, 1, 1);

            var expectedAmountInWords = "expectedAmountInWords";
            var expectedFormattedDate = "expectedFormattedDate";

            _chequeGenerator.Setup(p => p.Generate(name, amount, date))
                            .Returns(new Cheque(name, amount, expectedAmountInWords, expectedFormattedDate));

            // ACT
            var result = _subject.Generate(name, amount, date);

            // ASSERT
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            var resultValue = ((OkObjectResult)result).Value as Cheque;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual(name, resultValue.Name, "name");
            Assert.AreEqual(amount, resultValue.Amount, "amount");
            Assert.AreEqual(expectedAmountInWords, resultValue.AmountInWords, "amount in words");
            Assert.AreEqual(expectedFormattedDate, resultValue.FormattedDate, "formatted date");
        }

        [Test]
        public void GivenAmountIsNegative_WhenChequeGenerationRequested_ThenBadRequestIsReturned()
        {
            // ARRANGE
            var name = "name";
            var amount = -1m;
            var date = new DateTime(2018, 1, 1);

            _chequeGenerator.Setup(p => p.Generate(name, amount, date))
                            .Throws(new NegativeChequeAmountRequestedException(amount));

            // ACT
            var result = _subject.Generate(name, amount, date);

            // ASSERT
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
            var resultValue = ((BadRequestObjectResult)result).Value as string;
            Assert.AreEqual("Cannot generate cheque for negative amount: '-1'", resultValue);
        }

        [Test]
        public void GivenAmountIsZero_WhenChequeGenerationRequested_ThenBadRequestIsReturned()
        {
            // ARRANGE
            var name = "name";
            var amount = 0m;
            var date = new DateTime(2018, 1, 1);

            _chequeGenerator.Setup(p => p.Generate(name, amount, date))
                            .Throws(new ZeroChequeAmountRequestedException());

            // ACT
            var result = _subject.Generate(name, amount, date);

            // ASSERT
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
            var resultValue = ((BadRequestObjectResult)result).Value as string;
            Assert.AreEqual("Cannot generate cheque for zero amount", resultValue);
        }

        [Test]
        public void GivenAmountHasTooManyDecimals_WhenChequeGenerationRequested_ThenBadRequestIsReturned()
        {
            // ARRANGE
            var name = "name";
            var amount = 0.001m;
            var date = new DateTime(2018, 1, 1);

            _chequeGenerator.Setup(p => p.Generate(name, amount, date))
                            .Throws(new TooManyDecimalsRequestedException(amount));

            // ACT
            var result = _subject.Generate(name, amount, date);

            // ASSERT
            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
            var resultValue = ((BadRequestObjectResult)result).Value as string;
            Assert.AreEqual("Cannot generate cheque with as many decimal places as amount: '0.001'", resultValue);
        }
    }
}