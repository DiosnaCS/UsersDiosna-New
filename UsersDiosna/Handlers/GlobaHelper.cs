using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace UsersDiosna
{
    public static class PathDef
    {
        public static string PhysicalPath = System.Web.HttpRuntime.AppDomainAppPath;
    }
    public static class Error
    {
        public static string PathToErrorFile { get; set; }
        public static string timestamp { get; set; }
        public static void toFile(string message, string name)
        {
            DateTime now = DateTime.Now;
            timestamp = "\r\n"  + now.ToString();
            MvcApplication.ErrorId++;

            // full path to logfile
            if (PathToErrorFile == null)
                PathToErrorFile = PathDef.PhysicalPath + @"\ErrorLog\" + name + @"\log.txt";

            // create all directories if necessary
            string dirName = Path.GetDirectoryName(PathToErrorFile);
            Directory.CreateDirectory(dirName);

            // log it
            System.IO.File.AppendAllText(PathToErrorFile, timestamp + " ");
            System.IO.File.AppendAllText(PathToErrorFile, MvcApplication.ErrorId.ToString()); //set id  of Error
            System.IO.File.AppendAllText(PathToErrorFile, message);
        }

        static Stopwatch clock = null;

        public static void TraceStart() {
            string fullPath = PathDef.PhysicalPath + @"\ErrorLog\_TK\profile.log";

            // create all directories if necessary
            string dirName = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(dirName);

            System.IO.File.AppendAllText(fullPath, "\r\n" + "-- start --");
            clock = Stopwatch.StartNew(); //create and start the instance of Stopwatch
        }

        public static void TraceLog(string message) {
            string fullPath = PathDef.PhysicalPath + @"\ErrorLog\_TK\profile.log";

            // create all directories if necessary
            string dirName = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(dirName);

            DateTime now = DateTime.Now;
            timestamp = now.ToString();
            System.IO.File.AppendAllText(fullPath, "\r\n" + clock.ElapsedMilliseconds + "ms ");
            System.IO.File.AppendAllText(fullPath, timestamp + " ");
            System.IO.File.AppendAllText(fullPath, message);
        }

    }

    public static class Extension {

        public static T[] Populate<T>(this T[] array, T value) //Populate an array with value of T type
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array;
        }

        public static bool Exists<T>(T[] array, T value) //chack if exists a value in the array 
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i].GetType() == value.GetType())
                {
                    if (array[i].ToString() == value.ToString()) {
                        return true;
                    }
                } else
                {
                    return false;
                }
            }
            return false;
        }

        public static List<T> Replace<T>(this List<T> list, T OldValue, T NewValue)
        {
            foreach (var value in list) {
                if (value.GetType() ==  OldValue.GetType())
                {
                    if (value.ToString() == OldValue.ToString())
                    {
                        list.Remove(value);
                        list.Add(value);
                    }
                }
            }
            return list;
        }



        public static List<string> SplitToList(out List<string> list, string forSplit, string separator)
        {
            string[] separatorArray = new string[] { separator };
            list = forSplit.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).ToList();           
            return list;
        }
    }
}