﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foster.Framework
{
    public struct Point2
    {
        public static readonly Point2 Zero = new Point2(0, 0);
        public static readonly Point2 One = new Point2(1, 1);
        public static readonly Point2 Left = new Point2(-1, 0);
        public static readonly Point2 Right = new Point2(1, 0);
        public static readonly Point2 Up = new Point2(0, -1);
        public static readonly Point2 Down = new Point2(0, 1);

        public int X;
        public int Y;

        public float Length => (float)Math.Sqrt(X * X + Y * Y);
        public Vector2 Normal => new Vector2(X, Y) / Length;

        public Point2(int xy)
        {
            X = Y = xy;
        }

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj) => (obj is Point2 other) && (other == this);

        public override int GetHashCode()
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + X;
            hashCode = hashCode * 23 + Y;
            return hashCode;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public static Point2 operator -(Point2 point) => new Point2(-point.X, -point.Y);
        public static Point2 operator /(Point2 point, int scaler) => new Point2(point.X / scaler, point.Y / scaler);
        public static Point2 operator *(Point2 point, int scaler) => new Point2(point.X * scaler, point.Y * scaler);
        public static Point2 operator %(Point2 point, int scaler) => new Point2(point.X % scaler, point.Y % scaler);

        public static Vector2 operator /(Point2 point, float scaler) => new Vector2(point.X / scaler, point.Y / scaler);
        public static Vector2 operator* (Point2 point, float scaler) => new Vector2(point.X * scaler, point.Y * scaler);
        public static Vector2 operator %(Point2 point, float scaler) => new Vector2(point.X % scaler, point.Y % scaler);

        public static Point2 operator +(Point2 a, Point2 b) => new Point2(a.X + b.X, a.Y + b.Y);
        public static Point2 operator -(Point2 a, Point2 b) => new Point2(a.X - b.X, a.Y - b.Y);

        public static Rect operator +(Point2 a, Rect b) => new Rect(a.X + b.X, a.Y + b.Y, b.Width, b.Height);
        public static Rect operator +(Rect b, Point2 a) => new Rect(a.X + b.X, a.Y + b.Y, b.Width, b.Height);

        public static bool operator ==(Point2 a, Point2 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point2 a, Point2 b) => a.X != b.X || a.Y != b.Y;

    }
}
