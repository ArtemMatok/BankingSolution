using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions.Requests
{
    public class FundsRequest
    {
        public string CardNumber { get; set; } 
        public decimal Amount { get; set; }    
    }
}
