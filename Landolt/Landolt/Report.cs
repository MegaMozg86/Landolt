using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landolt
{
    public class Report
    {
        /// <summary>
        /// затраченное время
        /// </summary>
        public double t { get; set; } = 0; // 
        /// <summary>
        /// кол-во символов, которые нужно было вычеркнуть
        /// </summary>
        public double n { get; set; } = 0; // 
        /// <summary>
        /// кол-во всех вычеркнутых символов
        /// </summary>
        public double M { get; set; } = 0; // 
        /// <summary>
        /// 
        /// кол-во верно вычеркнутых символов (зеленые)
        /// </summary>
        public double S { get; set; } = 0; // 
        /// <summary>
        /// кол-во пропущенных (желтые)
        /// </summary>
        public double P { get; set; } = 0; // 
        /// <summary>
        /// кол-во ошибочных символов (красные)
        /// </summary>
        public double O { get; set; } = 0; // 
        /// <summary>
        /// кол-во просмотренных до последнего
        /// </summary>
        public double N { get; set; } = 0; // 
        /// <summary>
        /// кол-во строк просмотренных до последнего просмотренного
        /// </summary>
        public double C { get; set; } = 0; // 

        ///////////////////////////////////////////////////

        public double A { get { return N / t; } }
        public double T1 { get { return M / n; } }
        public double T2 { get { return S / n; } }
        public double T3 { get { return (M - O) / (M + P); } }
        public double E { get { return N * T2; } }
        public double Au { get { return (N / t) *((M - (O + P)) / n); } }
        public double K { get { return ((M - O) * 100) / n; } }
        public double Ku 
        { 
            get 
            {
                if (M == n)
                    return C * (C / (P + O + 1));
                else
                    return C * (C / (P + O));
            } 
        }
        public double V { get { return 0.5936 * N; } }
        public double Q { get { return (V - 2.807 * (P + O)) / t; } }

        public override string ToString()
        {
            string s = string.Empty;
            s += "A = " + A + Environment.NewLine;
            s += "T1 = " + T1 + Environment.NewLine;
            s += "T2 = " + T2 + Environment.NewLine;
            s += "T3 = " + T3 + Environment.NewLine;
            s += "E = " + E + Environment.NewLine;
            s += "Au = " + Au + Environment.NewLine;
            s += "K = " + K + Environment.NewLine;
            s += "Ku = " + Ku + Environment.NewLine;
            s += "V = " + V + Environment.NewLine;
            s += "Q = " + Q + Environment.NewLine;
            return s;
        }

    }
}
