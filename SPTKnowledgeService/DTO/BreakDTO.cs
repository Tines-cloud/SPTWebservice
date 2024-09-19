using System.ComponentModel.DataAnnotations;

namespace SPTKnowledgeService.DTO
{
    public class BreakDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public SubjectDTO Subject { get; set; }
        public DateTime Date { get; set; }
        public double TotalDuration { get; set; }
        public int SessionId { get; set; }
        public List<TimeSpan> BreakDurations { get; set; }
    }
}
