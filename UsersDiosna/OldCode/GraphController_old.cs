using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDiosna.Graph.Models;

namespace UsersDiosna.OldCode
{
    public class GraphController_old
    {
        #region UTC
        public string pkTimeToUTC(double time)
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
            try
            {
                utcTime = double.Parse(time);
            }
            catch (FormatException e)
            {
                Error.toFile(e.Message.ToString(), this.GetType().Name.ToString());
                time = time.Replace(".", ",");
                utcTime = double.Parse(time);
            }

            utcTime = Math.Round((utcTime - (24515445E-1)) * 86400);
            return (long)utcTime;
        }
        #endregion

        private void readResponse(List<object[]> rstObjects, DataRequest dataRequest, List<int> tagsPos, TableDef tabledef)
        {
            int rstPos = 0, buffPos = 0;
            List<double> vals_agreg = new List<double>();
            long time, startTime, endTime, low_buff_time, high_buff_time;
            startTime = dataRequest.beginTime;
            endTime = dataRequest.beginTime + dataRequest.timeAxisLength;

            for (int i = 1; i < rstObjects[0].Length; i++)
            {
                double[] vals_buffer = new double[(dataRequest.timeAxisLength) / dataRequest.tags[tagsPos[i - 1]].period]; //prepare values buffer
                for (int j = 0; j < rstObjects.Count; j++)
                {
                    object[] objectsArray = rstObjects[j];
                    low_buff_time = (startTime + (rstPos * dataRequest.tags[tagsPos[i - 1]].period));
                    high_buff_time = (startTime + ((rstPos + 1) * dataRequest.tags[tagsPos[i - 1]].period));
                    time = utcToPkTime(objectsArray[0].ToString());
                    if (low_buff_time <= time && high_buff_time >= time)
                    {
                        vals_agreg.Add(Convert.ToDouble(objectsArray[i]));
                        if ((time + dataRequest.tags[tagsPos[i - 1]].period) >= high_buff_time)
                        {
                            if (vals_agreg.Count != 0)
                            {
                                vals_agreg.Reverse();
                                vals_buffer[buffPos] = vals_agreg[0];
                                buffPos++;
                                vals_agreg.Clear();
                            }
                            else
                            {
                                if (buffPos < vals_buffer.Length)
                                {
                                    vals_buffer[buffPos] = double.NaN;
                                    buffPos++;
                                }
                            }
                        }
                        rstPos++;
                    }
                    else
                    {
                        if (low_buff_time > time && time > startTime)
                        {
                            rstPos--;
                        }
                        if (high_buff_time < time)
                        {
                            if (vals_agreg.Count != 0)
                            {
                                vals_agreg.Reverse();
                                vals_buffer[buffPos] = vals_agreg[0];
                                buffPos++;
                                vals_agreg.Clear();
                            }
                            else
                            {
                                if (buffPos < vals_buffer.Length)
                                {
                                    vals_buffer[buffPos] = double.NaN;
                                    buffPos++;
                                    rstPos++;
                                }
                            }
                        }
                    }
                }
                buffPos = 0;
                // dataRequest.tags[tagsPos[i-1]].vals = vals_buffer;
            }

        }
    }
}