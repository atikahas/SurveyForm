namespace SurveyForm.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Answer { get; set; }
        public Survey Survey { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
