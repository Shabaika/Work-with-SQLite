﻿using System;
using System.Windows.Forms;

namespace Lab4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.information) Application.Run(new Form2());
            Application.Run(new Form1());
        }
    }
}
