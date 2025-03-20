using System.ComponentModel.DataAnnotations;

namespace ScheduleLesson.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Order { get; set; }
        public required string Content { get; set; }
        public string ClassName { get; set; }
        public Guid UserGuid { get; set; }
    }
}
