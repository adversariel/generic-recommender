using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 

namespace RecEngModel
{
    public class Model
    {
        // MySQL string. 
        // Yes, this information works (as of this commit). If you drop my tables, I'll be sad.
        private string str = "server=155.97.209.239;" +
                             "uid=ariel;" +
                             "pwd=validpassword1;" +
                             "database=recommender; ";

        /// <summary>
        /// Model object to be passed to viewer and controller.
        /// </summary>
        public Model()
        {
            
        }

        /// <summary>
        /// Connects to the MySQL database and returns a list of ratings of the input book.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Returns list of ratings from raters who have rated the input book.</returns>
        public List<String[]> getData(string book)
        {
            List<String[]> ratings = new List<String[]>();
            
            // instancing and object handling
            using (MySqlConnection myConnection = new MySqlConnection(str)) 
            {
                // connection handling
                try
                {
                    myConnection.Open();
                    
                    // get the book ID
                    string bookstr1 = "SELECT ID FROM books WHERE Title=" + book + " LIMIT 1;";
                    MySqlCommand comm1 = new MySqlCommand(bookstr1,myConnection);
                    string id1 = Convert.ToString(comm1.ExecuteScalar());
                    if(id1 == "")
                    {
                        throw new Exception("Book not found: " + book);
                    }

                    // get list of all ratings by raters who have read the book
                    string sqlstr = "select * from ratings where ID in (select ID from ratings where Book="+id1+");";
                    MySqlCommand comm = new MySqlCommand(sqlstr,myConnection);
                    using (MySqlDataReader myReader = comm.ExecuteReader())
                    {
                        // get a line of the list
                        while (myReader.Read())
                        {
                            String[] data = new String[3];
                            data[0] = myReader.GetString(0);
                            data[1] = myReader.GetString(1);
                            data[2] = myReader.GetString(2);
                            ratings.Add(data);
                        }
                    }
                }
                finally
                {
                    myConnection.Close();
                }
            }
            return ratings;
        }

        /// <summary>
        /// Helper method to get book data from MySQL database.
        /// </summary>
        /// <param name="sqlstring"></param>
        /// <returns>Returns scalar value.</returns>
        public string sqlConnection(string sqlstring)
        {
            using (MySqlConnection myConnection = new MySqlConnection(str))
            {
                // connection handling
                try
                {
                    myConnection.Open();
                    MySqlCommand comm = new MySqlCommand(sqlstring, myConnection);
                    string id = Convert.ToString(comm.ExecuteScalar());
                    if (id == "")
                    {
                        throw new Exception("Book not found");
                    }
                    return id;
                }
                finally
                {
                    myConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get book ID from MySQL.
        /// </summary>
        /// <param name="bookname"></param>
        /// <returns>Returns book ID.</returns>
        public string getID(string bookname)
        {
            string bookstr = "SELECT ID FROM books WHERE Title=" + bookname + " LIMIT 1;";
            return sqlConnection(bookstr);
        }

        /// <summary>
        /// Get book author from MySQL.
        /// </summary>
        /// <param name="bookname"></param>
        /// <returns>Returns book author.</returns>
        public string getAuthor(string bookname)
        {
            string bookstr = "SELECT Author FROM books WHERE Title=" + bookname + " LIMIT 1;";
            return sqlConnection(bookstr);
        }

        /// <summary>
        /// Get book title from MySQL.
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>Returns book title</returns>
        public string getTitle(string bookID)
        {
            string bookstr = "SELECT Title FROM books WHERE ID=" + bookID + " LIMIT 1;";
            return sqlConnection(bookstr);
        }

        /// <summary>
        /// Calculates the Pearson correlation coefficient on the reviews for two books
        /// </summary>
        /// <param name="targetBook">Selected book</param>
        /// <param name="comparisonBook">Book being compared</param>
        /// <returns>Returns a double between -1 and 1 that describes how correlated two Book objects are in terms of reviews</returns>
        public double PearsonCorrelation(string targetBook, string comparisonBook, List<String[]> ratings)
        {
            // get list of raters who have reviewed both books
            Dictionary<string,double> similarRaters = new Dictionary<string,double>();
            Dictionary<string,double> primaryRaters = new Dictionary<string,double>();
            foreach (String[] rating in ratings)
            {
                if(rating[1]==comparisonBook){
                    if(!primaryRaters.ContainsKey(rating[0]))
                    similarRaters.Add(rating[0],Convert.ToDouble(rating[2]));
                }
                if(rating[1]==targetBook){
                    if(!primaryRaters.ContainsKey(rating[0]))
                        primaryRaters.Add(rating[0],Convert.ToDouble(rating[2]));
                }
            }

            // if the books don't have any raters in common, return 0
            if (similarRaters.Count == 0)
            {
                return 0;
            }

            // sum up the reviews of the target and comparison book
            double targetReviews = 0.0;
            double comparisonReviews = 0.0;
            foreach (KeyValuePair<string,double> r in similarRaters)
            {
                targetReviews += primaryRaters[r.Key];
                comparisonReviews += similarRaters[r.Key];
            }

            // sum up the squares of the reviews
            double targetReviews2 = 0.0;
            double comparisonReviews2 = 0.0;
            foreach (KeyValuePair<string,double> r in similarRaters)
            {
                targetReviews2 += Math.Pow(similarRaters[r.Key], 2);
                comparisonReviews2 += Math.Pow(primaryRaters[r.Key], 2);
            }

            // sum the products
            double reviewProduct = 0.0;
            foreach (KeyValuePair<string,double> r in similarRaters)
            {
                reviewProduct += primaryRaters[r.Key] * similarRaters[r.Key];
            }

            // calculate coefficient
            double numerator = reviewProduct - (similarRaters.Count * (targetReviews * comparisonReviews));
            double denominator = Math.Sqrt((similarRaters.Count * targetReviews2 - Math.Pow(targetReviews, 2)) * ((similarRaters.Count * comparisonReviews2 - Math.Pow(comparisonReviews, 2))));

            if (denominator == 0)
                return 0;

            return numerator / denominator;
        }

        /// <summary>
        /// Creates a list of recommended books using the Pearson Correlation Coefficient
        /// </summary>
        /// <param name="book">Book for which to get recommendations</param>
        /// <returns>Returns a list of recommended books</returns>
        public List<String> getRecommendation(String book)
        {
            List<String[]> ratings = getData(book);

            string bookID = getID(book);

            // create sublist  that contains recommended books
            HashSet<string> list = new HashSet<string>();
            foreach (String[] r in ratings)
            {
                if (r[1] != bookID)
                {
                    list.Add(r[1]);
                }
            }
            
            // run through list and compute Pearson Correlation Coefficient for each list item and the target book
            List<string> ids = list.OrderBy(x => PearsonCorrelation(bookID, x, ratings)).ToList();
            List<string> names = new List<string>();
            foreach (string id in ids)
            {
                names.Add(getTitle(id));
            }
            return names;
        }
    }

}
