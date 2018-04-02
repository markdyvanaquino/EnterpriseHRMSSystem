using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Areas.Identity.Data;
using System.ComponentModel;
namespace LibraryManagement.Models
{
    public class TransactionModel
    {

        [Key]
        [DisplayName("Transaction ID")]
        public Guid TransactionID { get; set; } = Guid.NewGuid();

        [Required]
        [DisplayName("User ID")]
        public string UserID { get; set; }

        [MaxLength(100)]
        [DisplayName("Borrower Name")]
        public string BorrowerName{ get; set; }

        [Required]
        [DisplayName("Book ISBN")]
        public string BookISBN { get; set; }

        [Required]
        [DisplayName("Transaction Type")]
        public string TransactionType { get; set; }

        [DisplayName("Borrow Date")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Due Date")]
        public DateTime DueDate 
        {
            get => BorrowDate.AddDays(3);
            private set { }
        }

        [Required]
        [DisplayName("Status")]
        public string Status { get; set; } = "Borrowed";

    }
}
