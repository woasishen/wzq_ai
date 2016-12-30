using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using wzq_ai;

namespace TestWzq
{
    public partial class MainControl : UserControl
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

        private bool gameOver;
        private float cellWStep;
        private float cellHStep;
        private readonly MaxMin maxMin;
        private readonly CellStatus[][] cellArr;
        private readonly Pen linePen = new Pen(Color.Black, 1);
        private float itemDiameter;
        private readonly Brush numBrush = new SolidBrush(Color.Red);

        private CellStatus curStatus = CellStatus.Black;
        private CellStatus CurStatus
        {
            set
            {
                curStatus = value;
                StepStatusChanged.Invoke();
            }
            get { return curStatus; }
        }

        private readonly Stack<Pos> steps = new Stack<Pos>();
        private const float NUM_W = 20;
        private const float NUM_H = 20;

        public MaxMin MaxMin => maxMin;

        public bool AutoCompute { get; set; }

        public Action StepStatusChanged { set; get; }

        public void GeneNextStep()
        {
            if (gameOver)
            {
                return;
            }
            var result = maxMin.GeneBestPos(CurStatus, 0);
            var pos = result.PosStack.Peek();
            cellArr[pos.X][pos.Y] = CurStatus;
            CheckGameOver(pos);
            CurStatus = CellStatusHelper.Not(CurStatus);
            Refresh();
        }

        public void Redo()
        {
            if (steps.Count < 2)
            {
                var pos = steps.Pop();
                cellArr[pos.X][pos.Y] = CellStatus.Empty;
                CurStatus = CellStatusHelper.Not(CurStatus);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    var pos = steps.Pop();
                    cellArr[pos.X][pos.Y] = CellStatus.Empty;
                }
                CurStatus = CellStatus.Black;
            }
            gameOver = false;
            Refresh();
        }

        public void RestartGame()
        {
            tableLayoutPanel.Visible = false;
            foreach (var cellLine in cellArr)
            {
                for (var j = 0; j < cellLine.Length; j++)
                {
                    cellLine[j] = CellStatus.Empty;
                }
            }
            CurStatus = CellStatus.Black;
            gameOver = false;
            Refresh();
        }

        public MainControl()
        {
            InitializeComponent();

            cellArr = new CellStatus[CELL_W][];
            for (var i = 0; i < cellArr.Length; i++)
            {
                cellArr[i] = new CellStatus[CELL_H];
                for (var j = 0; j < cellArr[i].Length; j++)
                {
                    cellArr[i][j] = CellStatus.Empty;
                }
            }
            maxMin = new MaxMin(cellArr, CELL_W, CELL_H);
            ReInitCellSize();
        }

        private void ReInitCellSize()
        {
            cellWStep = ((float)ClientSize.Width - Padding.Left - Padding.Right) / (CELL_W - 1);
            cellHStep = ((float)ClientSize.Height - Padding.Top - Padding.Bottom) / (CELL_H - 1);
            itemDiameter = Math.Min(cellWStep, cellHStep) * ITEM_SIZE_SCALE;
        }

        #region DrawView

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            tableLayoutPanel.Visible = gameOver;
            if (gameOver)
            {
                winLabel.Text = CurStatus == CellStatus.Black ? @"白方胜" : @"黑方胜";
                winLabel.ForeColor = CurStatus == CellStatus.Black ? Color.White : Color.Black;
                return;
            }

            DrawLines(e.Graphics);
            DrawNums(e.Graphics);
            DrawCells(e.Graphics);
        }

        private void DrawNums(Graphics g)
        {
            g.DrawString(
                "0",
                DefaultFont,
                numBrush,
                new RectangleF(Padding.Left - NUM_W, Padding.Top - NUM_H, NUM_W, NUM_H),
                new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                });
            for (int i = 1; i < CELL_W; i++)
            {
                g.DrawString(
                    i.ToString(),
                    DefaultFont,
                    numBrush,
                    new RectangleF(
                        i * cellWStep + Padding.Left - NUM_W / 2,
                        Padding.Top - NUM_H,
                        NUM_W,
                        NUM_H),
                    new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Far
                    });
            }
            for (int i = 1; i < CELL_H; i++)
            {
                g.DrawString(
                    i.ToString(),
                    DefaultFont,
                    numBrush,
                    new RectangleF(
                        Padding.Left - NUM_W,
                        i * cellHStep + Padding.Top - NUM_H / 2,
                        NUM_W,
                        NUM_H),
                    new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center
                    });
            }
        }

        private void DrawLines(Graphics g)
        {
            for (var i = 0; i < CELL_W; i++)
            {
                g.DrawLine(linePen,
                    i * cellWStep + Padding.Left,
                    Padding.Top,
                    i * cellWStep + Padding.Left,
                    ClientSize.Height - Padding.Bottom);
            }
            for (var i = 0; i < CELL_H; i++)
            {
                g.DrawLine(linePen,
                    Padding.Left,
                    i * cellHStep + Padding.Top,
                    ClientSize.Width - Padding.Right,
                    i * cellHStep + Padding.Top);
            }
        }

        private void DrawCells(Graphics g)
        {
            for (var i = 0; i < CELL_W; i++)
            {
                for (var j = 0; j < CELL_H; j++)
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

        #endregion

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Refresh();
            ReInitCellSize();
        }

        private void MainControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameOver)
            {
                return;
            }
            var x = (int)Math.Round((e.X - Padding.Left) / cellWStep);
            var y = (int)Math.Round((e.Y - Padding.Top) / cellHStep);
            if (x < 0 || y < 0 || x > CELL_W || y > CELL_H || cellArr[x][y] != CellStatus.Empty)
            {
                return;
            }

            cellArr[x][y] = CurStatus;
            CheckGameOver(new Pos(x, y));
            CurStatus = CellStatusHelper.Not(CurStatus);
            Refresh();
            if (AutoCompute)
            {
                GeneNextStep();
            }
        }

        private void CheckGameOver(Pos pos)
        {
            steps.Push(pos);
            if (maxMin.Evaluate.ComputePosGoleForSelf(CurStatus, pos) == Evaluate.GOLE_DICT[5])
            {
                gameOver = true;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Refresh();
        }
    }
}
