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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goButton_Click(object sender, EventArgs e)
        {
            string title = queryBox.Text;
            try
            {
                Book book = m.getBookByTitle(title)[0];
                List<Book> recs = m.getRecommendation(book);

                recBox.DataSource = recs;
                recBox.Visible = true;
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// Event handler for Enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goButton.PerformClick();
                
            }
        }
        
    }
}
