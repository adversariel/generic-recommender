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
        
        public View()
        {
            InitializeComponent();
            m = new Model();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            recBox.Visible = true;
        }

        
    }
}
