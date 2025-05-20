class Book:
    def __init__(self, book_id, title):
        self.book_id = book_id
        self.title = title
        self.available = True


class Member:
    def __init__(self, member_id, name):
        self.member_id = member_id
        self.name = name


class Library:
    def __init__(self):
        self.books = {}
        self.members = {}
        self.loans = {} 

    def add_book(self, book_id, title):
        if book_id in self.books:
            print("Książka już istnieje.")
        else:
            self.books[book_id] = Book(book_id, title)
            print(f"Dodano książkę: {title}")

    def add_member(self, member_id, name):
        if member_id in self.members:
            print("Użytkownik już istnieje.")
        else:
            self.members[member_id] = Member(member_id, name)
            print(f"Dodano użytkownika: {name}")

    def loan_book(self, book_id, member_id):
        if book_id not in self.books:
            print("Nie znaleziono książki.")
            return
        if member_id not in self.members:
            print("Nie znaleziono użytkownika.")
            return
        if not self.books[book_id].available:
            print("Książka jest już wypożyczona.")
            return

        self.books[book_id].available = False
        self.loans[book_id] = member_id
        print(f"{self.members[member_id].name} wypożyczył(a) książkę: {self.books[book_id].title}")

    def return_book(self, book_id):
        if book_id in self.loans:
            member_id = self.loans.pop(book_id)
            self.books[book_id].available = True
            print(f"Książka '{self.books[book_id].title}' została zwrócona przez {self.members[member_id].name}.")
        else:
            print("Ta książka nie była wypożyczona.")

    def show_loans(self):
        if not self.loans:
            print("Brak aktywnych wypożyczeń.")
            return
        print(">>> Aktywne wypożyczenia:")
        for book_id, member_id in self.loans.items():
            book = self.books[book_id]
            member = self.members[member_id]
            print(f"- {book.title} : {member.name}")

library = Library()

library.add_book(1, "Harry Potter")
library.add_book(2, "Hobbit")

library.add_member(1, "Jan")
library.add_member(2, "Janina")

library.loan_book(1, 1)
library.loan_book(2, 2)
library.show_loans()

library.return_book(1)
library.show_loans()