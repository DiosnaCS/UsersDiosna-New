using System;
using System.IO;
using System.Web;

namespace UsersDiosna
{
    public static class Path
    {
        public static string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath;
    }
    public static class Error
    {
        public static string PathToErrorFile { get; set; }
        public static string timestamp { get; set; }
        public static void toFile(string message, string name)
        {
            DateTime now = DateTime.Now;
            timestamp = "\r\n"  + now.ToString();
            if (PathToErrorFile != null)
            {
                System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                System.IO.File.AppendAllText(PathToErrorFile, message);
            }
            else {
                if (Directory.Exists(Path.physicalPath + @"\ErroLog") == true &&
                    Directory.GetDirectories(Path.physicalPath, name) != null) {
                    PathToErrorFile = Path.physicalPath + @"\ErrorLog\" + name + @"\log.txt";
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
                    System.IO.File.AppendAllText(PathToErrorFile, message); //Write Error to file
                } else {
                    Directory.CreateDirectory(Path.physicalPath + @"\ErrorLog\" + name);//If directory in the path does not exist create one 
                    PathToErrorFile = Path.physicalPath + @"\ErrorLog\" + name + @"\log.txt"; //Asign path to Path attribute
                    if (!System.IO.File.Exists(PathToErrorFile))
                    {
                        System.IO.File.Create(PathToErrorFile).Close(); //If log.txt does not exist create one
                    }
                    System.IO.File.AppendAllText(PathToErrorFile, timestamp);
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
    }
}