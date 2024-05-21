using System;

// Könyv osztály, amely egy könyvet reprezentál (SRP)
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

// E-könyv osztály, amely egy elektronikus könyvet reprezentál, örököl a Book osztályból (SRP, LSP)
public class EBook : Book
{
    public string Url { get; set; }

    public EBook(string title, string author, string url) : base(title, author)
    {
        Url = url;
    }
}

// ILibrary interfész, amely a könyvek tárolásáért és lekérdezéséért felel (SRP, OCP, DIP)
public interface ILibrary
{
    void AddBook(Book book);
    List<Book> GetAllBooks();
    List<Book> Search(string keyword);
}

// Könyvtár osztály, felelős a könyvek tárolásáért és lekérdezéséért, valamint implementálja az ISearchable interfészt (SRP, OCP)
public class Library: ILibrary
{
    protected List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public List<Book> Search(string keyword)
    {
        List<Book> searchResults = new List<Book>();
        foreach (var book in books)
        {
            if (book.Title.Contains(keyword) || book.Author.Contains(keyword))
            {
                searchResults.Add(book);
            }
        }
        return searchResults;
    }
}

// EbookLibrary osztály, felelős az e-könyvek tárolásáért és lekérdezéséért (SRP, ISP)
public interface ISearchEbookByTitle
{
    List<Book> SearchByTitle(string keyword);
}

public interface ISearchEbookByAuthor
{
    List<Book> SearchByAuthor(string keyword);
}

public class EbookLibrary : Library, ISearchEbookByTitle, ISearchEbookByAuthor
{
    public List<Book> SearchByTitle(string keyword)
    {
        List<Book> searchResults = new List<Book>();
        foreach (var book in books)
        {
            if (book.Title.Contains(keyword))
            {
                searchResults.Add(book);
            }
        }
        return searchResults;
    }

    public List<Book> SearchByAuthor(string keyword)
    {
        List<Book> searchResults = new List<Book>();
        foreach (var book in books)
        {
            if (book.Author.Contains(keyword))
            {
                searchResults.Add(book);
            }
        }
        return searchResults;
    }
}

//Példák
class Program
{
    static void Main()
    {
        // Könyvtár létrehozása
        Library library = new Library();

        // Könyvek hozzáadása
        library.AddBook(new Book("Harry Potter", "J.K. Rowling"));
        library.AddBook(new Book("Lord of the Rings", "J.R.R. Tolkien"));

        // Könyvek keresése
        var books = library.Search("Harry");
        Console.WriteLine("Könyvek:");
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} - {book.Author}");
        }

        // EbookLibrary létrehozása
        EbookLibrary ebookLibrary = new EbookLibrary();

        // E-könyvek hozzáadása
        ebookLibrary.AddBook(new EBook("Clean Code", "Robert C. Martin", "http://example.com/clean-code"));
        Console.Write((ebookLibrary.GetAllBooks()[0] as EBook).Url); //Írjuk ki az EBook Url-jét
    }
}
