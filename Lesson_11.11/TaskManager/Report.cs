using System;

namespace TaskManager
{
    internal class Report
    {
        private string reportText;
        private DateTime dateAcceptanceTheReport;
        private Employer executor;
        private Task taskToWhichReportBelongs;

        public string ReportText
        {
            get
            {
                return reportText;
            }
        }

        public DateTime DateAcceptanceTheReport
        {
            get
            {
                return dateAcceptanceTheReport;
            }
        }

        public Employer Executor
        {
            get
            {
                return executor;
            }
        }

        public Task TaskToWhichReportBelongs
        {
            get
            {
                return taskToWhichReportBelongs;
            }
        }

        // Метод для установки даты завершения работы над отчетом
        public void AddReportAcceptanceDate(DateTime dateCompletionReport)
        {
            dateAcceptanceTheReport = dateCompletionReport;

        }

        // Метод для изменения статуса отчёта
        public void RewriteTheTaskReport()
        {
            reportText = $"Отчет на тему {taskToWhichReportBelongs.TaskDescription}";
        }


        public Report(string reportText, Employer executor, Task taskToWhichReportBelongs)
        {
            this.reportText = reportText;
            this.executor = executor;
            this.taskToWhichReportBelongs = taskToWhichReportBelongs;
        }
    }
}
