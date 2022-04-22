using MetalBallsTriangulation.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalBallsTriangulation
{
    public partial class MainWindow : Form
    {
        int PointsCountX, PointsCountY;
        double BigRadius, SmallRadius, Q;
        double k, kx;
        Bitmap bmp;
        Graphics graph;
        Pen pen;

        Model metallBalls;
        PhysLines phLines;

        PointD BigCenter, SmallCenter;

        List<PointD> nodePoints;  //Список узловых точек 
        List<PointD> newNodePoints;

        List<Triangle> pTriangles;
        List<TriangleFirst> triangles;   //Список треугольников
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitData()
        {
            //PointsCount = Convert.ToInt32(numericUpDown_PointsCount.Value);
            BigRadius = Convert.ToDouble(numericUpDown_BigRadius.Value);
            SmallRadius = Convert.ToDouble(numericUpDown_SmallRadius.Value);
            PointsCountX = Convert.ToInt32(numUpDown_PointsCountX.Value);
            PointsCountY = Convert.ToInt32(numUpDown_PointsCountY.Value);
            Q = Convert.ToDouble(numericUpDownQ.Value);
            kx = (PointsCountX + 1) / (PointsCountY + 1) * 0.5;
            k = pictureBox_System.Height;
        }

       
        void IterDelaunayTriangulation(List<PointD> nodePoints)
        {
            newNodePoints = new List<PointD>();
            newNodePoints.Add( new PointD(0, 0));
            newNodePoints.Add(new PointD(0, pictureBox_System.Height));
            newNodePoints.Add(new PointD(pictureBox_System.Width, 0));
            newNodePoints.Add(new PointD(pictureBox_System.Width, pictureBox_System.Height));

            //rand = new Random(DateTime.Now.Millisecond);
            PointD new_point = nodePoints[1];
            newNodePoints.Add(new_point);

            DelaunayTriangulation(triangles, newNodePoints, new_point);

            for (int p = 2; p < nodePoints.Count; p++)
            {
                List<PointD> enterPoints = new List<PointD>();
                PointD newPoint = nodePoints[p];
                newNodePoints.Add(newPoint);
                enterPoints.Add(newPoint);
                for (int i = 0; i < triangles.Count; i++)
                {
                    if (!triangles[i].IsCheckingDelaunay(newPoint))
                    {
                        enterPoints.Add(triangles[i].P1);
                        enterPoints.Add(triangles[i].P2);
                        enterPoints.Add(triangles[i].P3);
                        triangles.Remove(triangles[i]);
                        i--;
                    }
                }
                enterPoints = enterPoints.Distinct().ToList();
                DelaunayTriangulation(triangles, enterPoints, newPoint, false);

            }
        }
        /// <summary>
        /// Триангуляция Делоне методом простого перебора
        /// </summary>
        void DelaunayTriangulation(List<TriangleFirst> TriangleFirsts, List<PointD> points, PointD checkPoint, bool isInit = true)
        {
            bool ischeckDelaunay = true;
            for (int i = 0; i < points.Count; i++)
                for (int j = i + 1; j < points.Count; j++)
                    for (int k = j + 1; k < points.Count; k++)
                    {
                        TriangleFirst triangle = new TriangleFirst(points[i], points[j], points[k]);
                        ischeckDelaunay = true;

                        for (int p = 0; p < points.Count; p++)
                        {
                            if (p == i || p == j || p == k) continue;

                            if (!triangle.IsCheckingDelaunay(points[p]))
                            {
                                ischeckDelaunay = false;
                                break;
                            }
                        }
                        if (ischeckDelaunay == true && isInit == false)
                        {
                            if (IsPointTriangle(triangle, checkPoint))
                            {
                                TriangleFirsts.Add(triangle);
                            }
                        }
                        else if (ischeckDelaunay == true && isInit)
                        {
                            triangles.Add(triangle);
                        }

                    }
        }
        bool IsPointTriangle(TriangleFirst check, PointD point)
        {
            return (check.P1.Equals(point) || check.P2.Equals(point) || check.P3.Equals(point));
        }

        /// <summary>
        /// Отрисовка треугольников после расчета триангуляции
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="triangles"></param>
        private void DrawTriangles(Pen pen, List<TriangleFirst> triangles)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                graph.DrawLine(pen, (float)triangles[i].P1.X, (float)triangles[i].P1.Y, (float)triangles[i].P2.X, (float)triangles[i].P2.Y);
                graph.DrawLine(pen, (float)triangles[i].P2.X, (float)triangles[i].P2.Y, (float)triangles[i].P3.X, (float)triangles[i].P3.Y);
                graph.DrawLine(pen, (float)triangles[i].P3.X, (float)triangles[i].P3.Y, (float)triangles[i].P1.X, (float)triangles[i].P1.Y);
            }
        }

        private void button_PhysWork_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            pTriangles = new List<Triangle>();
            phLines = new PhysLines(physicCulculateComplite, null, null);
            phLines.points = metallBalls.grid;
            for(int i=0;i<triangles.Count;i++)
            {
                pTriangles.Add(Triangle.FromTriangleFirst(triangles[i]));
            }
            Triangle.CreateNeighbours(ref phLines.points, ref pTriangles);
            phLines.triangles = pTriangles;

            // Инициализация граничных значений и непосредственно самих границ. 
            for (int i = 0; i < phLines.points.Count; i++)
            {
                for (int j = 0; j < metallBalls.CirclePoints.Count; j++)
                {
                    if (phLines.points[i] == metallBalls.CirclePoints[j])
                    {
                        phLines.points[i].border = true;

                        if (phLines.points[i].Point.X < 0)
                        { phLines.points[i].Potential = Q * (1 + (rnd.NextDouble() - 0.5f) * 0.00001); }
                        else
                        { phLines.points[i].Potential = -Q * (1 + (rnd.NextDouble() - 0.5f) * 0.00001); }

                        break;
                    }
                    // Установка малых случайных начальных значений.
                    if (!phLines.points[i].border) phLines.points[i].Potential = (rnd.NextDouble() - 0.5f) * 0.01f;
                }
            }

            // Начало расчета физики задачи.
            phLines.CalculateAsync();

        }

        private void DrawPoints(List<PointD> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                graph.FillEllipse(Brushes.White, (float)points[i].X - 3, (float)points[i].Y - 3, 6, 6);
            }
        }
        private void button_DrawSystem_Click(object sender, EventArgs e)
        {
            InitData();
            nodePoints = new List<PointD>();


            bmp = new Bitmap(pictureBox_System.Width, pictureBox_System.Height);
            graph = Graphics.FromImage(bmp);

            graph.SmoothingMode = SmoothingMode.HighQuality;
            pen = new Pen(Color.Red, 1);
            BigCenter = new PointD(pictureBox_System.Width / 3.0 - BigRadius / 2.0,
                pictureBox_System.Height / 2.0 - BigRadius / 2.0);
            SmallCenter = new PointD(pictureBox_System.Width / 2.0 + SmallRadius,
                pictureBox_System.Height / 2.0 - SmallRadius / 2.0);

            //Нарисовать большой шар
            graph.DrawEllipse(pen, (float)(BigCenter.X),
                (float)(BigCenter.Y), (float)(BigRadius), (float)(BigRadius));

            //Нарисовать малый шар
            pen = new Pen(Color.Blue, 1);
            graph.DrawEllipse(pen, (float)(SmallCenter.X),
                (float)(SmallCenter.Y), (float)(SmallRadius), (float)(SmallRadius));

            metallBalls = new Model(PointsCountX,PointsCountY);
            metallBalls.GenerateNodes(pictureBox_System.Width, pictureBox_System.Height, BigRadius, SmallRadius);

            DrawPoints(metallBalls.grid);
            triangles = new List<TriangleFirst>();
            foreach (var p in metallBalls.grid)
            {
                nodePoints.Add(p.Point);
            }
            IterDelaunayTriangulation(nodePoints);

            metallBalls.DeleteTriangleInCircles(triangles);
            DrawTriangles(new Pen(Color.Gold, 1), triangles);
            pictureBox_System.Image = bmp;

        }

        private void DrawPoints(List<PointWithPotential> PointsDP)
        {
            List<PointD> points = new List<PointD>();
            foreach (var p in PointsDP)
            {
                points.Add(p.Point);
            }
            DrawPoints(points);
        }
        private static void DrawIsolines(Graphics Gr, Dictionary<double, List<PointWithPotential>> isolines, double k = 1000, double kx = 0.5f)
        {
            Pen Darkness = new Pen(Color.Yellow);

            double maxP = isolines.Max(pt => Math.Abs(pt.Key));

            foreach (var line in isolines)
            {
                if (double.IsNaN(line.Key))
                {
                    Darkness.Color = Color.White;
                }
                else
                if (line.Key >= 0)
                {
                    int kc = (int)(255 * line.Key / maxP);
                    Darkness.Color = Color.FromArgb(kc, 0, 0);
                }
                else
                {
                    int kc = (int)(-255 * line.Key / maxP);
                    Darkness.Color = Color.FromArgb(0, 0, kc);
                }
                for (int i = 0; i < line.Value.Count - 1; i += 2)
                {
                    Gr.DrawLine(Darkness, (float)((line.Value[i].Point.X + kx) * k), (float)((line.Value[i].Point.Y + 0.5f) * k), (float)((line.Value[i + 1].Point.X + kx) * k), (float)((line.Value[i + 1].Point.Y + 0.5f) * k));
                }
            }

            Gr.Flush();
        }
        private static void DrawPhysic(Graphics Gr, List<PointWithPotential> pointPotencial, double k = 1000, double kx = 0.5f)
        {
            SolidBrush Darkness = new SolidBrush(Color.Yellow);
            float kpxl = 2;
            double maxP = pointPotencial.Max(pt => Math.Abs(pt.Potential));

            for (int i = 0; i < pointPotencial.Count; i++)
            {
                if (double.IsNaN(pointPotencial[i].Potential))
                {
                    Darkness.Color = Color.Black;
                }
                else
                if (pointPotencial[i].Potential >= 0)
                {
                    int kc = (int)(255 * pointPotencial[i].Potential / maxP);
                    Darkness.Color = Color.FromArgb(kc, 0, 0);
                }
                else
                {
                    int kc = (int)(-255 * pointPotencial[i].Potential / maxP);
                    Darkness.Color = Color.FromArgb(0, 0, kc);
                }

                Gr.FillEllipse(Darkness, (float)((pointPotencial[i].Point.X + kx) * k - kpxl), (float)((pointPotencial[i].Point.Y + 0.5f) * k - kpxl), 2 * kpxl, 2 * kpxl);
            }

            Gr.Flush();
        }
        public static void DrawFieldLines(Graphics Gr, List<List<PointF>> fieldlines, double k = 1000, double kx = 0.5f)
        {
            Pen Darkness = new Pen(Color.White);
            Random rand = new Random();
            foreach (var line in fieldlines)
            {
                Darkness.Color = Color.White;
                for (int i = 0; i < line.Count - 1; i++)
                {
                    //int color = rand.Next(255);
                    //Darkness.Color = Color.FromArgb( color, color, color);
                    Gr.DrawLine(Darkness, (float)((line[i].X + kx) * k), (float)((line[i].Y + 0.5f) * k), (float)((line[i + 1].X + kx) * k), (float)((line[i + 1].Y + 0.5f) * k));
                }
            }
            Gr.Flush();
        }
        private void physicCulculateComplite(object sender, System.ComponentModel.RunWorkerCompletedEventArgs rcea)
        {
            Draw();
        }
        public void Draw()
        {
            graph.Clear(Color.Black);
            //if (checkBoxGrid.Checked) { Drawer.DrawTriangles(gr, triangles, k, kx); }
            if (phLines != null)
            {
                if (checkIsolines.Checked) { DrawIsolines(graph, phLines.Isolines, k, kx); };
                if (checkFieldLines.Checked) { DrawFieldLines(graph, phLines.FieldLines, k, kx); };
                DrawPhysic(graph, phLines.points, k, kx);
            }
            pictureBox_System.Image = bmp;
        }
    }
}
