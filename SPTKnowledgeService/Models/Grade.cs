using System.ComponentModel.DataAnnotations;

namespace SPTKnowledgeService.Models
{
    public class Grade
    {
        [Key]
        public string code { get; set; }
        public string name { get; set; }
    }
}
