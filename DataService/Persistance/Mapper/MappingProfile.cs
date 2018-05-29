using AutoMapper;
using Model.DBObjects;
using Model.DTO.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<TestModelDto, Test>();
            CreateMap<AnswerModelDto, Answer>();
            CreateMap<QuestionModelDto, Question>();
        }
    }
}
