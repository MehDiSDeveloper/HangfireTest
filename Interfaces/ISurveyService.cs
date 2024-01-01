using Hangfire;
using TestHangfire2.Models;

namespace TestHangfire2.Interfaces
{
    public interface ISurveyService
    {
        void SetNewSurveyQ1();
        void SetNewSurveyQ2();
        void SetNewSurveyQ3();
    }
}
