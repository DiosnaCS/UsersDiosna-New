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
    }
}