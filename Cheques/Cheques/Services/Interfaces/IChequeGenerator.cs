using Cheques.Models;
using System;

namespace Cheques.Services.Interfaces
{
    public interface IChequeGenerator
    {
        Cheque Generate(string name, decimal amount, DateTime date);
    }
}