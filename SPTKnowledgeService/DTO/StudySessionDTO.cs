using System.ComponentModel.DataAnnotations;

namespace SPTKnowledgeService.DTO
{
    public class StudySessionDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public SubjectDTO Subject { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double TotalDuration { get; set; }

        public BreakDTO Break { get; set; }
    }
}
