using System;
using System.Collections.Generic;
using System.Linq;

namespace ContoursConstructor
{
    public class GeometryOperations
    {
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
        public static List<Poligon> GetOutsideContourPoligons(Contour contour, List<Poligon> poligons)
        {
            List<Poligon> outsideContourPoligons = new List<Poligon>();

            foreach (Poligon poligon in poligons)
            {
                bool isPoligonInsideContour = poligon.Points.Any(p => contour.IsPointInsideContour(p));
                if (isPoligonInsideContour) continue;
                outsideContourPoligons.Add(poligon);
            }

            return outsideContourPoligons;
        }
    }
}