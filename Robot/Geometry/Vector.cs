using System;

namespace Robot.Geometry
{
    public class Vector
    {
        public static readonly Vector NORTH = new Vector(0, 1);
        public static readonly Vector SOUTH = new Vector(0, -1);
        public static readonly Vector EAST = new Vector(1, 0);
        public static readonly Vector WEST = new Vector(-1, 0);
        public static readonly Vector ZERO = new Vector(0, 0);

        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        /// <summary>
        /// Return a direction string if the vector is one of the cardinal direction
        /// </summary>
        ///
        public string ToDirectionString()
        {
            string marker = $"{X}{Y}";
            switch (marker)
            {
                case "01":
                    return "NORTH";
                case "0-1":
                    return "SOUTH";
                case "10":
                    return "EAST";
                case "-10":
                    return "WEST";
                default:
                    throw new InvalidOperationException($"This vector is not a cardinal direction: {X}, {Y}.");
            }
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);

        /// <summary>
        /// Multiply the vector by the clockwise unit rotation matrix</summary>
        ///
        public Vector Right()
        {
            return new Vector(Y, -X);
        }

        /// <summary>
        /// Multiply the vector by the anti-clockwise unit rotation matrix</summary>
        ///
        public Vector Left()
        {
            return new Vector(-Y, X);
        }
    }
}
