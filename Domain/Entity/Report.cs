using System.ComponentModel.DataAnnotations.Schema;

namespace Work360.Domain.Entity
{
    public class Report
    {
        public Guid UserID { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int CompletedTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int FinishedMeetings { get; set; }

        public int FocusMinutes { get; set; }

        public double CompletionPercentage { get; set; }

        public string BurnoutRisk { get; set; }

        public string ProductivityTrend { get; set; }

        public string FocusTrend { get; set; }

        public string Insights { get; set; }

        public string AIRecommendation { get; set; }

        public string Summary { get; set; }

    }
}
