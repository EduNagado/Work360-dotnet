using System.ComponentModel.DataAnnotations.Schema;

namespace Work360.Domain.Entity
{

    public class Report
    {
        public Guid ReportID { get; set; }

        public Guid UserID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CompletedTask { get; set; }

        public int InProgresTask { get; set; }

        public int FinishedMeeting { get; set; }

        public int MinutesFocus { get; set; }

        public double PercentualConclusao { get; set; }

        public double RiscoBurnout { get; set; }

        public string TendenciaProdutividade { get; set; }

        public string TendenciaDeFoco { get; set; }

        public string Insights { get; set; }

        public string RecomendacaoIA { get; set; }

        public string ResumoGeral { get; set; }

        public DateTime CreateDate = DateTime.Now;
    }
}