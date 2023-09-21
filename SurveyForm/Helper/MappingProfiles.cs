using AutoMapper;
using SurveyForm.Dto;
using SurveyForm.Models;

namespace SurveyForm.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Survey, SurveyDto>();
            CreateMap<SurveyQuestion, SurveyQuestionDto>();
            CreateMap<SurveyQuestionDto, SurveyQuestion>(); //post
            CreateMap<SurveyAnswer, SurveyAnswerDto>();
            CreateMap<SurveyAnswerDto, SurveyAnswer>(); //post
        }
    }
}
