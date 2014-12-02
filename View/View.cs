using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecEngModel;                  // reference to Model
using CustomNetworking;
using System.Net.Sockets;          

namespace View
{
    public partial class View: Form
    {
        bool first = false;
        string queryBook;
        StringSocket ss;
        TcpClient tc;
        
        /// <summary>
        /// Constructs a View object
        /// </summary>
        public View()
        {
            InitializeComponent();
            tc = new TcpClient("155.97.209.239", 2000);
            ss = new StringSocket(tc.Client, new UTF8Encoding());
            ss.BeginReceive(messageReceived, ss);
            this.FormClosing += Closing;
        }

        static void Main(string[] args)
        {
            View viewer = new View();
        }

        /// <summary>
        /// Closes the client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Closing(Object sender, FormClosingEventArgs e)
        {
            tc.Close();
        }

        /// <summary>
        /// Recieves message from server.
        /// </summary>
        /// <param name="message">Protocol string</param>
        /// <param name="e">Exception thrown by String Socket (if any)</param>
        /// <param name="payload">String Socket object</param>
        private void messageReceived(string message, Exception e, Object payload)
        {
            if (message.ToUpper().StartsWith("RECOMMEND "))
            {
                message = message.Substring(10);
                first = true;
                List<String> recommendations = new List<String>(message.Split(new String[]{@"%%%"}, StringSplitOptions.None));
                recBox.DataSource = recommendations;
                if (recommendations.Count == 0)
                    recBox.DataSource = new List<string>() { "No matches found" };
                recBox.Visible = true;
            }
            else if(message.ToUpper().StartsWith("AUTHOR "))
            {
                message = message.Substring(7);
                authorLabel.Text = message;
            }

        }

        /// <summary>
        /// Throws exceptions.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="payload"></param>
        private void sendCallback(Exception e, Object payload)
        {
            if (e != null && e.GetType() != typeof(ObjectDisposedException))
            {
                throw e;
            }
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
            ss.BeginSend("RECOMMEND " + title, sendCallback, null);      
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
                    ss.BeginSend("AUTHOR " + title, sendCallback, null);

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
