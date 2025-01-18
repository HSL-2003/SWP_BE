namespace SWP391_BE.Dtos
{
    public class SkinAssessmentDto
    {
        public List<SkinQuestionAnswerDto> Answers { get; set; }
    }

    public class SkinQuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

    public class SkinRoutineDto
    {
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; }
        public List<RoutineStepDto> Steps { get; set; }
    }
} 