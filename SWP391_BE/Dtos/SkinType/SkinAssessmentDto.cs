namespace SWP391_BE.Dtos.SkinType
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
} 