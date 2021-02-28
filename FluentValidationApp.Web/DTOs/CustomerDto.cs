using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Eposta { get; set; }
        public int Yas { get; set; }
        public string FullName { get; set; }

        //flattening örnekleri
        public string CreditCardNumber { get; set; }//flattening
        public DateTime CreditCardValidDate { get; set; }//flattening

        //public string CCNumber { get; set; }//flattening yapmamaız durumunda CustomerProfile classında mapping işlemi tanımladık
        //public DateTime CCValidDate { get; set; }////flattening yapmamaız durumunda CustomerProfile classında mapping işlemi tanımladık


        //IncludeMembers() örnekleri

        public string Number { get; set; }
        public DateTime ValidDate { get; set; }
    }
}
