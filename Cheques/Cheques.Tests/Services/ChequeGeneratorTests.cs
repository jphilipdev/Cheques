using Cheques.Controllers;
using Cheques.Models;
using Cheques.Models.Exceptions;
using Cheques.Services;
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
    public class ChequeGeneratorTests
    {
        private Mock<INumberTextConverter> _numberTextGenerator;
        private ChequeGenerator _subject;

        [SetUp]
        public void Setup()
        {
            _numberTextGenerator = new Mock<INumberTextConverter>();
            _subject = new ChequeGenerator(_numberTextGenerator.Object);
        }

        [Test]
        public void GivenValidRequest_WhenChequeGenerationRequested_ThenChequeIsReturned()
        {
            // ARRANGE
            var name = "name";
            var amount = 1m;
            var date = new DateTime(2018, 1, 1);

            var expectedAmountInWords = "expectedAmountInWords";
            var expectedFormattedDate = "1/01/2018";

            _numberTextGenerator.Setup(p => p.Convert(amount))
                                .Returns(expectedAmountInWords);

            // ACT
            var result = _subject.Generate(name, amount, date);

            // ASSERT
            Assert.AreEqual(name, result.Name, "name");
            Assert.AreEqual(amount, result.Amount, "amount");
            Assert.AreEqual(expectedAmountInWords, result.AmountInWords, "amount in words");
            Assert.AreEqual(expectedFormattedDate, result.FormattedDate, "formatted date");
        }

        [Test]
        public void GivenAmountIsNegative_WhenChequeGenerationRequested_ThenNegativeChequeAmountRequestedExceptionIsThrown()
        {
            // ARRANGE
            var name = "name";
            var amount = -1m;
            var date = new DateTime(2018, 1, 1);

            // ACT
            TestDelegate result = () => _subject.Generate(name, amount, date);

            // ASSERT
            var exception = Assert.Throws<NegativeChequeAmountRequestedException>(result);
            Assert.AreEqual(amount, exception.Amount);
        }

        [Test]
        public void GivenAmountIsZero_WhenChequeGenerationRequested_ThenZeroChequeAmountRequestedExceptionIsThrown()
        {
            // ARRANGE
            var name = "name";
            var amount = 0m;
            var date = new DateTime(2018, 1, 1);

            // ACT
            TestDelegate result = () => _subject.Generate(name, amount, date);

            // ASSERT
            Assert.Throws<ZeroChequeAmountRequestedException>(result);
        }

        [Test]
        public void GivenAmountHasTooManyDecimals_WhenChequeGenerationRequested_ThenTooManyDecimalsRequestedExceptionIsThrown()
        {
            // ARRANGE
            var name = "name";
            var amount = 0.001m;
            var date = new DateTime(2018, 1, 1);

            // ACT
            TestDelegate result = () => _subject.Generate(name, amount, date);

            // ASSERT
            var exception = Assert.Throws<TooManyDecimalsRequestedException>(result);
            Assert.AreEqual(amount, exception.Amount);
        }
    }
}