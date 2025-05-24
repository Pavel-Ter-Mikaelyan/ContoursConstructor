using System;
using System.Collections.Generic;

namespace ContoursConstructor
{
    public class Contour
    {
        public List<Point> ContourPoints { get; set; } = new List<Point>();

        public Contour(List<Point> contourPoints)
        {
            int n = contourPoints.Count;
            if (n < 3)
            {
                throw new Exception("Не удалось образовать корректный контур. Полученный контур состоит менее, чем из 3-х вершин.");
            }

            ContourPoints = contourPoints;
        }      
    }
}