using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersDiosna.Handlers
{
    public class GraphReportHelper
    {
        #region UTC
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
        public long utcToPkTime(string time)
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
                Error.toFile(e.Message.ToString() + " String time " + time, this.GetType().Name.ToString());
                utcTime = double.Parse(time);
            }

            utcTime = Math.Round((utcTime - (24515445E-1)) * 86400);
            return (long)utcTime;
        }
        #endregion
    }
}