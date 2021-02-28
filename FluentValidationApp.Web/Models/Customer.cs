using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }
        public IList<Address> Addresses { get; set; }//adresi index olarak kullanabilmek için ılist yaptık yoksa ıcollection yapıcaktık.
        public Gender Gender { get; set; }
        public string GetFullName()
            => $"{Name}-{Email}-{Age}";
        public string FullName2()
            => $"{Name}-{Email}";

        public CreditCard CreditCard { get; set; }
    }
}
