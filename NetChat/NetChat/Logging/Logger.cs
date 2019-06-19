using System;
using System.IO;

public static class Logger
{
    public static string path = Path.GetTempPath() + "/" + AppDomain.CurrentDomain.FriendlyName + " Debug.log";
    public static bool PrintErrorPath = false;

    public static void Start()
    {
        File.AppendAllText(path, "\n >>>Starting Program<<< \n\n");
    }

    public static void Debug(string message)
    {
        WriteToConsole(message, ConsoleColor.DarkMagenta);
        WriteToFile(message, "Debug");
    }

    public static void Error(string message)
    {
        WriteToConsole(message, ConsoleColor.DarkRed);
        WriteToFile(message, "Error");
    }

    public static void Info(string message, ConsoleColor color = ConsoleColor.Blue)
    {
        WriteToConsole(message, color);
        WriteToFile(message, "Info");
    }

    public static void Warning(string message)
    {
        WriteToConsole(message, ConsoleColor.DarkYellow);
        WriteToFile(message, "Warning");
    }

    private static void WriteToConsole(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
    }

    private static void WriteToFile(string message, string msgType)
    {
        try
        {
            if (!File.Exists(path))
                File.Create(path).Close();
            File.AppendAllText(path, "[" + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss") + "][" + msgType + "] >>> " + message + "\n");
        } catch(IOException)
        {

        }
    }

    public static void Error(string message, Exception e)
    {
        Error(message);
        Error(e);
    }
    public static void Error(Exception ex)
    {
        if (PrintErrorPath)
            Error(ex.ToString());
        else
            Error(ex.Message);
    }

}
