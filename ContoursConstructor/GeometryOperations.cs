using System;
using System.Collections.Generic;
using System.Linq;

namespace ContoursConstructor
{
    public class GeometryOperations
    {
        /// <summary>
        /// Получить угол между векторами, выходящими из одной точки. 
        /// Угол рассчитывается по часовой стрелке от первого вектора до второго.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static double GetAngleBetweenVectors(Vector vector1, Vector vector2)
        {
            // компоненты первого вектора
            double dx1 = vector1.EndPoint.X - vector1.StartPoint.X;
            double dy1 = vector1.EndPoint.Y - vector1.StartPoint.Y;
            // и второго
            double dx2 = vector2.EndPoint.X - vector2.StartPoint.X;
            double dy2 = vector2.EndPoint.Y - vector2.StartPoint.Y;

            // проверка на нулевой вектор
            if ((dx1 == 0 && dy1 == 0) || (dx2 == 0 && dy2 == 0))
            {
                throw new InvalidOperationException("Задан нулевой вектор, поэтому угол не определён.");
            }

            // углы в радианах относительно оси X в стандартной системе (atan2: от −π до +π)
            double theta1 = Math.Atan2(dy1, dx1);
            double theta2 = Math.Atan2(dy2, dx2);

            // вычисляем смещение по часовой стрелке
            double delta = theta1 - theta2;
            // нормализация в [0; 2π)
            if (delta < 0)
            {
                delta += 2 * Math.PI;
            }

            // переводим в градусы
            return delta * (180.0 / Math.PI);
        }
        /// <summary>
        /// Получить полигоны, находящиеся вне контура
        /// </summary>
        /// <param name="contour"></param>
        /// <param name="poligons"></param>
        /// <returns></returns>
        public static List<Poligon> GetOutsideContourPoligons(Contour contour, List<Poligon> poligons, double doubleEps)
        {
            List<Poligon> outsideContourPoligons = new List<Poligon>();

            foreach (Poligon poligon in poligons)
            {
                bool isPoligonInsideContour = 
                    poligon.Points.Any(p => IsPointInsideContour(contour.ContourPoints, p, doubleEps));
                if (isPoligonInsideContour) continue;
                outsideContourPoligons.Add(poligon);
            }

            return outsideContourPoligons;
        }
        static bool IsPointOnSegment(Point p, Point a, Point b, double doubleEps)
        {
            // векторное произведение == 0 ⇒ точки коллинеарны,
            // скалярное произведение ≤ 0 ⇒ p лежит между a и b (включая концы)
            double cross = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
            if (Math.Abs(cross) > doubleEps) return false;

            double dot = (p.X - a.X) * (p.X - b.X) + (p.Y - a.Y) * (p.Y - b.Y);
            return dot <= doubleEps;
        }
        static bool IsPointInsideContour(List<Point> contourPoints, Point point, double doubleEps)
        {
            bool inside = false;
            int n = contourPoints.Count;

            // проходим все рёбра (pj,p i) — j = предыдущая вершина, i = текущая
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                Point pi = contourPoints[i];
                Point pj = contourPoints[j];

                // 1) отдельная проверка: лежит ли point прямо на ребре
                if (IsPointOnSegment(point, pj, pi, doubleEps))
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
    }
}