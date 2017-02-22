namespace TestWzq
{
    partial class MainForm
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
            this.autoComputeCheckBox = new System.Windows.Forms.CheckBox();
            this.restarBtn = new System.Windows.Forms.Button();
            this.redo = new System.Windows.Forms.Button();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.blackGoleLabel = new System.Windows.Forms.Label();
            this.whiteGoleLabel = new System.Windows.Forms.Label();
            this.totalGoleLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.computeTimesLabel = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.mainControl = new TestWzq.MainControl();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.bottomPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // computeBtn
            // 
            this.computeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.computeBtn.Location = new System.Drawing.Point(11, 6);
            this.computeBtn.Name = "computeBtn";
            this.computeBtn.Size = new System.Drawing.Size(75, 23);
            this.computeBtn.TabIndex = 1;
            this.computeBtn.Text = "计算";
            this.computeBtn.UseVisualStyleBackColor = true;
            this.computeBtn.Click += new System.EventHandler(this.computeBtn_Click);
            // 
            // autoComputeCheckBox
            // 
            this.autoComputeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoComputeCheckBox.AutoSize = true;
            this.autoComputeCheckBox.Checked = true;
            this.autoComputeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoComputeCheckBox.Location = new System.Drawing.Point(141, 9);
            this.autoComputeCheckBox.Name = "autoComputeCheckBox";
            this.autoComputeCheckBox.Size = new System.Drawing.Size(96, 16);
            this.autoComputeCheckBox.TabIndex = 2;
            this.autoComputeCheckBox.Text = "自动人机对战";
            this.autoComputeCheckBox.UseVisualStyleBackColor = true;
            this.autoComputeCheckBox.CheckedChanged += new System.EventHandler(this.autoComputeCheckBox_CheckedChanged);
            // 
            // restarBtn
            // 
            this.restarBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restarBtn.Location = new System.Drawing.Point(849, 6);
            this.restarBtn.Name = "restarBtn";
            this.restarBtn.Size = new System.Drawing.Size(75, 23);
            this.restarBtn.TabIndex = 4;
            this.restarBtn.Text = "重新开始";
            this.restarBtn.UseVisualStyleBackColor = true;
            this.restarBtn.Click += new System.EventHandler(this.restarBtn_Click);
            // 
            // redo
            // 
            this.redo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.redo.Location = new System.Drawing.Point(768, 6);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(75, 23);
            this.redo.TabIndex = 5;
            this.redo.Text = "撤销";
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.redo);
            this.bottomPanel.Controls.Add(this.computeBtn);
            this.bottomPanel.Controls.Add(this.autoComputeCheckBox);
            this.bottomPanel.Controls.Add(this.restarBtn);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 560);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(940, 32);
            this.bottomPanel.TabIndex = 8;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 5;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.Controls.Add(this.blackGoleLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.whiteGoleLabel, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.totalGoleLabel, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.timeLabel, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.computeTimesLabel, 4, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(940, 27);
            this.tableLayoutPanel.TabIndex = 10;
            // 
            // blackGoleLabel
            // 
            this.blackGoleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blackGoleLabel.Location = new System.Drawing.Point(3, 0);
            this.blackGoleLabel.Name = "blackGoleLabel";
            this.blackGoleLabel.Size = new System.Drawing.Size(182, 27);
            this.blackGoleLabel.TabIndex = 0;
            this.blackGoleLabel.Text = "黑方得分：";
            this.blackGoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // whiteGoleLabel
            // 
            this.whiteGoleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiteGoleLabel.Location = new System.Drawing.Point(191, 0);
            this.whiteGoleLabel.Name = "whiteGoleLabel";
            this.whiteGoleLabel.Size = new System.Drawing.Size(182, 27);
            this.whiteGoleLabel.TabIndex = 1;
            this.whiteGoleLabel.Text = "白方得分：";
            this.whiteGoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalGoleLabel
            // 
            this.totalGoleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalGoleLabel.Location = new System.Drawing.Point(379, 0);
            this.totalGoleLabel.Name = "totalGoleLabel";
            this.totalGoleLabel.Size = new System.Drawing.Size(182, 27);
            this.totalGoleLabel.TabIndex = 2;
            this.totalGoleLabel.Text = "本次走法得分：";
            this.totalGoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeLabel
            // 
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Location = new System.Drawing.Point(567, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(182, 27);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "计算时长";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // computeTimesLabel
            // 
            this.computeTimesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computeTimesLabel.Location = new System.Drawing.Point(755, 0);
            this.computeTimesLabel.Name = "computeTimesLabel";
            this.computeTimesLabel.Size = new System.Drawing.Size(182, 27);
            this.computeTimesLabel.TabIndex = 4;
            this.computeTimesLabel.Text = "递归次数";
            this.computeTimesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.logRichTextBox);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(644, 27);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(296, 533);
            this.rightPanel.TabIndex = 11;
            // 
            // mainControl
            // 
            this.mainControl.AutoCompute = true;
            this.mainControl.BackColor = System.Drawing.Color.Peru;
            this.mainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControl.Location = new System.Drawing.Point(0, 27);
            this.mainControl.Name = "mainControl";
            this.mainControl.Padding = new System.Windows.Forms.Padding(30);
            this.mainControl.Size = new System.Drawing.Size(644, 533);
            this.mainControl.StepStatusChanged = null;
            this.mainControl.TabIndex = 9;
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(296, 533);
            this.logRichTextBox.TabIndex = 0;
            this.logRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(940, 592);
            this.Controls.Add(this.mainControl);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.bottomPanel);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.CheckBox autoComputeCheckBox;
        private System.Windows.Forms.Button restarBtn;
        private System.Windows.Forms.Button redo;
        private System.Windows.Forms.Panel bottomPanel;
        private TestWzq.MainControl mainControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label blackGoleLabel;
        private System.Windows.Forms.Label whiteGoleLabel;
        private System.Windows.Forms.Label totalGoleLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label computeTimesLabel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.RichTextBox logRichTextBox;
    }
}