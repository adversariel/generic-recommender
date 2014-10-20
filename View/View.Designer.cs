namespace View
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.queryBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.recBox = new System.Windows.Forms.ListBox();
            this.bookDetails = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.staticPredicted = new System.Windows.Forms.Label();
            this.ratingLabel = new System.Windows.Forms.Label();
            this.metricLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(43, 151);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(282, 20);
            this.queryBox.TabIndex = 0;
            this.queryBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.queryBox_KeyDown);
            // 
            // goButton
            // 
            this.goButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(84)))));
            this.goButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.goButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.goButton.Location = new System.Drawing.Point(327, 146);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 30);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "go";
            this.goButton.UseVisualStyleBackColor = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(56, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "what did I like?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(98, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "What did you read?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Book Recommender";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.label4.Location = new System.Drawing.Point(38, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(364, 84);
            this.label4.TabIndex = 5;
            this.label4.Text = "1. Type in a book title you enjoy\r\n2. Press Go\r\n3. Get a ranked list of book reco" +
    "mmendations\r\n4. Select a book to get more details\r\n";
            // 
            // recBox
            // 
            this.recBox.FormattingEnabled = true;
            this.recBox.Items.AddRange(new object[] {
            "No matches found"});
            this.recBox.Location = new System.Drawing.Point(43, 183);
            this.recBox.Name = "recBox";
            this.recBox.Size = new System.Drawing.Size(282, 69);
            this.recBox.TabIndex = 6;
            this.recBox.Visible = false;
            this.recBox.SelectedIndexChanged += new System.EventHandler(this.recBox_SelectedIndexChanged);
            // 
            // bookDetails
            // 
            this.bookDetails.AutoSize = true;
            this.bookDetails.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bookDetails.Location = new System.Drawing.Point(468, 50);
            this.bookDetails.Name = "bookDetails";
            this.bookDetails.Size = new System.Drawing.Size(115, 21);
            this.bookDetails.TabIndex = 7;
            this.bookDetails.Text = "Book Details";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.titleLabel.Location = new System.Drawing.Point(478, 85);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(41, 21);
            this.titleLabel.TabIndex = 8;
            this.titleLabel.Text = "Title";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Italic);
            this.authorLabel.Location = new System.Drawing.Point(480, 113);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(53, 17);
            this.authorLabel.TabIndex = 9;
            this.authorLabel.Text = "Author";
            // 
            // staticPredicted
            // 
            this.staticPredicted.AutoSize = true;
            this.staticPredicted.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.staticPredicted.Location = new System.Drawing.Point(478, 166);
            this.staticPredicted.Name = "staticPredicted";
            this.staticPredicted.Size = new System.Drawing.Size(171, 20);
            this.staticPredicted.TabIndex = 10;
            this.staticPredicted.Text = "Your predicted rating: ";
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ratingLabel.Location = new System.Drawing.Point(512, 214);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(81, 21);
            this.ratingLabel.TabIndex = 11;
            this.ratingLabel.Text = "no rating";
            this.ratingLabel.Visible = false;
            // 
            // metricLabel
            // 
            this.metricLabel.AutoSize = true;
            this.metricLabel.Location = new System.Drawing.Point(479, 190);
            this.metricLabel.Name = "metricLabel";
            this.metricLabel.Size = new System.Drawing.Size(93, 13);
            this.metricLabel.TabIndex = 12;
            this.metricLabel.Text = "(between 1 and 5)";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.metricLabel);
            this.Controls.Add(this.ratingLabel);
            this.Controls.Add(this.staticPredicted);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.bookDetails);
            this.Controls.Add(this.recBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.queryBox);
            this.Name = "View";
            this.Size = new System.Drawing.Size(724, 281);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox queryBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox recBox;
        private System.Windows.Forms.Label bookDetails;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label staticPredicted;
        private System.Windows.Forms.Label ratingLabel;
        private System.Windows.Forms.Label metricLabel;
    }
}
