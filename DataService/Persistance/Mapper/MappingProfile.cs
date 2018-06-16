using AutoMapper;
using Model.DBObjects;
using Model.DTO.Test;

namespace Persistance.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestModelDto, Test>();
            CreateMap<AnswerModelDto, Answer>();
            CreateMap<Answer, AnswerModelDto>();
            CreateMap<QuestionModelDto, Question>();
            CreateMap<Test, TestModelDto>();
        }
    }
}