namespace TestWzq
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
            this.goleLabel = new System.Windows.Forms.Label();
            this.computeBtn = new System.Windows.Forms.Button();
            this.aotoComputeCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // goleLabel
            // 
            this.goleLabel.AutoSize = true;
            this.goleLabel.Location = new System.Drawing.Point(265, 2);
            this.goleLabel.Name = "goleLabel";
            this.goleLabel.Size = new System.Drawing.Size(59, 12);
            this.goleLabel.TabIndex = 0;
            this.goleLabel.Text = "goleLabel";
            // 
            // computeBtn
            // 
            this.computeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.computeBtn.Location = new System.Drawing.Point(197, 493);
            this.computeBtn.Name = "computeBtn";
            this.computeBtn.Size = new System.Drawing.Size(75, 23);
            this.computeBtn.TabIndex = 1;
            this.computeBtn.Text = "计算";
            this.computeBtn.UseVisualStyleBackColor = true;
            this.computeBtn.Click += new System.EventHandler(this.computeBtn_Click);
            // 
            // aotoComputeCheckBox
            // 
            this.aotoComputeCheckBox.AutoSize = true;
            this.aotoComputeCheckBox.Location = new System.Drawing.Point(316, 497);
            this.aotoComputeCheckBox.Name = "aotoComputeCheckBox";
            this.aotoComputeCheckBox.Size = new System.Drawing.Size(96, 16);
            this.aotoComputeCheckBox.TabIndex = 2;
            this.aotoComputeCheckBox.Text = "自动人机对战";
            this.aotoComputeCheckBox.UseVisualStyleBackColor = true;
            // 
            // TestWzq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(641, 518);
            this.Controls.Add(this.aotoComputeCheckBox);
            this.Controls.Add(this.computeBtn);
            this.Controls.Add(this.goleLabel);
            this.DoubleBuffered = true;
            this.Name = "TestWzq";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.Text = "TestWzq";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TestWzq_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label goleLabel;
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.CheckBox aotoComputeCheckBox;






    }
}