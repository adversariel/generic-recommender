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
        string queryBook;
        
        /// <summary>
        /// Constructs a View object
        /// </summary>
        public View()
        {
            InitializeComponent();
            m = new Model();
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
            List<string> recommendations = m.getRecommendation(title);

            first = true;
            recBox.DataSource = recommendations;
            if (recommendations.Count == 0)
                recBox.DataSource = new List<string>() { "No matches found" };
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
                    string title = current;
                    titleLabel.Text = title;
                    string author = m.getAuthor(title);
                    authorLabel.Text = author;
                    double corrRating = 0.0; // m.PearsonCorrelation(queryBook, b);   FIX THIS LATER
                    double scaledRating = (corrRating + 1) * 2 + 1;
                    /*if (corrRating == 0.0)              // math hack for edge cases of numerical instability
                    {
                        double sum = 0.0;
                        foreach (KeyValuePair<Rater, double> r in b.bookRating)
                        {
                            sum += r.Value;
                        }
                        scaledRating = sum / b.bookRating.Count;
                    }*/

                    ratingLabel.Text = Math.Round(scaledRating, 2).ToString();
                    ratingLabel.Visible = true;
                }
                catch { }
            }
            first = false;
        } 
    }
}
