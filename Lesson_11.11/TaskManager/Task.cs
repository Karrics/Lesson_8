using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    enum TaskStatuses
    {
        Не_назначена,
        Назначена,
        В_работе,
        На_проверке,
        Выполнена
    }
    internal class Task
    {

        private string taskDescription;
        private DateTime taskDeadline;
        private DateTime startDateAfterReport;
        private DateTime nextTaskReportDate;
        private Employer taskAssigner;
        private Employer taskPerformer;
        private TaskStatuses taskStatus;
        private Project projectToWhichItRelates;
        private List<Report> taskReports = new List<Report>();

        public string TaskDescription
        {
            get
            {
                return taskDescription;
            }
        }

        public DateTime TaskDeadline
        {
            get
            {
                return taskDeadline;
            }
        }

        public DateTime StartDateAfterReport
        {
            get
            {
                return startDateAfterReport;
            }
        }

        public DateTime NextTaskReportDate
        {
            get
            {
                return nextTaskReportDate;
            }
        }

        public Employer TaskAssigner
        {
            get
            {
                return taskAssigner;
            }
        }

        public Employer TaskPerformer
        {
            get
            {
                return taskPerformer;
            }
        }

        public TaskStatuses TaskStatus
        {
            get
            {
                return taskStatus;
            }
        }

        public Project ProjectToWhichItRelates
        {
            get
            {
                return projectToWhichItRelates;
            }
        }

        public List<Report> TaskReports
        {
            get
            {
                return taskReports;
            }
        }

        // Назначение задачи
        public void AddingTaskPerformer(Employer newTaskPerformer)
        {
            Task projectTask = newTaskPerformer.Assigned_Task as Task;

            if (projectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект)
            {
                if (projectTask != null)
                {
                    projectTask.taskPerformer = null;
                    projectTask.taskStatus = TaskStatuses.Не_назначена;
                }

                taskPerformer = newTaskPerformer;
                taskStatus = TaskStatuses.Назначена;
            }
        }

        // Изменение статуса
        public void TransitionToStatusWork()
        {
            if (projectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект)
            {
                taskStatus = TaskStatuses.В_работе;
            }
        }

        // Дата отчёта
        public void SettingReportDate(DateTime taskReportDate, DateTime dateAfterReport)
        {
            startDateAfterReport = dateAfterReport;
            nextTaskReportDate = taskReportDate;
        }

        // Убирание задачи с работника
        public void RemovePerformer()
        {
            if (projectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект)
            {
                taskPerformer = null;
                taskStatus = TaskStatuses.Не_назначена;
            }
        }

        // Добавление отчёта
        public void AddTaskReportToList(Report taskReport)
        {
            if (taskStatus == TaskStatuses.В_работе)
            {
                taskReports.Add(taskReport);
            }
        }

        // Проверка задачи
        public void CheckingTask(Employer taskAssigner)
        {
            if (this.taskAssigner == TaskAssigner)
            {
                taskStatus = TaskStatuses.На_проверке;
                TransitionToStageCompleted();
            }
        }

        // Изменение статуса заадчи 
        private void TransitionToStageCompleted()
        {
            if (taskStatus == TaskStatuses.На_проверке)
            {
                taskStatus = TaskStatuses.Выполнена;
            }
        }

        public Task(string taskDescription, DateTime taskDeadline, Employer taskAssigner, Project projectToWhichItRelates)
        {
            this.taskDescription = taskDescription;
            this.taskDeadline = taskDeadline;
            this.taskAssigner = taskAssigner;
            this.projectToWhichItRelates = projectToWhichItRelates;
            taskStatus = TaskStatuses.Не_назначена;
            taskPerformer = null;
        }
    }
}
