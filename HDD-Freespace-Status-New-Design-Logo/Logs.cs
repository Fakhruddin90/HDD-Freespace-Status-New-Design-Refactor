using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDD_Freespace_Status_Design_1
{
    public class Logs
    {
        public static void CSVLogs(string text)
        {
            //string text = "A class is the most powerful data type in C#. Like a structure, " +
            //    "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file. You do NOT need to call Flush() or Close().
            string filename = Application.StartupPath + $"\\CSVLogs.txt";
            System.IO.File.WriteAllText(filename, text);
        }
    }
}
