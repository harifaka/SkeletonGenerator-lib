using System;
using System.IO;

namespace SkeletonGenerator_lib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The skelton generator STARTED!");

            string strCmdText;
            strCmdText = "java -version";
            var cmdResponse = RunCommandLineCommand(strCmdText);
            //Console.WriteLine("The CMD response was: " + System.Environment.NewLine + cmdResponse);
            CreateCsharpConsoleApplication("TestConsole");
            CreateCsharpWpfApplication("TestWpf");

            Console.WriteLine("The skelton generator FINISHED!");
        }

        static string RunCommandLineCommand(string command) {
            string response = "";
            try
            {
                var process = System.Diagnostics.Process.Start("CMD.exe", "/C " + command);
                process.StartInfo.RedirectStandardOutput = true;
                process.WaitForExit();
                response += process.StandardOutput.ReadToEnd() + System.Environment.NewLine;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("The command line execution error: " + e.Message);
                response = "Something wrong happened!";
            }
            return response;
        }

        static void CreateCsharpApplication(string type, string appName)
        {
            string command = "dotnet new " +type + " --name " + appName;
            RunCommandLineCommand(command);
            string actualPath = System.Environment.CurrentDirectory;

            string sourceFile = actualPath + @"\templates\.gitignore";
            string destinationFile = actualPath + @"\" + appName + @"\.gitignore";
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
        static void CreateCsharpConsoleApplication(string appName)
        {
            CreateCsharpApplication("console", appName);
        }
        static void CreateCsharpWpfApplication(string appName)
        {
            CreateCsharpApplication("wpf", appName);
        }
    }
}
