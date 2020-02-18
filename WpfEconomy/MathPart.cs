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

        public Plate(double n, double tend, double l, double lamda, double ro, double c, double t0, double t1, double tp)
        {
            N = n;
            TEnd = tend;
            L = l;
            Lamda = lamda;
            Ro = ro;
            C = C;
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
                T.Add(0);
            }

            alpha = new List<double>(T);
            beta = new List<double>(T);
        }

        private List<double> Count()
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
