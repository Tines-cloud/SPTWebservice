namespace SPTKnowledgeService.DTO
{
    public class KnowledgeDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public SubjectDTO Subject { get; set; }
        public GradeDTO CurrentGrade { get; set; }
        public DateTime Date { get; set; }
    }
}
