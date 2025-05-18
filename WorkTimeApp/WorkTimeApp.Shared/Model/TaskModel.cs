using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeApp.Shared.Model
{
    public class TaskModel
    {
        public long UserID { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public float TimePassed { get; set; }
        public string Status { get; set; }

    }
}
