using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeApp.Shared.Model
{
    internal class TaskTimerModel
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long TaskID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
