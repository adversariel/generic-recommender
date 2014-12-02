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

namespace Client
{
    public partial class View: Form
    {
        bool first = false;
        string queryBook;
        StringSocket ss;
        TcpClient tc;
        List<string> ids = new List<string>();
        
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
            StringSocket ss = (StringSocket)payload;
            if(message == null)         // handles forcible closure
            {
                return;
            }
            else if (message.ToUpper().StartsWith("RECOMMEND "))
            {
                message = message.Substring(10);
                first = true;
                message = message.Replace("\"", "");
                List<String> recs = new List<String>(message.Split(new String[]{@"%%%"}, StringSplitOptions.None));
                List<String> recommendations = new List<String>();
                ids = new List<string>();
                for (int i = 0; i < recs.Count; i+=2 )
                {
                    ids.Add(recs[i]);
                    recommendations.Add(recs[i + 1]);

                }
                recBox.Invoke((Action)(() =>
                {
                    recBox.DataSource = recommendations;
                    if (recommendations.Count == 0)
                        recBox.DataSource = new List<string>() { "No matches found" };
                    recBox.Visible = true;
                    loadingBar.Visible = false;
                    loadingLabel.Visible = false;
                }));
            }
            else if(message.ToUpper().StartsWith("AUTHOR "))
            {
                message = message.Substring(7);
                authorLabel.Invoke((Action)(()=>authorLabel.Text = message));
            }
            
            ss.BeginReceive(messageReceived, ss);
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
                if (e.GetType() == typeof (SocketException))
                {
                    MessageBox.Show("Server closed unexpectedly. Please restart the client.");
                }
                else
                {
                    throw e;
                }
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
            ss.BeginSend("RECOMMEND " + title + "\n", sendCallback, null);
            loadingLabel.Visible = true;
            loadingBar.Visible = true;
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
                    ss.BeginSend("AUTHOR " + ids[recBox.SelectedIndex] + "\n", sendCallback, null);
                }
                catch { }
            }
            first = false;
        } 
    }
}
