namespace Library_Project;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    // like an ID
    public bool IsAvailable { get; set; } = true;
    // it means that by default the book is available
    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }

    public void showInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Available: {IsAvailable}");
    }

}

class Member
{
    private static int count = 1;
    public int MemberID { get;}
    public string FirstName { get; set; }
    public string Email { get; set; }

    public Member(string firstName, string email)
    {
        MemberID = count++;
        FirstName = firstName;
        Email = email;
    }

    public void showInfo()
    {
        Console.WriteLine($"Member Id: {MemberID}, First Name: {FirstName}, Email: {Email}");
    }
    
    
    
}

class Library
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();

    public void addBook()
    {
        Console.WriteLine("please enter the name of the book you want to add");
        string title = Console.ReadLine();
        Console.WriteLine("please enter the name of the author of the author");
        string author = Console.ReadLine();
        Console.WriteLine("please enter the QR code number or ISBN of the book");
        string isbn = Console.ReadLine();
        
        Book newBook = new Book(title, author, isbn);
        books.Add(newBook);
        Console.WriteLine("please enter the name of the book you want to add");
        
        Console.WriteLine("the book added successfully");
    }

    public void addMember()
    {
        Console.WriteLine("please enter the name of the member");
        string firstName = Console.ReadLine();
        Console.WriteLine("please enter the member's email address");
        string email = Console.ReadLine();
        
        members.Add(new Member(firstName, email));
        Console.WriteLine("the member added successfully");
    }

    public void DisplayBooks()
    {
        foreach (var books in books)
        {
            books.showInfo();
        }
    }

    public void DisplayMembers()
    {
        foreach (var members in members)
        {
            members.showInfo();
        }
    }

    public void BorrowBook()
    {
        Console.WriteLine("Enter the Enter your Member ID");
        int memberID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Enter the Book ID");
        string isbn = Console.ReadLine();
        
        Book book = books.Find(b => b.ISBN == isbn);
        Member member = members.Find(m => m.MemberID == memberID);

        if (member != null && book != null && book.IsAvailable)
        {
            book.IsAvailable = false;
            Console.WriteLine($"The member {member.FirstName} borrowed {book.Title}");
        }
        else
        {
            Console.WriteLine("There is no member with the given ID or the book you want to borrow is not available now");
        }
    }
    
    public void ReturnBook()
    {
        Console.Write("Enter Book ISBN: ");
        string isbn = Console.ReadLine();

        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null && !book.IsAvailable)
        {
            book.IsAvailable = true;
            Console.WriteLine($"\"{book.Title}\" has been returned.");
        }
        else
        {
            Console.WriteLine("Return failed. Book not found or already returned.");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        while (true)
        {
            Console.WriteLine("press 1 for adding a book");
            Console.WriteLine("press 2 for adding a member");
            Console.WriteLine("press 3 for displaying all books");
            Console.WriteLine("press 4 for displaying all members");
            Console.WriteLine("press 5 for borrowing a book");
            Console.WriteLine("press 6 for returning a book");
            Console.WriteLine("press 7 for exit");
            string result = Console.ReadLine();

            if (result == "1")
            {
                library.addBook();
            }
            if (result == "2")
            {
                library.addMember();
            }

            if (result == "3")
            {
                library.DisplayBooks();
            }

            if (result == "4")
            {
                library.DisplayMembers();
            }
            if (result == "5")
            {
                library.BorrowBook();
            }
            if (result == "6")
            {
                library.ReturnBook();
            }
            if (result == "7")
            {
                break;
            }
        }
    }
}