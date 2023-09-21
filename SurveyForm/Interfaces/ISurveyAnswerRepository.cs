using SurveyForm.Models;

namespace SurveyForm.Interfaces
{
    public interface ISurveyAnswerRepository
    {
        ICollection<SurveyAnswer> GetSurveyAnswers();
        SurveyAnswer GetSurveyAnswer(int id);
        SurveyAnswer GetSurveyAnswer(string answer);
        ICollection<SurveyAnswer> GetQuestion(int questId);
        bool SurveyAnswerExists(int ansid);
        bool CreateSurveyAnswer(SurveyAnswer surveyAnswer);
        bool Save();
    } 

}