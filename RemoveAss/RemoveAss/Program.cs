using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RemoveAss
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            Recur(@"D:\EZE\Development\Dev_IOIWidget\NET\Projects\EMS\Code");
        }

        static void Recur(string path)
        {
            Regex obj = new Regex("<Compile/sInclude=\"Properties\\AssemblyInfo.cs\"/s/>");
            FileStream wFile = new FileStream(@"d:\log.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(wFile);
            try
            {
                foreach (string dirStr in Directory.GetDirectories(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(dirStr);
                    FileSystemInfo[] fileArr = dir.GetFileSystemInfos("*.csproj");
                    for (int i = 0; i < fileArr.Length; i++)
                    {
                        bool has = false;

                        FileStream aFile = new FileStream(fileArr[i].FullName, FileMode.Open);
                        StreamReader sr = new StreamReader(aFile);
                        string line = string.Empty;
                        try
                        {
                            line = sr.ReadToEnd();
                            if (obj.IsMatch(line))
                            {
                                if (!Directory.Exists(dir.FullName + @"\Properties\AssemblyInfo.cs"))
                                {
                                    sw.WriteLine(fileArr[i].FullName);
                                    has = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            sr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                sw.Close();
            }
        }
    }
}
