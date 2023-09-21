using Microsoft.EntityFrameworkCore;
using SurveyForm.Models;

namespace SurveyForm.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        { 

        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyAnswer>()
                .HasKey(pc => new { pc.SurveyId, pc.SurveyQuestionId });
            modelBuilder.Entity<SurveyAnswer>()
                .HasOne(p => p.Survey)
                .WithMany(pc => pc.SurveyAnswers)
                .HasForeignKey(p => p.SurveyId);
            modelBuilder.Entity<SurveyAnswer>()
                .HasOne(p => p.SurveyQuestion)
                .WithMany(pc => pc.SurveyAnswers)
                .HasForeignKey(p => p.SurveyQuestionId);
        }

    }
}
