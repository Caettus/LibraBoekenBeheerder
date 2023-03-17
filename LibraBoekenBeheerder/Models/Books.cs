using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraBoekenBeheerder.Models;

public class Books
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(50, ErrorMessage = "Title can have at most 50 characters.")]
    public string Title { get; set; }

    [StringLength(30, ErrorMessage = "Author can have at most 30 characters.")]
    public string Author { get; set; }

    [StringLength(20, ErrorMessage = "ISBN number can have at most 20 characters.")]
    public string IsbnNumber { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Pages must be a non-negative number.")]
    public int? Pages { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Pages read must be a non-negative number.")]
    public int? PagesRead { get; set; }

    public int? CollectionId { get; set; }

    [StringLength(int.MaxValue, ErrorMessage = "Summary can have at most {1} characters.")]
    public string Summary { get; set; }

    public byte[] CoverImage { get; set; }
}