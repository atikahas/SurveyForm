using SurveyForm.Models;

namespace SurveyForm.Interfaces
{
    public interface ISurveyQuestionRepository
    {
        ICollection<SurveyQuestion> GetSurveyQuestions();
        SurveyQuestion GetSurveyQuestion(int id);
        SurveyQuestion GetSurveyQuestion(string question);
        bool SurveyQuestionExists(int questid);  
        bool CreateSurveyQuestion(SurveyQuestion surveyQuestion);
        bool Save();
    }
}
