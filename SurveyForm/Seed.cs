using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyForm.Models;

namespace SurveyForm
{
    public class Seed : IEntityTypeConfiguration<Survey>, IEntityTypeConfiguration<SurveyQuestion>, IEntityTypeConfiguration<SurveyAnswer>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasData(
                new Survey
                {
                    Id = 1,
                    SubmitTime = DateTime.Now.AddDays(-5),
                },
                new Survey
                {
                    Id = 2,
                    SubmitTime = DateTime.Now.AddDays(-3),
                }
                // Add more survey data as needed
            );
        }

        public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
        {
            builder.HasData(
                new SurveyQuestion
                {
                    Id = 1,
                    Question = "How satisfied are you with our service?",
                },
                new SurveyQuestion
                {
                    Id = 2,
                    Question = "Would you recommend us to a friend?",
                }
                // Add more survey question data as needed
            );
        }

        public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
        {
            builder.HasData(
                new SurveyAnswer
                {
                    Id = 1,
                    SurveyId = 1,
                    SurveyQuestionId = 1,
                    Answer = "Very satisfied",
                },
                new SurveyAnswer
                {
                    Id = 2,
                    SurveyId = 1,
                    SurveyQuestionId = 2,
                    Answer = "Yes",
                }
                // Add more survey answer data as needed
            );
        }
    }
}
