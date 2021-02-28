using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //CreateMap<Customer, CustomerDto>().ReverseMap();//tersine de imkan sağlıyor
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Isim, source => source.MapFrom(s => s.Name))
                .ForMember(dest => dest.Eposta, source => source.MapFrom(s => s.Email))
                .ForMember(dest => dest.Yas, source => source.MapFrom(s => s.Age))
                .ForMember(dest => dest.FullName, source => source.MapFrom(s => s.FullName2()));
            //.ForMember(dest=>dest.CCNumber, opt=>opt.MapFrom(s=>s.CreditCard.Number))//flattening yapmamamız durumunda
            //.ForMember(dest=> dest.CCValidDate, opt=>opt.MapFrom(s=>s.CreditCard.ValidDate));//flattening yapmamamız durumunda

            //IncludeMembers()

            CreateMap<CreditCard, CustomerDto>();
            CreateMap<Customer, CustomerDto>().IncludeMembers(s => s.CreditCard);//Prop isimleri aynı ise Complex type'ı tek satırda maplenmesini sağlıyor.

        }
    }
}
