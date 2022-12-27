using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp2
{
    internal class JsonDS
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string pathAllJson;
        public static T Deserialize<T>(string NameFile)
        {
            T list = JsonConvert.DeserializeObject<T>(File.ReadAllText(pathAllJson + "\\AllJsonFile\\" + NameFile));
            return list;
        }
        public static void Serialize<T>(T list, string NameFile)
        {
            File.WriteAllText(pathAllJson + "\\AllJsonFile\\" + NameFile, JsonConvert.SerializeObject(list));
            if (Directory.Exists(path + "\\AllJsonFile"))
            {
                Directory.Delete(path + "\\AllJsonFile", true);
                Directory.CreateDirectory(path + "\\AllJsonFile");
            }
            else
            {
                Directory.CreateDirectory(path + "\\AllJsonFile");
            }
            FileSystem.CopyDirectory(pathAllJson + "\\AllJsonFile", path + "\\AllJsonFile");
        }
        public static void SerchJsonFile()
        {
            bool stop = false;
            string a = Directory.GetCurrentDirectory();
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (Convert.ToString(a[i]) != @"\") a = a.Remove(i);
                else
                {
                    a = a.Remove(i);
                    var c = a;
                    foreach (var element in Directory.GetDirectories(c))
                    {
                        if (element == c + "\\AllJsonFile")
                        {
                            pathAllJson = c;
                            if (!Directory.Exists(path + "\\AllJsonFile"))
                            {
                                Directory.CreateDirectory(path + "\\AllJsonFile");
                                FileSystem.CopyDirectory(pathAllJson + "\\AllJsonFile", path + "\\AllJsonFile");
                            }
                            else
                            {
                                Directory.Delete(path + "\\AllJsonFile", true);
                                Directory.CreateDirectory(path + "\\AllJsonFile");
                                FileSystem.CopyDirectory(pathAllJson + "\\AllJsonFile", path + "\\AllJsonFile");
                            }
                            stop = true;
                            break;
                        }
                    }
                    if (stop) break;
                }
            }
        }
    }
}