namespace LibraDTO;

public class BooksDTO
{
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBNNumber { get; set; }

        public int? Pages { get; set; }

        public int? PagesRead { get; set; }

        public string? Summary { get; set; }

        public int? SelectedCollectionId { get; set; }
}