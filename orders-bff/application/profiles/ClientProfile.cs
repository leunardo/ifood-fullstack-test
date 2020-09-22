using application.dtos.response;
using application.models;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace application.profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<JToken, Client>()
                .ForMember(dest => dest.Name, src => src.MapFrom(i => i.Value<string>("name")))
                .ForMember(dest => dest.Id, src => src.MapFrom(i => GetIdFromHref(
                    i.SelectToken("_links.self.href").Value<string>()))
                )
                .ForMember(dest => dest.Email, src => src.MapFrom(i => i.Value<string>("email")))
                .ForMember(dest => dest.Phone, src => src.MapFrom(i => i.Value<string>("phone")));


            CreateMap<Client, ClientDto>();
        }

        private string GetIdFromHref(string href)
        {
            return href.Substring(
                href.LastIndexOf("/") + 1
            );
        }
    }
}