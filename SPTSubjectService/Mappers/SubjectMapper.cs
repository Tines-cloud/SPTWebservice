using SPTSubjectService.DTO;
using SPTSubjectService.Models;

namespace SPTSubjectService.Mappers
{
    public static class SubjectMapper
    {
        public static SubjectDTO MapSubjectToSubjectDto(Subject subject)
        {
            if (subject == null) return null;

            return new SubjectDTO
            {
                SubjectCode = subject.SubjectCode,
                SubjectName = subject.SubjectName,
                Modules = subject.Modules

            };
        }

        public static Subject MapSubjectDtoToSubject(SubjectDTO subjectDTO)
        {
            if (subjectDTO == null) return null;

            var user = new Subject
            {
                SubjectCode = subjectDTO.SubjectCode,
                SubjectName = subjectDTO.SubjectName,
                Modules = subjectDTO.Modules
            };

            return user;
        }
    }
}
