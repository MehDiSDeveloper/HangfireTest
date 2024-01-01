using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestHangfire2.Interfaces;
using TestHangfire2.Models;

//Test2 Home controller

namespace TestHangfire2.Controllers
{
    [Queue("q2")]
    public class HomeController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ISurveyService surveyService)
        {
            _logger = logger;
            _surveyService = surveyService;
        }


        #region Hangfire test
        [HttpGet]
        public IActionResult TestEnqueueQ1()
        {
            // fire and forget job
            var i = BackgroundJob.Enqueue(() => _surveyService.SetNewSurveyQ1());


            //removing job object using job identifier
            //RecurringJob.RemoveIfExists("sdfasdfaf");

            //triggering (running) job using its id
            //RecurringJob.Trigger("dsafasdf");


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TestEnqueueQ2()
        {
            // fire and forget job
            var i = BackgroundJob.Enqueue(() => _surveyService.SetNewSurveyQ2());


            //removing job object using job identifier
            //RecurringJob.RemoveIfExists("sdfasdfaf");

            //triggering (running) job using its id
            //RecurringJob.Trigger("dsafasdf");


            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult ScheduleQ1()
        {
            //delayed job
            var i = BackgroundJob.Schedule(() => _surveyService.SetNewSurveyQ1(), new TimeSpan(0, 0, 2, 0));

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ScheduleQ2()
        {
            //delayed job
            var i = BackgroundJob.Schedule(() => _surveyService.SetNewSurveyQ2(), new TimeSpan(0, 0, 2, 0));

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ScheduleQ3()
        {
            //delayed job
            var i = BackgroundJob.Schedule(() => _surveyService.SetNewSurveyQ3(), new TimeSpan(0, 0, 2, 0));

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult TestAddOrUpdate()
        {
            //recurring job with job identifier and cron
            string jobIdentifier = Guid.NewGuid().ToString();
            RecurringJob.AddOrUpdate(jobIdentifier, () => _surveyService.SetNewSurveyQ3(), Cron.MinuteInterval(2).ToString());

            return RedirectToAction("Index");
        }
        #endregion





        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
