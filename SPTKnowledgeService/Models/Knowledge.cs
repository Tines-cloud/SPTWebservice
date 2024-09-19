using System.ComponentModel.DataAnnotations;

namespace SPTKnowledgeService.Models
{
    public class Knowledge
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string SubjectCode { get; set; }
        public string GradeCode { get; set; }
        public DateTime Date { get; set; }

    }
}
