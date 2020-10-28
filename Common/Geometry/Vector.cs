using System;

namespace Common
{
    public class Vector
    {
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

    }
}
