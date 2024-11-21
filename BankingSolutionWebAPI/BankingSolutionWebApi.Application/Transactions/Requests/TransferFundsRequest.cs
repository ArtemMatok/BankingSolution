using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Application.Transactions.Requests
{
    public class TransferFundsRequest
    {
        public string FromCardNumber { get; set; }
        public string ToCardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
