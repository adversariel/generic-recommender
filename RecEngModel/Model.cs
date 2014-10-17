using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecEngModel
{
    public class Model
    {
        // lists to search through books and raters
        List<Book> bookList;
        List<Rater> raterList;
         
        // Model object to be passed to viewer and controller.
        public Model()
        {
            bookList = new List<Book>();
            raterList = new List<Rater>();
        }

        // get Book object by title and author
        public Book getBook(String title, String author)
        {
            foreach (Book book in bookList)
            {
                if (book.title.Equals(title) && book.author.Equals(author))
                {
                    return book;
                }
            }
            return null;
        }

        // get Book object by title. Returns a list of books with that title
        public List<Book> getBookByTitle(String title)
        {
            List<Book> match = new List<Book>();
            foreach (Book book in bookList)
            {
                if(book.title.Equals(title))
                {
                    match.Add(book);
                }
            }
            return match;
        }

        // get Book object by author. Returns a list of books with that author
        public List<Book> getBookByAuthor(String author)
        {
            List<Book> match = new List<Book>();
            foreach (Book book in bookList)
            {
                if (book.author.Equals(author))
                {
                    match.Add(book);
                }
            }
            return match;
        }

        // get Rater by name
        public Rater getRater(String name)
        {
            foreach(Rater bookworm in raterList)
            {
                if (bookworm.name.Equals(name))
                {
                    return bookworm;
                }
            }
            return null;
        }

        // add Book
        public void addBook(String title, String author)
        {
            Book newBook = new Book(title, author);
            addBook(newBook);
        }

        // add Book - this method is used to add already-existing books to the system
        public void addBook(Book book)
        {
            if (getBook(book.title, book.author) == null)
            {
                Book newBook = book;
                bookList.Add(newBook);
            }
            else
                throw new AlreadyExistsException();
        }

        // add Rater
        public void addRater(String name)
        {
            Rater newRater = new Rater(name);
            addRater(newRater);
        }

        // add Rater - this method is used to add already-existing raters to the system
        public void addRater(Rater bookworm)
        {
            if (getRater(bookworm.name) == null)
            {
                Rater newRater = new Rater(bookworm.name);
                raterList.Add(newRater);
            }
            else
                throw new AlreadyExistsException();
        }

        // exception definition
        public class AlreadyExistsException : System.Exception { }
    }

    /// <summary>
    /// Book object for recommendation system.
    /// </summary>
    public class Book
    {
        public String title;
        public String author;
        public Dictionary<Rater, float> bookRating;

        // constructor
        public Book(String title, String author)
        {
            this.title = title;
            this.author = author;
            this.bookRating = new Dictionary<Rater, float>();
        }

        // operator overload
        // NOTE: we don't override getHashCode() because it isn't used in our override of .Equals
        public override bool Equals(Object book)
        {
            try
            {
                Book b = (Book)book;
                return (this.author.Equals(b.author)) || (this.title.Equals(b.title));
            }
            catch
            {
                return false;
            }
        }

        // add rating to bookRating dictionary
        public void addRating(Rater rater, float rating)
        {
            // check if rater exists
            if (rater == null)
                throw new NullReferenceException();

            // add rating to both dictionaries
            bookRating.Add(rater, rating);
            rater.ratedBooks.Add(this, rating);
        }
    }

    /// <summary>
    /// Rater object for recommendation system.
    /// </summary>
    public class Rater
    {
        public String name;
        public Dictionary<Book, float> ratedBooks;

        // constructor
        public Rater(String name)
        {
            this.name = name;
            this.ratedBooks = new Dictionary<Book, float>();
        }

        // operator overload
        // NOTE: we don't override getHashCode() because it isn't used in our override of .Equals
        public override bool Equals(Object bookworm)
        {
            try
            {
                Rater r = (Rater)bookworm;
                return (this.name.Equals(r.name));
            }
            catch
            {
                return false;
            }
        }

        // add rating to ratedBooks dictionary
        public void addRating(Book book, float rating)
        {
            // check if book exists
            if (book == null)
                throw new NullReferenceException();
            
            // add rating to both dictionaries
            ratedBooks.Add(book, rating);
            book.bookRating.Add(this, rating);
        }
    }

}
