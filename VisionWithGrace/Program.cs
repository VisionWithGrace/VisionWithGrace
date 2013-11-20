using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule;
namespace VisionWithGrace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.startMongoProcess();
            DatabaseInterface dbInterface1 = new DatabaseInterface("SelectedObjects");
            DatabaseInterface dbInterface2 = new DatabaseInterface("SelectedObjects");

            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            Dictionary<string, object> dict2 = new Dictionary<string, object>();
            dict1.Add("test1", "test1");
            dict2.Add("TEST2", "TEst2");
            dbInterface1.Insert(dict1);
            dbInterface2.Insert(dict2);

            var selection = dbInterface1.getSelection("TEST2", "TEst2");


            int i = 0;
        }
    }
}
