using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Member Mappings
            CreateMap<Member, MemberDto>()
                .ForMember(dest => dest.MembershipName, opt => opt.MapFrom(src => src.Membership.Name))
                .ReverseMap();
            CreateMap<MemberCreateDto, Member>();
            CreateMap<MemberUpdateDto, Member>();

            // Trainer Mappings
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<TrainerCreateDto, Trainer>();
            CreateMap<TrainerUpdateDto, Trainer>();

            // Membership Mappings
            CreateMap<Membership, MembershipDto>().ReverseMap();
            CreateMap<MembershipCreateDto, Membership>();
            CreateMap<MembershipUpdateDto, Membership>();

            // GymClass Mappings
            CreateMap<GymClass, GymClassDto>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => $"{src.Trainer.FirstName} {src.Trainer.LastName}"))
                .ReverseMap();
            CreateMap<GymClassCreateDto, GymClass>();
            CreateMap<GymClassUpdateDto, GymClass>();

            // Enrollment Mappings
            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.GymClass.Name))
                .ReverseMap();
            CreateMap<EnrollmentCreateDto, Enrollment>();
            CreateMap<EnrollmentUpdateDto, Enrollment>();
        }
    }
}
