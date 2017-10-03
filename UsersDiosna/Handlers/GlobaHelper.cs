using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace UsersDiosna
{
    public static class Path
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
            if (PathToErrorFile != null)
            {
                System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                System.IO.File.AppendAllText(PathToErrorFile, MvcApplication.ErrorId.ToString()); //set id  of Error
                System.IO.File.AppendAllText(PathToErrorFile, message);
            }
            else {
                if (Directory.Exists(Path.PhysicalPath + @"\ErroLog") == true &&
                    Directory.GetDirectories(Path.PhysicalPath, name) != null) {
                    PathToErrorFile = Path.PhysicalPath + @"\ErrorLog\" + name + @"\log.txt";
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, MvcApplication.ErrorId.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, message); //Write Error to file
                } else {
                    Directory.CreateDirectory(Path.PhysicalPath + @"\ErrorLog\" + name);//If directory in the path does not exist create one 
                    PathToErrorFile = Path.PhysicalPath + @"\ErrorLog\" + name + @"\log.txt"; //Asign path to Path attribute
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, MvcApplication.ErrorId.ToString()); //set id  of Error
                    System.IO.File.AppendAllText(PathToErrorFile, message); //Write Error to file
                }
            }
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

        public static bool Exists<T>(T[] array, T value) //Populate an array with value of T type
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