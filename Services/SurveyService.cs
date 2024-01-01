using Hangfire;
using Microsoft.EntityFrameworkCore;
using TestHangfire2.Contexts.HangfireDbContext;
using TestHangfire2.Interfaces;
using TestHangfire2.Models;

namespace TestHangfire2.Services
{
    [Queue("q3")]
    public class SurveyService : ISurveyService
    {

        private readonly ApplicationDbContext _context;
        public SurveyService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [Queue("q1")]
        public void SetNewSurveyQ1()
        {
            int count = 0;
            Thread.Sleep(10000);
            count = _context.Surveys.Count();
            Survey survey = new Survey();
            survey.Counter = count + 1;
            _context.Surveys.Add(survey);
            _context.SaveChanges();
        }

        [Queue("q2")]

        public void SetNewSurveyQ2()
        {
            int count = 0;
            Thread.Sleep(10000);
            count = _context.Surveys.Count();
            Survey survey = new Survey();
            survey.Counter = count + 1;
            _context.Surveys.Add(survey);
            _context.SaveChanges();
        }


        [Queue("q3")]

        public void SetNewSurveyQ3()
        {
            int count = 0;
            Thread.Sleep(10000);
            count = _context.Surveys.Count();
            Survey survey = new Survey();
            survey.Counter = count + 1;
            _context.Surveys.Add(survey);
            _context.SaveChanges();
        }
    }
}
