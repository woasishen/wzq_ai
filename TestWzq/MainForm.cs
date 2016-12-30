using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using wzq_ai;

namespace TestWzq
{
    public partial class MainForm : Form
    {
        private readonly StringBuilder sb = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();
            mainControl.StepStatusChanged += UpdateGoleText;
        }


        private void computeBtn_Click(object sender, EventArgs e)
        {
            mainControl.GeneNextStep();
        }

        private void restarBtn_Click(object sender, EventArgs e)
        {
            mainControl.RestartGame();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            mainControl.Redo();
        }

        private void autoComputeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainControl.AutoCompute = autoComputeCheckBox.Checked;
        }

        private void UpdateGoleText()
        {
            UpdateGoleLabel(CellStatus.Black);
            UpdateGoleLabel(CellStatus.White);
        }

        private void UpdateGoleLabel(CellStatus cellStatus)
        {
            var str = mainControl.MaxMin.Evaluate.ComputeGole(cellStatus).ToString();
            sb.Clear();
            sb.Append(cellStatus);
            sb.Append(":");
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[0] != '-' && i > 0 || i > 1) && (str.Length - i)%2 == 0)
                {
                    sb.Append(',');
                }
                sb.Append(str[i]);
            }

            Label tempLabel;
            switch (cellStatus)
            {
                case CellStatus.Black:
                    tempLabel = balckGoleLabel;
                    break;
                case CellStatus.White:
                    tempLabel = whiteGoleLabel;
                    break;
                default:
                    throw new NotSupportedException();
            }
            tempLabel.Text = sb.ToString().TrimEnd(',');
        }
    }
}
