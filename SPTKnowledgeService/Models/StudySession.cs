using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPTKnowledgeService.Models
{
    public class StudySession
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string SubjectCode { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Break Break { get; set; }
    }

    public class Break
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("StudySession")]
        public int StudySessionId { get; set; }
        public string Username { get; set; }

        public StudySession StudySession { get; set; }

        public string SubjectCode { get; set; }

        public DateTime Date { get; set; }

        public List<BreakDuration> BreakDurations { get; set; }
    }

    public class BreakDuration
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Break")]
        public int BreakId { get; set; }

        public TimeSpan Duration { get; set; }

        public Break Break { get; set; }
    }
}
