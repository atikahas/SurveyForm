using SurveyForm.Models;

namespace SurveyForm.Interfaces
{
    public interface ISurveyRepository
    {
        ICollection<Survey> GetSurveys();
        Survey GetSurvey(int id);
        Survey GetSurvey(DateTime submittime);
        bool SurveyExists(int survId);
        
    }
}
