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

        string GetK()
        {
            if (K >= 81 && K <= 100) return " (Очень хороший)";
            if (K >= 61 && K <= 80) return " (Хороший)";
            if (K >= 41 && K <= 60) return " (Средний)";
            if (K >= 21 && K <= 40) return " (Плохой)";
            if (K >= 0 && K <= 20) return " (Очень плохой)";
            return string.Empty;
        }

        public override string ToString()
        {
            string s = string.Empty;
            s += "t (Затраченное время (секунды)) = " + t + Environment.NewLine;
            s += "n (Кол-во символов, которые нужно было вычеркнуть) = " + n + Environment.NewLine;
            s += "M (Кол-во всех вычеркнутых символов) = " + M + Environment.NewLine;
            s += "S (Кол-во верно вычеркнутых символов (зеленые)) = " + S + Environment.NewLine;
            s += "P (Кол-во пропущенных (желтые)) = " + P + Environment.NewLine;
            s += "O (Кол-во ошибочных символов (красные)) = " + O + Environment.NewLine;
            s += "N (Кол-во просмотренных до последнего) = " + N + Environment.NewLine;
            s += "C (Кол-во строк просмотренных до последнего просмотренного) = " + C + Environment.NewLine;

            s += Environment.NewLine;

            s += "A (Показатель скорости внимания (производительности внимания)) = " + A + Environment.NewLine;
            s += "T1 (Показатель точности работы (первый вариант)) = " + T1 + Environment.NewLine;
            s += "T2 (Показатель точности работы (второй вариант)) = " + T2 + Environment.NewLine;
            s += "T3 (Показатель точности работы (третий вариант, по Уиппу)) = " + T3 + Environment.NewLine;
            s += "E (Коэффициент умственной продуктивности) = " + E + Environment.NewLine;
            s += "Au (Умственная работоспособность) = " + Au + Environment.NewLine;
            s += "K (Концентрация внимания (процент правильно выделенных символов от всех, что нужно было выделить)) = " + K + GetK() + Environment.NewLine;
            s += "Ku (Показатель устойчивости концентрации внимания) = " + Ku + Environment.NewLine;
            s += "V (Объем зрительной информации) = " + V + Environment.NewLine;
            s += "Q (Скорости переработки) = " + Q + Environment.NewLine;
            return s;
        }

    }
}
