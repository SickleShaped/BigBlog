using AutoMapper;
using BigBlog.Models.Db;
using System.Text.Json.Nodes;

namespace BigBlog.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuxilaryArticle, Article>()
                .ReverseMap();

            CreateMap<AuxinaryUser, User>() .ReverseMap();
        }
    }
}
