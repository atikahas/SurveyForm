using SurveyForm.Data;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly DataContext _context;
        public SurveyRepository(DataContext context)
        {
            _context = context;
        }

        public Survey GetSurvey(int id)
        {
            return _context.Surveys.Where(p => p.Id == id).FirstOrDefault();
        }

        public Survey GetSurvey(DateTime submittime)
        {
            return _context.Surveys.Where(p => p.SubmitTime == submittime).FirstOrDefault();
        }

        public ICollection<Survey> GetSurveys() 
        {
            return _context.Surveys.OrderBy(p => p.Id).ToList(); 
        }

        public bool SurveyExists(int survId)
        {
            return _context.Surveys.Any(p => p.Id == survId);
        }
    }
}
