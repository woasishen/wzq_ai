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
            this.computeBtn = new System.Windows.Forms.Button();
            this.aotoComputeCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.winLabel = new System.Windows.Forms.Label();
            this.restarBtn = new System.Windows.Forms.Button();
            this.redo = new System.Windows.Forms.Button();
            this.goleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // computeBtn
            // 
            this.computeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.computeBtn.Location = new System.Drawing.Point(196, 501);
            this.computeBtn.Name = "computeBtn";
            this.computeBtn.Size = new System.Drawing.Size(75, 23);
            this.computeBtn.TabIndex = 1;
            this.computeBtn.Text = "计算";
            this.computeBtn.UseVisualStyleBackColor = true;
            this.computeBtn.Click += new System.EventHandler(this.computeBtn_Click);
            // 
            // aotoComputeCheckBox
            // 
            this.aotoComputeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aotoComputeCheckBox.AutoSize = true;
            this.aotoComputeCheckBox.Location = new System.Drawing.Point(306, 505);
            this.aotoComputeCheckBox.Name = "aotoComputeCheckBox";
            this.aotoComputeCheckBox.Size = new System.Drawing.Size(96, 16);
            this.aotoComputeCheckBox.TabIndex = 2;
            this.aotoComputeCheckBox.Text = "自动人机对战";
            this.aotoComputeCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.winLabel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(30, 40);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(579, 439);
            this.tableLayoutPanel.TabIndex = 3;
            this.tableLayoutPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.LawnGreen;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(573, 219);
            this.label1.TabIndex = 0;
            this.label1.Text = "五子连珠";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // winLabel
            // 
            this.winLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winLabel.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.winLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.winLabel.Location = new System.Drawing.Point(3, 219);
            this.winLabel.Name = "winLabel";
            this.winLabel.Size = new System.Drawing.Size(573, 220);
            this.winLabel.TabIndex = 1;
            this.winLabel.Text = "黑方胜";
            this.winLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // restarBtn
            // 
            this.restarBtn.Location = new System.Drawing.Point(549, 501);
            this.restarBtn.Name = "restarBtn";
            this.restarBtn.Size = new System.Drawing.Size(75, 23);
            this.restarBtn.TabIndex = 4;
            this.restarBtn.Text = "重新开始";
            this.restarBtn.UseVisualStyleBackColor = true;
            this.restarBtn.Click += new System.EventHandler(this.restarBtn_Click);
            // 
            // redo
            // 
            this.redo.Location = new System.Drawing.Point(468, 501);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(75, 23);
            this.redo.TabIndex = 5;
            this.redo.Text = "撤销";
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // goleLabel
            // 
            this.goleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goleLabel.Location = new System.Drawing.Point(27, 1);
            this.goleLabel.Name = "goleLabel";
            this.goleLabel.Size = new System.Drawing.Size(579, 17);
            this.goleLabel.TabIndex = 6;
            this.goleLabel.Text = "走起……";
            this.goleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestWzq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(639, 539);
            this.Controls.Add(this.goleLabel);
            this.Controls.Add(this.redo);
            this.Controls.Add(this.restarBtn);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.aotoComputeCheckBox);
            this.Controls.Add(this.computeBtn);
            this.DoubleBuffered = true;
            this.Name = "TestWzq";
            this.Padding = new System.Windows.Forms.Padding(30, 40, 30, 60);
            this.Text = "TestWzq";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TestWzq_MouseClick);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.CheckBox aotoComputeCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label winLabel;
        private System.Windows.Forms.Button restarBtn;
        private System.Windows.Forms.Button redo;
        private System.Windows.Forms.Label goleLabel;
    }
}