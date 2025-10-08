using AutoMapper;
using Challenge.Microservices.SubscriptionApi.Commands;
using Challenge.Microservices.SubscriptionApi.Infra.Data.Entities;

namespace Challenge.Microservices.SubscriptionApi.AutoMapper
{
    /// <summary>
    /// AutoMapper profile for mapping between commands and entities
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configures the mapping rules for the application
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CreateSubscriptionCommand, Subscription>()
                .ForMember(dest => dest.DailyCost, opt => opt.MapFrom(src => RentalPlans.Plans[src.PlanDays]))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}