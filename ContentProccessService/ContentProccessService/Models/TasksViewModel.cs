using ContentProccessService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Models
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? IsActive { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UsersModels Writer { get; set; }
        public StatusModels Status { get; set; }
    }

    public class TasksViewByEditorModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTasks status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PublishTime { get; set; }

    }

    public class StatusTaskModels
    {
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }

    }

    public class UsersModels
    {
        public int? IdUser { get; set; }
        public string Name { get; set; }

    }
    public class StatusModels
    {
        public int? IdStatus { get; set; }
        public string Name { get; set; }

    }

    public class TaskModel
    {

        public int Id { get; set; }
        public int IdCampaign { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? Status { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? IdWriter { get; set; }

    }

    public class CreateTaskModel
    {
        public int IdCampaign { get; set; }
        public int IdWriter { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PublishTime { get; set; }

        public CreateTaskModel(int idCampaign, int idWriter, string title, string description, DateTime? deadline, DateTime? publishTime)
        {
            IdCampaign = idCampaign;
            IdWriter = idWriter;
            Title = title;
            Description = description;
            Deadline = deadline;
            PublishTime = publishTime;

        }
    }
}
