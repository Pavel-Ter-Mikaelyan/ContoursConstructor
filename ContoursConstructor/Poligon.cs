using System;
using System.Collections.Generic;

namespace ContoursConstructor
{
    public class Poligon
    {
        public int Id { get; set; }
        public List<Point> Points { get; set; }

        public Poligon(int id, List<Point> points)
        {
            Id = id;
            Points = points;
            if (points.Count < 3)
            {
                throw new ArgumentException("Кол-во точек в полигоне должно быть больше 2-х.");
            }
        }
    }
}