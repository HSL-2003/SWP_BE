namespace SWP391_BE.Dtos.SkinType
{
    public class SkinQuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
} 