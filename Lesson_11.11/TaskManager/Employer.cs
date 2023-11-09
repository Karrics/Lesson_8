using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class Employer
    {
        private string name;
        private object assigned_Task;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public object Assigned_Task
        {
            get
            {
                return assigned_Task;
            }
        }

        // Ответственный человек создаёт задачу
        public bool CreatingTask(string taskDescription, DateTime taskDeadline)
        {
            Project project = assigned_Task as Project;

            if ((project != null) && (project.ProjectStatus == ProjectStatuses.Проект))
            {
                Task projectTask = new Task(taskDescription, taskDeadline, this, project);
                project.AddingTaskToList(projectTask);

                return true;
            }

            return false;
        }
        
        // Ответсвенный человек назначает задачу
        public bool DistributeTheTask(Task projectTask, Employer tasPerformer, DateTime taskReportDate, DateTime taskStartDate)
        {
            Project project = assigned_Task as Project;

            if ((project != null) && (projectTask.TaskPerformer == null) && (tasPerformer.Assigned_Task == null) && (project.ProjectStatus == ProjectStatuses.Проект))
            {
                tasPerformer.AddTask(projectTask);
                projectTask.AddingTaskPerformer(tasPerformer);
                projectTask.SettingReportDate(taskReportDate, taskStartDate);

                return true;
            }

            return false;
        }

        // Удаление задачи 
        public bool DeleteTask(Task projectTask)
        {
            Project project = assigned_Task as Project;

            if ((project != null) && (project.ProjectStatus == ProjectStatuses.Проект))
            {
                project.RemoveTaskFromList(projectTask);

                return true;
            }

            return false;
        }

        // Добавление задачи 
        public bool AddTask(object task)
        {
            Project project = task as Project;
            Task projectTask = task as Task;

            if ((project != null) && (assigned_Task == null) && (project.TeamLead == null) && (project.ProjectStatus == ProjectStatuses.Проект))
            {
                assigned_Task = project;

                return true;
            }
            else if ((projectTask != null) && (assigned_Task == null) && (projectTask.TaskPerformer == null) && (projectTask.ProjectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект))
            {
                assigned_Task = projectTask;

                return true;
            }

            return false;
        }

        // Взять задачу
        public bool TakeTask()
        {
            Task projectTask = assigned_Task as Task;

            if ((projectTask != null) && (projectTask.ProjectToWhichItRelates != null) && (projectTask.ProjectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект))
            {
                projectTask.TransitionToStatusWork();
                projectTask.ProjectToWhichItRelates.TransitionToExecutionStatus();

                return true;
            }

            return false;
        }


        // Передача одной задачи другому
        public bool DelegateTask(Employer newTaskPerformer)
        {
            Task projectTask = assigned_Task as Task;

            if ((projectTask != null) && (projectTask.ProjectToWhichItRelates != null) && (projectTask.ProjectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект))
            {
                projectTask.AddingTaskPerformer(newTaskPerformer);

                assigned_Task = null;
                newTaskPerformer.assigned_Task = projectTask;

                return true;
            }

            return false;
        }

        // Отказ от задачи
        public bool AbandonTheTask()
        {
            Task projectTask = assigned_Task as Task;

            if ((projectTask != null) && (projectTask.ProjectToWhichItRelates != null) && (projectTask.ProjectToWhichItRelates.ProjectStatus == ProjectStatuses.Проект))
            {
                projectTask.RemovePerformer();
                assigned_Task = null;

                return true;
            }

            return false;
        }


        // Создание отчёта
        public Report CreatingReport(string reportText, DateTime dateAfterReport)
        {
            Task projectTask = assigned_Task as Task;

            if ((projectTask != null) && (projectTask.TaskStatus == TaskStatuses.В_работе))
            {
                Report taskReport = new Report(reportText, this, projectTask);
                DateTime nextTaskReportDate = projectTask.NextTaskReportDate.Add(projectTask.NextTaskReportDate.Subtract(projectTask.StartDateAfterReport));

                if (nextTaskReportDate <= projectTask.TaskDeadline)
                {
                    projectTask.SettingReportDate(nextTaskReportDate, dateAfterReport);
                }
                else
                {
                    projectTask.SettingReportDate(projectTask.TaskDeadline, dateAfterReport);
                }

                return taskReport;
            }

            return null;
        }

        // Метод для проверки отчёта
        public bool CheckingTheReport(bool checkResult, Report taskReport, DateTime date)
        {
            if (taskReport.TaskToWhichReportBelongs.TaskAssigner == this)
            {
                if (checkResult)
                {
                    taskReport.AddReportAcceptanceDate(date);
                    taskReport.TaskToWhichReportBelongs.AddTaskReportToList(taskReport);
                    return true;
                }
                else
                {
                    taskReport.RewriteTheTaskReport();
                    return false;
                }
            }

            return false;
        }

        public Employer(string employeeName)
        {
            this.name = employeeName;
        }
    }
}
