using Microsoft.EntityFrameworkCore.Diagnostics;
using SurveyForm.Data;
using SurveyForm.Interfaces;
using SurveyForm.Models;

namespace SurveyForm.Repository
{
    public class SurveyQuestionRepository : ISurveyQuestionRepository
    {
        private readonly DataContext _context;
        public SurveyQuestionRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<SurveyQuestion> GetSurveyQuestions()
        {
            return _context.SurveyQuestions.OrderBy(q => q.Id).ToList();
        }

        public SurveyQuestion GetSurveyQuestion(int id)
        {
            return _context.SurveyQuestions.Where(q => q.Id == id).FirstOrDefault();
        }

        public SurveyQuestion GetSurveyQuestion(string question)
        {
            return _context.SurveyQuestions.Where(q => q.Question == question).FirstOrDefault();
        }

        public bool SurveyQuestionExists(int questid)
        {
            return _context.SurveyQuestions.Any(q => q.Id == questid);
        }

        public bool CreateSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            _context.Add(surveyQuestion);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
