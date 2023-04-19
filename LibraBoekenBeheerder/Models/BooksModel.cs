using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LibraBoekenBeheerder.Models;
public class BooksModel
{
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(13)]
        public string ISBNNumber { get; set; }

    public int? Pages { get; set; }

    public int? PagesRead { get; set; }

    public string? Summary { get; set; }
    public int? CollectionID { get; set; }

    public ICollection<CollectionBooksModel> CollectionsBooksIsAPartOf { get; set; } = new HashSet<CollectionBooksModel>();
}