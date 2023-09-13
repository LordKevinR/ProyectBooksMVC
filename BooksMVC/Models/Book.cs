using BooksMVC.Validations;
using System.ComponentModel.DataAnnotations;

namespace BooksMVC.Models
{
	public class Book
	{
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title of the book")]
        [FirstCapitalLetter]
        public string Title { get; set; }

        public string Description { get; set; }

		[Display(Name = "Number of pages")]
		public int PageCount { get; set; }

        public string Excerpt { get; set; }

		[Display(Name = "Publish Date")]
		public DateTime PublishDate { get; set; }
    }
}
