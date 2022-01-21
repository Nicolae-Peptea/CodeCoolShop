using AutoMapper;
using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OrderViewDetailsModel, Customer>()
                .ForMember(dest => dest.BillingName, opt => opt.MapFrom(src => src.StripeBillingName))
                .ForMember(dest => dest.BillingAddressCity, opt => opt.MapFrom(src => src.StripeBillingAddressCity))
                .ForMember(dest => dest.BillingAddressCountry, opt => opt.MapFrom(src => src.StripeBillingAddressCountry))
                .ForMember(dest => dest.BillingAddressLine1, opt => opt.MapFrom(src => src.StripeBillingAddressLine1))
                .ForMember(dest => dest.BillingAddressZip, opt => opt.MapFrom(src => src.StripeBillingAddressZip))

                .ForMember(dest => dest.ShippingName, opt => opt.MapFrom(src => src.StripeShippingName))
                .ForMember(dest => dest.ShippingAddressCity, opt => opt.MapFrom(src => src.StripeShippingAddressCity))
                .ForMember(dest => dest.ShippingAddressCountry, opt => opt.MapFrom(src => src.StripeShippingAddressCountry))
                .ForMember(dest => dest.ShippingAddressLine1, opt => opt.MapFrom(src => src.StripeShippingAddressLine1))
                .ForMember(dest => dest.ShippingAddressZip, opt => opt.MapFrom(src => src.StripeShippingAddressZip))

                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.StripeEmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.StripeBillingName));

            CreateMap<Customer, Customer>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? null));
        }
    }
}
