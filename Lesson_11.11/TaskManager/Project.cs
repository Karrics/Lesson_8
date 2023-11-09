using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    enum ProjectStatuses
    {
        Проект,
        Исполнение,
        Закрыт
    }
    internal class Project
    {
        private string projectDescription;
        private DateTime projectDeadline;
        private string projectCustomer;
        private Employer teamLead;
        private ProjectStatuses projectStatus;
        private List<Task> projectTasks = new List<Task>();

        public string ProjectDescription
        {
            get
            {
                return projectDescription;
            }
        }

        public DateTime ProjectDeadline
        {
            get
            {
                return projectDeadline;
            }
        }

        public string ProjectCustomer
        {
            get
            {
                return projectCustomer;
            }
        }

        public Employer TeamLead
        {
            get
            {
                return teamLead;
            }
        }

        public ProjectStatuses ProjectStatus
        {
            get
            {
                return projectStatus;
            }
        }

        public List<Task> ProjectTasks
        {
            get
            {
                return projectTasks;
            }
        }


        // Метод, позволяющий назначить ответсвенного за проект
        public bool AppointTeamLeader(Employer teamLead)
        {
            if ((teamLead.Assigned_Task == null) && (this.teamLead == null) && (projectStatus == ProjectStatuses.Проект))
            {
                teamLead.AddTask(this);
                this.teamLead = teamLead;

                return true;
            }

            return false;
        }


        // Добавление задачи в проект
        public bool AddingTaskToList(Task projectTask)
        {
            if (projectStatus == ProjectStatuses.Проект)
            {
                projectTasks.Add(projectTask);

                return true;
            }

            return false;
        }


        // Удаление задачи 
        public bool RemoveTaskFromList(Task projectTask)
        {
            if (projectStatus == ProjectStatuses.Проект)
            {
                projectTasks.Remove(projectTask);

                return true;
            }

            return false;
        }


        // Изменение статуса проекта
        public void TransitionToExecutionStatus()
        {
            if (projectStatus == ProjectStatuses.Проект)
            {
                bool checkResult = true;

                for (int i = 0; i < projectTasks.Count; i++)
                {
                    if (projectTasks[i].TaskStatus != TaskStatuses.В_работе)
                    {
                        checkResult = false;
                        break;
                    }
                }

                if (checkResult)
                {
                    projectStatus = ProjectStatuses.Исполнение;
                }
            }
        }

        // Изменение статуса проекта
        public void TransitionToClosedStatus()
        {
            if (projectStatus == ProjectStatuses.Исполнение)
            {
                bool checkResult = true;

                for (int i = 0; i < projectTasks.Count; i++)
                {
                    if (projectTasks[i].TaskStatus != TaskStatuses.Выполнена)
                    {
                        checkResult = false;
                        break;
                    }
                }

                if (checkResult)
                {
                    projectStatus = ProjectStatuses.Закрыт;
                }
            }
        }

        public Project(string projectDescription, DateTime projectDeadline, string projectCustomer)
        {
            this.projectDescription = projectDescription;
            this.projectDeadline = projectDeadline;
            this.projectCustomer = projectCustomer;
            projectStatus = ProjectStatuses.Проект;
            teamLead = null;
        }
    }
}
