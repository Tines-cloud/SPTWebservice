using SPTGradeService.Repositories;
using SPTKnowledgeService.DTO;
using SPTKnowledgeService.Models;
using SPTKnowledgeService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPTKnowledgeService.Mappers
{
    public class KnowledgeMapper
    {
        private readonly ISubjectService _subjectService;
        private readonly IGradeRepository _gradeRepository;

        public KnowledgeMapper(ISubjectService subjectService, IGradeRepository gradeRepository)
        {
            _subjectService = subjectService;
            _gradeRepository = gradeRepository;
        }

        public async Task<KnowledgeDTO> MapKnowledgeToKnowledgeDtoAsync(Knowledge knowledge)
        {
            if (knowledge == null) return null;

            var knowledgeDto = new KnowledgeDTO
            {
                Id = knowledge.Id,
                Username = knowledge.Username,
                Date = knowledge.Date,
            };

            var subjectDTO = await _subjectService.GetSubjectByCodeAsync(knowledge.SubjectCode);
            var gradeDTO = await _gradeRepository.GetGradeByCodeAsync(knowledge.GradeCode);

            knowledgeDto.Subject = subjectDTO;
            knowledgeDto.CurrentGrade = MapGradeToGradeDto(gradeDTO);

            return knowledgeDto;
        }

        public Knowledge MapKnowledgeDtoToKnowledge(KnowledgeDTO knowledgeDTO)
        {
            if (knowledgeDTO == null) return null;

            return new Knowledge
            {
                Id = knowledgeDTO.Id,
                Username = knowledgeDTO.Username,
                Date = knowledgeDTO.Date,
                SubjectCode = knowledgeDTO.Subject.SubjectCode,
                GradeCode = knowledgeDTO.CurrentGrade.GradeCode
            };
        }

        public GradeDTO MapGradeToGradeDto(Grade grade)
        {
            if (grade == null) return null;

            var gradeDto = new GradeDTO
            {
                GradeCode = grade.code,
                GradeName = grade.name,
            };
            return gradeDto;
        }

        public Grade MapGradeDtoToGrade(GradeDTO gradeDTO)
        {
            if (gradeDTO == null) return null;

            var grade = new Grade
            {
                code = gradeDTO.GradeCode,
                name = gradeDTO.GradeName
            };
            return grade;
        }

        public async Task<StudySessionDTO> MapStudySessionToStudySessionDtoAsync(StudySession studySession)
        {
            if (studySession == null) return null;

            var studySessionDto = new StudySessionDTO
            {
                Id = studySession.Id,
                Username = studySession.Username,
                Date = studySession.Date,
                StartTime = studySession.StartTime,
                EndTime = studySession.EndTime,
            };

            var subjectDTO = await _subjectService.GetSubjectByCodeAsync(studySession.SubjectCode);
            studySessionDto.Subject = subjectDTO;

            if (studySession.Break != null)
            {
                studySessionDto.Break = await MapBreakToBreakDtoAsync(studySession.Break);
            }

            double duration = 0;
            duration = (studySessionDto.EndTime.TotalMinutes - studySessionDto.StartTime.TotalMinutes);
            BreakDTO breakSession = studySessionDto.Break;
            if (breakSession != null)
            {
               duration -= breakSession.TotalDuration;      
            }
            studySessionDto.TotalDuration = duration;

            return studySessionDto;
        }

        public StudySession MapStudySessionDtoToStudySession(StudySessionDTO studySessionDto)
        {
            if (studySessionDto == null) return null;

            var studySession = new StudySession
            {
                Id = studySessionDto.Id,
                Username = studySessionDto.Username,
                Date = studySessionDto.Date,
                SubjectCode = studySessionDto.Subject.SubjectCode,
                StartTime = studySessionDto.StartTime,
                EndTime = studySessionDto.EndTime,
            };

            if (studySessionDto.Break != null)
            {
                studySession.Break = MapBreakDtoToBreak(studySessionDto.Break);
            }

            return studySession;
        }

        public async Task<BreakDTO> MapBreakToBreakDtoAsync(Break breakEntity)
        {
            if (breakEntity == null) return null;

            var breakDTO = new BreakDTO();

            breakDTO.Id = breakEntity.Id;
            breakDTO.SessionId = breakEntity.StudySessionId;
            breakDTO.Username = breakEntity.Username;
            breakDTO.Date = breakEntity.Date;
            breakDTO.BreakDurations = breakEntity.BreakDurations.Select(bd => bd.Duration).ToList();

            double totalDuration = 0;

            foreach (var breaks in breakDTO.BreakDurations)
            {
                totalDuration += breaks.TotalMinutes;
            }

            breakDTO.TotalDuration= totalDuration;

            var subjectDTO = await _subjectService.GetSubjectByCodeAsync(breakEntity.SubjectCode);
            breakDTO.Subject = subjectDTO;

            return breakDTO;
        }

        public Break MapBreakDtoToBreak(BreakDTO breakDto)
        {
            if (breakDto == null) return null;

            var breakEntity = new Break
            {
                Id = breakDto.Id,
                StudySessionId = breakDto.SessionId,
                Username = breakDto.Username,
                SubjectCode = breakDto.Subject.SubjectCode,
                Date = breakDto.Date,
                BreakDurations = breakDto.BreakDurations.Select(duration => new BreakDuration
                {
                    Duration = duration
                }).ToList()
            };

            return breakEntity;
        }

        /*public async Task<BreakDTO> MapBreakToBreakDtoAsync(Break breakEntity)
        {
            if (breakEntity == null) return null;

            var breakDTO = new BreakDTO
            {
                Id = breakEntity.Id,
                SessionId = breakEntity.StudySessionId,
                Username = breakEntity.Username,
                Date = breakEntity.Date,
                BreakDurations = breakEntity.BreakDurations.Select(bd => bd.Duration).ToList()
            };

            var subjectDTO = await _subjectService.GetSubjectByCodeAsync(breakEntity.SubjectCode);
            breakDTO.Subject = subjectDTO;

            return breakDTO;
        }

        public Break MapBreakDtoToBreak(BreakDTO breakDto)
        {
            if (breakDto == null) return null;

            var breakEntity = new Break
            {
                Id = breakDto.Id,
                StudySessionId = breakDto.SessionId,
                SubjectCode = breakDto.Subject.SubjectCode,
                Date = breakDto.Date,
                Username = breakDto.Username,
                BreakDurations = breakDto.BreakDurations.Select(duration => new BreakDuration
                {
                    Duration = duration
                }).ToList()
            };

            return breakEntity;
        }*/
    }
}
