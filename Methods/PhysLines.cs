using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MetalBallsTriangulation.Methods
{
    class PhysLines
    {
        private BackgroundWorker PhysicProc;

        public List<PointWithPotential> points;
        public List<Triangle> triangles;
        public Dictionary<double, List<PointWithPotential>> Isolines;
        public List<List<PointF>> FieldLines;
        public PhysLines(RunWorkerCompletedEventHandler Completed_Mesh, RunWorkerCompletedEventHandler Completed_PowerLines, ProgressChangedEventHandler Progress_PowerLines)
        {
            PhysicProc = new BackgroundWorker() { WorkerSupportsCancellation = true };
            PhysicProc.DoWork += Calculate;
            PhysicProc.RunWorkerCompleted += Completed_Mesh;
        }
        public void CalculateAsync()
        {
            PhysicProc.RunWorkerAsync();
        }
        public void Cancel()
        {
            if (PhysicProc.IsBusy)
                PhysicProc.CancelAsync();
        }

        public static Dictionary<double, List<PointWithPotential>> CalculateIsolines(double[] Z, List<PointWithPotential> points, List<Triangle> triangles)
        {
            Dictionary<double, List<PointWithPotential>> Isolines = new Dictionary<double, List<PointWithPotential>>();

            for (int z = 0; z < Z.Length; z++)
            {
                var isol = new List<PointWithPotential>();

                foreach (var tri in triangles)
                {
                    var Order = new List<PointWithPotential> { tri.P1, tri.P2, tri.P3 };

                    Order.Sort((n1, n2) => n1.Potential.CompareTo(n2.Potential));

                    if ((Z[z] >= Order[0].Potential) && (Z[z] <= Order[2].Potential))
                    {
                        if (Z[z] >= Order[1].Potential)
                            isol.Add(GetIsoPoint(Z[z], Order[1], Order[2]));
                        else isol.Add(GetIsoPoint(Z[z], Order[0], Order[1]));

                        isol.Add(GetIsoPoint(Z[z], Order[0], Order[2]));
                    }
                }
                Isolines.Add(Z[z], isol);
            }

            return Isolines;
        }

        private static Func<double, PointWithPotential, PointWithPotential, PointWithPotential> GetIsoPoint = (T, K1, K2) => {
            if (double.IsPositiveInfinity(K1.Potential)) return new PointWithPotential(new PointD(K1.Point.X, K1.Point.Y));
            if (double.IsPositiveInfinity(K2.Potential)) return new PointWithPotential(new PointD(K2.Point.X, K2.Point.Y));
            double Ratio = (K2.Potential - T) / (K2.Potential - K1.Potential);
            if (double.IsNaN(Ratio)) { Ratio = 0; };
            double x = K2.Point.X + (K1.Point.X - K2.Point.X) * Ratio;
            double y = K2.Point.Y + (K1.Point.Y - K2.Point.Y) * Ratio;
            return new PointWithPotential(new PointD(x, y));
        };

        public static List<List<PointF>> CalculateFieldLines(List<PointWithPotential> points, List<Triangle> triangles, float k = 0.01f)
        {
            var FieldLines = new System.Collections.Concurrent.ConcurrentBag<List<PointF>>();
            List<PointWithPotential> startPoints = points.Where(pnt => pnt.border && pnt.Potential != 0).ToList();

            Parallel.ForEach(startPoints, sPnt =>
            {
                List<PointF> FieldLine = new List<PointF>();
                FieldLine.Add(new PointF((float)sPnt.Point.X, (float)sPnt.Point.Y));

                float gX = 0;
                float gY = 0;
                float gI = 0;
                foreach (var tri in sPnt.nTriangles)
                {
                    var g = Grad(tri);
                    gX += g.X;
                    gY += g.Y;
                    gI++;
                }
                gX = gX / gI;
                gY = gY / gI;
                gI = k / (float)Math.Sqrt(gX * gX + gY * gY);
                FieldLine.Add(new PointF(FieldLine.Last().X + gX * gI, FieldLine.Last().Y + gY * gI));

                Triangle triangle = sPnt.nTriangles.Find(tri => tri.Contains(new PointWithPotential(new PointD(FieldLine.Last().X, FieldLine.Last().Y))));

                while (triangle != null)
                {
                    PointF grad = Grad(triangle);
                    gI = k / (float)Math.Sqrt(grad.X * grad.X + grad.Y * grad.Y);
                    FieldLine.Add(new PointF(FieldLine.Last().X + grad.X * gI, FieldLine.Last().Y + grad.Y * gI));
                    if (grad.X * grad.X + grad.Y * grad.Y < float.Epsilon) { break; }
                    float distance1 = Triangle.Distance(new PointF((float)triangle.P1.Point.X, (float)triangle.P1.Point.Y), FieldLine.Last());
                    float distance2 = Triangle.Distance(new PointF((float)triangle.P2.Point.X, (float)triangle.P2.Point.Y), FieldLine.Last());
                    float distance3 = Triangle.Distance(new PointF((float)triangle.P3.Point.X, (float)triangle.P3.Point.Y), FieldLine.Last());
                    PointWithPotential searchPnt = triangle.P1;
                    if (distance2 < distance1 && distance2 < distance3) { searchPnt = triangle.P2; };
                    if (distance3 < distance1 && distance3 < distance2) { searchPnt = triangle.P3; };
                    try
                    {
                        triangle = searchPnt.nTriangles.First(tri => tri.Contains(new PointWithPotential(new PointD(FieldLine.Last().X, FieldLine.Last().Y))));
                    }
                    catch (Exception) { triangle = null; }
                }
                FieldLine.RemoveAt(FieldLine.Count - 1);

                FieldLines.Add(FieldLine);
            });

            Parallel.ForEach(startPoints, sPnt =>
            {
                List<PointF> FieldLine = new List<PointF>();
                FieldLine.Add(new PointF((float)sPnt.Point.X, (float)sPnt.Point.Y));

                float gX = 0;
                float gY = 0;
                float gI = 0;
                foreach (var tri in sPnt.nTriangles)
                {
                    var g = Grad(tri);
                    gX += g.X;
                    gY += g.Y;
                    gI++;
                }
                gX = gX / gI;
                gY = gY / gI;
                gI = k / (float)Math.Sqrt(gX * gX + gY * gY);
                FieldLine.Add(new PointF(FieldLine.Last().X - gX * gI, FieldLine.Last().Y - gY * gI));

                Triangle triangle = sPnt.nTriangles.Find(tri => tri.Contains(new PointWithPotential(new PointD(FieldLine.Last().X, FieldLine.Last().Y))));

                while (triangle != null)
                {
                    PointF grad = Grad(triangle);
                    gI = k / (float)Math.Sqrt(grad.X * grad.X + grad.Y * grad.Y);
                    FieldLine.Add(new PointF(FieldLine.Last().X - grad.X * gI, FieldLine.Last().Y - grad.Y * gI));
                    if (grad.X * grad.X + grad.Y * grad.Y < float.Epsilon) { break; }
                    float distance1 = Triangle.Distance(new PointF((float)triangle.P1.Point.X, (float)triangle.P1.Point.Y), FieldLine.Last());
                    float distance2 = Triangle.Distance(new PointF((float)triangle.P2.Point.X, (float)triangle.P2.Point.Y), FieldLine.Last());
                    float distance3 = Triangle.Distance(new PointF((float)triangle.P3.Point.X, (float)triangle.P3.Point.Y), FieldLine.Last());
                    PointWithPotential searchPnt = triangle.P1;
                    if (distance2 < distance1 && distance2 < distance3) { searchPnt = triangle.P2; };
                    if (distance3 < distance1 && distance3 < distance2) { searchPnt = triangle.P3; };
                    try
                    {
                        triangle = searchPnt.nTriangles.First(tri => tri.Contains(new PointWithPotential(new PointD(FieldLine.Last().X, FieldLine.Last().Y))));
                    }
                    catch (Exception) { triangle = null; }
                }
                FieldLine.RemoveAt(FieldLine.Count - 1);

                FieldLines.Add(FieldLine);
            });

            return FieldLines.ToList();
        }

        public static PointF Grad(Triangle triangle)
        {
            Vector3D norm = Vector3D.CrossProduct(
                new Vector3D(
                    triangle.P1.Point.X - triangle.P2.Point.X,
                    triangle.P1.Point.Y - triangle.P2.Point.Y,
                    triangle.P1.Potential - triangle.P2.Potential),
                new Vector3D(
                    triangle.P1.Point.X - triangle.P3.Point.X,
                    triangle.P1.Point.Y - triangle.P3.Point.Y,
                    triangle.P1.Potential - triangle.P3.Potential));
            if (norm.Z < 0) { norm *= -1; } // Разворачиваем вектор
            Vector3D tang = Vector3D.CrossProduct(new Vector3D(0, 0, 1), norm);
            Vector3D grad = Vector3D.CrossProduct(tang, norm);

            return new PointF((float)grad.X, (float)grad.Y);
        }
        private void Calculate(object sender, DoWorkEventArgs e)
        {
            double[,] A;
            double[] R;

            // Формирование системы уравнений.
            //GalerkinOld.ConstructModel(points, out A, out R);
            // Решение системы уравнений.
            // GalerkinOld.Kaczmarz(A, R, ref points, 1e-9);

            Galerkin.GetAR(points, triangles, out A, out R);

            var result = Accord.Math.Matrix.Decompose(A).Solve(R);
            int i = 0;
            foreach (var pnt in points)
            {
                if (pnt.border) { continue; }
                pnt.Potential = result[i];
                i++;
            }
            double[] Z = new double[201];
            double max = points.Max(p => Math.Abs(p.Potential));
            int kZ = Z.Length / 2;
            for (i = -kZ; i <= kZ; i++)
            {
                Z[i + kZ] = max * i / kZ;
            }
            Isolines = CalculateIsolines(Z, points, triangles);
            FieldLines = CalculateFieldLines(points, triangles, (float)(0.5 / Math.Sqrt(points.Count)));

            e.Result = 1;
        }
    }
}
