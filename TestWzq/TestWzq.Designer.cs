namespace wzq_ai
{
    partial class TestWzq
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.goldLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // goldLabel
            // 
            this.goldLabel.AutoSize = true;
            this.goldLabel.Location = new System.Drawing.Point(265, 2);
            this.goldLabel.Name = "goldLabel";
            this.goldLabel.Size = new System.Drawing.Size(41, 12);
            this.goldLabel.TabIndex = 0;
            this.goldLabel.Text = "goldLabel";
            // 
            // TestWzq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(641, 518);
            this.Controls.Add(this.goldLabel);
            this.DoubleBuffered = true;
            this.Name = "TestWzq";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.Text = "TestWzq";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TestWzq_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goldLabel;






    }
}