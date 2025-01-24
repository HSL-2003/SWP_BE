using System.ComponentModel.DataAnnotations;

namespace SWP391_BE.Dtos.SkinType
{
    public class SkinAssessmentDto
    {
        [Required(ErrorMessage = "Phải có ít nhất một câu trả lời")]
        [MinLength(1, ErrorMessage = "Phải có ít nhất một câu trả lời")]
        public List<SkinQuestionAnswerDto> Answers { get; set; }
    }

    public class SkinQuestionAnswerDto
    {
        [Required(ErrorMessage = "ID câu hỏi không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "ID câu hỏi không hợp lệ")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "ID câu trả lời không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "ID câu trả lời không hợp lệ")]
        public int AnswerId { get; set; }
    }
} 