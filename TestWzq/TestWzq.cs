using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using wzq_ai;

namespace TestWzq
{
    public partial class TestWzq : Form
    {
        private readonly StringBuilder sb = new StringBuilder();

        public TestWzq()
        {
            InitializeComponent();
        }


        private void computeBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void restarBtn_Click(object sender, EventArgs e)
        {

        }

        private void UpdateGoleText()
        {
            //var str = maxMin.Evaluate.ComputeTotalGole(curStatus).ToString();
            //sb.Clear();
            //for (int i = 0; i < str.Length; i++)
            //{
            //    if ((str[0] != '-' && i > 0 || i > 1) && (str.Length - i) % 2 == 0)
            //    {
            //        sb.Append(',');
            //    }
            //    sb.Append(str[i]);
            //}
            //goleLabel.Text = sb.ToString().TrimEnd(',');
        }

        private void redo_Click(object sender, EventArgs e)
        {

        }

        private void aotoComputeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}
