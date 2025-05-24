using System;
using System.Collections.Generic;
using System.Linq;

namespace ContoursConstructor
{
    /// <summary>
    /// Получение замкнутых контуров Contours, которыми можно окружить заданные полигоны.
    /// Точки в каждом контуре Contour представляются в порядке обхода контура, например:
    /// Contour.ContourPoints[0], Contour.ContourPoints[1] - первый отрезок контура,
    /// Contour.ContourPoints[1], Contour.ContourPoints[2] - второй отрезок контура,
    /// ...
    /// Contour.ContourPoints[n], Contour.ContourPoints[0] - последний отрезок
    /// </summary>
    public class ContoursConstructor
    {
        public List<Contour> Contours = new List<Contour>();

        List<Poligon> _allPoligons = new List<Poligon>();        
        //максимально допустимое кол-во точек, которое находится на контуре
        const int _pointsMaxCount = 500000;
        //максимально допустимое кол-во контуров
        const int _contoursMaxCount = 20000;
        //допуск на вычисление угла
        const double _angleEps = 1e-3;
        //допуск на double
        const double _doubleEps = 1e-10;

        public ContoursConstructor(List<Poligon> allPoligons)
        {
            _allPoligons = allPoligons;
            if (_allPoligons.Count == 0)
            {
                throw new ArgumentException("Ошибка. Кол-во полигонов должно быть больше 0. Контуры не получены.");
            }
            ContoursInitialize();
        }
        void ContoursInitialize()
        {
            //Полигоны вне контура
            //(начальное значение _allPoligons, т.к. ни одного контура еще не образовано)
            List<Poligon> outsideContourPoligons = _allPoligons;
            int iteration = 0;
            while (outsideContourPoligons.Count > 0)
            {
                iteration++;
                Contour contour = GetContour(outsideContourPoligons);
                Contours.Add(contour);
                outsideContourPoligons = 
                    GeometryOperations.GetOutsideContourPoligons(contour, outsideContourPoligons, _doubleEps);
                if (iteration > _contoursMaxCount)
                {
                    throw new Exception("Не удалось сформировать контуры по полигонам. " +
                                        "Превышено максимально допустимое количество контуров: " + _contoursMaxCount + " шт");
                }
            }
        }
        static Contour GetContour(List<Poligon> poligons)
        {
            List<Point> contourPoints = new List<Point>();
            double maxY = poligons.SelectMany(s => s.Points).Max(p => p.Y);
            List<Point> pointsWithMaxY = poligons.SelectMany(s => s.Points.Where(p => p.Y == maxY)).ToList();
            double minX = pointsWithMaxY.Min(p => p.X);
            Point startPoint = new Point(minX, maxY);
            contourPoints.Add(startPoint);

            Point currentPoint = startPoint;
            Point previousPoint = new Point(startPoint.X - 10, maxY);
            Point nextPoint = GetNextPoint(poligons, currentPoint, previousPoint, out _);

            int iteration = 0;
            while (!nextPoint.Equals(startPoint))
            {
                iteration++;
                previousPoint = currentPoint;
                currentPoint = nextPoint;
                nextPoint = GetNextPoint(poligons, currentPoint, previousPoint, out double angle);
                if (Math.Abs(angle - 180) > _angleEps)
                {
                    contourPoints.Add(currentPoint);
                }
                if (iteration > _pointsMaxCount)
                {
                    throw new Exception("Ошибка при формировании контура. Неудалось замкнуть контур." +
                                        " Превышено максимально допустимое кол-во точек для расчета: " +
                                        _pointsMaxCount + " шт.");
                }
            }

            return new Contour(contourPoints);
        }
        static Point GetNextPoint(List<Poligon> poligons, Point currPoint, Point previousPoint, out double angle)
        {
            Vector vector1 = new Vector(currPoint, previousPoint);
            Dictionary<double, Point> angles = new Dictionary<double, Point>();
            List<Poligon> poligonsWithCurrPoint = GetPoligonsByPoint(poligons, currPoint);
            foreach (Point point in poligonsWithCurrPoint.SelectMany(s => s.Points))
            {
                if (point.Equals(currPoint)) continue;
                Vector vector2 = new Vector(currPoint, point);
                double angleBetweenVectors = GeometryOperations.GetAngleBetweenVectors(vector1, vector2);
                if (Math.Abs(angleBetweenVectors) < _angleEps) continue;
                if (angles.ContainsKey(angleBetweenVectors)) continue;
                angles[angleBetweenVectors] = point;
            }
            angle = angles.Min(q => q.Key);
            return angles[angle];
        }
        static List<Poligon> GetPoligonsByPoint(List<Poligon> poligons, Point point)
        {
            return poligons.Where(s => s.Points.Any(c => c.Equals(point))).ToList();
        }
    }
}