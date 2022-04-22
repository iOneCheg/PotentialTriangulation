﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MetalBallsTriangulation.Methods
{
    class Galerkin
    {
        private static double K_ij(PointWithPotential NI, PointWithPotential NJ, List<Triangle> triangles)
        {
            var tempTri = NI.nTriangles.Where(tri => NJ.nTriangles.Contains(tri)).ToList();

            double k = 0;
            if (tempTri.Count != 2 && tempTri.Count != 0) throw new InvalidOperationException();
            foreach (var tri in tempTri)
            {
                var NA = new Point3D(tri.P1.Point.X, tri.P1.Point.Y, 0);
                var NB = new Point3D(tri.P2.Point.X, tri.P2.Point.Y, 0);
                var NC = new Point3D(tri.P3.Point.X, tri.P3.Point.Y, 0);

                var N1 = new Point3D(tri.P1.Point.X, tri.P1.Point.Y, 0);
                var N2 = new Point3D(tri.P2.Point.X, tri.P2.Point.Y, 0);
                var N3 = new Point3D(tri.P3.Point.X, tri.P3.Point.Y, 0);

                if (tri.P1 == NI)
                {
                    if (tri.P2 == NJ) { N1 = NA; N2 = NB; N3 = NC; }
                    if (tri.P3 == NJ) { N1 = NA; N2 = NC; N3 = NB; }
                }
                if (tri.P2 == NI)
                {
                    if (tri.P1 == NJ) { N1 = NB; N2 = NA; N3 = NC; }
                    if (tri.P3 == NJ) { N1 = NB; N2 = NC; N3 = NA; }
                }
                if (tri.P3 == NI)
                {
                    if (tri.P1 == NJ) { N1 = NC; N2 = NA; N3 = NB; }
                    if (tri.P2 == NJ) { N1 = NC; N2 = NB; N3 = NA; }
                }

                var vxi = Vector3D.CrossProduct(
                    new Vector3D(N2.X - N1.X, N2.Y - N1.Y, -1),
                    new Vector3D(N3.X - N1.X, N3.Y - N1.Y, -1));
                var vxj = Vector3D.CrossProduct(
                    new Vector3D(N2.X - N1.X, N2.Y - N1.Y, 1),
                    new Vector3D(N3.X - N1.X, N3.Y - N1.Y, 0));

                k += 0.5 * vxi.Length * (vxi.X * vxj.X + vxi.Y * vxj.Y);
            }
            return k;
        }

        public static void GetAR(List<PointWithPotential> points, List<Triangle> triangles, out double[,] A, out double[] R)
        {
            var pointsInner = new List<PointWithPotential>();
            var pointsBorder = new List<PointWithPotential>();
            foreach (var ptr in points)
            {
                if (ptr.border)
                    pointsBorder.Add(ptr);
                else pointsInner.Add(ptr);
            }
            var tA = new double[pointsInner.Count, pointsInner.Count];
            var tR = new double[pointsInner.Count];


            Parallel.For(0, pointsInner.Count, i =>
            {
                Parallel.For(0, pointsInner.Count, j =>
                {
                    if (j == i)
                    {
                        tA[i, j] = 0;
                        foreach (var tri in pointsInner[i].nTriangles)
                        {
                            var NA = new Point3D(tri.P1.Point.X, tri.P1.Point.Y, 0);
                            var NB = new Point3D(tri.P2.Point.X, tri.P2.Point.Y, 0);
                            var NC = new Point3D(tri.P3.Point.X, tri.P3.Point.Y, 0);
                            if (tri.P1 == pointsInner[i])
                                NA = new Point3D(tri.P1.Point.X, tri.P1.Point.Y, 1);
                            else if (tri.P2 == pointsInner[i])
                                NB = new Point3D(tri.P2.Point.X, tri.P2.Point.Y, 1);
                            else if (tri.P3 == pointsInner[i])
                                NC = new Point3D(tri.P3.Point.X, tri.P3.Point.Y, 1);
                            else throw new InvalidOperationException("The triangle is in 2D plane!");

                            var vx = Vector3D.CrossProduct(NB - NA, NC - NA);
                            tA[i, j] += 0.5 * vx.Length * (vx.X * vx.X + vx.Y * vx.Y);
                        }
                    }
                    else tA[i, j] = K_ij(pointsInner[i], pointsInner[j], triangles);
                });

                tR[i] = 0;
                foreach (var NK in pointsBorder)
                {
                    if (!pointsInner[i].nPoints.Contains(NK)) continue;
                    tR[i] -= NK.Potential * K_ij(pointsInner[i], NK, triangles);
                }
            });
            A = tA; R = tR;
        }
    }
}
