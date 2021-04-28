using System;

namespace MinimizationMethods
{
    internal class ParabolaMethod
    {
        public static void Main(string[] args)
        {
            // не обещает сходимости, друг мой :(
            
            int it_num = 0;
            int funccall_num = 0;
            double left = 0.1;
            double right = 6;
            double epsilon = 0.00001;
            Random rand = new Random();
            double prevMin = 0;
            double middle = right - left;
            double f_middle = f(middle);

            if (left == 0)
            {
                left += epsilon;
            }

            if (right == 0)
            {
                right -= epsilon;
            }
            
            double f_left = f(left);
            double f_right = f(right);

            var curMin = CalculateMin();
            double f_curMin = f(curMin);
            
            while (Math.Abs(curMin - prevMin) > epsilon)
            {
                Console.WriteLine($"Iteration: {it_num + 1}, Current interval: ({left}:{right}), xmin: {curMin}, current amount of func calls: {funccall_num}");
                var prev_interval = Math.Abs(right - left);
                SetNewInterval();
                it_num += 1;
                curMin = CalculateMin();
                f_curMin = f(curMin);
            }
            
            Console.WriteLine("funccall: " + funccall_num + ", iter: " + it_num + "\ncurmin: " + curMin);
            
            // Math.Abs(curMin-middle) = 0 -> curMin = middle
            
            double f(double x)
            {
                funccall_num += 1;
                // Console.WriteLine("Номер обращения к функции вычисления: " + funccall_num);
                return Math.Sin(x) - Math.Log(Math.Pow(x, 2)) - 1;
            }
            
            double CalculateMin()
            {
                return middle - (Math.Pow(middle - left, 2) * (f_middle - f_right) - Math.Pow(middle - right, 2) *
                        (f_middle - f_left))
                    / (2 * ((middle - left) * (f_middle - f_right) - (middle - right) * (f_middle - f_left)));
            }

            void SetNewInterval()
            {
                prevMin = curMin;
            
                if (left < curMin && curMin < middle)
                {
                    if (f_curMin >= f_middle)
                    {
                        left = curMin;
                        f_left = f_curMin;
                    }
                    else if (f_curMin < f_middle)
                    {
                        right = middle;
                        f_right = f_middle;
                        middle = curMin;
                        f_middle = f_curMin;
                    }
                }
                else if (middle < curMin && curMin < right)
                {
                    if (f_middle >= f_curMin)
                    {
                        left = middle;
                        f_left = f_middle;
                        middle = curMin;
                        f_middle = f_curMin;
                    }
                    else if (f_middle < f_curMin)
                    {
                        right = curMin;
                        f_right = f_curMin;
                    }
                }
            }
        }
    }
}