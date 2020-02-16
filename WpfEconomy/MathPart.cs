using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEconomy
{
    public class Plate
    {
        private double N { get; set; }
        private double TEnd { get; set; }
        private double L { get; set; }
        private double Lamda { get; set; }
        private double Ro { get; set; }
        private double C { get; set; }
        private double T0 { get; set; }
        private double T1 { get; set; }
        private double Tp { get; set; }

        double H { get; set; }
        double Tau { get; set; }

        private List<double> T;
        private List<double> alpha;
        private List<double> beta;

        private double Request(string name)
        {
            Console.WriteLine("Enter object " + name + ": ");
            return Convert.ToDouble(Console.ReadLine());
        }

        public void report_params()
        {
            N = Request("Количество узлов");
            TEnd = Request("Окончание по времени");
            L = Request("Толщину пластины");
            Lamda = Request("Коэффициент теплопроводности материала");
            Ro = Request("плотность материала");
            C = Request("Теплоёмкость материала пластины");
            T0 = Request("Начальная температура");
            T1 = Request("Температура на границице 0");
            Tp = Request($"Температура на границице {L:##.###}");
        }

        private void Count_step()
        {
            this.H = L / (N - 1);
            this.Tau = TEnd / 100;
        }

        private void InizializtionOfVectors()
        {
            T = new List<double>();
            for (int i = 0; i < N; i++)
            {
                T.Add(0);
            }

            alpha = new List<double>(T);
            beta = new List<double>(T);
        }

        private void Count()
        {
            double ai, bi, ci, fi;
            for (double TStart = 0; TStart <= TEnd; TStart += Tau)
            {
                alpha[0] = 0;
                beta[0] = T1;
                for (int i = 1; i < T.Count - 1; i++)
                {
                    ai = Lamda / Math.Sqrt(H);
                    bi = 2 * Lamda / Math.Sqrt(H) + Ro * C / Tau;
                    ci = Lamda / Math.Sqrt(H);
                    fi = -Ro * C * T[i] / Tau;
                    alpha[i] = ai / (bi - ci * alpha[i - 1]);
                    beta[i] = (ci * beta[i - 1] - fi) / (bi - ci * alpha[i - 1]);
                }

                T[T.Count - 1] = Tp;  
                for (int i = T.Count - 2; i >= 0; i--)
                {
                    T[i] = alpha[i] * T[i + 1] + beta[i];
                }
            }

        }

        private void Display()
        {
            Console.WriteLine($"Толщина пластины L = {L}");
            Console.WriteLine($"Число узлов N= {N}");
            Console.WriteLine($"Коэффициент теплопроводности Lamda= {Lamda}");
            Console.WriteLine($"Плотность материала пластины Ro= {Ro}");
            Console.WriteLine($"Теплоёмкость пластины C= {C}");
            Console.WriteLine($"Начальная температура T0= {T0}");
            Console.WriteLine($"Температура на границаT1= {T1}");
            Console.WriteLine($"Tr= {Tp}");
            Console.WriteLine($"h= {H}");
            Console.WriteLine($"tau= {Tau}");
            Console.WriteLine($"T= {TEnd}");

            Console.WriteLine();

            for (int i = 0; i < T.Count; i++)
            {
                Console.WriteLine($"h={H * i:##.###}  T={T[i]:##.###}");
            }

        }


        public void Start()
        {
            Count_step();
            InizializtionOfVectors();
            Count();
            Display();
        }

    }
}
