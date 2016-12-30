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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.whiteGoleLabel = new System.Windows.Forms.Label();
            this.balckGoleLabel = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.mainControl = new TestWzq.MainControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.bottomPanel.SuspendLayout();
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
            this.restarBtn.Location = new System.Drawing.Point(548, 6);
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
            this.redo.Location = new System.Drawing.Point(467, 6);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(75, 23);
            this.redo.TabIndex = 5;
            this.redo.Text = "撤销";
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.whiteGoleLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.balckGoleLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(639, 34);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // whiteGoleLabel
            // 
            this.whiteGoleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiteGoleLabel.Location = new System.Drawing.Point(322, 0);
            this.whiteGoleLabel.Name = "whiteGoleLabel";
            this.whiteGoleLabel.Size = new System.Drawing.Size(314, 34);
            this.whiteGoleLabel.TabIndex = 1;
            this.whiteGoleLabel.Text = "label2";
            this.whiteGoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // balckGoleLabel
            // 
            this.balckGoleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.balckGoleLabel.Location = new System.Drawing.Point(3, 0);
            this.balckGoleLabel.Name = "balckGoleLabel";
            this.balckGoleLabel.Size = new System.Drawing.Size(313, 34);
            this.balckGoleLabel.TabIndex = 0;
            this.balckGoleLabel.Text = "label1";
            this.balckGoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.redo);
            this.bottomPanel.Controls.Add(this.computeBtn);
            this.bottomPanel.Controls.Add(this.autoComputeCheckBox);
            this.bottomPanel.Controls.Add(this.restarBtn);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 507);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(639, 32);
            this.bottomPanel.TabIndex = 8;
            // 
            // mainControl
            // 
            this.mainControl.AutoCompute = false;
            this.mainControl.BackColor = System.Drawing.Color.Peru;
            this.mainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainControl.Location = new System.Drawing.Point(0, 34);
            this.mainControl.Name = "mainControl";
            this.mainControl.Padding = new System.Windows.Forms.Padding(30);
            this.mainControl.Size = new System.Drawing.Size(639, 473);
            this.mainControl.StepStatusChanged = null;
            this.mainControl.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(639, 539);
            this.Controls.Add(this.mainControl);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "TestWzq";
            this.Text = "MainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.CheckBox autoComputeCheckBox;
        private System.Windows.Forms.Button restarBtn;
        private System.Windows.Forms.Button redo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label whiteGoleLabel;
        private System.Windows.Forms.Label balckGoleLabel;
        private System.Windows.Forms.Panel bottomPanel;
        private TestWzq.MainControl mainControl;
    }
}