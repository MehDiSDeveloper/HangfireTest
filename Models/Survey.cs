namespace TestHangfire2.Models
{
    public class Survey
    {
        public Guid SurveyId { get; set; }
        public int Counter { get; set; }
        public string Content { get; set; }

        public Survey()
        {
            Content = Counter.ToString();
        }
    }
}
