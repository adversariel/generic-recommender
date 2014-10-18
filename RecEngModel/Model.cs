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
         
        /// <summary>
        /// Model object to be passed to viewer and controller.
        /// </summary>
        public Model()
        {
            bookList = new List<Book>();
            raterList = new List<Rater>();
        }

        /// <summary>
        /// Gets Book object by title and author
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
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
        
        /// <summary>
        /// Get Book object by title
        /// </summary>
        /// <param name="title">Book title</param>
        /// <returns>Returns a list of books with the specified title</returns>
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
 
        /// <summary>
        /// Get Book object by author
        /// </summary>
        /// <param name="author">Book author</param>
        /// <returns>Returns a list of books with the specified author</returns>
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

        /// <summary>
        /// Get Rater by name
        /// </summary>
        /// <param name="name">Rater name</param>
        /// <returns>Returns a rater</returns>
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
        /// <summary>
        /// Add Book to list of books
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
        public void addBook(String title, String author)
        {
            Book newBook = new Book(title, author);
            addBook(newBook);
        }

        /// <summary>
        /// Add Book to list of books. This method is used to add already-existing Book objects to the system.
        /// </summary>
        /// <param name="book">Book object</param>
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

        /// <summary>
        /// Add Rater to list of raters
        /// </summary>
        /// <param name="name">Rater name</param>
        public void addRater(String name)
        {
            Rater newRater = new Rater(name);
            addRater(newRater);
        }

        /// <summary>
        /// Add Rater to the list of raters. This method is used to add already-existing raters to the system.
        /// </summary>
        /// <param name="bookworm">Rater name</param>
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

        /// <summary>
        /// Exception for if a Rater or Book already exists in the system
        /// </summary>
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

        /// <summary>
        /// Constructs a Book object.
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
        public Book(String title, String author)
        {
            this.title = title;
            this.author = author;
            this.bookRating = new Dictionary<Rater, float>();
        }

        /// <summary>
        /// Operator overload for .Equals method. We don't need to override getHashCode() because it isn't used in our override of .Equals
        /// </summary>
        /// <param name="book">Book object to compare</param>
        /// <returns>Returns boolean value indicating if two Book objects are the same</returns>
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

        /// <summary>
        /// Adds a rater and a rating on a book to the book rating dictionary. 
        /// </summary>
        /// <param name="rater">Rater object</param>
        /// <param name="rating"></param>
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

        /// <summary>
        /// Constructs a Rater object
        /// </summary>
        /// <param name="name">Rater name</param>
        public Rater(String name)
        {
            this.name = name;
            this.ratedBooks = new Dictionary<Book, float>();
        }

        /// <summary>
        /// Operator overload for .Equals method. We don't need to override getHashCode() because it isn't used in our override of .Equals
        /// </summary>
        /// <param name="bookworm">Rater object to compare</param>
        /// <returns>Returns boolean value indicating if two Rater objects are the same</returns>
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

        /// <summary>
        /// Adds rating to rated book dictionary
        /// </summary>
        /// <param name="book">Book being rated</param>
        /// <param name="rating">Book rating</param>
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
