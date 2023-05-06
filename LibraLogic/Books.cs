namespace LibraLogic;

public class Books
{
    public int BookId { get; private set; }

    public string Title { get; private set; }

    public string Author { get; private set; }

    public string ISBNNumber { get; private set; }

    public int? Pages { get; private set; }

    public int? PagesRead { get; private set; }

    public string? Summary { get; private set; }
}