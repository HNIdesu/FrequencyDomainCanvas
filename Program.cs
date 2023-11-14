using HNIdesu.Logger;
using System;

namespace ChartTest
{
    internal static class Program
    {
        //internal static PopupWindowLogger Logger { get; } = new PopupWindowLogger();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //Logger.Open();
            ApplicationConfiguration.Initialize();
            Application.Run(MainForm.Instance);
            
        }
    }
}