using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkTimeApp.Shared.Model
{
    public class TaskTimerModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("userID")]
        public long UserID { get; set; }
        [JsonPropertyName("taskID")]
        public long TaskID { get; set; }
        [JsonPropertyName("dateTimeCreated")]
        public DateTime DateTimeCreated { get; set; }
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
    }
}
