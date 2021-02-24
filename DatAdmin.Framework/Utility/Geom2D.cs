using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class Geom2D
    {
        public static readonly float Epsilon = 0.001f;
    }

    public struct Vector2D
    {
        public float X, Y;
        public Vector2D(float x, float y) { X = x; Y = y; }
        public Vector2D Transposed { get { return new Vector2D(Y, X); } }
        public Vector2D NormalVector { get { return new Vector2D(-Y, X); } }
        public Vector2D Normalized { get { return new Vector2D(X / Size, Y / Size); } }
        public static Vector2D operator +(Vector2D a, Vector2D b) { return new Vector2D(a.X + b.X, a.Y + b.Y); }
        public static Vector2D operator -(Vector2D a, Vector2D b) { return new Vector2D(a.X - b.X, a.Y - b.Y); }
        public static Vector2D operator *(Vector2D a, float k) { return new Vector2D(a.X * k, a.Y * k); }
        public static Vector2D operator /(Vector2D a, float k) { return new Vector2D(a.X / k, a.Y / k); }
        // skalarni soucin
        public static float operator *(Vector2D a, Vector2D b) { return a.X * b.X + a.Y * b.Y; } 
        public static float AngleBetween(Vector2D a, Vector2D b) { return (float)Math.Acos(Math.Abs(a * b) / (a.Size * b.Size)); }
        public float AngleBetween(Vector2D b) { return Vector2D.AngleBetween(this, b); }
        public float Size
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
            set
            {
                float koef = value / Size;
                X *= koef;
                Y *= koef;
            }
        }
        public override string ToString()
        {
            return String.Format("[{0};{1}]", X, Y);
        }
    }

    public struct Point2D
    {
        public float X, Y;
        public Point2D(float x, float y) { X = x; Y = y; }
        public static Vector2D operator -(Point2D a, Point2D b) { return new Vector2D(a.X - b.X, a.Y - b.Y); }
        public static Point2D operator +(Point2D a, Vector2D d) { return new Point2D(a.X + d.X, a.Y + d.Y); }
        public static Point2D operator -(Point2D a, Vector2D d) { return new Point2D(a.X - d.X, a.Y - d.Y); }
        public static explicit operator Point2D(Point pt) { return new Point2D(pt.X, pt.Y); }
        public static explicit operator Point(Point2D elem) { return new Point((int)(elem.X + 0.5), (int)(elem.Y + 0.5)); }
        public static implicit operator PointF(Point2D elem) { return new PointF(elem.X, elem.Y); }
        public override string ToString()
        {
            return String.Format("[{0};{1}]", X, Y);
        }
    }

    public struct Line2D
    {
        public Point2D A; // bod
        public Vector2D D; // smerovy vektor

        public Point2D B { get { return A + D; } }
        public Line2D Reverted { get { return new Line2D(B, A); } }

        public Line2D Vertical { get { return new Line2D(A, D.NormalVector); } }
        public Line2D VerticalB { get { return new Line2D(B, D.NormalVector); } }


        public Line2D(Point2D a, Vector2D d) { A = a; D = d; }
        public Line2D(Point2D a, Point2D b) { A = a; D = b - a; }

        //vzdalenost od primky
        public float Dist(Point2D pt)
        {
            float a, b, c;
            MakeImplicitFunc(out a, out b, out c);
            return (float)(Math.Abs(a * pt.X + b * pt.Y + c) / Math.Sqrt(a * a + b * b));
        }
        //vrati vzdalenost od usecky
        public float DistFromLine(Point2D pt)
        {
            if (Size < Geom2D.Epsilon) return (A - pt).Size;
            float d = Size, d1 = Vertical.Dist(pt), d2 = VerticalB.Dist(pt);
            if (d1 < d2)
            {//pt je blize bodu a nez b
                if (d2 > d) return (pt - A).Size;
            }
            else
            {
                if (d1 > d) return (pt - B).Size;
            }
            return Dist(pt);
        }

        //vyrobi obecnou rovnici primky ve tvaru a*x^2+b*x+c=0
        public void MakeImplicitFunc(out float a, out float b, out float c)
        {
            a = D.Y;
            b = -D.X;
            c = A.Y * D.X - A.X * D.Y;
        }

        public Point2D? ComPoint(Line2D line)
        {
            float jmenovatel = line.D.X * D.Y - line.D.Y * D.X;
            if (Math.Abs(jmenovatel) < Geom2D.Epsilon) return null;
            float s = (A.X * D.Y - A.Y * D.X + line.A.Y * D.X - line.A.X * D.Y) / jmenovatel;
            return line[s];
        }

        public float Size { get { return D.Size; } }

        public Point2D this[float t] { get { return A + D * t; } }

        public override string ToString()
        {
            return String.Format("{0}->{1}", A, B);
        }
    }

    public struct Matrix2D
    {
        float a11, a12, a21, a22;
        public void DoRotate(double angle)
        {
            float c = (float)Math.Cos(angle), s = (float)Math.Sin(angle);
            a11 = c; a12 = s;
            a21 = -s; a22 = c;
        }
        public Matrix2D(float b11, float b12, float b21, float b22) { a11 = b11; a12 = b12; a21 = b21; a22 = b22; }
        public static Matrix2D Identity() { return new Matrix2D(1, 0, 0, 1); }
        public Matrix2D Transp() { return new Matrix2D(a11, a21, a12, a22); }
        public void DoTransp() { float tmp = a12; a12 = a21; a21 = tmp; }
        public float this[int i, int j]
        {
            get
            {
                switch (i)
                {
                    case 1:
                        {
                            switch (j)
                            {
                                case 1: return a11;
                                case 2: return a12;
                            }
                            break;
                        }
                    case 2:
                        {
                            switch (j)
                            {
                                case 1: return a21;
                                case 2: return a22;
                            }
                            break;
                        }
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                switch (i)
                {
                    case 1:
                        {
                            switch (j)
                            {
                                case 0: a11 = value; return;
                                case 1: a12 = value; return;
                            }
                            break;
                        }
                    case 2:
                        {
                            switch (j)
                            {
                                case 0: a21 = value; return;
                                case 1: a22 = value; return;
                            }
                            break;
                        }
                }
                throw new IndexOutOfRangeException();
            }
        }
        public static Vector2D operator *(Matrix2D a, Vector2D v)
        {
            return new Vector2D(a.a11 * v.X + a.a12 * v.Y, a.a21 * v.X + a.a22 * v.Y);
        }
        public static Point2D operator *(Matrix2D a, Point2D v)
        {
            return new Point2D(a.a11 * v.X + a.a12 * v.Y, a.a21 * v.X + a.a22 * v.Y);
        }
        public override string ToString()
        {
            return String.Format("[[{0};{1}];[{2};{3}]]", a11, a12, a21, a22);
        }
    }

    public struct Interval
    {
        public float A, B;
        public Interval(float a, float b) { A = a; B = b; }
        public static Interval Intersection(Interval x, Interval y) { return new Interval(Math.Max(x.A, y.A), Math.Min(x.B, y.B)); }
        public Interval Intersection(Interval y) { return Interval.Intersection(this, y); }
        public bool IsEmpty { get { return B <= A; } }
        public float Size { get { return B - A; } }
        public bool Contains(float value) { return A <= value && value <= B; }
        public static bool operator <=(Interval x, Interval y) { return x.B <= y.A; }
        public static bool operator >=(Interval x, Interval y) { return x.A >= y.B; }
        public override string ToString()
        {
            return String.Format("[{0};{1}]", A, B);        }
    }
}
