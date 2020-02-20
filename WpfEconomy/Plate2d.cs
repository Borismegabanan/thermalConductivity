using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEconomy
{
    class Plate2d
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

        public Plate2d(double n, double tend, double l, double lamda, double ro, double c, double t0, double t1, double tp)
        {
            N = n;
            TEnd = tend;
            L = l;
            Lamda = lamda;
            Ro = ro;
            C = c;
            T0 = t0;
            T1 = t1;
            Tp = tp;
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
                T.Add(T0);
            }

            alpha = new List<double>(T); //0
            beta = new List<double>(T); //0
        }

        private List<double> Count()
        {
            double ai, bi, ci, fi;
            for (double TStart = Tau; TStart <= TEnd; TStart += Tau)
            {
                alpha[0] = 0;
                beta[0] = T1;
                for (int i = 1; i < T.Count - 1; i++)
                {
                    ai = Lamda / (H * H);
                    bi = 2.0 * Lamda / (H * H) + Ro * C / Tau;
                    ci = ai;
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
            return T;
        }


        public List<double> Start()
        {
            Count_step();
            InizializtionOfVectors();
            return Count();
        }
    }
}
