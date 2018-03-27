using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureRobot.FuroWare.Common.Utils
{
    public class MathUtil
    {
        public static byte CalcCheckSum(List<byte> msg, int len)
        {
            var checkSum = (byte)0;

            for (var i = 2; i < len - 1; i++)
                checkSum += msg[i];

            return (byte)~checkSum;
        }

        public static double RAD2DEG(double rad)
        {
            return 180 * rad / Math.PI;
        }

        public static double DEG2RAD(double deg)
        {
            return Math.PI * deg / 180;
        }

        public static double TrimRadianAngle(double rad)
        {
            rad = rad - Convert.ToInt32(rad / (2 * Math.PI)) * (2 * Math.PI);
            if (rad >= Math.PI) rad = rad - 2 * Math.PI;
            if (rad < -Math.PI) rad = rad + 2 * Math.PI;
            return rad;
        }

        public static string TimeCalcuate(DateTime StartTime, DateTime EndTime)
        {
            var Nowtime = (EndTime - StartTime);
            var Message = string.Format("작업시간: {0}시간 {1}분 {2}초", Nowtime.Hours, Nowtime.Minutes, Nowtime.Seconds);
            return Message;
        }
        
        /// <summary>
        /// 평면 상 두 점간의 거리
        /// 2013.12.17 by Mini
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        /// <summary>
        /// 회전 변환
        /// 2013.10.24 by Mini
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="degree"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        public static void Rotation(double x, double y, double degree, out double rx, out double ry)
        {
            rx = x * Math.Cos(MathUtil.DEG2RAD(degree)) - y * Math.Sin(MathUtil.DEG2RAD(degree));
            ry = x * Math.Sin(MathUtil.DEG2RAD(degree)) + y * Math.Cos(MathUtil.DEG2RAD(degree));
        }

        /// <summary>
        /// 1st order Low Pass Filter
        /// 2013.11.01 by Mini
        /// </summary>
        /// <param name="oldXLPF">이전 return 값</param>
        /// <param name="X">현재 측정 치</param>
        /// <param name="alpha"> a^2(1-a) < a(1-a) < 1-a </param>
        /// <returns></returns>
        public static double LowPassFilter(double oldXLPF, double X, double alpha = 0.7)
        {
            return alpha * oldXLPF + (1 - alpha) * X;
        }

        /// <summary>
        /// 1st order IIR Filter
        /// 2013.10.28 by Mini
        /// </summary>
        /// <param name="dblMEAS"></param>
        /// <param name="dblSetPoint"></param>
        /// <param name="dblPBAND"></param>
        /// <param name="dblINT"></param>
        /// <param name="dblDERIV"></param>
        /// <param name="dblFBK"></param>
        /// <param name="dblBIAS"></param>
        /// <param name="dblGAIN"></param>
        /// <param name="bNoOrder"></param>
        /// <param name="bNoINT"></param>
        /// <param name="pfbPID"></param>
        /// <returns></returns>
        public static double IIRFilter(double dblMEAS, double dblSetPoint, double dblPBAND, double dblINT, double dblDERIV, double dblFBK, double dblBIAS, double dblGAIN, bool bNoOrder, bool bNoINT, ref  IIR_PIDInfo pfbPID)
        {
            double dblOUT1;
            double dblError;
            double dblT;

            double dblA1, dblB0, dblB1;
            double dblW;
            double dblQD, dblOldQD, dblQI, dblOldQI, dblXI, dblOldXI;
            double dblOldMEAS;

            dblT = pfbPID.dblT;
            dblOUT1 = 0.0;
            dblError = 0.0;

            dblQD = 0.0;

            dblOldMEAS = pfbPID.dblOldMEAS;
            dblOldQD = pfbPID.dblOldQD;
            dblOldXI = pfbPID.dblOldXI;
            dblOldQI = pfbPID.dblOldQI;

            dblError = dblSetPoint - dblMEAS;

            dblOUT1 += dblError;

            // D Term
            if (bNoOrder == false)
            {
                dblA1 = (1.0 - 12.0 * dblDERIV / dblT) / (12.0 * dblDERIV / dblT + 1.0);
                dblB0 = (20.0 / dblT) / (12.0 * dblDERIV / dblT + 1.0);
                dblB1 = -dblB0;

                dblW = dblB0 * dblMEAS + dblB1 * dblOldMEAS;
                dblQD = dblW - dblA1 * dblOldQD;

                dblOUT1 -= dblQD;
            }


            // P Term
            dblOUT1 *= 100.0 / dblPBAND;

            // I Term
            {
                dblA1 = -(120.0 * dblINT / dblT - 1.0) / (120.0 * dblINT / dblT + 1.0);
                dblB0 = 1.0 / (120.0 * dblINT / dblT + 1.0);
                dblB1 = dblB0;

                if (bNoINT == false)
                {
                    dblXI = dblFBK - dblGAIN * dblBIAS;
                }
                else
                {
                    dblXI = 0.0;
                }

                dblW = dblB0 * dblXI + dblB1 * dblOldXI;
                dblQI = dblW - dblA1 * dblOldQI;

                dblOUT1 += dblQI;

            }

            dblOUT1 += dblGAIN * dblBIAS;


            pfbPID.dblOldMEAS = dblMEAS;
            pfbPID.dblOldQD = dblQD;
            pfbPID.dblOldQI = dblQI;
            pfbPID.dblOldXI = dblXI;

            return dblOUT1;
        }
    }

    public class IIR_PIDInfo
    {
        public double dblOld2MEAS;
        public double dblOldMEAS;
        public double dblOldOut;
        public double dblOldError;
        public double dblOldQD;
        public double dblOldQI;
        public double dblOldX;
        public double dblOldXI;
        public double dblT;

        public IIR_PIDInfo()
        {
            dblOld2MEAS = 0.0;
            dblOldMEAS = 0.0;
            dblOldOut = 0.0;
            dblOldError = 0.0;
            dblOldQD = 0.0;
            dblOldQI = 0.0;
            dblOldX = 0.0;
            dblT = 0.2;
            dblOldXI = 0.0;
        }
    }
}
