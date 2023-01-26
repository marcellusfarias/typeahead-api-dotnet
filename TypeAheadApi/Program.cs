using System;
using System.IO;
using System.Xml.Serialization;
using log4net;

public class Program
{
    static void Main(string[] args)
    {
        FileInfo fileInfo = new FileInfo("./log4net.config");
        log4net.Config.XmlConfigurator.Configure(fileInfo);
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

        log.Info("Starting service...");
        Console.WriteLine("Hello World");

        //[TODO] Read configurations...
        string host = string.Empty;
        string fileName = string.Empty;
        string port = string.Empty;
        int suggestionNumber = 0;

        try
        {
            string fileContent = File.ReadAllText(fileName);
        }
        catch (Exception)
        {
            log.Error("Could not load configuration environment!");
            return;
        }
    }
}