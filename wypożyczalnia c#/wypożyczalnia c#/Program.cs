using System;
using System.Collections.Generic;

class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public bool Available { get; set; }

    public Book(int bookId, string title)
    {
        BookId = bookId;
        Title = title;
        Available = true;
    }
}

class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }

    public Member(int memberId, string name)
    {
        MemberId = memberId;
        Name = name;
    }
}

class Library
{
    private Dictionary<int, Book> books = new Dictionary<int, Book>();
    private Dictionary<int, Member> members = new Dictionary<int, Member>();
    private Dictionary<int, int> loans = new Dictionary<int, int>(); 

    public void AddBook(int bookId, string title)
    {
        if (books.ContainsKey(bookId))
        {
            Console.WriteLine("Książka już istnieje.");
        }
        else
        {
            books[bookId] = new Book(bookId, title);
            Console.WriteLine($"Dodano książkę: {title}");
        }
    }

    public void AddMember(int memberId, string name)
    {
        if (members.ContainsKey(memberId))
        {
            Console.WriteLine("Użytkownik już istnieje.");
        }
        else
        {
            members[memberId] = new Member(memberId, name);
            Console.WriteLine($"Dodano użytkownika: {name}");
        }
    }

    public void LoanBook(int bookId, int memberId)
    {
        if (!books.ContainsKey(bookId))
        {
            Console.WriteLine("Nie znaleziono książki.");
            return;
        }

        if (!members.ContainsKey(memberId))
        {
            Console.WriteLine("Nie znaleziono użytkownika.");
            return;
        }

        if (!books[bookId].Available)
        {
            Console.WriteLine("Książka jest już wypożyczona.");
            return;
        }

        books[bookId].Available = false;
        loans[bookId] = memberId;
        Console.WriteLine($"{members[memberId].Name} wypożyczył(a) książkę: {books[bookId].Title}");
    }

    public void ReturnBook(int bookId)
    {
        if (loans.ContainsKey(bookId))
        {
            int memberId = loans[bookId];
            loans.Remove(bookId);
            books[bookId].Available = true;
            Console.WriteLine($"Książka '{books[bookId].Title}' została zwrócona przez {members[memberId].Name}.");
        }
        else
        {
            Console.WriteLine("Ta książka nie była wypożyczona.");
        }
    }

    public void ShowLoans()
    {
        if (loans.Count == 0)
        {
            Console.WriteLine("Brak aktywnych wypożyczeń.");
            return;
        }

        Console.WriteLine(">>> Aktywne wypożyczenia:");
        foreach (var loan in loans)
        {
            Book book = books[loan.Key];
            Member member = members[loan.Value];
            Console.WriteLine($"- {book.Title} : {member.Name}");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        library.AddBook(1, "Harry Potter");
        library.AddBook(2, "Hobbit");

        library.AddMember(1, "Jan");
        library.AddMember(2, "Janina");

        library.LoanBook(1, 1);
        library.LoanBook(2, 2);
        library.ShowLoans();

        library.ReturnBook(1);
        library.ShowLoans();
    }
}