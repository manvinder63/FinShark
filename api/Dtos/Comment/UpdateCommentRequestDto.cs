using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]

        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }

    }

}