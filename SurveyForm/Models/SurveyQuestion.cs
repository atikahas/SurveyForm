﻿namespace SurveyForm.Models
{
    public class SurveyQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public ICollection<SurveyAnswer> SurveyAnswers { get; set; }
    }
}
