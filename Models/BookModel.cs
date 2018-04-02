using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Book Title")]
        [StringLength(100,ErrorMessage ="Title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, ErrorMessage = "Genre cannot exceed 50 characters.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; }

        [Key]
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 13 characters.")]
        public string ISBN { get; set; }

        public bool isBorrowed { get; set; } = false;

    }
}