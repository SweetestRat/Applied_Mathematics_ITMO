using System;
using System.Collections.Generic;

namespace PRIMAT_LAB1
{
    /// <summary>
    /// Base method class
    /// </summary>
    abstract class Method
    {
        public static int interamount;
        public static int amountoffunccomputation;
        public static double f(double x)
        {
            amountoffunccomputation++;
            return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
        }
        public int getAmountOfComputations()
        {
            return amountoffunccomputation;
        }

        public int getIterAmount()
        {
            return interamount;
        }
    }
    /// <summary>
    /// Fedor's Dihotomy Method
    /// </summary>
    class DichotomyMethod:Method
    {
        //public static int interamount;
        public static double deltaa;
        public static double xmin;
        private static double previouslength;

        public DichotomyMethod()
        {
            interamount = 0;
            amountoffunccomputation = 0;
        }
        public double calc(double epsilon, double a, double b)
        {
            xmin = 0;
            Console.WriteLine("Interation: " + interamount + ", Current interval: (" + a + ", " + b + ")" + ", difference betweent previous interval: " + (previouslength / Math.Abs(b - a)) + ", xmin: " + ((b + a) / 2) +  ", Current amount of func calculations: " + amountoffunccomputation);
            xmin = (a + b) / 2;
            Random rand = new Random();
            deltaa = rand.NextDouble() * ((b - a) / 2) ;
            while (deltaa > epsilon/2)
            {
                interamount++;
                double x1 = ((a + b) / 2) - deltaa;
                double x2 = ((a + b) / 2) + deltaa;
                double fx1 = f(x1);
                double fx2 = f(x2);
                previouslength = b - a;
                if (fx1 > fx2)
                {
                    calc(epsilon, x1, b);
                }
                else
                {
                    calc(epsilon, a, x2);
                }
            }
            return xmin;
        }
    }
    /// <summary>
    /// Vlada's GoldenRatio Method
    /// </summary>
    class GoldenRatioMethod
    {
        private static double it_num;
        private static double funccall_num;
        public double calc(double epsilon, double a, double b)
        {
            double GRconst = (3 - Math.Sqrt(5)) / 2;
            double interval = b - a;
            double shear_length = GRconst * interval;
            double x1 = a + shear_length;
            double x2 = b - shear_length;
            double f_x1 = f(x1);
            double f_x2 = f(x2);
            double previouslen = b - a;
            funccall_num = 0;
            it_num = 0;

            while (interval > epsilon)
            {
                it_num += 1;
                Console.WriteLine("Номер итерации: " + it_num + ", current interval: " + a + ", " + b + ", difference bewteen stuff: " + (previouslen / Math.Abs(b-a)) + ", Amount of func calls: " + funccall_num);
                previouslen = b - a;
                shear_length = GRconst * interval;
                
                if (f_x1 > f_x2)
                {
                    a = x1;
                    interval = b - a;
                    x1 = x2;
                    x2 = a + shear_length;
                    f_x1 = f_x2;
                    f_x2 = f(x2);
                }
                else
                {
                    b = x2;
                    interval = b - a;
                    x2 = x1;
                    x1 = b - shear_length;
                    f_x2 = f_x1;
                    f_x1 = f(x1);
                }
            }

            if (f(a) < f(b))
            {
                Console.WriteLine("Minimum: " + a);
                return a;
            }
            else
            {
                Console.WriteLine("Minimum: " + b);
                return b;
            }

            double f(double x)
            {
                funccall_num += 1;
                return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
            }
        }
    }
    /// <summary>
    /// Lera's Fibonacci Method
    /// </summary>
    class FibonacciMethod:Method
    {
        public double calc(double a, double b, double eps)
        {
            amountoffunccomputation = 0;
            double previouslength = b - a;
            List<int> fibonacciSeries = new List<int>{1, 1};
            while(true)
            {
                int newItem = fibonacciSeries[^1] + fibonacciSeries[^2];
                fibonacciSeries.Add(newItem);
                if(newItem > Math.Abs(b - a) / eps)
                    break;
            }
            int n = fibonacciSeries.Count - 1;
            double x1 = a + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (b - a);
            double x2 = a + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (b - a);
            double fx1 = f(x1);
            double fx2 = f(x2);
            while(n > 2)
            {
                Console.WriteLine("Interation: " + interamount + ", Current interval: (" + a + ", " + b + ")" + ", difference betweent previous interval: " + (previouslength / Math.Abs(b - a)) + /*", xmin: " + ((a + b) / 2) + */ ", Current amount of func calculations: " + amountoffunccomputation);
                n--;
                interamount++;
                previouslength = b - a;
                if (fx1 < fx2)
                {
                    (b, x2, fx2) = (x2, x1, fx1);
                    x1 = a + ((double)fibonacciSeries[n - 2] / fibonacciSeries[n]) * (b - a);
                    fx1 = f(x1);
                }
                else
                {
                    (a, x1, fx1) = (x1, x2, fx2);
                    x2 = a + ((double)fibonacciSeries[n - 1] / fibonacciSeries[n]) * (b - a);
                    fx2 = f(x2);
                }
                
            }
            return ((a + b) / 2);
        }
    }
    /// <summary>
    /// Vlada's Parabola Method
    /// </summary>
    class ParabolaMethod:Method
    {
        private static int funccall_num;

        public double calc(double epsilon, double a, double b)
        {
            int it_num = 0;
            Random rand = new Random();
            double prevMin = 0;
            double middle = rand.NextDouble() * (b - a) + a;
            double f_middle = f(middle);

            if (a == 0)
            {
                a += epsilon;
            }

            if (b == 0)
            {
                b -= epsilon;
            }

            double f_left = f(a);
            double f_right = f(b);

            while (!(f_left > f_middle && f_right > f_middle))
            {
                middle = rand.NextDouble() * (b - a) + a;
                f_middle = f(middle);

                if (f_middle > f_left && f_middle > f_right)
                {
                    throw new Exception("Parabola has no minimum");
                }
            }

            var curMin = CalculateMin();
            double f_curMin = f(curMin);

            Console.WriteLine("Левая граница: " + a);
            Console.WriteLine("Правая граница: " + b);
            Console.WriteLine("Рандомная точка на интервале: " + middle);
            Console.WriteLine("Минимум функции: " + curMin
                                                  + "\n-----------------");

            while (Math.Abs(curMin - prevMin) > epsilon)
            {
                var prev_interval = Math.Abs(b - a);
                SetNewInterval();
                it_num += 1;
                Console.WriteLine("Номер итерации: " + it_num + ", Левая граница: " + a + ", Правая граница: " + b + ", Рандомная точка на интервале: " + middle + ", Минимум функции: " + curMin + ", Интервал сократился на: " + (prev_interval / (b - a)) + ", Номер обращения к функции вычисления: " + funccall_num);
                curMin = CalculateMin();
                f_curMin = f(curMin);
            }
            Console.WriteLine("\nMinimum = " + curMin);
            Console.WriteLine("----------------------" +
                              "\nВсего количество итераций: " + it_num
                              + "\nВсего обращений к функции вычисления: " + funccall_num);
            double f(double x)
            {
                funccall_num += 1;
                return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
            }
            double CalculateMin()
            {
                return middle - (Math.Pow(middle - a, 2) * (f_middle - f_right) - Math.Pow(middle - b, 2) *
                        (f_middle - f_left))
                    / (2 * ((middle - a) * (f_middle - f_right) - (middle - b) * (f_middle - f_left)));
            }

            void SetNewInterval()
            {
                prevMin = curMin;
                if (a < curMin && curMin < middle)
                {
                    if (f_curMin >= f_middle)
                    {
                        a = curMin;
                        f_left = f_curMin;
                    }
                    else if (f_curMin < f_middle)
                    {
                        b = middle;
                        f_right = f_middle;
                        middle = curMin;
                        f_middle = f_curMin;
                    }
                }
                else if (middle < curMin && curMin < b)
                {
                    if (f_middle >= f_curMin)
                    {
                        a = middle;
                        f_left = f_middle;
                        middle = curMin;
                        f_middle = f_curMin;
                    }
                    else if (f_middle < f_curMin)
                    {
                        b = curMin;
                        f_right = f_curMin;
                    }
                }
            }
            return curMin;
        }
    }
    /// <summary>
    /// Fedor's Brent Method
    /// </summary>
    class BrentCombinedMethod:Method
    {
        public double calc(double a, double c, double epsilon)
        {
            double w, v, x;
            x = w = v = (a + c) / 2;
            double fw, fv, fx;
            fx = fw = fv = f(x);
            double d = c - a;
            double e = c - a;
            //double K = (Math.Sqrt(5) - 1) / 2;
            double K = (3 - Math.Sqrt(5)) / 2;
            double u = 0;
            interamount = 0;
            amountoffunccomputation = 0;
            while (Math.Round(Math.Abs(c - a), epsilon.ToString().Length - 3) > epsilon)
            {
                
                Console.WriteLine("Interation: " + interamount + ", Current interval: (" + a + ", " + c + ")" + ", xmin: " + ((c + a) / 2) +  ", Current amount of func calculations: " + amountoffunccomputation);
                double g = e;
                e = d;

                if (!(isSame(x, w, v) && isSame(fx, fw, fv)))
                {
                    u = parabolaMin(x, w, v, fx, fw, fv);
                }

                if (a + epsilon <= u && c - epsilon >= u && Math.Abs(u - x) < 0.5 * g)
                {
                    d = Math.Abs(u - x);
                }
                else
                {
                    if (x < (a + c) / 2)
                    {
                        u = x + K * (c - x);
                        d = c - x;
                    }
                    else
                    {
                        u = x - K * (x - a);
                        d = x - a;
                    }
                }
                if (Math.Abs(u - x) < epsilon)
                {
                    u = x + Math.Sign(u - x) * epsilon;
                }
                double fu = f(u);
                if (fu <= fx)
                {
                    if (u >= x)
                    {
                        a = x;
                    }
                    else
                    {
                        c = x;
                    }
                    v = w;
                    w = x;
                    x = u;
                    fv = fw;
                    fw = fx;
                    fx = fu;
                }
                else
                {
                    if (u >= x)
                    {
                        c = u;
                    }
                    else
                    {
                        a = u;
                    }
                    if (fu <= fw || w == x)
                    {
                        v = w;
                        w = u;
                        fv = fw;
                        fw = fu;
                    }
                    else if (fu <= fu || v == x || v == w)
                    {
                        v = u;
                        fv = fu;
                    }
                }
                interamount++;
            }
            return w;
        }
        private static double parabolaMin(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return x2 - (Math.Pow(x2 - x1, 2) * (y2 - y3) - Math.Pow(x2 - x3, 2) *
                    (y2 - y1))
                / (2 * ((x2 - x1) * (y2 - y3) - (x2 - x3) * (y2 - y1)));
        }
        bool isSame(double val1, double val2, double val3)
        { 
            return Math.Abs(val1 - val2) < 1e-5 && Math.Abs(val1 - val3) < 1e-5 &&
                 Math.Abs(val2 - val3) < 1e-5;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //init vals
            double epsilon = 0.0001;
            double left = 0;
            double right = 6;
            double xmin = 0;
            
            //dichotomy method
            Console.WriteLine("Dichotomy method:");
            DichotomyMethod dichotomyMethod = new DichotomyMethod();
            xmin = dichotomyMethod.calc(epsilon, left, right);
            Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + dichotomyMethod.getIterAmount() + ", amount of func calculations: " + dichotomyMethod.getAmountOfComputations());
            
            //Golden method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Golden Ratio Method: ");
            GoldenRatioMethod goldenRatioMethod = new GoldenRatioMethod();
            xmin = goldenRatioMethod.calc(epsilon, left, right);
            Console.WriteLine(xmin + ", yval: " + f(xmin));
            
            //Fibonnachi method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Fibbonachi method: ");
            FibonacciMethod fibonacciMethod = new FibonacciMethod();
            xmin = fibonacciMethod.calc(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + fibonacciMethod.getIterAmount() + ", amount of func calculations: " + fibonacciMethod.getAmountOfComputations());
            
            //Parabola method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Parabola method: ");
            ParabolaMethod pmethod = new ParabolaMethod();
            xmin = pmethod.calc(epsilon, left, right);
            Console.WriteLine(xmin + ", yval: " + f(xmin));
            
            //Brent method
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Brent Method: ");
            BrentCombinedMethod bmethod2 = new BrentCombinedMethod();
            xmin = bmethod2.calc(left, right, epsilon);
            Console.WriteLine(xmin + ", yval: " + f(xmin) + ", amount of iterations: " + bmethod2.getIterAmount() + ", amount of func calculations: " + bmethod2.getAmountOfComputations());
        }
        static double f(double x)
        {
            return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
        }
    }
}
