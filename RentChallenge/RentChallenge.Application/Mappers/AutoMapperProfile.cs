using AutoMapper;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentChallenge.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterMotorcycleRequestDTO, Motorcycle>();
            CreateMap<RegisterMotorcycleRequestDTO, EventMotorcycleRegistered>()
                .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.DateRecived, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.SerializedPayload, opt => opt.MapFrom(src => SerializePayload(src)));

            CreateMap<RegisterDeliveryManDTO, DeliveryMan>();
        }

        private string SerializePayload(RegisterMotorcycleRequestDTO src) => JsonSerializer.Serialize(src);
    }
}
