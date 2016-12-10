using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TestWzq.Properties;

namespace wzq_ai
{
    public partial class TestWzq : Form
    {
        private const int CELL_W = 15;
        private const int CELL_H = 15;
        private const float ITEM_SIZE_SCALE = 0.5f;
        private static readonly Dictionary<CellStatus, Brush> CellBrush =
            new Dictionary<CellStatus, Brush>
            {
                {CellStatus.Black, new SolidBrush(Color.Black)},
                {CellStatus.White, new SolidBrush(Color.White)}
            };
        private float cellWStep;
        private float cellHStep;
        private readonly Evaluate evaluate;
        private readonly CellStatus[][] cellArr;
        private readonly Pen linePen = new Pen(Color.Black, 1);
        private float itemDiameter;
        private bool isManual = true;
        
        public TestWzq()
        {
            InitializeComponent();
            evaluate = new Evaluate(CELL_W, CELL_H);
            cellArr = new CellStatus[CELL_W][];
            for (var i = 0; i < cellArr.Length; i++)
            {
                cellArr[i] = new CellStatus[CELL_H];
                for (int j = 0; j < cellArr[i].Length; j++)
                {
                    cellArr[i][j] = CellStatus.Empty;
                }
            }
            ReInitCellSize();
        }

        private void ReInitCellSize()
        {
            cellWStep = ((float)ClientSize.Width - Padding.Left - Padding.Right) / (CELL_W - 1);
            cellHStep = ((float)ClientSize.Height - Padding.Top - Padding.Bottom) / (CELL_H - 1);
            itemDiameter = Math.Min(cellWStep, cellHStep)*ITEM_SIZE_SCALE;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawLines(e.Graphics);
        }

        private void DrawLines(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < CELL_W; i++)
            {
                g.DrawLine(linePen,
                    i * cellWStep + Padding.Left,
                    Padding.Top,
                    i * cellWStep + Padding.Left,
                    ClientSize.Height - Padding.Bottom);
            }
            for (int i = 0; i < CELL_H; i++)
            {
                g.DrawLine(linePen,
                    Padding.Left,
                    i * cellHStep + Padding.Top,
                    ClientSize.Width - Padding.Right,
                    i * cellHStep + Padding.Top);
            }

            for (int i = 0; i < CELL_W; i++)
            {
                for (int j = 0; j < CELL_H; j++)
                {
                    if (cellArr[i][j] == CellStatus.Empty)
                    {
                        continue;
                    }
                    g.FillEllipse(CellBrush[cellArr[i][j]],
                        Padding.Left + i * cellWStep - itemDiameter / 2,
                        Padding.Top + j * cellHStep - itemDiameter / 2,
                        itemDiameter,
                        itemDiameter); 
                }
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Refresh();
            ReInitCellSize();
        }

        private void TestWzq_MouseClick(object sender, MouseEventArgs e)
        {
            var x = (int)Math.Round((e.X - Padding.Left)/cellWStep);
            var y = (int) Math.Round((e.Y - Padding.Top)/cellHStep);
            if (x < 0 || y < 0 || x > CELL_W || y > CELL_H || cellArr[x][y] != CellStatus.Empty)
            {
                return;
            }
            cellArr[x][y] = isManual ? CellStatus.Black : CellStatus.White;
            isManual = !isManual;
            Refresh();
        }
    }
}
