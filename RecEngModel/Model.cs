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

        /// <summary>
        /// Calculates the Pearson correlation coefficient on the reviews for two books
        /// </summary>
        /// <param name="targetBook">Selected book</param>
        /// <param name="comparisonBook">Book being compared</param>
        /// <returns>Returns a double between -1 and 1 that describes how correlated two Book objects are in terms of reviews</returns>
        public double PearsonCorrelation(Book targetBook, Book comparisonBook)
        {
            
            // get list of raters who have reviewed both books
            List<Rater> similarRaters = new List<Rater>();
            similarRaters = targetBook.bookRating.Keys.Intersect(comparisonBook.bookRating.Keys).ToList();

            // if the books don't have any raters in common, return 0
            if (similarRaters.Count == 0)
            {
                return 0;
            }

            // sum up the reviews of the target and comparison book
            double targetReviews = 0.0;
            double comparisonReviews = 0.0;
            foreach (Rater r in similarRaters)
            {
                targetReviews += targetBook.bookRating[r];
                comparisonReviews += comparisonBook.bookRating[r];
            }

            // sum up the squares of the reviews
            double targetReviews2 = 0.0;
            double comparisonReviews2 = 0.0;
            foreach (Rater r in similarRaters)
            {
                targetReviews2 += Math.Pow(targetBook.bookRating[r], 2);
                comparisonReviews2 += Math.Pow(comparisonBook.bookRating[r], 2);
            }

            // sum the products
            double reviewProduct = 0.0;
            foreach (Rater r in similarRaters)
            {
                reviewProduct += targetBook.bookRating[r] * comparisonBook.bookRating[r];
            }

            // calculate coefficient
            double numerator = reviewProduct - ((targetReviews * comparisonReviews) / similarRaters.Count);
            double denominator = (double) Math.Sqrt((targetReviews2 - Math.Pow(targetReviews, 2) / similarRaters.Count) * ((comparisonReviews2 - Math.Pow(comparisonReviews, 2)) / similarRaters.Count));

            if (denominator == 0)
                return 0;

            return numerator / denominator;
        }

        /// <summary>
        /// Creates a list of recommended books using the Pearson Correlation Coefficient
        /// </summary>
        /// <param name="book">Book for which to get recommendations</param>
        /// <returns>Returns a list of recommended books</returns>
        public List<Book> getRecommendation(Book book)
        {
            // create sublist that excludes the target book
            List<Book> list = bookList.Where(x => x.title != book.title).ToList();
            
            // run through list and compute Pearson Correlation Coefficient for each list item and the target book
            list.OrderBy(x => PearsonCorrelation(book, x)).ToList();
            return list;
        }
    }

    /// <summary>
    /// Book object for recommendation system.
    /// </summary>
    public class Book
    {
        public String title;
        public String author;
        public Dictionary<Rater, double> bookRating;

        /// <summary>
        /// Constructs a Book object.
        /// </summary>
        /// <param name="title">Book title</param>
        /// <param name="author">Book author</param>
        public Book(String title, String author)
        {
            this.title = title;
            this.author = author;
            this.bookRating = new Dictionary<Rater, double>();
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
        public void addRating(Rater rater, double rating)
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
        public Dictionary<Book, double> ratedBooks;

        /// <summary>
        /// Constructs a Rater object
        /// </summary>
        /// <param name="name">Rater name</param>
        public Rater(String name)
        {
            this.name = name;
            this.ratedBooks = new Dictionary<Book, double>();
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
        public void addRating(Book book, double rating)
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
