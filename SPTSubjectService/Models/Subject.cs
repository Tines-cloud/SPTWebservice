using System.ComponentModel.DataAnnotations;

namespace SPTSubjectService.Models
{
    public class Subject
    {
        [Key]
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Modules { get; set; }

    }
}
