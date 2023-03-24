using System.ComponentModel.DataAnnotations;

public class BooksModel
{
    public int BookId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBNNumber { get; set; }

    public int? Pages { get; set; }

    public int? PagesRead { get; set; }

    public string Summary { get; set; }
    
    
    
}