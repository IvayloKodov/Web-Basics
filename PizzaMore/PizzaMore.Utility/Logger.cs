namespace PizzaMore.Utility
{
    using System;
    using System.IO;

    public static class Logger
    {
        public static void Log(string text)
        {
            File.AppendAllText("../PizzaLab/log.txt", text + Environment.NewLine);
        }
    }
}
