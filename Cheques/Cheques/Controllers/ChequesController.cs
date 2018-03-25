using Cheques.Models.Exceptions;
using Cheques.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cheques.Controllers
{
    [Route("api/[controller]")]
    public class ChequesController : Controller
    {
        private readonly IChequeGenerator _chequeGenerator;

        public ChequesController(IChequeGenerator chequeGenerator)
        {
            _chequeGenerator = chequeGenerator;
        }

        [HttpGet, Route("[action]")]
        public IActionResult Generate(string name, decimal amount, DateTime date)
        {
            try
            {
                var cheque = _chequeGenerator.Generate(name, amount, date);
                return Ok(cheque);
            }
            catch(NegativeChequeAmountRequestedException e)
            {
                return BadRequest($"Cannot generate cheque for negative amount: '{e.Amount}'");
            }
            catch (ZeroChequeAmountRequestedException)
            {
                return BadRequest("Cannot generate cheque for zero amount");
            }
            catch (TooManyDecimalsRequestedException e)
            {
                return BadRequest($"Cannot generate cheque with as many decimal places as amount: '{e.Amount}'");
            }
        }
    }
}