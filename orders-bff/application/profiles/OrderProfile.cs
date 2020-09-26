using System;
using application.dtos.response;
using application.models;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace application.profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<JToken, Item>()
                .ForMember(dest => dest.Description, src => src.MapFrom(i => i.Value<string>("description")))
                .ForMember(dest => dest.Price, src => src.MapFrom(i => i.Value<double>("price")))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(i => i.Value<int>("quantity")));

            CreateMap<JToken, Order>()
                .ForMember(dest => dest.Id, src => src.MapFrom(i =>
                     GetIdFromHref(i.SelectToken("_links.self.href").Value<string>()))
                )
                .ForMember(dest => dest.ClientId, src => src.MapFrom(i => Guid.Parse(i.Value<string>("clientId"))))
                .ForMember(dest => dest.RestaurantId, src => src.MapFrom(i => Guid.Parse(i.Value<string>("restaurantId"))))
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(i => i.Value<DateTime>("createdAt")))
                .ForMember(dest => dest.Items, src => src.MapFrom(i => i.SelectToken("items")))
                .ForMember(dest => dest.ConfirmedAt, src => src.MapFrom(i => i.Value<DateTime>("confirmedAt")))
                .ReverseMap();
                

            CreateMap<Item, ItemDto>();
                
        }

        private string GetIdFromHref(string href)
        {
            return href.Substring(
                href.LastIndexOf("/") + 1
            );
        }

    }
}