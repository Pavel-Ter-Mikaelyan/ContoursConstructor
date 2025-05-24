using System;
using System.Collections.Generic;

namespace ContoursConstructor
{
    public class Contour
    {
        public List<Point> ContourPoints { get; set; } = new List<Point>();
        double _doubleEps;

        public Contour(List<Point> contourPoints, double doubleEps)
        {
            _doubleEps = doubleEps;
            int n = contourPoints.Count;
            if (n < 3)
            {
                throw new Exception("Не удалось образовать корректный контур. Полученный контур состоит менее, чем из 3-х вершин.");
            }

            ContourPoints = contourPoints;
        }
        public bool IsPointInsideContour(Point point)
        {
            bool inside = false;           
            int n = ContourPoints.Count;

            // проходим все рёбра (pj,p i) — j = предыдущая вершина, i = текущая
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                Point pi = ContourPoints[i];
                Point pj = ContourPoints[j];

                // 1) отдельная проверка: лежит ли point прямо на ребре
                if (IsPointOnSegment(point, pj, pi))
                    return true;

                // 2) Ray Casting: пересекает ли горизонтальный луч вправо это ребро?
                //(так же тут учитывается пересечение луча с вершиной)
                bool crossesY = (pi.Y > point.Y) != (pj.Y > point.Y);
                if (crossesY)
                {
                    double xAtY = pj.X + (point.Y - pj.Y) * (pi.X - pj.X) / (pi.Y - pj.Y);
                    if (xAtY > point.X) inside = !inside;
                }
            }
            return inside;
        }
        bool IsPointOnSegment(Point p, Point a, Point b)
        {
            // векторное произведение == 0 ⇒ точки коллинеарны,
            // скалярное произведение ≤ 0 ⇒ p лежит между a и b (включая концы)
            double cross = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
            if (Math.Abs(cross) > _doubleEps) return false;

            double dot = (p.X - a.X) * (p.X - b.X) + (p.Y - a.Y) * (p.Y - b.Y);
            return dot <= _doubleEps;
        }
    }
}