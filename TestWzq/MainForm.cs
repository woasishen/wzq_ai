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
            mainControl.StepStatusChanged = UpdateGoleText;
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
            Label tempLabel;
            switch (cellStatus)
            {
                case CellStatus.Black:
                    tempLabel = blackGoleLabel;
                    break;
                case CellStatus.White:
                    tempLabel = whiteGoleLabel;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
