using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersDiosna.Handlers
{
    public class Helpers
    {
        /// <summary>
        /// This function takes a value and return offseted on specified decimals
        /// </summary>
        public static double ratio(int input, int decimals)
        {
            double converted = input / Math.Pow(10,decimals);
            return converted;
        }

        public static long utcToPkTime(string time)
        {
            double utcTime;
            //zakrácení času v utc na fixní délku 'od ":" až do konce odmažeme'
            if (time.IndexOf(":") >= 0)
            {
                int idx = time.IndexOf(":");
                time = time.Substring(0, idx - 1);
            }
            if (time.Contains("."))
            {
                time = time.Replace(".", ",");
            }
            try
            {
                utcTime = double.Parse(time);
            }
            catch (FormatException e)
            {
                time = time.Replace(".", ",");
                Error.toFile(e.Message.ToString() + " String time " + time, "Global");
                utcTime = double.Parse(time);
            }

            utcTime = Math.Round((utcTime - (24515445E-1)) * 86400);
            return (long)utcTime;
        }
        public static string pkTimeToUTC(double time)
        {
            double utcTime = (time / 86400) + 2451544.5;
            string utc = utcTime.ToString();
            if (utc.Contains(","))
            {
                utc = utc.Replace(",", ".");
            }
            return utc;
        }
        public static Int32 ConvertDT2pkTime(DateTime dateTime)
        {
            DateTime pkTimeStart = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc);
            Int32 pkTime = 0;
            pkTime = (Int32)(dateTime.ToUniversalTime() - pkTimeStart).TotalSeconds;

            return pkTime;
        }
        public static DateTime ConvertpkTime2DT(long pkTime)
        {
            long timeInNanoSeconds = pkTime * 10000000;
            DateTime dateTime = new DateTime((630822816000000000) + timeInNanoSeconds);
            return dateTime;
        }
    }
}