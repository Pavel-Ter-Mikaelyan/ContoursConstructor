using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContoursConstructor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Тестовые данные
            Poligon poligon1 = new Poligon(1, new List<Point> { new Point(1, 1), new Point(2, 1), new Point(2, 2), new Point(1, 2) });
            Poligon poligon2 = new Poligon(1, new List<Point> { new Point(1, 2), new Point(2, 2), new Point(1.5, 3) });
            Poligon poligon3 = new Poligon(1, new List<Point> { new Point(2, 1), new Point(2, 2), new Point(3, 2) });
            Poligon poligon4 = new Poligon(1, new List<Point> { new Point(2, 1), new Point(3, 1), new Point(3, 2) });
            Poligon poligon5 = new Poligon(1, new List<Point> { new Point(2, 2), new Point(2, 4), new Point(3, 2), new Point(3, 4) });
            Poligon poligon6 = new Poligon(1, new List<Point> { new Point(2, 2), new Point(1.5, 3), new Point(2, 4) });
            Poligon poligon7 = new Poligon(1, new List<Point> { new Point(1.5, 3), new Point(1, 4), new Point(2, 4) });
            Poligon poligon8 = new Poligon(1, new List<Point> { new Point(1, 2), new Point(1, 4), new Point(1.5, 3) });
            Poligon poligon9 = new Poligon(1, new List<Point> { new Point(1, 4), new Point(2, 4), new Point(1, 5) });
            Poligon poligon10 = new Poligon(1, new List<Point> { new Point(2, 4), new Point(1, 5), new Point(2, 5) });
            Poligon poligon11 = new Poligon(1, new List<Point> { new Point(2, 4), new Point(2, 5), new Point(3, 4) });
            Poligon poligon12 = new Poligon(1, new List<Point> { new Point(3, 4), new Point(3, 5), new Point(2, 5) });
            Poligon poligon13 = new Poligon(1, new List<Point> { new Point(3, 4), new Point(3, 5), new Point(4, 4) });
            Poligon poligon14 = new Poligon(1, new List<Point> { new Point(3, 2), new Point(3, 4), new Point(4, 2), new Point(4, 4) });
            Poligon poligon15 = new Poligon(1, new List<Point> { new Point(4, 2), new Point(4, 4), new Point(5, 3), new Point(5, 2) });
            Poligon poligon16 = new Poligon(1, new List<Point> { new Point(5, 2), new Point(5, 3), new Point(6, 2) });
            Poligon poligon17 = new Poligon(1, new List<Point> { new Point(6, 2), new Point(3, 1), new Point(3, 2) });

            Poligon poligonForAnotherСontour = new Poligon(1, new List<Point> { new Point(5, 4), new Point(5, 5), new Point(6, 4) });

            List<Poligon> allPoligons = new List<Poligon> { poligon1,
                                                            poligon2 ,
                                                            poligon3 ,
                                                            poligon4 ,
                                                            poligon5 ,
                                                            poligon6 ,
                                                            poligon7 ,
                                                            poligon8 ,
                                                            poligon9 ,
                                                            poligon10,
                                                            poligon11,
                                                            poligon12,
                                                            poligon13,
                                                            poligon14,
                                                            poligon15,
                                                            poligon16,
                                                            poligon17,
                                                            poligonForAnotherСontour};
            #endregion

            //получение контуров
            ContoursConstructor contoursConstructor = new ContoursConstructor(allPoligons);
            List<Contour> contours = contoursConstructor.Contours;
        }
    }
}