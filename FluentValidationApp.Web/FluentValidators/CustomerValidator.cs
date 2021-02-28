using FluentValidation;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator:AbstractValidator<Customer>//dto, entity nesnesi verildi
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz.";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email alanı dogru formatta olmalıdır");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60).WithMessage("Age alanı 18 ile 60 arasında olmalıdır.");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18) >= x;
            }).WithMessage("Yaşınız 18'den büyük olmalıdır.");

            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());//customer içindeki her bir adres elemanına addressvalidator'ı işlemesini söyledik.

            RuleFor(x => x.Gender).IsInEnum().WithMessage("{PropertyName} alanı erkek için 1 kadın için 2 olmalıdır");
        }
    }
}
