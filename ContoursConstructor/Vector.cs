using System;

namespace ContoursConstructor
{
    public class Vector
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Vector(Point startPoint, Point endPoint)
        {
            if (startPoint.Equals(endPoint))
            {
                throw new ArgumentException("Начальная и конечная точка равны. Вектор не может быть нулевым.");
            }
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}