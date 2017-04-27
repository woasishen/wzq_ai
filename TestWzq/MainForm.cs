using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wzq_ai;

namespace TestWzq
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Configs.ShowStep = showStepCheckBox.Checked;
            mainControl.AutoCompute = autoComputeCheckBox.Checked;
            mainControl.StepStatusChanged = UpdateGoleText;
            mainControl.ComputeFinished = (i, span) =>
            {
                computeTimesLabel.Text = span.ToString();
                timeLabel.Text = i.ToString();
            };
            Configs.LogMsg += s =>
            {
                logRichTextBox.AppendText(s);
                logRichTextBox.AppendText(Environment.NewLine);
            };
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
            totalGoleLabel.Text = mainControl.GetRoleGole().ToString();
        }

        private void showStepCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Configs.ShowStep = showStepCheckBox.Checked;
        }
    }
}
