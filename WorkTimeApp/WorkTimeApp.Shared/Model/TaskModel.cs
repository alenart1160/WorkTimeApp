using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkTimeApp.Shared.Model
{
    public class TaskModel
    {
        public long id { get; set; }
        public long userID { get; set; }
        public string? title { get; set; }
        [Required(ErrorMessage = "Termin jest wymagany.")]
        public DateTime dueDate { get; set; }
        public int priority { get; set; }
        public string? description { get; set; }
        public DateTime dateTimeCreated { get; set; }
        public string? timePassed { get; set; }
        public  EnumStatus status { get; set; }
        public bool completed { get; set; }
        public DateTime timeStart { get; set; }


    }
    public enum EnumStatus
    {
        InComplete = 0,
        Active = 1,
        Completed = 2,
    }
}
