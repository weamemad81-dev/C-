using System;

namespace Assi session8
{
   
    public class Point3D : IComparable<Point3D>, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D() : this(0, 0, 0) { }

        public Point3D(int x) : this(x, 0, 0) { }

        public Point3D(int x, int y) : this(x, y, 0) { }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point3D other)
            {
                return X == other.X && Y == other.Y && Z == other.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Point3D? left, Point3D? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Point3D? left, Point3D? right)
        {
            return !(left == right);
        }

        public int CompareTo(Point3D? other)
        {
            if (other is null) return 1;

            if (X != other.X)
                return X.CompareTo(other.X);

            return Y.CompareTo(other.Y);
        }

        public object Clone()
        {
            return new Point3D(X, Y, Z);
        }
    }

    public static class Maths
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    public class Duration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Duration() : this(0, 0, 0) { }

        public Duration(int hours, int minutes, int seconds)
        {
            int totalSeconds = hours * 3600 + minutes * 60 + seconds;
            NormalizeSeconds(totalSeconds);
        }

        public Duration(int totalSeconds)
        {
            NormalizeSeconds(totalSeconds);
        }

        private void NormalizeSeconds(int totalSeconds)
        {
            if (totalSeconds < 0) totalSeconds = 0;

            Hours = totalSeconds / 3600;
            totalSeconds %= 3600;
            Minutes = totalSeconds / 60;
            Seconds = totalSeconds % 60;
        }

        private int ToTotalSeconds() => Hours * 3600 + Minutes * 60 + Seconds;

        public override string ToString()
        {
            if (Hours > 0)
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";
            if (Minutes > 0)
                return $"Minutes :{Minutes}, Seconds :{Seconds}";

            return $"Seconds :{Seconds}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Duration other)
            {
                return ToTotalSeconds() == other.ToTotalSeconds();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToTotalSeconds().GetHashCode();
        }

        public static Duration operator +(Duration d1, Duration d2)
        {
            return new Duration(d1.ToTotalSeconds() + d2.ToTotalSeconds());
        }

        public static Duration operator +(Duration d, int seconds)
        {
            return new Duration(d.ToTotalSeconds() + seconds);
        }

        public static Duration operator +(int seconds, Duration d)
        {
            return d + seconds;
        }

        public static Duration operator -(Duration d1, Duration d2)
        {
            return new Duration(d1.ToTotalSeconds() - d2.ToTotalSeconds());
        }

        public static Duration operator ++(Duration d)
        {
            return new Duration(d.ToTotalSeconds() + 60);
        }

        public static Duration operator --(Duration d)
        {
            return new Duration(d.ToTotalSeconds() - 60);
        }

        public static bool operator >(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() > d2.ToTotalSeconds();
        }

        public static bool operator <(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() < d2.ToTotalSeconds();
        }

        public static bool operator >=(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() >= d2.ToTotalSeconds();
        }

        public static bool operator <=(Duration d1, Duration d2)
        {
            return d1.ToTotalSeconds() <= d2.ToTotalSeconds();
        }

        public static bool operator ==(Duration d1, Duration d2)
        {
            if (ReferenceEquals(d1, d2)) return true;
            if (d1 is null || d2 is null) return false;
            return d1.Equals(d2);
        }

        public static bool operator !=(Duration d1, Duration d2)
        {
            return !(d1 == d2);
        }

        public static bool operator true(Duration d)
        {
            return d.ToTotalSeconds() > 0;
        }

        public static bool operator false(Duration d)
        {
            return d.ToTotalSeconds() == 0;
        }

        public static explicit operator DateTime(Duration d)
        {
            return new DateTime(1, 1, 1, d.Hours % 24, d.Minutes, d.Seconds);
        }
    }

    class Program
    {
        static void Main()
        {
            // Project 1
            Point3D P = new Point3D(10, 10, 10);
            Console.WriteLine(P.ToString());

            Point3D P1 = ReadPointWithTryParse("P1");
            Point3D P2 = ReadPointWithParseAndConvert("P2");

            Console.WriteLine($"P1: {P1}");
            Console.WriteLine($"P2: {P2}");

            Console.WriteLine($"Does P1 == P2? {P1 == P2}");

            Point3D[] points = new Point3D[]
            {
                new Point3D(10, 20, 5),
                new Point3D(5, 15, 10),
                new Point3D(10, 5, 0),
                new Point3D(2, 30, 1)
            };

            Array.Sort(points);

            foreach (var pt in points)
            {
                Console.WriteLine(pt);
            }

            Point3D clonedPoint = (Point3D)P1.Clone();
            Console.WriteLine($"Cloned P1: {clonedPoint}");

            // Project 2
            double x = 20, y = 5;
            Console.WriteLine($"Add: {Maths.Add(x, y)}");
            Console.WriteLine($"Subtract: {Maths.Subtract(x, y)}");
            Console.WriteLine($"Multiply: {Maths.Multiply(x, y)}");
            Console.WriteLine($"Divide: {Maths.Divide(x, y)}");

            // Project 3
            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine(D1.ToString());

            Duration D2 = new Duration(3600);
            Console.WriteLine(D2.ToString());

            Duration D3 = new Duration(7800);
            Console.WriteLine(D3.ToString());

            Duration D4 = new Duration(666);
            Console.WriteLine(D4.ToString());

            D1 = new Duration(1, 10, 15);
            D2 = new Duration(2, 10, 0);

            D3 = D1 + D2;
            Console.WriteLine($"D3 = D1 + D2: {D3}");

            D3 = D1 + 7800;
            Console.WriteLine($"D3 = D1 + 7800: {D3}");

            D3 = 666 + D3;
            Console.WriteLine($"D3 = 666 + D3: {D3}");

            D3 = ++D1;
            Console.WriteLine($"D3 = ++D1: {D3}");

            D3 = --D2;
            Console.WriteLine($"D3 = --D2: {D3}");

            D1 = D1 - D2;
            Console.WriteLine($"D1 = D1 - D2: {D1}");

            if (D1 > D2)
                Console.WriteLine("D1 > D2");

            if (D1 <= D2)
                Console.WriteLine("D1 <= D2");

            if (D1)
                Console.WriteLine("D1 is True");

            DateTime obj = (DateTime)D1;
            Console.WriteLine($"DateTime Obj: {obj:HH:mm:ss}");
        }

        static Point3D ReadPointWithTryParse(string pointName)
        {
            Console.WriteLine($"Enter coordinates for {pointName}:");
            int x, y, z;

            Console.Write("X: ");
            while (!int.TryParse(Console.ReadLine(), out x))
                Console.Write("X: ");

            Console.Write("Y: ");
            while (!int.TryParse(Console.ReadLine(), out y))
                Console.Write("Y: ");

            Console.Write("Z: ");
            while (!int.TryParse(Console.ReadLine(), out z))
                Console.Write("Z: ");

            return new Point3D(x, y, z);
        }

        static Point3D ReadPointWithParseAndConvert(string pointName)
        {
            Console.WriteLine($"Enter coordinates for {pointName}:");
            try
            {
                Console.Write("X: ");
                int x = int.Parse(Console.ReadLine()!);

                Console.Write("Y: ");
                int y = Convert.ToInt32(Console.ReadLine());

                Console.Write("Z: ");
                int z = int.Parse(Console.ReadLine()!);

                return new Point3D(x, y, z);
            }
            catch
            {
                return new Point3D(0, 0, 0);
            }
        }
    }
}