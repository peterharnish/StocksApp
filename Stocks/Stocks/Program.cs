using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Stocks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string s = System.Configuration.ConfigurationManager.AppSettings["Repository"];
            Type t = Type.GetType(s);
            Assembly a = Assembly.GetAssembly(t);
            Stocks.DataAccess.IRepository repository = (Stocks.DataAccess.IRepository)a.CreateInstance(t.FullName);
            Form1.Repository = repository;
            Application.Run(new Form1());
        }
    }
}
