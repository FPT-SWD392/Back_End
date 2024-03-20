using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.SqlObject;
using BusinessObject.MongoDbObject;

namespace WebAPI.MappingProfile
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<ArtInfo, ArtworkPreviewDTO>()
                .ForMember(dest => dest.CreatorNickName, option => option.MapFrom(src => src.CreatorInfo.UserInfo.NickName))
                .ForMember(dest => dest.CreatorProfilePicture, option => option.MapFrom(src => src.CreatorInfo.UserInfo.ProfilePicture))
                .ForMember(dest => dest.Tags, option => option.MapFrom(src => src.ArtTags.Select(x => x.TagId).ToList()))
                .ForMember(dest => dest.RatingCount, option => option.MapFrom(src=>src.ArtRatings.Count()))
                .ForMember(dest => dest.AverageRating, option => option.MapFrom((src, dest) =>
                    {
                        if (dest.RatingCount == 0) return 0;
                        return (double)src.ArtRatings.Select(x=>x.Rating).Sum()/dest.RatingCount;
                    }));
            CreateMap<ArtInfo, ArtworkDetailDTO>()
                .ForMember(dest => dest.CreatorNickName, option => option.MapFrom(src => src.CreatorInfo.UserInfo.NickName))
                .ForMember(dest => dest.CreatorProfilePicture, option => option.MapFrom(src => src.CreatorInfo.UserInfo.ProfilePicture))
                .ForMember(dest => dest.Tags, option => option.MapFrom(src => src.ArtTags.Select(x => x.TagId).ToList()))
                .ForMember(dest => dest.RatingCount, option => option.MapFrom(src => src.ArtRatings.Count()))
                .ForMember(dest => dest.AverageRating, option => option.MapFrom((src, dest) =>
                {
                    if (dest.RatingCount == 0) return 0;
                    return (double)src.ArtRatings.Select(x => x.Rating).Sum() / dest.RatingCount;
                }));
        }
    }
}
