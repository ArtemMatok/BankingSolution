using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolutionWebApi.Domain.Entities
{
    public class BankingAccount
    {
        public int BankingAccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public string CardNumber { get; set; } 
        public string UserId { get; set; }  
        public AppUser User { get; set; }   

        public BankingAccount()
        {
            CardNumber = GenerateCardNumber();
        }

        private string GenerateCardNumber()
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, 4)
                .Select(_ => random.Next(1000, 9999).ToString()))
                .Insert(4, " ")
                .Insert(9, " ")
                .Insert(14, " "); 
        }
    }
}
