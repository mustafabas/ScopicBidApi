using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Entities
{
    public class UserEntity:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal MaxAmount { get; set; }
        
    }
}
