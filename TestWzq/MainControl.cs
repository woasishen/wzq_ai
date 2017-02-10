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
        private float itemDiameter;

        private readonly Pen linePen = new Pen(Color.Black, 1);
        private readonly Brush numBrush = new SolidBrush(Color.Red);
        private readonly Brush gameOverBrush = new SolidBrush(Color.Green);
        private readonly Pen curStepTips = new Pen(Color.Green, 3);

        private CellStatus curStatus = CellStatus.Black;
        public CellStatus CurStatus
        {
            private set
            {
                StepStatusChanged.Invoke();
                curStatus = value;
            }
            get { return curStatus; }
        }

        private readonly Stack<Pos> steps = new Stack<Pos>();
        private const float NUM_W = 20;
        private const float NUM_H = 20;

        public bool AutoCompute { get; set; }

        public Action StepStatusChanged { set; get; }

        public void GeneNextStep()
        {
            if (gameOver)
            {
                return;
            }
        }

        public void Redo()
        {
            if (steps.Count == 0)
            {
                return;
            }
            var pos = steps.Pop();
            CurStatus = CellStatusHelper.Not(CurStatus);
            gameOver = false;
            Refresh();
        }

        public void RestartGame()
        {
            while (steps.Count > 0)
            {
                var tempPos = steps.Pop();
            }
            CurStatus = CellStatus.Black;
            gameOver = false;
            Refresh();
        }

        public MainControl()
        {
            InitializeComponent();
            var border = new Border();
            ReInitCellSize();
        }

        private void ReInitCellSize()
        {
            cellWStep = ((float)ClientSize.Width - Padding.Left - Padding.Right)
                / (GlobalConst.BORDER_SIZE - 1);
            cellHStep = ((float)ClientSize.Height - Padding.Top - Padding.Bottom)
                / (GlobalConst.BORDER_SIZE - 1);
            itemDiameter = Math.Min(cellWStep, cellHStep) * ITEM_SIZE_SCALE;
        }

        #region DrawView

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawLines(e.Graphics);
            DrawNums(e.Graphics);
            DrawCells(e.Graphics);

            DrawGameOver(e.Graphics);
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
            for (int i = 1; i < GlobalConst.BORDER_SIZE; i++)
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
            for (int i = 1; i < GlobalConst.BORDER_SIZE; i++)
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
            if (gameOver)
            {
                return;
            }
            for (var i = 0; i < GlobalConst.BORDER_SIZE; i++)
            {
                g.DrawLine(linePen,
                    i * cellWStep + Padding.Left,
                    Padding.Top,
                    i * cellWStep + Padding.Left,
                    ClientSize.Height - Padding.Bottom);
            }
            for (var i = 0; i < GlobalConst.BORDER_SIZE; i++)
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
            for (var i = 0; i < GlobalConst.BORDER_SIZE; i++)
            {
                for (var j = 0; j < GlobalConst.BORDER_SIZE; j++)
                {
                    //if (cellArr[i][j] == CellStatus.Empty)
                    //{
                    //    continue;
                    //}

                    //g.FillEllipse(CellBrush[cellArr[i][j]],
                    //    Padding.Left + i * cellWStep - itemDiameter / 2,
                    //    Padding.Top + j * cellHStep - itemDiameter / 2,
                    //    itemDiameter,
                    //    itemDiameter);
                    if (i == steps.Peek().X && j == steps.Peek().Y)
                    {
                        g.DrawLine(curStepTips,
                            Padding.Left + i * cellWStep - -itemDiameter / 2,
                            Padding.Top + j * cellHStep,
                            Padding.Left + i * cellWStep + -itemDiameter / 2,
                            Padding.Top + j * cellHStep);
                        g.DrawLine(curStepTips,
                            Padding.Left + i * cellWStep,
                            Padding.Top + j * cellHStep - -itemDiameter / 2,
                            Padding.Left + i * cellWStep,
                            Padding.Top + j * cellHStep + -itemDiameter / 2);
                    }
                }
            }
        }

        private void DrawGameOver(Graphics g)
        {
            if (!gameOver)
            {
                return;
            }
            var str = CurStatus == CellStatus.Black ? @"白方胜" : @"黑方胜";
            g.DrawString(
                "五子连珠" + str,
                new Font(FontFamily.GenericSerif, 30), 
                gameOverBrush,   
                ClientRectangle,
                new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
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
            //if (gameOver)
            //{
            //    return;
            //}
            //var x = (int)Math.Round((e.X - Padding.Left) / cellWStep);
            //var y = (int)Math.Round((e.Y - Padding.Top) / cellHStep);
            //if (x < 0 || y < 0 || x > GlobalConst.BORDER_SIZE || y > GlobalConst.BORDER_SIZE || cellArr[x][y] != CellStatus.Empty)
            //{
            //    return;
            //}

            //cellArr[x][y] = CurStatus;
            //CheckGameOver(new Pos(x, y));
            //CurStatus = CellStatusHelper.Not(CurStatus);
            //Refresh();
            //if (AutoCompute)
            //{
            //    GeneNextStep();
            //}
        }

        private void CheckGameOver(Pos pos)
        {
            //steps.Push(pos);
            //if (maxMin.Evaluate.ComputePosGoleForSelf(CurStatus, pos) == Evaluate.GOLE_DICT[5])
            //{
            //    gameOver = true;
            //}
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Refresh();
        }
    }
}
