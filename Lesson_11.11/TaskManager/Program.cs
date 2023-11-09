using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание сотрудников
            Employer Andrew = new Employer("Эндрю Уилсон");
            Employer Trip = new Employer("Трип Хокинс");
            Employer Larry = new Employer("Ларри Пробст");
            Employer Adel = new Employer("Adel");

            // Назначение главного и проекта
            Project gameDevelopment = new Project("Разработать компьютерную игру", DateTime.Today.AddYears(1), "Electronic Arts");
            gameDevelopment.AppointTeamLeader(Andrew);

            // Создание задач
            Andrew.CreatingTask("Изменить застаку", DateTime.Today.AddDays(2));
            Andrew.CreatingTask("Скопировать геймплей из прошлых частей", DateTime.Today.AddMonths(4));
            Andrew.CreatingTask("Добавить баги", DateTime.Today.AddMonths(2));
            Andrew.CreatingTask("Забыть про оптимизацию", DateTime.Today.AddDays(1));

            List<Task> projectTasks = gameDevelopment.ProjectTasks;


            for (int i = 0; i < projectTasks.Count; i++)
            {
                Console.WriteLine($"{projectTasks[i].TaskDescription}", $"{projectTasks[i].TaskDeadline.ToLongDateString()}",
                                                                     $"{projectTasks[i].TaskAssigner.Name}", $"{projectTasks[i].TaskStatus}");
            }

            Andrew.DistributeTheTask(projectTasks[0], Adel, DateTime.Today.AddDays(7), DateTime.Today);
            Andrew.DistributeTheTask(projectTasks[1], Trip, DateTime.Today.AddMonths(2), DateTime.Today);
            Andrew.DistributeTheTask(projectTasks[2], Trip, DateTime.Today.AddMonths(3), DateTime.Today);
            Andrew.DistributeTheTask(projectTasks[3], Larry, DateTime.Today.AddDays(1), DateTime.Today);

            projectTasks = gameDevelopment.ProjectTasks;

            Adel.TakeTask();
            Trip.TakeTask();
            Larry.TakeTask();
            Trip.AbandonTheTask();


            for (DateTime date = DateTime.Today; date <= gameDevelopment.ProjectDeadline; date = date.AddDays(1))
            {
                for (int i = 0; i < projectTasks.Count; i++)
                {
                    if ((projectTasks[i].NextTaskReportDate == date) && (projectTasks[i].TaskStatus == TaskStatuses.В_работе))
                    {
                        bool checkResult = false;
                        Report taskReport = projectTasks[i].TaskPerformer.CreatingReport($"Отчет задачи {projectTasks[i].TaskDescription}", date);

                        do
                        {
                            bool result;
                            Random randomNumbers = new Random();
                            int randomNum = randomNumbers.Next(0, 2);

                            if (randomNum == 0)
                            {
                                result = false;
                            }
                            else
                            {
                                result = true;
                            }

                            checkResult = projectTasks[i].TaskAssigner.CheckingTheReport(result, taskReport, date);
                        } while (!checkResult);

                        if (checkResult)
                        {
                            if (date == projectTasks[i].TaskDeadline)
                            {
                                projectTasks[i].CheckingTask(Andrew);
                                projectTasks[i].ProjectToWhichItRelates.TransitionToClosedStatus();
                            }

                            Console.WriteLine($"{taskReport.ReportText}", $"{taskReport.DateAcceptanceTheReport.ToLongDateString()}", $"{taskReport.Executor.Name}");
                        }
                    }
                }
            }

            Console.WriteLine($"\nОписание проекта: {gameDevelopment.ProjectDescription}. Срок сдачи: {gameDevelopment.ProjectDeadline.ToLongDateString()}\n" +
                              $"Заказчик: {gameDevelopment.ProjectCustomer}. Ответсвенный: {gameDevelopment.TeamLead.Name}. Статус проекта: {gameDevelopment.ProjectStatus}\n");


            for (int i = 0; i < projectTasks.Count; i++)
            {
                Console.WriteLine($"{projectTasks[i].TaskDescription}", $"{projectTasks[i].TaskDeadline.ToLongDateString()}",
                                                                     $"{projectTasks[i].TaskPerformer.Name}", $"{projectTasks[i].TaskStatus}");
            }
            Console.ReadKey();
        }
    }
}
