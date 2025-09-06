using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkTimeApp.Shared.Model
{
    public class TaskModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("userID")]
        public long UserID { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Termin jest wymagany.")]
        [JsonPropertyName("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("dateTimeCreated")]
        public DateTime DateTimeCreated { get; set; }

        [JsonPropertyName("timePassed")]
        public string? TimePassed { get; set; }

        [JsonPropertyName("status")]
        public EnumStatus Status { get; set; }

        [JsonPropertyName("completed")]
        public bool Completed { get; set; }

        [JsonPropertyName("timeStart")]
        public DateTime TimeStart { get; set; }


    }
    public enum EnumStatus
    {
        InComplete = 0,
        Active = 1,
        Completed = 2,
    }
}
