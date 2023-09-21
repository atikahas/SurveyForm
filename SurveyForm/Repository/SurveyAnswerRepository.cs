using AutoMapper;
using SurveyForm.Data;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Repository
{
    public class SurveyAnswerRepository : ISurveyAnswerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SurveyAnswerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<SurveyAnswer> GetQuestion(int questId)
        {
            return _context.SurveyAnswers.Where(a => a.SurveyQuestion.Id == questId).ToList();
        }

        public SurveyAnswer GetSurveyAnswer(int id)
        {
            return _context.SurveyAnswers.Where(a => a.Id == id).FirstOrDefault();
        }

        public SurveyAnswer GetSurveyAnswer(string answer)
        {
            return _context.SurveyAnswers.Where(a => a.Answer == answer).FirstOrDefault();
        }

        public bool SurveyAnswerExists(int ansid)
        {
            return _context.SurveyAnswers.Any(a => a.Id == ansid);
        }

        public ICollection<SurveyAnswer> GetSurveyAnswers() 
        {
            return _context.SurveyAnswers.ToList();
        }

        public bool CreateSurveyAnswer(SurveyAnswer surveyAnswer)
        {
            _context.Add(surveyAnswer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
