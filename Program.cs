using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCabinet
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
            Application.Run(new Form1());
        }
    }
    public class EditorWindow
    {
        // indicates what happened in the editor window
        public static string CloseState { get; set; }
        public static string ReturnState { get; set; }

        // tell editor window what task we have to do on which table
        public static string Task { get; set; }
        public static string Table { get; set; }
        
        // store values of the text boxes on editor form 
        public static string Title { get; set; }
        public static string Description { get; set; }

        // track records to modify and total records
        public static int RemainingRecs { get; set; }
        public static int TotalRecs { get; set; }
        public static void Display()
        {
            Form form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
