using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecEngModel;          // reference to Model

namespace View
{
    public partial class View: UserControl
    {
        Model m;
        bool first = false;
        Book queryBook;
        
        /// <summary>
        /// Constructs a View object
        /// </summary>
        public View()
        {
            InitializeComponent();
            m = new Model();

            Book a = new Book("Matrix Analysis", "Roger Horn");
            Book b = new Book("Matrix Computations", "Gene Golub");
            Book c = new Book("The Matrix", "The Wachowskis");
            Rater one = new Rater("Bertrand Russell");
            Rater two = new Rater("William Hamilton");
            Rater three = new Rater("Johann Gauss");
            Rater four = new Rater("Georg Frobenius");

            m.addBook(a);
            m.addBook(b);
            m.addBook(c);
            a.addRating(one, 5.0);
            a.addRating(two, 5.0);
            a.addRating(four, 5.0);
            b.addRating(one, 5.0);
            b.addRating(three, 5.0);
            b.addRating(four, 5.0);
            c.addRating(two, 1.0);
            c.addRating(three, 1.0);
            c.addRating(four, 1.0);
        }

        /// <summary>
        /// Event handler for Go button click
        /// </summary>
        private void goButton_Click(object sender, EventArgs e)
        {
            string title = queryBox.Text;
            if (title == "")
            {
                MessageBox.Show("You must enter a title to get a recommendation.");
            }
            try
            {
                queryBook = m.getBookByTitle(title)[0];
                List<Book> recommendations = m.getRecommendation(queryBook);
                List<string> list = new List<string>();
                foreach (Book b in recommendations)
                {
                    list.Add(b.title);
                }

                first = true;
                recBox.DataSource = list;
                
            }
            catch 
            {
                first = true;
                recBox.DataSource = new List<string>(){"No matches found"};
            }
            recBox.Visible = true;
        }
        
        /// <summary>
        /// Event handler for Enter key
        /// </summary>
        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goButton.PerformClick();
            }
        }

        /// <summary>
        /// Event handler for recommendation box item selection
        /// </summary>
        private void recBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                try
                {
                    string current = (string)recBox.Items[recBox.SelectedIndex];
                    Book b = m.getBookByTitle(current)[0];
                    string title = b.title;
                    titleLabel.Text = title;
                    string author = b.author;
                    authorLabel.Text = author;
                    double corrRating = m.PearsonCorrelation(queryBook, b);
                    double scaledRating = (corrRating + 1) * 2 + 1;
                    if (corrRating == 0.0)              // math hack for edge cases of numerical instability
                    {
                        double sum = 0.0;
                        foreach (KeyValuePair<Rater, double> r in b.bookRating)
                        {
                            sum += r.Value;
                        }
                        scaledRating = sum / b.bookRating.Count;
                    }

                    ratingLabel.Text = Math.Round(scaledRating, 2).ToString();
                    ratingLabel.Visible = true;
                }
                catch { }
            }
            first = false;
        } 
    }
}
